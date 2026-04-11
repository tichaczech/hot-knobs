using AutoMapper;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Domains.Dummy.UseCases;

namespace thc.HotKnobs.Domains.Dummy.Server;

internal class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		#region v1

		// Author
		_ = CreateMap<Contracts.v1.AuthorCreateRequest, AuthorCreateModel>();
		_ = CreateMap<Contracts.v1.AuthorUpdateRequest, AuthorUpdateModel>();
		_ = CreateMap<Author, Contracts.v1.AuthorResponse>();

		// Book
		_ = CreateMap<Contracts.v1.BookCreateOrUpdateRequest, BookCreateModel>();
		_ = CreateMap<Contracts.v1.BookCreateOrUpdateRequest, BookUpdateModel>();
		_ = CreateMap<Book, Contracts.v1.BookResponse>();

		// Loan
		_ = CreateMap<Contracts.v1.LoanCreateRequest, LoanCreateModel>();
		_ = CreateMap<Contracts.v1.LoanUpdateRequest, LoanUpdateModel>();
		_ = CreateMap<Loan, Contracts.v1.LoanResponse>();

		// Patron
		_ = CreateMap<Contracts.v1.PatronCreateRequest, PatronCreateModel>();
		_ = CreateMap<Contracts.v1.PatronUpdateRequest, PatronUpdateModel>();
		_ = CreateMap<Patron, Contracts.v1.PatronResponse>();

		// Reservation
		_ = CreateMap<Contracts.v1.ReservationCreateRequest, ReservationCreateModel>();
		_ = CreateMap<Contracts.v1.ReservationUpdateRequest, ReservationUpdateModel>();
		_ = CreateMap<Reservation, Contracts.v1.ReservationResponse>();

		#endregion

		#region v2

		// Author
		_ = CreateMap<Contracts.v2.AuthorCreateRequest, AuthorCreateModel>();
		_ = CreateMap<Contracts.v2.AuthorUpdateRequest, AuthorUpdateModel>();
		_ = CreateMap<Author, Contracts.v2.AuthorResponse>();

		#endregion
	}
}
