using Microsoft.AspNetCore.Authentication.JwtBearer;
using TechFluency.Context;
using TechFluency.Repository;
using TechFluency.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TechFluency.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddHttpContextAccessor();

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

// USER
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtService>();

// BADGE
builder.Services.AddScoped<BadgeService>();
builder.Services.AddScoped<BadgeRepository>();

//LEVEL ADVANCEMENT
builder.Services.AddScoped<LevelAdvancementService>();

//FLASHCARDS
builder.Services.AddScoped<FlashcardRepository>();
builder.Services.AddScoped<FlashcardGroupRepository>();
builder.Services.AddScoped<FlashcardGroupService>();
builder.Services.AddScoped<FlashcardService>();

// Adds authentication for Swagger requests
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };
    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Configures authentication and validates token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies.ContainsKey("jwt");
            if (token)
            {
                context.Token = context.Request.Cookies["jwt"];
            }

            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };

});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
}
);
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
