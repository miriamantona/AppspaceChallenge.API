using AppspaceChallenge.DataAccess.DBContext;
using AppspaceChallenge.DataAccess.Repositories;
using AppspaceChallenge.API.Services;
using Microsoft.EntityFrameworkCore;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IKeywordsRepository, KeywordsRepository>();
builder.Services.AddScoped<IDetailsRepository, DetailsRepository>();
builder.Services.AddScoped<IIntelligentBillBoardManager, IntelligentBillBoardManager>();


builder.Services.AddDbContext<BeezyCinemaDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeezyCinemaReadOnly")));

//Enable CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: myAllowSpecificOrigins,
    builder =>
    {
      builder.WithOrigins("https://localhost:4200/")
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
