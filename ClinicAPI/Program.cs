using ClinicAPI.Data;
using ClinicAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Microsoft.EntityFrameworkCore.Tools
// Microsoft.EntityFrameworkCore.SqlServer
builder.Services.AddControllers();
var connString = builder.Configuration["DBConnectionString"];
builder.Services.AddDbContext<ClinicDbContext>(option => option.UseSqlServer(connString));
builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddTransient<IVisitRepository, VisitRepository>();






var app = builder.Build();

app.MapControllers();
app.Run();
