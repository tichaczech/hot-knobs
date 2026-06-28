using Mapster;

using thc.HotKnobs.Domains.Dummy.Commands;
using thc.HotKnobs.Domains.Dummy.Models;

namespace thc.HotKnobs.Domains.Dummy;

#pragma warning disable CS8603 // Possible null reference return

public class MapperProfile : IRegister
{
	// // Patron
	// _ = CreateMap<PatronCreateModel, Patron>()
	// 	.ForMember(p => p.Id, opt => opt.Ignore())
	// 	.ForMember(p => p.IsActive, opt => opt.Ignore())
	// 	.ForMember(p => p.DateOfBirth, opt => opt.MapFrom(r => r.DateOfBirth.UtcDateTime));
	// _ = CreateMap<PatronUpdateModel, Patron>()
	// 	.ForMember(p => p.Id, opt => opt.Ignore())
	// 	.ForMember(p => p.IsActive, opt => opt.Ignore())
	// 	.ForMember(p => p.DateOfBirth, opt => opt.MapFrom(r => r.DateOfBirth.UtcDateTime));

	// // Reservation
	// _ = CreateMap<ReservationCreateModel, Reservation>()
	// 	.ForMember(r => r.Id, opt => opt.Ignore())
	// 	.ForMember(r => r.IsActive, opt => opt.Ignore())
	// 	.ForMember(r => r.StartsOn, opt => opt.MapFrom(r => r.StartsOn.UtcDateTime))
	// 	.ForMember(r => r.EndsOn, opt => opt.MapFrom(r => r.EndsOn.UtcDateTime));
	// _ = CreateMap<ReservationUpdateModel, Reservation>()
	// 	.ForMember(r => r.Id, opt => opt.Ignore())
	// 	.ForMember(r => r.IsActive, opt => opt.Ignore())
	// 	.ForMember(r => r.BookId, opt => opt.Ignore())
	// 	.ForMember(r => r.PatronId, opt => opt.Ignore())
	// 	.ForMember(r => r.StartsOn, opt => opt.MapFrom(r => r.StartsOn.UtcDateTime))
	// 	.ForMember(r => r.EndsOn, opt => opt.MapFrom(r => r.EndsOn.UtcDateTime));

	public void Register(TypeAdapterConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		// Author
		_ = config.NewConfig<AuthorModel, Author>()
			.Ignore(a => a.Id, a => a.IsActive);

		// Patron
		_ = config.NewConfig<PatronModel, Patron>()
			.Ignore(p => p.Id, p => p.IsActive)
			.Map(p => p.DateOfBirth, m => m.DateOfBirth.UtcDateTime);

		// Book
		_ = config.NewConfig<BookModel, Book>()
			.Ignore(b => b.Id, b => b.IsActive, b => b.PurchasedOn);

		// Reservation
		_ = config.NewConfig<ReservationModel, Reservation>()
			.Ignore(r => r.Id, r => r.IsActive)
			.Map(r => r.StartsOn, m => m.StartsOn.UtcDateTime)
			.Map(r => r.EndsOn, m => m.EndsOn.UtcDateTime);

		// Loan
		_ = config.NewConfig<Reservation, Loan>()
			.Map(l => l.ReservationId, r => r.Id);
		// .ForMember(l => l.BookId, opt => opt.MapFrom(r => r.BookId))
		// .ForMember(l => l.ReservationId, opt => opt.MapFrom(r => r.Id))
		// .ForMember(l => l.PatronId, opt => opt.MapFrom(r => r.PatronId));
		_ = config.NewConfig<LoanModel, Loan>()
			.Ignore(l => l.Id, l => l.IsActive, l => l.ReservationId)
			.Map(l => l.DueOn, m => m.DueOn.UtcDateTime.Date)
			.Map(l => l.LoanedOn, m => m.LoanedOn.UtcDateTime)
			.Map(l => l.ReturnedOn, m => m.ReturnedOn.HasValue ? m.ReturnedOn.Value.UtcDateTime : (DateTime?)null);
		// .ForMember(l => l.Id, opt => opt.Ignore())
		// .ForMember(l => l.IsActive, opt => opt.Ignore())
		// .ForMember(l => l.DueOn, opt => opt.MapFrom(r => r.DueOn.UtcDateTime.Date))
		// .ForMember(l => l.LoanedOn, opt => opt.MapFrom(r => r.LoanedOn.UtcDateTime))
		// .ForMember(l => l.ReservationId, opt => opt.Ignore())
		// .ForMember(l => l.ReturnedOn, opt => opt.MapFrom(r => r.ReturnedOn.HasValue ? r.ReturnedOn.Value.UtcDateTime : (DateTime?)null));
		// _ = CreateMap<LoanUpdateModel, Loan>()
		// 	.ForMember(l => l.Id, opt => opt.Ignore())
		// 	.ForMember(l => l.IsActive, opt => opt.Ignore())
		// 	.ForMember(l => l.DueOn, opt => opt.MapFrom(r => r.DueOn.UtcDateTime.Date))
		// 	.ForMember(l => l.LoanedOn, opt => opt.Ignore())
		// 	.ForMember(l => l.ReservationId, opt => opt.Ignore())
		// 	.ForMember(l => l.BookId, opt => opt.Ignore())
		// 	.ForMember(l => l.PatronId, opt => opt.Ignore())
		// 	.ForMember(l => l.ReturnedOn, opt => opt.MapFrom(r => r.ReturnedOn.HasValue ? r.ReturnedOn.Value.UtcDateTime : (DateTime?)null));
	}
}

#pragma warning restore CS8603 // Possible null reference return
