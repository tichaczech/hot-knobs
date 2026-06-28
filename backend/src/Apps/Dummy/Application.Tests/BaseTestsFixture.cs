using FakeItEasy;

using Mapster;

using MapsterMapper;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Domains.Dummy;
using thc.HotKnobs.Domains.Dummy.Commands;
using thc.HotKnobs.Domains.Dummy.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;
using thc.HotKnobs.Domains.Dummy.Queries.Handlers;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests;

public class BaseTestsFixture
{
	public BaseTestsFixture()
	{
		InitiateMapper();
		InitializeStorage();
	}

	public IMapper EntityMapper { get; private set; } = default!;
	public TypeAdapterConfig MappingConfig { get; private set; } = default!;

	public IRepository<Author> AuthorRepository { get; protected set; } = default!;
	public IRepository<Patron> PatronRepository { get; protected set; } = default!;
	public IRepository<Book> BookRepository { get; protected set; } = default!;
	public IRepository<Reservation> ReservationRepository { get; protected set; } = default!;
	public IRepository<Loan> LoanRepository { get; protected set; } = default!;

	public List<Author> Authors { get; private set; } = [];
	public List<Patron> Patrons { get; private set; } = [];
	public List<Book> Books { get; private set; } = [];
	public List<Reservation> Reservations { get; private set; } = [];
	public List<Loan> Loans { get; private set; } = [];

	public AuthorCreateCommandHandler AuthorCreateCommandHandler { get; protected set; } = default!;
	public AuthorDeleteCommandHandler AuthorDeleteCommandHandler { get; protected set; } = default!;
	public AuthorUpdateCommandHandler AuthorUpdateCommandHandler { get; protected set; } = default!;
	public AuthorGetByIdQueryHandler AuthorGetByIdQueryHandler { get; protected set; } = default!;
	public AuthorGetByExternalIdQueryHandler AuthorGetByExternalIdQueryHandler { get; protected set; } = default!;
	public AuthorListQueryHandler<Author> AuthorListQueryHandler { get; protected set; } = default!;

	public PatronCreateCommandHandler PatronCreateCommandHandler { get; protected set; } = default!;
	public PatronDeleteCommandHandler PatronDeleteCommandHandler { get; protected set; } = default!;
	public PatronUpdateCommandHandler PatronUpdateCommandHandler { get; protected set; } = default!;
	public PatronGetByIdQueryHandler PatronGetByIdQueryHandler { get; protected set; } = default!;
	public PatronListQueryHandler<Patron> PatronListQueryHandler { get; protected set; } = default!;

	public BookCreateCommandHandler BookCreateCommandHandler { get; protected set; } = default!;
	public BookDeleteCommandHandler BookDeleteCommandHandler { get; protected set; } = default!;
	public BookUpdateCommandHandler BookUpdateCommandHandler { get; protected set; } = default!;
	public BookGetByIdQueryHandler BookGetByIdQueryHandler { get; protected set; } = default!;
	public BookGetByExternalIdQueryHandler BookGetByExternalIdQueryHandler { get; protected set; } = default!;
	public BookListQueryHandler<Book> BookListQueryHandler { get; protected set; } = default!;

	public ReservationCreateCommandHandler ReservationCreateCommandHandler { get; protected set; } = default!;
	public ReservationDeleteCommandHandler ReservationDeleteCommandHandler { get; protected set; } = default!;
	public ReservationUpdateCommandHandler ReservationUpdateCommandHandler { get; protected set; } = default!;
	public ReservationGetByIdQueryHandler ReservationGetByIdQueryHandler { get; protected set; } = default!;
	public ReservationListQueryHandler<Reservation> ReservationListQueryHandler { get; protected set; } = default!;

	public LoanCreateCommandHandler LoanCreateCommandHandler { get; protected set; } = default!;
	public LoanCreateFromReservationCommandHandler LoanCreateFromReservationCommandHandler { get; protected set; } = default!;
	public LoanDeleteCommandHandler LoanDeleteCommandHandler { get; protected set; } = default!;
	public LoanUpdateCommandHandler LoanUpdateCommandHandler { get; protected set; } = default!;
	public LoanGetByIdQueryHandler LoanGetByIdQueryHandler { get; protected set; } = default!;
	public LoanListQueryHandler<Loan> LoanListQueryHandler { get; protected set; } = default!;

	public Author GetRandomAuthor() => Authors.First(author => author.IsActive);
	public Patron GetRandomPatron() => Patrons.First(patron => patron.IsActive);
	public Book GetRandomBook() => Books.First(book => book.IsActive);
	public Reservation GetRandomReservation() => Reservations.First(reservation => reservation.IsActive);
	public Loan GetRandomLoan() => Loans.First(loan => loan.IsActive);

	public AuthorCreateCommand CreateAuthorCommand() => new()
	{
		Id = Guid.NewGuid().ToString(),
		CreateModel = new AuthorModel
		{
			ExternalId = $"author-{Guid.NewGuid():N}",
			FirstName = "Test",
			LastName = "Author"
		}
	};

	public AuthorUpdateCommand CreateAuthorUpdateCommand(Author author) => new()
	{
		Id = author.Id,
		ETag = author.ETag,
		UpdateModel = new AuthorModel
		{
			ExternalId = author.ExternalId,
			FirstName = "Updated",
			LastName = author.LastName
		}
	};

	public PatronCreateCommand CreatePatronCommand() => new()
	{
		Id = Guid.NewGuid().ToString(),
		CreateModel = new PatronModel
		{
			FirstName = "Test",
			LastName = "Reader",
			DateOfBirth = DateTimeOffset.UtcNow.AddYears(-18)
		}
	};

	public PatronUpdateCommand CreatePatronUpdateCommand(Patron patron) => new()
	{
		Id = patron.Id,
		ETag = patron.ETag,
		UpdateModel = new PatronModel
		{
			FirstName = "Updated",
			LastName = patron.LastName,
			DateOfBirth = new DateTimeOffset(patron.DateOfBirth, TimeSpan.Zero)
		}
	};

	public BookCreateCommand CreateBookCommand(string authorId) => new()
	{
		Id = Guid.NewGuid().ToString(),
		CreateModel = new BookModel
		{
			AuthorId = authorId,
			ExternalId = $"book-{Guid.NewGuid():N}",
			Name = "Some random book",
			OriginalPrice = 199,
			PublishYear = 2020
		}
	};

	public BookUpdateCommand CreateBookUpdateCommand(Book book, string authorId) => new()
	{
		Id = book.Id,
		ETag = book.ETag,
		UpdateModel = new BookModel
		{
			AuthorId = authorId,
			ExternalId = book.ExternalId,
			Name = "Updated book",
			OriginalPrice = 249,
			PublishYear = 2021
		}
	};

	public ReservationCreateCommand CreateReservationCommand(string bookId, string patronId) => new()
	{
		Id = Guid.NewGuid().ToString(),
		CreateModel = new ReservationModel
		{
			BookId = bookId,
			PatronId = patronId,
			StartsOn = DateTimeOffset.UtcNow,
			EndsOn = DateTimeOffset.UtcNow.AddDays(14)
		}
	};

	public ReservationUpdateCommand CreateReservationUpdateCommand(Reservation reservation) => new()
	{
		Id = reservation.Id,
		ETag = reservation.ETag,
		UpdateModel = new ReservationModel
		{
			BookId = reservation.BookId,
			PatronId = reservation.PatronId,
			StartsOn = new DateTimeOffset(reservation.StartsOn, TimeSpan.Zero).AddDays(1),
			EndsOn = new DateTimeOffset(reservation.EndsOn, TimeSpan.Zero).AddDays(1)
		}
	};

	public LoanCreateCommand CreateLoanCommand(string bookId, string patronId) => new()
	{
		Id = Guid.NewGuid().ToString(),
		CreateModel = new LoanModel
		{
			BookId = bookId,
			PatronId = patronId,
			LoanedOn = DateTimeOffset.UtcNow,
			DueOn = DateTimeOffset.UtcNow.AddDays(14),
			ReturnedOn = null
		}
	};

	public LoanCreateFromReservationCommand CreateLoanFromReservationCommand(string reservationId) => new()
	{
		Id = Guid.NewGuid().ToString(),
		ReservationId = reservationId,
		CreateModel = new LoanModel
		{
			BookId = string.Empty,
			PatronId = string.Empty,
			LoanedOn = DateTimeOffset.UtcNow,
			DueOn = DateTimeOffset.UtcNow.AddDays(14)
		}
	};

	public LoanUpdateCommand CreateLoanUpdateCommand(Loan loan) => new()
	{
		Id = loan.Id,
		ETag = loan.ETag,
		UpdateModel = new LoanModel
		{
			BookId = loan.BookId,
			PatronId = loan.PatronId,
			LoanedOn = new DateTimeOffset(loan.LoanedOn, TimeSpan.Zero),
			DueOn = new DateTimeOffset(loan.DueOn, TimeSpan.Zero),
			ReturnedOn = DateTimeOffset.UtcNow
		}
	};

	public static async Task<List<TRepresentation>> ReadAllAsync<TRepresentation>(IAsyncEnumerable<TRepresentation> items)
	{
		var results = new List<TRepresentation>();
		await foreach (var item in items)
			results.Add(item);

		return results;
	}

	protected static TLogger CreateLogger<TLogger>() where TLogger : class => A.Fake<TLogger>();

	private void InitializeStorage()
	{
		Authors =
		[
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ExternalId = "author-1", FirstName = "Mark", LastName = "Brek", IsActive = true, ETag = "1", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ExternalId = "author-2", FirstName = "Chal", LastName = "Dal", IsActive = true, ETag = "2", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ExternalId = "author-3", FirstName = "Krt", LastName = "Vrt", IsActive = false, ETag = "3", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		];

		Patrons =
		[
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Ala", LastName = "Vita", IsActive = true, ETag = "1", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Val", LastName = "Suk", IsActive = true, ETag = "2", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "Pal", LastName = "Gek", IsActive = true, ETag = "3", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), FirstName = "As", LastName = "Prd", IsActive = false, ETag = "4", DateOfBirth = DateTime.UtcNow.AddYears(-30), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		];

		Books =
		[
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[0].Id, ExternalId = "book-1", Name = "Test book 1", OriginalPrice = 299, PublishYear = 2021, IsActive = true, ETag = "1", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[0].Id, ExternalId = "book-2", Name = "Test book 2", OriginalPrice = 299, PublishYear = 2022, IsActive = true, ETag = "2", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[1].Id, ExternalId = "book-3", Name = "Test book 3", OriginalPrice = 299, PublishYear = 2023, IsActive = true, ETag = "3", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), AuthorId = Authors[1].Id, ExternalId = "book-4", Name = "Test book 4", OriginalPrice = 299, PublishYear = 2017, IsActive = false, ETag = "4", PurchasedOn = DateTimeOffset.UtcNow.AddDays(-4), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		];

		Reservations =
		[
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[0].Id, PatronId = Patrons[0].Id, StartsOn = DateTime.UtcNow.AddDays(-4), EndsOn = DateTime.UtcNow.AddDays(10), IsActive = true, ETag = "1", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[2].Id, PatronId = Patrons[1].Id, StartsOn = DateTime.UtcNow.AddDays(-24), EndsOn = DateTime.UtcNow.AddDays(-10), IsActive = false, ETag = "2", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), BookId = Books[2].Id, PatronId = Patrons[2].Id, StartsOn = DateTime.UtcNow.AddDays(-5), EndsOn = DateTime.UtcNow.AddDays(9), IsActive = true, ETag = "3", UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		];

		Loans =
		[
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[0].Id, BookId = Reservations[0].BookId, PatronId = Reservations[0].PatronId, IsActive = true, ETag = "1", LoanedOn = Reservations[0].StartsOn, DueOn = Reservations[0].EndsOn, ReturnedOn = DateTime.UtcNow.AddDays(-6), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[1].Id, BookId = Reservations[1].BookId, PatronId = Reservations[1].PatronId, IsActive = false, ETag = "2", LoanedOn = Reservations[1].StartsOn, DueOn = Reservations[1].EndsOn, ReturnedOn = DateTime.UtcNow.AddDays(-12), UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
			new() { CreatedAt = DateTimeOffset.UtcNow, CreatedBy = "TestUser", Id = Guid.NewGuid().ToString(), ReservationId = Reservations[2].Id, BookId = Reservations[2].BookId, PatronId = Reservations[2].PatronId, IsActive = true, ETag = "3", LoanedOn = Reservations[2].StartsOn, DueOn = Reservations[2].EndsOn, UpdatedAt = DateTimeOffset.UtcNow, UpdatedBy = "TestUser" },
		];
	}

	private void InitiateMapper()
	{
		MappingConfig = new TypeAdapterConfig();
		new MapperProfile().Register(MappingConfig);
		EntityMapper = new Mapper(MappingConfig);
	}
}
