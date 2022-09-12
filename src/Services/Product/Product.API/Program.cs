using Common.Logging ;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog ; 


Log.Information("Starting Product API up"); 
var builder = WebApplication.CreateBuilder(args);

try{
    builder.Host.UseSerilog(Common.Logging.Serilogger.Configure());
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddScoped(typeof(IRepositoryBaseAsync<,,>) , typeof(RepositoryBaseAsync<,,>));
    var app = builder.Build();
    app.MigrateDatabase<ProductContext>(
        async (context , service) => {
         var logger = service.GetRequiredService<Serilog.ILogger>(); 
         ProductContextSeed.SeedProductAsync(context , logger).Wait();
        }
    );
    // Configure the HTTP request pipeline.
    app.UseInfrastructure();
    app.Run();
}
catch(Exception ex){
    string TypeName = ex.GetType().Name;
    if(TypeName.Equals("StopTheHostException" , StringComparison.Ordinal)){
        throw new Exception(); 
    }
        Log.Fatal(ex , $"Unhandled exception: {ex.Message}");
}
finally{
    Log.Information("Shutdown Product API complete");
    Log.CloseAndFlush();
}