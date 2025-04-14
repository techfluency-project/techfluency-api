using TechFluency.Context;
using TechFluency.Repository;
using TechFluency.Services;

var builder = WebApplication.CreateBuilder(args);

// Cria o servico pra desativar o cors pro localhost:3000

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped(typeof(ITechFluencyRepository<>), typeof(TechFluencyRepository<>));

// USER PROGRESS
builder.Services.AddScoped<UserProgresRepository>();

// QUESTION
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<QuestionService>();

// PLACEMENT TEST
builder.Services.AddScoped<PlacementTestService>();

// LEARNING PATH
builder.Services.AddScoped<LearningPathRepository>();
builder.Services.AddScoped<LearningPathService>();

// PATHSTAGE
builder.Services.AddScoped<PathStageRepository>();
builder.Services.AddScoped<PathStageService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// desativa o cors

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
