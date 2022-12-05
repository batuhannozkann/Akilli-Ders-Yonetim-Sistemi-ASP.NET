using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using Adys.Repository.Contexts;
using Adys.Repository.Repositories;
using Adys.Repository.Services;
using Adys.Repository.UnitOfWork;
using Adys.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<AdysAppContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AdysAppContext)).GetName().Name);
    });
}
);

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