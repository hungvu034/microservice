using Basket.API.Extensions;
using Serilog ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

 builder.Host.UseSerilog(Common.Logging.Serilogger.Configure());
 builder.Services.AddService().ConfigureRedis(builder.Configuration);
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
app.MapGet("/" ,async (context)=>{ context.Response.Redirect("/swagger");});

app.UseAuthorization();

app.MapControllers();
app.Run();
