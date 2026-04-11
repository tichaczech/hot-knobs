using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using thc.HotKnobs.Domains.Dummy.UseCases;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Hosting;

/// <summary>
/// Host Builder Factory for Dummy Services.
/// </summary>
public class DummyServicesHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);

		_ = builder.Services.AddScoped<IAuthorUseCases, AuthorUseCases>();
		_ = builder.Services.AddScoped<IBookUseCases, BookUseCases>();
		_ = builder.Services.AddScoped<ILoanUseCases, LoanUseCases>();
		_ = builder.Services.AddScoped<IPatronUseCases, PatronUseCases>();
		_ = builder.Services.AddScoped<IReservationUseCases, ReservationUseCases>();

		return builder;
	}
}
