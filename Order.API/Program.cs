using FluentValidation.AspNetCore;
using Order.Application.Services;
using Order.Domain.Services;
using Order.Infrastructure.ResourceManagerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IResourceManager, ResourceProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AnotherValidationClass>());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
