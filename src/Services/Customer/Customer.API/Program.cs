using System.Reflection.Metadata.Ecma335;
using System;

using Serilog ; 
using Common.Logging ;
using Customer.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Customer.API.Repositories.Interfaces;
using Customer.API.Repositories;
using Customer.API.Services.Interfaces;
using Customer.API.Services;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Shared.DTOs.Customer;
using Customer.API;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(Serilogger.Configure());
Log.Information("Start Customer API up ");
try{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionStrings");
    Console.WriteLine(ConnectionString);
    builder.Services.AddDbContext<CustomerContext>(
        options => options.UseNpgsql(ConnectionString)
    );
    builder.Services.AddScoped(typeof(IUnitofWork<>) , typeof(UnitOfWork<>));
    builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
    builder.Services.AddScoped<ICustomerService , CustomerService>();
    builder.Services.AddScoped<ICustomerAdvanceService , CustomerAdvanceService>();
    builder.Services.AddScoped<IMinimalValidator,MinimalValidator>();
    builder.Services.AddAutoMapper(conf => conf.AddProfile(new MappingProfile()));
    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.MapGet("/api/customers/" 
    , ([FromServices]ICustomerService service) => service.GetAllCustomers());

    app.MapGet("/api/customer/{username?}"
    , ([FromServices]ICustomerService service , [FromRoute]string username) => {
        if(!service.GetAllCustomers().Any(x => x.UserName == username)){
            return Results.NotFound();
        }
        return service.GetCustomerByUserName(username);
    });
    app.MapDelete("/api/customer/{id?}",
        async ([FromServicesAttribute]ICustomerService service , [FromRouteAttribute]int? id) => {
            if(id == null){
                var customers = service.GetAllCustomers().ToArray();
                int[] Ids = customers.Select(x => x.Id).ToArray();
                foreach(var customer in customers){
                   await service.DeleteCustomer(customer);
                }
                return Results.Ok(Ids);
            }
            var DeleteCustomer = service.GetAllCustomers().Where(x => x.Id == id).FirstOrDefault();
            if(DeleteCustomer == null){
                return Results.NotFound();
            }
            await service.DeleteCustomer(DeleteCustomer);
            return Results.Ok(id);
        }
    );

    app.MapPost("/api/customer/" ,
        async ([FromServices]ICustomerService service , [FromServices]IMinimalValidator validator , [FromServices]IMapper mapper , [FromBodyAttribute]CreatedCustomerDto dto) => {
            var Result = validator.Validate(dto);
            if(!Result.IsValid){
                return Results.Ok(Result.Errors)  ; 
            }
            var customer = mapper.Map<Customer.API.Entites.Customer>(dto);
            try{
                int id = await service.CreateCustomer(customer);
                return Results.Ok(id);
            }
            catch(DbUpdateException ex){
              //  DbUpdateException ex = e as DbUpdateException  ;
                // if(ex == null){
                //     return Results.Ok(ex.GetType().Name);
                // }
                if(ex.GetBaseException() is PostgresException postgresException){
                    switch(postgresException.Code){
                        case "23505": 
                            if(postgresException.ConstraintName.Contains("EmailAddress")){
                                return Results.Ok("EmailAddress existed");
                            }
                            else if(postgresException.ConstraintName.Contains("UserName")){
                                return Results.Ok("UserName existed");
                            }
                        break;
                    }
                }
                return Results.Ok(ex.Message);
            }
        }
    );
    
    app.MapPut("/api/customer/{id:int}" , 
        async ([FromServicesAttribute] ICustomerAdvanceService service , [FromServicesAttribute]IMapper mapper , [FromRouteAttribute]int id , [FromBodyAttribute]UpdateCustomerDto dto ) => {
            var exist = service.FindCustomerById(id);
            if(exist == null){
                return Results.NotFound();
            }
            var updateData =  mapper.Map(dto , exist);
            try{
                await service.UpdateCustomer(updateData);
            }
            catch(DbUpdateException ex){
                if(ex.GetBaseException() is PostgresException postgresException){
                    switch(postgresException.Code){
                        case "23505":
                            if(postgresException.ConstraintName.Contains("EmailAddress"))
                                return Results.Ok("Email address is existed");
                            break; 
                        default:
                            throw ; 
                    }

                }    
            }
            return Results.Ok(mapper.Map<CustomerDto>(updateData));
        }
    );
   

    app.MapPost("/api/update-email-by-username/{username}" 
    ,  ([FromServices]ICustomerAdvanceService service , [FromBodyAttribute]string email , [FromRouteAttribute] string username) => {
       bool success = service.UpdateEmailByUserName(email , username);
       if(!success)
            return Results.NotFound();
        return Results.Ok();
    }
    );

    app.MapGet("/api/get-fullname/{username}"
    , ([FromServices]ICustomerAdvanceService service , [FromRouteAttribute]string username) => {
        return service.GetFullNameByUserName(username);
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapGet("/" , (HttpContext context ) => context.Response.Redirect("/swagger"));

    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedingCustomer().Run();
}
catch(Exception e){
    string ExceptionName = e.GetType().Name ; 
    if(ExceptionName.Equals("StopTheHostException",StringComparison.Ordinal)){
        throw ;
    }
    Log.Information(e , $"Unhandled exception: {e.Message}");
}
finally{
    Log.Information("Shutdown Customer API");
    Log.CloseAndFlush();
}
