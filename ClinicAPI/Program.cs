using ClinicAPI.Data;
using ClinicAPI.Auth.Models;
using ClinicAPI.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClinicAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddControllers();
var connString = builder.Configuration["AZURE_SQL_CONNECTIONSTRING"];

builder.Services.AddIdentity<ClinicUser, IdentityRole>()
    .AddEntityFrameworkStores<ClinicDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<ClinicDbContext>(option => option.UseSqlServer(connString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
    options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));

});

builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddTransient<IVisitRepository, VisitRepository>();
builder.Services.AddTransient<IProcedureRepository, ProcedureRepository>();

builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<AuthDbSeedRoles>();

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
});

builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

var dbSeeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<AuthDbSeedRoles>();
await dbSeeder.SeedAsync();



app.Run();
