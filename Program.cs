global using CrudDapperMultiQuerySchoolApi.Common;
global using CrudDapperMultiQuerySchoolApi.Models;
global using CrudDapperMultiQuerySchoolApi.IServices;
global using CrudDapperMultiQuerySchoolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Global.ConnectionString = builder.Configuration.GetConnectionString("SchoolDB");
builder.Services.AddScoped<ISchoolService, SchoolService>();

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
