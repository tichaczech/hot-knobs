using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Persistence;

public sealed class CosmosDummyContext(DbContextOptions<CosmosDummyContext> options) : CosmosRepositoryContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntity<Author>("authors", e =>
		{
			_ = e.Property(a => a.ExternalId).ToJsonProperty("externalId");
			_ = e.Property(a => a.FirstName).ToJsonProperty("firstName");
			_ = e.Property(a => a.LastName).ToJsonProperty("lastName");
			_ = e.Property(a => a.Nationality).ToJsonProperty("nationality");
		});

		_ = modelBuilder.AddEntity<Book>("books", e =>
		{
			_ = e.Property(a => a.ExternalId).ToJsonProperty("externalId");
			_ = e.Property(a => a.AuthorId).ToJsonProperty("authorId");
			_ = e.Property(a => a.OriginalPrice).ToJsonProperty("originalPrice");
			_ = e.Property(a => a.PublishYear).ToJsonProperty("publishYear");
			_ = e.Property(a => a.Name).ToJsonProperty("name");
		});

		_ = modelBuilder.AddEntity<Loan>("loans", e =>
		{
			_ = e.Property(a => a.BookId).ToJsonProperty("bookId");
			_ = e.Property(a => a.DueOn).ToJsonProperty("dueOn");
			_ = e.Property(a => a.LoanedOn).ToJsonProperty("loanedOn");
			_ = e.Property(a => a.PatronId).ToJsonProperty("patronId");
			_ = e.Property(a => a.ReservationId).ToJsonProperty("reservationId");
			_ = e.Property(a => a.ReturnedOn).ToJsonProperty("returnedOn");
		});

		_ = modelBuilder.AddEntity<Patron>("patrons", e =>
		{
			_ = e.Property(a => a.DateOfBirth).ToJsonProperty("dateOfBirth");
			_ = e.Property(a => a.FirstName).ToJsonProperty("firstName");
			_ = e.Property(a => a.LastName).ToJsonProperty("lastName");
		});

		_ = modelBuilder.AddEntity<Reservation>("reservations", e =>
		{
			_ = e.Property(a => a.BookId).ToJsonProperty("firstName");
			_ = e.Property(a => a.EndsOn).ToJsonProperty("endsOn");
			_ = e.Property(a => a.PatronId).ToJsonProperty("patronId");
			_ = e.Property(a => a.StartsOn).ToJsonProperty("startsOn");
		});
	}
}
