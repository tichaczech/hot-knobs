using thc.HotKnobs.Runtime.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.InitializeBuilder<Program>();

using var app = builder.Build();
app.InitializeApplication();

app.Run();
