using Inventory.Api;
using Sales.Api;
using SharedKernel.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

#region Add Modules

builder.Services.AddSalesModules(builder.Configuration);
builder.Services.AddInventoryModules(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(SalesModules).Assembly)
    .AddApplicationPart(typeof(InventoryModules).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.UnmappedMemberHandling =
           JsonUnmappedMemberHandling.Disallow;


        //JSON enums serialize as strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();

