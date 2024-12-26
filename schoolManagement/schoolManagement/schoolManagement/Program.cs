using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interface;
using SchoolManagement.Application.UseCases.Command;
using SchoolManagement.Application.UseCases.StudentRepository;
using ShoolManagement.Infrastructure;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SchoolManagementContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// repositories
builder.Services.AddScoped<IStudent, StudentRepo>();

builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.RegisterServicesFromAssembly(typeof(CreateStudentCommand).Assembly);
    //config.RegisterServicesFromAssembly(typeof(SchoolManagement.Application.UseCases).Assembly);
});


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
