using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record LoanGetByIdQuery : EntityGetByIdQuery<Loan>;


public record LoanListQuery<TRepresentation> : EntityListQuery<Loan, TRepresentation>
	where TRepresentation : class, IModel
{
	public LoanListQuery()
	{
		FilteringEnabled = false;
	}
}
