using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Runtime.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.InitializeBuilder<Program>();
builder.Services.AddControllers().AddOData(options => options.AddRouteComponents("odata", GetEdmModel()).Select().Filter().OrderBy().Expand().Count().SetMaxTop(null));

static IEdmModel GetEdmModel()
{
	var builder = new ODataConventionModelBuilder();
	_ = builder.EntitySet<Author>("Authors");
	return builder.GetEdmModel();
}

using var app = builder.Build();
app.InitializeApplication();

app.Run();
