    using System.Security.Claims;
using InstantAPIs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using WebApplication1;
using WebApplication1.Service;
using DbContext = WebApplication1.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<AppLogs>>();
builder.Services.AddSingleton(typeof(ILogger), logger);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
});


builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseMySql("Server=localhost; Database=ef; Uid=lord; Pwd=localhost;",
        ServerVersion.AutoDetect("Server=localhost; Database=ef; Uid=lord; Pwd=localhost;"));
});
builder.Services.AddSingleton<IGeolocationService, GeolocationService>();
builder.Services.AddCors();
builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddTransient<IProductMatcherService, ProductMatcherService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddControllers();

builder.Services.AddIdentity<Account, IdentityRole>(o =>
    {
        o.Password.RequireDigit = true;
        o.Password.RequireUppercase = true;
        o.User.RequireUniqueEmail = true;
        o.Password.RequireNonAlphanumeric = false;
        o.SignIn.RequireConfirmedEmail = true;
    })

    .AddEntityFrameworkStores<DbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthentication();

app.UseAuthorization();
app.Run();
