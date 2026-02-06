using FoodingApp.Api.Extensions;
using FoodingApp.Api.Services.Mapping;
using FoodingApp.EfCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions
                (o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }
                );
builder.Services.AddAutoMapper(cfg => { },typeof(ItemsAndCategoriesMapper));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddRouting(o=> o.LowercaseUrls= true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<FoodingAppDb>(builder.Configuration.GetConnectionString("Test"));

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
