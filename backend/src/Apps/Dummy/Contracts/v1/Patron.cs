using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Domains.Dummy.Contracts.v1;

/// <summary>
/// Represents a patron.
/// </summary>
public abstract class PatronContract
{
	/// <summary>
	/// The first name of the patron.
	/// </summary>
	[Required]
	public string? FirstName { get; set; }

	/// <summary>
	/// The last name of the patron.
	/// </summary>
	[Required]
	public string? LastName { get; set; }

	/// <summary>
	/// Date of birth of the patron.
	/// </summary>
	[Required]
	public DateTimeOffset DateOfBirth { get; set; }
}

/// <summary>
/// Request to create a patron.
/// </summary>
public class PatronCreateRequest : PatronContract, IResourceCreateRequest { }

/// <summary>
/// General response to patron requests.
/// </summary>
public class PatronResponse : PatronContract, IResourceResponse
{
	/// <summary>
	/// Indicates whether the patron is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Patron ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }
}

/// <summary>
/// Request to update a patron.
/// </summary>
public class PatronUpdateRequest : PatronContract, IResourceUpdateRequest { }

/// <summary>
/// Provides methods for managing patrons.
/// </summary>
public interface IPatronService : IResourceOperationsWithCreateAndUpdate<PatronResponse, PatronCreateRequest, PatronUpdateRequest>;
