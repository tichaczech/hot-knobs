using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Persistence;

public sealed class MongoDummyContext(DbContextOptions<MongoDummyContext> options) : MongoRepositoryContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		_ = modelBuilder.AddEntity<Author>("authors", e =>
		{
			_ = e.Property(a => a.ExternalId).HasElementName("externalId");
			_ = e.Property(a => a.FirstName).HasElementName("firstName");
			_ = e.Property(a => a.LastName).HasElementName("lastName");
			_ = e.Property(a => a.Nationality).HasElementName("nationality");
		});

		_ = modelBuilder.AddEntity<Book>("books", e =>
		{
			_ = e.Property(a => a.AuthorId).HasElementName("authorId");
			_ = e.Property(a => a.ExternalId).HasElementName("externalId");
			_ = e.Property(a => a.OriginalPrice).HasElementName("originalPrice");
			_ = e.Property(a => a.PublishYear).HasElementName("publishYear");
			_ = e.Property(a => a.Name).HasElementName("name");
		});

		_ = modelBuilder.AddEntity<Loan>("loans", e =>
		{
			_ = e.Property(a => a.BookId).HasElementName("bookId");
			_ = e.Property(a => a.DueOn).HasElementName("dueOn");
			_ = e.Property(a => a.LoanedOn).HasElementName("loanedOn");
			_ = e.Property(a => a.PatronId).HasElementName("patronId");
			_ = e.Property(a => a.ReservationId).HasElementName("reservationId");
			_ = e.Property(a => a.ReturnedOn).HasElementName("returnedOn");
		});

		_ = modelBuilder.AddEntity<Patron>("patrons", e =>
		{
			_ = e.Property(a => a.DateOfBirth).HasElementName("dateOfBirth");
			_ = e.Property(a => a.FirstName).HasElementName("firstName");
			_ = e.Property(a => a.LastName).HasElementName("lastName");
		});

		_ = modelBuilder.AddEntity<Reservation>("reservations", e =>
		{
			_ = e.Property(a => a.BookId).HasElementName("firstName");
			_ = e.Property(a => a.EndsOn).HasElementName("endsOn");
			_ = e.Property(a => a.PatronId).HasElementName("patronId");
			_ = e.Property(a => a.StartsOn).HasElementName("startsOn");
		});
	}
}
