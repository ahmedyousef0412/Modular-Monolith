using Inventory.Api;
using Sales.Api;
using SharedKernel.Middlewares;

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
            System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;
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

