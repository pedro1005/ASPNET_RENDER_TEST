using EisntMvc.Data;
using Microsoft.EntityFrameworkCore;
using EisntMvc.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Set up PostgreSQL connection using the connection string from appsettings or environment variable
builder.Services.AddDbContext<EisntDbContext>(options =>
    options.UseNpgsql("Host=dpg-d0f9ehpr0fns7397cui0-a.frankfurt-postgres.render.com;Database=gestor_clientes;Username=root;Password=yoBnbpLHsok1eQOUw1tQ6qYWUTiJ4nIM"));

// Register your repository
builder.Services.AddScoped<IRepositorioProdutos, RepositorioProdutos>();

// Session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Time the session will remain active
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Sessions, Authentication, and MVC
builder.Services.AddSession();

var app = builder.Build();

// Enable Swagger only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestao de Produtos API v1");
    });
}

// Configure the HTTP request pipeline for non-development environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Default HSTS value is 30 days. You might want to change this for production.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session, authentication, and authorization
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Default routing configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// API route
app.MapControllers();

app.Run();