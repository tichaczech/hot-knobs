using AutoMapper;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.UseCases;

namespace thc.HotKnobs.Domains.Dummy;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		// Author
		_ = CreateMap<AuthorCreateModel, Author>()
			.ForMember(a => a.Id, opt => opt.Ignore())
			.ForMember(a => a.IsActive, opt => opt.Ignore());
		_ = CreateMap<AuthorUpdateModel, Author>()
			.ForMember(a => a.Id, opt => opt.Ignore())
			.ForMember(a => a.IsActive, opt => opt.Ignore());

		// Book
		_ = CreateMap<BookCreateModel, Book>()
			.ForMember(b => b.Id, opt => opt.Ignore())
			.ForMember(b => b.IsActive, opt => opt.Ignore())
			.ForMember(b => b.PurchasedOn, opt => opt.Ignore());
		_ = CreateMap<BookUpdateModel, Book>()
			.ForMember(b => b.Id, opt => opt.Ignore())
			.ForMember(b => b.IsActive, opt => opt.Ignore())
			.ForMember(b => b.PurchasedOn, opt => opt.Ignore());

		// Loan
		_ = CreateMap<Reservation, Loan>()
			.ForMember(l => l.BookId, opt => opt.MapFrom(r => r.BookId))
			.ForMember(l => l.ReservationId, opt => opt.MapFrom(r => r.Id))
			.ForMember(l => l.PatronId, opt => opt.MapFrom(r => r.PatronId));
		_ = CreateMap<LoanCreateModel, Loan>()
			.ForMember(l => l.Id, opt => opt.Ignore())
			.ForMember(l => l.IsActive, opt => opt.Ignore())
			.ForMember(l => l.DueOn, opt => opt.MapFrom(r => r.DueOn.UtcDateTime.Date))
			.ForMember(l => l.LoanedOn, opt => opt.MapFrom(r => r.LoanedOn.UtcDateTime))
			.ForMember(l => l.ReservationId, opt => opt.Ignore())
			.ForMember(l => l.ReturnedOn, opt => opt.MapFrom(r => r.ReturnedOn.HasValue ? r.ReturnedOn.Value.UtcDateTime : (DateTime?)null));
		_ = CreateMap<LoanUpdateModel, Loan>()
			.ForMember(l => l.Id, opt => opt.Ignore())
			.ForMember(l => l.DueOn, opt => opt.MapFrom(r => r.DueOn.UtcDateTime))
			.ForMember(l => l.LoanedOn, opt => opt.Ignore())
			.ForMember(l => l.IsActive, opt => opt.Ignore())
			.ForMember(l => l.ReservationId, opt => opt.Ignore())
			.ForMember(l => l.BookId, opt => opt.Ignore())
			.ForMember(l => l.PatronId, opt => opt.Ignore())
			.ForMember(l => l.ReturnedOn, opt => opt.MapFrom(r => r.ReturnedOn.HasValue ? r.ReturnedOn.Value.UtcDateTime : (DateTime?)null));

		// Patron
		_ = CreateMap<PatronCreateModel, Patron>()
			.ForMember(p => p.Id, opt => opt.Ignore())
			.ForMember(p => p.IsActive, opt => opt.Ignore())
			.ForMember(p => p.DateOfBirth, opt => opt.MapFrom(r => r.DateOfBirth.UtcDateTime));
		_ = CreateMap<PatronUpdateModel, Patron>()
			.ForMember(p => p.Id, opt => opt.Ignore())
			.ForMember(p => p.IsActive, opt => opt.Ignore())
			.ForMember(p => p.DateOfBirth, opt => opt.MapFrom(r => r.DateOfBirth.UtcDateTime));

		// Reservation
		_ = CreateMap<ReservationCreateModel, Reservation>()
			.ForMember(r => r.Id, opt => opt.Ignore())
			.ForMember(r => r.IsActive, opt => opt.Ignore())
			.ForMember(r => r.StartsOn, opt => opt.MapFrom(r => r.StartsOn.UtcDateTime))
			.ForMember(r => r.EndsOn, opt => opt.MapFrom(r => r.EndsOn.UtcDateTime));
		_ = CreateMap<ReservationUpdateModel, Reservation>()
			.ForMember(r => r.Id, opt => opt.Ignore())
			.ForMember(r => r.IsActive, opt => opt.Ignore())
			.ForMember(r => r.BookId, opt => opt.Ignore())
			.ForMember(r => r.PatronId, opt => opt.Ignore())
			.ForMember(r => r.StartsOn, opt => opt.MapFrom(r => r.StartsOn.UtcDateTime))
			.ForMember(r => r.EndsOn, opt => opt.MapFrom(r => r.EndsOn.UtcDateTime));
	}
}
