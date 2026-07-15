using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Persistence;

public sealed class PostgresDummyContext : PostgresRepositoryContext
{
	public PostgresDummyContext() : base(new DbContextOptionsBuilder<PostgresRepositoryContext>().UseNpgsql("Host=postgres;Database=domains-dummy;Username=postgres;Password=postgres").Options) { }

	public PostgresDummyContext(DbContextOptions<PostgresDummyContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntity<Author>("authors", e =>
		{
			_ = e.Property(a => a.ExternalId).HasColumnName("externalId");
			_ = e.Property(a => a.FirstName).HasColumnName("firstName");
			_ = e.Property(a => a.LastName).HasColumnName("lastName");
			_ = e.Property(a => a.Nationality).HasColumnName("nationality");
		});

		_ = modelBuilder.AddEntity<Book>("books", e =>
		{
			_ = e.Property(a => a.AuthorId).HasColumnName("authorId");
			_ = e.Property(a => a.ExternalId).HasColumnName("externalId");
			_ = e.Property(a => a.OriginalPrice).HasColumnName("originalPrice");
			_ = e.Property(a => a.PublishYear).HasColumnName("publishYear");
			_ = e.Property(a => a.Name).HasColumnName("name");
		});

		_ = modelBuilder.AddEntity<Loan>("loans", e =>
		{
			_ = e.Property(a => a.BookId).HasColumnName("bookId");
			_ = e.Property(a => a.DueOn).HasColumnName("dueOn");
			_ = e.Property(a => a.LoanedOn).HasColumnName("loanedOn");
			_ = e.Property(a => a.PatronId).HasColumnName("patronId");
			_ = e.Property(a => a.ReservationId).HasColumnName("reservationId");
			_ = e.Property(a => a.ReturnedOn).HasColumnName("returnedOn");
		});

		_ = modelBuilder.AddEntity<Patron>("patrons", e =>
		{
			_ = e.Property(a => a.DateOfBirth).HasColumnName("dateOfBirth");
			_ = e.Property(a => a.FirstName).HasColumnName("firstName");
			_ = e.Property(a => a.LastName).HasColumnName("lastName");
		});

		_ = modelBuilder.AddEntity<Reservation>("reservations", e =>
		{
			_ = e.Property(a => a.BookId).HasColumnName("firstName");
			_ = e.Property(a => a.EndsOn).HasColumnName("endsOn");
			_ = e.Property(a => a.PatronId).HasColumnName("patronId");
			_ = e.Property(a => a.StartsOn).HasColumnName("startsOn");
		});
	}
}
