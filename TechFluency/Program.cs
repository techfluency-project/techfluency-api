using TechFluency.Context;
using TechFluency.Repository;
using TechFluency.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped(typeof(ITechFluencyRepository<>), typeof(TechFluencyRepository<>));
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<PlacementTestService>();


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
