using System.Text;
using Basket.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(Common.Logging.Serilogger.Configure());
builder.Services.AddService()
                .ConfigureMapper()
                .ConfigureRedis(builder.Configuration)
                .ConfigureMassTransit()
                .ConfigureGrpcClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
Log.Information("Start Basket API");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.MapGet("/", async (context)=> context.Response.Redirect("/swagger"));
app.UseHttpsRedirection();



// app.MapWhen( (context) => {
//    if(context.Request.Path.Value.Contains("xxx")){
//         return true ; 
//    }
//    return false ; 
// } , (builder) => {
//     builder.Run(async (context)=>{ 
//         context.Response.Redirect("/swgger");
//         Console.WriteLine("vao repsond tra ve");
//     });
// } );
app.MapGet("/{id?}" ,async (HttpContext context)=>{ 
    byte[] body = new byte[200];
    await context.Request.Body.ReadAsync(body,0,200);
    var id = context.Request.RouteValues["id"];
    Console.WriteLine(Encoding.UTF8.GetString(body) + "id" +  id + "");
    context.Response.Redirect("/swagger");
});

app.UseAuthorization();

app.MapControllers();
app.Run();
