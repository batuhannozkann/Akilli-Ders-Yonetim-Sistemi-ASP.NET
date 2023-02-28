using Adys.Core.Configuration;
using Adys.Core.Identity;
using Adys.Core.Identity.Service;
using Adys.Core.Repositories;
using Adys.Core.Services;
using Adys.Core.UnitOfWork;
using Adys.Repository.Contexts;
using Adys.Repository.Repositories;
using Adys.Repository.UnitOfWork;
using Adys.Service.Mapping;
using Adys.Service.Services;
using Adys.SharedLibrary.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true
    };
});
var MyAllowSpesificOrigins = "_myAllowSpesificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpesificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IAcademicianService, AcademicianService>();
builder.Services.AddScoped<IAcademicianRepository, AcademicianRepository>();
builder.Services.AddScoped(typeof(IIdentityGenericRepository<>), (typeof(IdentityGenericRepository<>)));
builder.Services.AddScoped(typeof(IIdentityGenericService<>), typeof(IdentityGenericService<>));
builder.Services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ILessonStudentRepository, LessonStudentRepository>();
builder.Services.AddScoped<ILessonStudentService, LessonStudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILessonFileRepository, LessonFileRepository>();
builder.Services.AddScoped<ILessonFileService, LessonFileService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddIdentity<UserApp, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<IdentityContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityContext)).GetName().Name);
    });

});
builder.Services.AddDbContext<AdysAppContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"), option =>
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
app.UseCors(MyAllowSpesificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

