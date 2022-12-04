using FluentValidation;
using WebApplication1.Application.User.Create;
using WebApplication1.BusinessLogic.Factory;
using WebApplication1.BusinessLogic.Provider;
using WebApplication1.BusinessLogic.Repositories;
using WebApplication1.BusinessLogic.Services;
using WebApplication1.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services
builder.Services.AddTransient<IUserService, UserService>();

//Providers
builder.Services.AddTransient<IUserProvider, UserProvider>();

//Repository
builder.Services.AddTransient<IUserRepository, UserRepository>();

//FluenValidator
builder.Services.AddScoped<IValidator<RequestCreate>, UserValidator>();

//Factory
builder.Services.AddTransient<IUserTypeFactory, UserTypeFactory>();


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
