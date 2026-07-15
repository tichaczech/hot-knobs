using Azure.Storage.Queues;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Xunit;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public class AzureQueueStorageProducerIntegrationTests : IClassFixture<QueueServiceClientFixture>, IDisposable
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

	private const string NON_EXISTENT_QUEUE = "non-existent-queue";

	private const string MESSAGE_TEXT = "test message";

	private readonly IQueueProducer _impl;

	private readonly AzureQueueStorageConsumerOptions _consumerOptions = new() { };

	private readonly QueueClient _queueClient;

	private readonly string _queueName = Guid.NewGuid().ToString();

	private readonly TimeSpan _visibilityTimeout = TimeSpan.FromMinutes(5);

	private bool _disposed;

	public AzureQueueStorageProducerIntegrationTests(QueueServiceClientFixture fixture)
	{
		_queueClient = fixture.QueueServiceClient.GetQueueClient(_queueName);

		if (_queueClient.Exists())
			_ = _queueClient.Delete();
		_ = _queueClient.Create();

		var logger = NullLoggerFactory.Instance.CreateLogger<AzureQueueStorageProducer>();
		var options = new AzureQueueStorageProducerOptions { };
		_impl = new AzureQueueStorageProducer(logger, fixture.QueueServiceClient, Options.Create(options));
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

	#region SendMessageAsync

	public static IEnumerable<object[]> SendMessageAsync_ShouldSucceed_TestData()
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
	[MemberData(nameof(SendMessageAsync_ShouldSucceed_TestData))]
	public async Task SendMessageAsync_ShouldSucceed<T>(T message)
		where T : notnull
	{
		// Arrange

		// Act
		var sendResult = await _impl.SendMessageAsync(_queueName, message);

		// Assert
		var receivedMessage = await _queueClient.ReceiveMessageAsync(_visibilityTimeout);
		var deserializedMessage = _consumerOptions.DeserializerFactory.Create<T>().DeserializeMessage(receivedMessage.Value.MessageText);
		Assert.Equal(sendResult.MessageId, receivedMessage.Value.MessageId);
		Assert.Equivalent(message, deserializedMessage);
	}

	[Fact]
	public async Task SendMessageAsync_ShouldThrowQueueNotFoundException_WhenQueueDoesNotExists()
	{
		// Arrange
		var queueName = NON_EXISTENT_QUEUE;

		// Act & Assert
		_ = await Assert.ThrowsAsync<QueueNotFoundException>(() => _impl.SendMessageAsync(queueName, MESSAGE_TEXT));
	}

	#endregion // SendMessageAsync
}
