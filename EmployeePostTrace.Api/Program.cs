using EmployeePostTrace.Api.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using EmployeePostTrace.BusinessLayer.Middleware;
using EmployeePostTrace.Api.Extentions;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;

var dbConfig = new DbConfig();
builder.Configuration.Bind(dbConfig);

builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(dbConfig.CONNECTION_STRING));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentications();
builder.Services.AddServices();

builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
