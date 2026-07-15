using Azure.Storage.Queues;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Xunit;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public class AzureQueueStorageConsumerIntegrationTests : IClassFixture<QueueServiceClientFixture>, IDisposable
{
	public class InnerMessage
	{
		public required DateTimeOffset Created { get; set; }
	}

	public class TestMessage
	{
		public required Guid Id { get; set; }

		public required string Content { get; set; }

		public required InnerMessage Inner { get; set; }
	}

	public class TestReceiveResult : IReceiveResult<TestMessage>
	{
		public TestMessage Message => new() { Id = Guid.NewGuid(), Content = MESSAGE_TEXT, Inner = new InnerMessage { Created = DateTimeOffset.UtcNow } };

		public DateTimeOffset? ConfirmUntil => DateTimeOffset.MaxValue;
	}

	private const string MESSAGE_TEXT = "test message";

	private const string NON_EXISTENT_QUEUE = "non-existent-queue";

	private readonly IQueueConsumer _impl;

	private readonly AzureQueueStorageConsumerOptions _consumerOptions;

	private readonly AzureQueueStorageProducerOptions _producerOptions = new() { };

	private readonly QueueClient _queueClient;

	private readonly string _queueName = Guid.NewGuid().ToString();

	private bool _disposed;

	public AzureQueueStorageConsumerIntegrationTests(QueueServiceClientFixture fixture)
	{
		_queueClient = fixture.QueueServiceClient.GetQueueClient(_queueName);

		if (_queueClient.Exists())
			_ = _queueClient.Delete();
		_ = _queueClient.Create();

		var logger = NullLoggerFactory.Instance.CreateLogger<AzureQueueStorageConsumer>();
		_consumerOptions = new AzureQueueStorageConsumerOptions { VisibilityTimeout = TimeSpan.FromMinutes(5) };
		_impl = new AzureQueueStorageConsumer(logger, fixture.QueueServiceClient, Options.Create(_consumerOptions));
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing && _queueClient.Exists())
				_ = _queueClient.Delete();

			_disposed = true;
		}

	}

	#region ReceiveMessageAsync

	public static IEnumerable<object[]> ReceiveMessageAsync_ShouldSucceed_TestData()
	{
		yield return new object[] { 174 };
		yield return new object[] { 174.1 };
		yield return new object[] { 174.1f };
		yield return new object[] { 174.1d };
		yield return new object[] { 174.1m };
		yield return new object[] { true };
		yield return new object[] { MESSAGE_TEXT };
		yield return new object[] { new Dictionary<string, string> { ["Id"] = "1", ["Name"] = "test" } };
		yield return new object[] { new TestMessage { Id = Guid.NewGuid(), Content = MESSAGE_TEXT, Inner = new InnerMessage { Created = DateTimeOffset.UtcNow } } };
		yield return new object[] { new DateTimeOffset[] { DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(2) } };
	}

	[Theory]
	[MemberData(nameof(ReceiveMessageAsync_ShouldSucceed_TestData))]
	public async Task ReceiveMessageAsync_ShouldSucceed<T>(T message)
		where T : notnull
	{
		// Arrange
		var serializedMessage = _producerOptions.SerializerFactory.Create<T>().SerializeMessage(message);
		_ = await _queueClient.SendMessageAsync(serializedMessage);

		// Act
		var receivedMessage = await _impl.ReceiveMessageAsync<T>(_queueName);

		// Assert
		Assert.Equivalent(message, receivedMessage!.Message);
	}

	[Fact]
	public async Task ReceiveMessageAsync_ShouldSucceed_WhenNoMessageInQueue()
	{
		// Arrange

		// Act
		var receivedMessage = await _impl.ReceiveMessageAsync<string>(_queueName);

		// Assert
		Assert.Null(receivedMessage);
	}

	[Fact]
	public async Task ReceiveMessageAsync_ShouldThrowQueueNotFoundException_WhenQueueDoesNotExists()
	{
		// Arrange
		var queueName = NON_EXISTENT_QUEUE;

		// Act & Assert
		_ = await Assert.ThrowsAsync<QueueNotFoundException>(() => _impl.ReceiveMessageAsync<object>(queueName));
	}

	#endregion // ReceiveMessageAsync

	#region ConfirmReceiveAsync

	[Fact]
	public async Task ConfirmReceiveAsync_ShouldSucceed()
	{
		// Arrange
		var serializedMessage = _producerOptions.SerializerFactory.Create<string>().SerializeMessage(MESSAGE_TEXT);
		_ = await _queueClient.SendMessageAsync(serializedMessage);

		// Act
		var receivedMessage = await _impl.ReceiveMessageAsync<string>(_queueName);
		await _impl.ConfirmReceiveAsync(_queueName, receivedMessage!);

		// Assert
		var nullMessage = await _queueClient.ReceiveMessageAsync();
		Assert.Null(nullMessage.Value);
	}

	[Fact]
	public async Task ConfirmReceiveAsync_ShouldThrowMessageConfirmationFailedException_WhenVisibilityTimeoutExpires()
	{
		// Arrange
		var serializedMessage = _producerOptions.SerializerFactory.Create<string>().SerializeMessage(MESSAGE_TEXT);
		_ = await _queueClient.SendMessageAsync(serializedMessage);

		_consumerOptions.VisibilityTimeout = TimeSpan.FromSeconds(5);

		// Act
		var receivedMessage = await _impl.ReceiveMessageAsync<string>(_queueName);
		await Task.Delay(_consumerOptions.VisibilityTimeout + TimeSpan.FromSeconds(1));
		// NOTE: Azure Queue Storage allows message deletion after the visibility timeout expires (with the original pop receipt) when the message has not been "received" again.
		_ = await _impl.ReceiveMessageAsync<string>(_queueName);

		// Assert
		_ = await Assert.ThrowsAsync<MessageConfirmationFailedException>(() => _impl.ConfirmReceiveAsync(_queueName, receivedMessage!));
	}

	[Fact]
	public async Task ConfirmReceiveAsync_ShouldThrowQueueNotFoundException_WhenQueueDoesNotExists()
	{
		// Arrange
		var testReceiveResult = new TestReceiveResult();
		var queueName = NON_EXISTENT_QUEUE;

		// Act & Assert
		_ = await Assert.ThrowsAsync<QueueNotFoundException>(() => _impl.ConfirmReceiveAsync(queueName, testReceiveResult));
	}

	#endregion // ConfirmReceiveAsync
}
