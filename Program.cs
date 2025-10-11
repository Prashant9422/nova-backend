// using Microsoft.EntityFrameworkCore;
// using NovaApp.Data;
// using NovaApp.Repositories;
// using NovaApp.Services;
// using NovaApp.Mappings;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container
// builder.Services.AddControllersWithViews();
// builder.Services.AddSwaggerGen();

// // Register DbContext before building the app
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8, 0, 36)) // Replace with your actual MySQL version
//     ));

// //Automapper
// // builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

// //Repository 
// builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
// builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
// builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();

// //Services
// builder.Services.AddScoped<IProductService, ProductService>();
// builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// builder.Services.AddScoped<IServiceService, ServiceService>();
// builder.Services.AddScoped<IAuthService, AuthService>();

// //JWT Authentication
// var jwt = builder.Configuration.GetSection("Jwt");
// var key = jwt.GetValue<string>("Key")!;
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.RequireHttpsMetadata = false; // set true in production
//     options.SaveToken = true;
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = jwt.GetValue<string>("Issuer"),
//         ValidAudience = jwt.GetValue<string>("Audience"),
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
//     };
// });

// // Authorization policies (optional — you can also use [Authorize(Roles="Admin")])
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//     options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
// });

// // Build the application
// var app = builder.Build();

// // Configure the HTTP request pipeline
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios
//     // see https://aka.ms/aspnetcore-hsts
//     app.UseHsts();
// }

// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();




using Microsoft.EntityFrameworkCore;
using NovaApp.Data;
using NovaApp.Repositories;
using NovaApp.Services;
using NovaApp.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    // Define the Bearer token security scheme for Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your Bearer token in the format **Bearer {your token here}**"
    });

    // Add security requirement globally (i.e., this will apply to all endpoints)
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Register DbContext before building the app
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)) // Replace with your actual MySQL version
    ));

// Automapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

// Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Authentication
var jwt = builder.Configuration.GetSection("Jwt");
var key = jwt.GetValue<string>("Key")!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // set true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.GetValue<string>("Issuer"),
        ValidAudience = jwt.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// Authorization policies (optional — you can also use [Authorize(Roles="Admin")])
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios
    // see https://aka.ms/aspnetcore-hsts
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NovaApp API V1");
    c.RoutePrefix = string.Empty;  // This makes Swagger UI available at the root of your app
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add Authentication middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
