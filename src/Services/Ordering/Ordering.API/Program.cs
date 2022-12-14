using Common.Logging;
using Serilog;
using Ordering.Infrastructure.Persisten;
using Ordering.Infrastructure;
using Ordering.Application;
using FluentValidation;
using Ordering.API.Extensions;
using FluentValidation.AspNetCore;

// dotnet ef migrations add "Update_OrderingDb" --project Ordering.Infrastructure --startup-project Ordering.API --output-dir Persisten/Migrations
// dotnet ef database update --project Ordering.Infrastructure --startup-project Ordering.API 
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.UseSerilog(Serilogger.Configure());
builder.Services.AddControllers();
// builder.Services.AddFluentValidation(
//     s =>
//     {
//         s.RegisterValidatorsFromAssemblyContaining<Program>();
//     }
// );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddServiceExtensions(builder.Configuration);

//builder.Services.ConfigureMassTransit();
var app = builder.Build();
Log.Information("Start Ordering API up");
Log.Information(builder.Configuration.GetConnectionString("DefaultConnectionString"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", (HttpContext context) => { context.Response.Redirect("/swagger"); });
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var seeding = scope.ServiceProvider.GetRequiredService<OrderingSeedingContext>();
    await seeding.InitialiseAsync();
    await seeding.SeedAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.LisenMessage();
app.Run();

