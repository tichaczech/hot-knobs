import "@typespec/http";
import "@typespec/versioning";
import "../../../Common/Contracts/Contracts.tsp";

using TypeSpec.Http;
using TypeSpec.Versioning;
using HotKnobs.Contracts;

@service(#{
	title: "HotKnobs: Domains.Dummy",
})
@server("http://localhost:7901", "Locally hosted instance")
@server("https://domains-dummy.happywater-27678224.westeurope.azurecontainerapps.io", "Development instance")

@versioned(HotKnobs.Domains.Dummy.Contracts.Versions)
namespace HotKnobs.Domains.Dummy.Contracts;

enum Versions {
	v1,
	v2
}

alias claimAuthorRead = "author.read";
alias claimAuthorWrite = "author.write";
alias claimBookRead = "book.read";
alias claimBookWrite = "book.write";
alias claimLoanRead = "loan.read";
alias claimLoanWrite = "loan.write";
alias claimPatronRead = "patron.read";
alias claimPatronWrite = "patron.write";
alias claimReservationRead = "reservation.read";
alias claimReservationWrite = "reservation.write";

/**
 * Represents an Author.
 */
model Author extends Resource {
	/**
	 * Author first name.
	 */
	firstName: string;

	/**
	 * Author last name.
	 */
	lastName: string;

	/**
	 * Author nationality.
	 */
	@added(HotKnobs.Domains.Dummy.Contracts.Versions.v2)
	nationality: string;
}

model AuthorSummary extends Summary {
	...OmitProperties<Author, "nationality">;
}

/**
 * Author service.
 */
@route("/authors")
@tag("Author")
interface AuthorService extends ResourceOperationsWithCreateAndUpdate<Author, AuthorSummary, [claimAuthorRead], [claimAuthorWrite]> { }

/**
 * Represents a Book.
 */
model Book extends Resource {
	/**
	 * Book author.
	 */
	authorId: ResourceId;

	/**
	 * The original price of the book.
	 */
	originalPrice: decimal;

	/**
	 * The year the book was published.
	 */
	publishYear: int16;

	/**
	 * The date the book was purchased.
	 */
	@visibility(Lifecycle.Read)
	purchasedOn: offsetDateTime;

	/**
	 * Book title.
	 */
	title: string;
}

model BookSummary extends Summary {
	...OmitProperties<Book, "originalPrice" | "purchasedOn">;
}

/**
 * Book service.
 */
@route("/books")
@tag("Book")
interface BookService extends ResourceOperationsWithCreateAndUpdate<Book, BookSummary, [claimBookRead], [claimBookWrite]> { }

/**
 * Represents a Loan.
 */
model Loan extends Resource {
	/**
	 * The book being loaned.
	 */
	@visibility(Lifecycle.Create, Lifecycle.Read)
	bookId: ResourceId;

	/**
	 * The date the loan is due.
	 */
	dueOn: plainDate;

	/**
	 * The date the loan was created.
	 */
	@visibility(Lifecycle.Read)
	loanedOn: offsetDateTime;

	/**
	 * The user who loaned the book.
	 */
	@visibility(Lifecycle.Create, Lifecycle.Read)
	patronId: ResourceId;

	/**
	 * The reservation associated with the loan.
	 */
	@visibility(Lifecycle.Read)
	reservationId: ResourceId;

	/**
	 * The date the loan was returned.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Update)
	returnedOn?: offsetDateTime;
}

model LoanCreateFromReservationRequestModel extends Resource {
	@path()
	reservationId: ResourceId;
}

model LoanSummary extends Summary {
	...OmitProperties<Loan, "dueOn" | "loanedOn" | "reservationId" | "returnedOn">;
}

/**
 * Loan service.
 */
@route("/loans")
@tag("Loan")
interface LoanService extends ResourceOperationsWithCreateAndUpdate<Loan, LoanSummary, [claimLoanRead], [claimLoanWrite]> {
	/**
	 * Creates a new instance of the Loan.
	 */
	@TypeSpec.Rest.autoRoute()
	@TypeSpec.Rest.createsResource(Loan)
	@useAuth(Auth<[claimLoanWrite]>)
	createFromReservation(...LoanCreateFromReservationRequestModel): ResourceCreatedResponseModel<Loan> | CreateRequestError;
}

model Patron extends Resource {
	/**
	 * Patron birth date.
	 */
	birthDate: plainDate;

	/**
	 * Patron first name.
	 */
	firstName: string;

	/**
	 * Patron last name.
	 */
	lastName: string;
}

model PatronSummary extends Summary {
	...OmitProperties<Patron, "birthDate">;
}

/**
 * Patron service.
 */
@route("/patrons")
@tag("Patron")
interface PatronService extends ResourceOperationsWithCreateAndUpdate<Patron, PatronSummary, [claimPatronRead], [claimPatronWrite]> { }

/**
 * Represents a Reservation.
 */
model Reservation extends Resource {
	/**
	 * The book being reserved.
	 */
	@visibility(Lifecycle.Create, Lifecycle.Read)
	bookId: ResourceId;

	/**
	 * The date reservation ends.
	 */
	endsOn: plainDate;

	/**
	 * The user who reserved the book.
	 */
	@visibility(Lifecycle.Create, Lifecycle.Read)
	patronId: ResourceId;

	/**
	 * The date reservation starts.
	 */
	startsOn: plainDate;
}

model ReservationSummary extends Summary {
	...OmitProperties<Reservation, "endsOn" | "startsOn">;
}

/**
 * Reservation service.
 */
@route("/reservations")
@tag("Reservation")
interface ReservationService extends ResourceOperationsWithCreateAndUpdate<Reservation, ReservationSummary, [claimReservationRead], [claimReservationWrite]> { }
