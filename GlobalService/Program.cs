using GlobalService.Utilities;
using GlobalService.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GlobalService.Hubs;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GlobalService.Services;

#region Service
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddRouting();

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lavoro-io.UserService", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(p => 
    p.AddPolicy("corsapp", builder =>
        {
            builder.AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   .WithOrigins("https://lavoro-io.azurewebsites.net",
                                "http://localhost:4200");
        })
    );

//register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatService, ChatService>();

//create connection string to Db
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration["Settings:LvrIoDb"]);
conStrBuilder.Password = builder.Configuration["Settings:DbPassword"];
conStrBuilder.UserID = builder.Configuration["Settings:DbUser"];
var connection = conStrBuilder.ConnectionString;

var options = new DbContextOptionsBuilder<GloabalContext>()
                   .UseSqlServer(connection)
                   .Options;

builder.Services.AddDbContext<GloabalContext>(option =>
    option.UseSqlServer(connection)
);

//UserContextInitializer.InitDbContext(options); //Use only to add 

#endregion

#region App
var app = builder.Build();

app.UseMiddleware<AuthorizationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();

#endregion