using FoodingApp.Api.Extensions;
using FoodingApp.EfCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.JsonPatch;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions
                (o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }
                );



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<FoodingAppDb>(builder.Configuration.GetConnectionString("Sqlite"));

// custom extension method to add scoped services
builder.Services.AddRepos();



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
