using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Contracts;
using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.Runtime.Persistence;
using thc.HotKnobs.Domains.Dummy.UseCases;
using thc.HotKnobs.Model.Entities;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.Runtime.Configuration;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class IntegrationTestsFixture : BaseTestsFixture, IDisposable
{
	private CosmosDummyContext _dbContext;
	private ServiceProvider _serviceProvider;
	private readonly ServiceCollection _serviceCollection = new();

	private readonly CosmosConfiguration _cosmosConfig = new()
	{
		ConnectionString = "AccountEndpoint=https://localhost:8081;AccountKey=your_account_key;",
		DatabaseName = "HotKnobsDummyTests"
	};

	private static readonly object CreateDatabaseLock = new();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public IntegrationTestsFixture()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	{
		LoadConfiguration();
		InitiateMapper();
		RegisterServices();
		InitiateRepositories();
		InitiateServices();
		lock (CreateDatabaseLock)
		{
			CreateDatabase().GetAwaiter().GetResult();
		}
	}

	public void Dispose()
	{
		CleanupData(AuthorRepository).GetAwaiter().GetResult();
		CleanupData(BookRepository).GetAwaiter().GetResult();
		CleanupData(LoanRepository).GetAwaiter().GetResult();
		CleanupData(PatronRepository).GetAwaiter().GetResult();
		CleanupData(ReservationRepository).GetAwaiter().GetResult();

		DeleteDatabase().GetAwaiter().GetResult();
		_dbContext?.Dispose();
		_serviceProvider?.Dispose();
	}

	public async Task<Author> PrepareAuthor()
	{
		var response = await AuthorUseCases.CreateAsync(default, AuthorCreateModel);
		return response;
	}

	public async Task<Book> PrepareBook()
	{
		var author = await PrepareAuthor();
		var bookCreateModel = new BookCreateModel
		{
			Name = BookCreateModel.Name,
			OriginalPrice = BookCreateModel.OriginalPrice,
			PublishYear = BookCreateModel.PublishYear,
			AuthorId = author.Id
		};
		var response = await BookUseCases.CreateAsync(default, bookCreateModel);
		return response;
	}

	public async Task<Patron> PreparePatron()
	{
		var response = await PatronUseCases.CreateAsync(default, PatronCreateModel);
		return response;
	}

	public async Task<Reservation> PrepareReservation()
	{
		var patron = await PreparePatron();
		var book = await PrepareBook();
		var reservationCreateModel = new ReservationCreateModel()
		{
			StartsOn = ReservationCreateModel.StartsOn,
			EndsOn = ReservationCreateModel.EndsOn,
			BookId = book.Id,
			PatronId = patron.Id
		};
		var response = await ReservationUseCases.CreateAsync(default, reservationCreateModel);
		return response;
	}

	public async Task<Loan> PrepareLoan()
	{
		var patron = await PreparePatron();
		var book = await PrepareBook();

		var loanCreateModel = new LoanCreateModel()
		{
			LoanedOn = LoanCreateModel.LoanedOn,
			DueOn = LoanCreateModel.DueOn,
			ReturnedOn = LoanCreateModel.ReturnedOn,
			BookId = book.Id,
			PatronId = patron.Id
		};

		var response = await LoanUseCases.CreateAsync(default, loanCreateModel, default);
		return response;
	}

	public async Task CleanupAuthor(string id)
	{
		await AuthorUseCases.DeleteAsync(id);
	}

	public async Task CleanupBook(string id)
	{
		await BookUseCases.DeleteAsync(id);
	}

	public async Task CleanupPatron(string id)
	{
		await PatronUseCases.DeleteAsync(id);
	}

	public async Task CleanupReservation(string id)
	{
		await ReservationUseCases.DeleteAsync(id);
	}

	public async Task CleanupLoan(string id)
	{
		await LoanUseCases.DeleteAsync(id);
	}

	private void LoadConfiguration()
	{
		var configBuilder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables();
		var configuration = configBuilder.Build();

		configuration.GetSection("HotKnobs:Runtime:Persistence:CosmosDB").Bind(_cosmosConfig);
		_cosmosConfig.DatabaseName = $"{_cosmosConfig.DatabaseName}_{Guid.NewGuid()}";
	}

	private void InitiateMapper()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToArray();
		var mapperConfig = new MapperConfiguration(cfg =>
		{
			cfg.AddMaps(assemblies);
		});
		EntityMapper = mapperConfig.CreateMapper();
	}

	private void InitiateServices()
	{
		AuthorUseCases = _serviceProvider.GetRequiredService<IAuthorUseCases>();
		BookUseCases = _serviceProvider.GetRequiredService<IBookUseCases>();
		LoanUseCases = _serviceProvider.GetRequiredService<ILoanUseCases>();
		PatronUseCases = _serviceProvider.GetRequiredService<IPatronUseCases>();
		ReservationUseCases = _serviceProvider.GetRequiredService<IReservationUseCases>();
	}

	private void InitiateRepositories()
	{
		_dbContext = _serviceProvider.GetRequiredService<CosmosDummyContext>();
		AuthorRepository = _serviceProvider.GetRequiredService<IRepository<Author>>();
		BookRepository = _serviceProvider.GetRequiredService<IRepository<Book>>();
		LoanRepository = _serviceProvider.GetRequiredService<IRepository<Loan>>();
		PatronRepository = _serviceProvider.GetRequiredService<IRepository<Patron>>();
		ReservationRepository = _serviceProvider.GetRequiredService<IRepository<Reservation>>();
	}

	private void RegisterServices()
	{
		_ = _serviceCollection.AddDbContext<CosmosDummyContext>(options => options.UseCosmos(_cosmosConfig.ConnectionString, _cosmosConfig.DatabaseName), ServiceLifetime.Transient);
		_ = _serviceCollection.AddLogging(loggingBuilder =>
		{
			_ = loggingBuilder.AddDebug();
			_ = loggingBuilder.SetMinimumLevel(LogLevel.Information);
		});

		_ = _serviceCollection.AddTransient(_ => EntityMapper);

		_ = _serviceCollection.AddTransient<IRepository<Author>, CosmosRepository<Author, CosmosDummyContext>>();
		_ = _serviceCollection.AddTransient<IRepository<Book>, CosmosRepository<Book, CosmosDummyContext>>();
		_ = _serviceCollection.AddTransient<IRepository<Loan>, CosmosRepository<Loan, CosmosDummyContext>>();
		_ = _serviceCollection.AddTransient<IRepository<Patron>, CosmosRepository<Patron, CosmosDummyContext>>();
		_ = _serviceCollection.AddTransient<IRepository<Reservation>, CosmosRepository<Reservation, CosmosDummyContext>>();

		_ = _serviceCollection.AddTransient<IAuthorUseCases, AuthorUseCases>();
		_ = _serviceCollection.AddTransient<IBookUseCases, BookUseCases>();
		_ = _serviceCollection.AddTransient<ILoanUseCases, LoanUseCases>();
		_ = _serviceCollection.AddTransient<IPatronUseCases, PatronUseCases>();
		_ = _serviceCollection.AddTransient<IReservationUseCases, ReservationUseCases>();

		_ = _serviceCollection.Configure<ServiceConfiguration>(config =>
		{
			config.CommonName = "Integration Tests";
		});

		_serviceProvider = _serviceCollection.BuildServiceProvider();
	}

	private void SeedData()
	{
		SeedData(AuthorRepository, Authors).GetAwaiter().GetResult();
		SeedData(BookRepository, Books).GetAwaiter().GetResult();
		SeedData(LoanRepository, Loans).GetAwaiter().GetResult();
		SeedData(PatronRepository, Patrons).GetAwaiter().GetResult();
		SeedData(ReservationRepository, Reservations).GetAwaiter().GetResult();
	}

	private async Task CreateDatabase()
	{
		_ = await _dbContext.Database.EnsureCreatedAsync();
	}

	private async Task DeleteDatabase()
	{
		_ = await _dbContext.Database.EnsureDeletedAsync();
	}

	private static async Task SeedData<T>(IRepository<T> repository, IEnumerable<T> entities) where T : Entity
	{
		foreach (var entity in entities)
		{
			_ = await repository.CreateAsync(entity);
		}
		_ = await repository.SaveChangesAsync();
	}

	private static async Task CleanupData<T>(IRepository<T> repository) where T : Entity
	{
		foreach (var entity in repository.AsQueryable())
		{
			await repository.DeleteAsync(entity);
		}
		_ = await repository.SaveChangesAsync();
	}
}
