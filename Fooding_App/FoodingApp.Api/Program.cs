using FoodingApp.Api.Extensions;
using FoodingApp.EfCore;
using Serilog;
using Serilog.Formatting.Json;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/log.txt",
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 10)
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();

builder.Host.UseSerilog();

builder.Services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions
                (o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }
                )
                .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<FoodingAppDb>(builder.Configuration.GetConnectionString("Test"));

// custom extension method to add scoped services
builder.Services.AddRepos();





    var app = builder.Build();
    Console.WriteLine("Building app succeeded.");




    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseGlobalExceptionHandling();

    app.MapControllers();

    app.Run();

