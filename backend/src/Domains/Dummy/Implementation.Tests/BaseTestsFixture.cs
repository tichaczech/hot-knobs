using AutoMapper;

using FakeItEasy;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using thc.HotKnobs.Contracts;
using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.UseCases;
using thc.HotKnobs.Model.Repository;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests;

public class BaseTestsFixture
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	public BaseTestsFixture() => InitiateMapper();
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	public IMapper EntityMapper;

	#region Public random Getters for entities
	public static Author GetRandomAuthor() => Authors.First(a => a.IsActive);
	public static Patron GetRandomPatron() => Patrons.First(p => p.IsActive);
	public static Book GetRandomBook() => Books.First(b => b.IsActive);
	public static Reservation GetRandomReservation() => Reservations.First(r => r.IsActive);
	public static Loan GetRandomLoan() => Loans.First(l => l.IsActive);
	#endregion

	#region Repositories
	public IRepository<Author> AuthorRepository = A.Fake<IRepository<Author>>();
	public IRepository<Patron> PatronRepository = A.Fake<IRepository<Patron>>();
	public IRepository<Book> BookRepository = A.Fake<IRepository<Book>>();
	public IRepository<Reservation> ReservationRepository = A.Fake<IRepository<Reservation>>();
	public IRepository<Loan> LoanRepository = A.Fake<IRepository<Loan>>();
	#endregion

	#region Services
	public IAuthorUseCases AuthorUseCases;
	public IPatronUseCases PatronUseCases;
	public IBookUseCases BookUseCases;
	public IReservationUseCases ReservationUseCases;
	public ILoanUseCases LoanUseCases;
	#endregion

	#region Create Models
	public readonly AuthorCreateModel AuthorCreateModel = new() { FirstName = "Test", LastName = "Author" };
	public readonly PatronCreateModel PatronCreateModel = new() { FirstName = "Test", LastName = "Reader", DateOfBirth = DateTime.Now.AddYears(-18) };
	public readonly BookCreateModel BookCreateModel = new() { Name = "Some random book", OriginalPrice = 199, PublishYear = 2020 };
	public readonly ReservationCreateModel ReservationCreateModel = new() { StartsOn = DateTimeOffset.UtcNow, EndsOn = DateTimeOffset.UtcNow.AddDays(14) };
	public readonly LoanCreateModel LoanCreateModel = new() { LoanedOn = DateTimeOffset.UtcNow, DueOn = DateTimeOffset.UtcNow.AddDays(14) };
	#endregion

	#region Update Models
	public readonly AuthorUpdateModel AuthorUpdateModel = new() { FirstName = "Tezt" };
	public readonly PatronUpdateModel PatronUpdateModel = new() { FirstName = "Tezz" };
	public readonly BookUpdateModel BookUpdateModel = new() { Name = "Tezt name" };
	public readonly ReservationUpdateModel ReservationUpdateModel = new() { StartsOn = DateTimeOffset.UtcNow.AddDays(-1), EndsOn = DateTimeOffset.UtcNow.AddDays(13) };
	public readonly LoanUpdateModel LoanUpdateModel = new() { ReturnedOn = DateTimeOffset.UtcNow };
	#endregion

	#region Predefined entities lists
	internal static List<Author> Authors =
	[
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Mrk", LastName = "Brk", IsActive = true, ETag = "1", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Chal", LastName = "Dal", IsActive = true, ETag = "2", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Krt", LastName = "Vrt", IsActive = false, ETag = "3", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
	];
	internal static List<Patron> Patrons =
	[
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Ala", LastName = "Vita", IsActive= true, ETag = "1", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Val", LastName = "Šuk", IsActive= true, ETag = "2", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Pal", LastName = "Gek", IsActive = true, ETag = "3", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Áš", LastName = "Prd", IsActive = false, ETag = "4", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
	];
	internal static List<Book> Books =
	[
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[0].Id, Name = "Testovací kniha 1", OriginalPrice = 299, PublishYear = 2021, IsActive = true, ETag = "1", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[0].Id, Name = "Testovací kniha 2", OriginalPrice = 299, PublishYear = 2022, IsActive = true, ETag = "2", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[1].Id, Name = "Testovací kniha 3", OriginalPrice = 299, PublishYear = 2023, IsActive = true, ETag = "3", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[1].Id, Name = "Testovací kniha 4", OriginalPrice = 299, PublishYear = 2017, IsActive = false, ETag = "4", PurchasedOn = DateTimeOffset.UtcNow.AddDays(-4), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[2].Id, Name = "Testovací kniha 5", OriginalPrice = 299, PublishYear = 2020, IsActive = false, ETag = "5", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[2].Id, Name = "Testovací kniha 6", OriginalPrice = 299, PublishYear = 2019, IsActive = false, ETag = "6", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
	];
	internal static List<Reservation> Reservations =
	[
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[0].Id, PatronId = Patrons[0].Id, StartsOn = DateTime.UtcNow.AddDays(-4), EndsOn = DateTime.UtcNow.AddDays(10), IsActive = true, ETag = "1", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[2].Id, PatronId = Patrons[1].Id, StartsOn = DateTime.UtcNow.AddDays(-24), EndsOn = DateTime.UtcNow.AddDays(-10), IsActive = false, ETag = "2", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[3].Id, PatronId = Patrons[2].Id, StartsOn = DateTime.UtcNow.AddDays(-30), EndsOn = DateTime.UtcNow.AddDays(-16), IsActive = false, ETag = "3", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[4].Id, PatronId = Patrons[3].Id, StartsOn = DateTime.UtcNow.AddDays(-1), EndsOn = DateTime.UtcNow.AddDays(13), IsActive = true, ETag = "4", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[2].Id, PatronId = Patrons[2].Id, StartsOn = DateTime.UtcNow.AddDays(-5), EndsOn = DateTime.UtcNow.AddDays(9), IsActive = true, ETag = "5", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
	];
	internal static List<Loan> Loans =
	[
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[0].Id, BookId = Reservations[0].BookId, PatronId = Reservations[0].PatronId, IsActive = true, ETag = "1", LoanedOn = Reservations[0].StartsOn, DueOn = Reservations[0].EndsOn, ReturnedOn =  DateTime.UtcNow.AddDays(-6), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[1].Id, BookId = Reservations[1].BookId, PatronId = Reservations[1].PatronId, IsActive = false, ETag = "2", LoanedOn = Reservations[1].StartsOn, DueOn = Reservations[1].EndsOn, ReturnedOn =  DateTime.UtcNow.AddDays(-12), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[2].Id, BookId = Reservations[2].BookId, PatronId = Reservations[2].PatronId, IsActive = false, ETag = "3", LoanedOn = Reservations[2].StartsOn, DueOn = Reservations[2].EndsOn, ReturnedOn =  DateTime.UtcNow.AddDays(-21), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[3].Id, BookId = Reservations[3].BookId, PatronId = Reservations[3].PatronId, IsActive = true, ETag = "4", LoanedOn = Reservations[3].StartsOn, DueOn = Reservations[3].EndsOn, UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
	];
	#endregion

	#region Loggers
	internal readonly ILogger<AuthorUseCases> AuthorServiceLogger = A.Fake<ILogger<AuthorUseCases>>();
	internal readonly ILogger<PatronUseCases> PatronServiceLogger = A.Fake<ILogger<PatronUseCases>>();
	internal readonly ILogger<BookUseCases> BookServiceLogger = A.Fake<ILogger<BookUseCases>>();
	internal readonly ILogger<ReservationUseCases> ReservationServiceLogger = A.Fake<ILogger<ReservationUseCases>>();
	internal readonly ILogger<LoanUseCases> LoanServiceLogger = A.Fake<ILogger<LoanUseCases>>();
	#endregion

	protected static ServiceConfiguration InitiateAppServiceConfig(string serviceName) => new() { CommonName = serviceName };

	protected static IOptions<ServiceConfiguration> InitiateAppServiceConfigOptions(string serviceName)
		=> Options.Create(InitiateAppServiceConfig(serviceName));

	private void InitiateMapper()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies()
			.Where(a => !a.IsDynamic)
			.ToArray();

		var configuration = new MapperConfiguration(cfg =>
		{
			cfg.AddMaps(assemblies);
		}, null);

		EntityMapper = configuration.CreateMapper();
	}
}
