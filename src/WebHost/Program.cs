
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

#region Add Modules

builder.Services.AddSalesModules(builder.Configuration);
builder.Services.AddInventoryModules(builder.Configuration);
builder.Services.AddIdentityModule(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(SalesModules).Assembly)
    .AddApplicationPart(typeof(InventoryModules).Assembly)
    .AddApplicationPart(typeof(IdentityModule).Assembly)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;

        //JSON enums serialize as strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

#endregion


#region Configure Jwt Authentication

var jwtSettings = builder.Configuration
    .GetSection(JwtSettings.SectionName)
    .Get<JwtSettings>() 
    ?? throw new InvalidOperationException("JwtSettings section is missing in appsettings.json");

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role",
        NameClaimType = "sub",
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))

    };
});



#endregion



builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


#region CORS


var spaOrigins = builder.Configuration
    .GetSection(CorsPolicies.Sections.SpaOrigins)
    .Get<string[]>() ?? [];


builder.Services.AddCors(options => 
{
    options.AddPolicy(CorsPolicies.Spa, policy =>
    {
        policy
        .WithOrigins(spaOrigins)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetPreflightMaxAge(TimeSpan.FromHours(1));
    });
});

#endregion

#region Configure TimeOut

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
//});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();


app.UseCors(CorsPolicies.Spa); 
//app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#region Seeding Default Data

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

await DataSeeder.SeedAsync(dbContext, passwordHasher);

#endregion



app.Run();

