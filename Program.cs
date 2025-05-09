using EisntMvc.Data;
using Microsoft.EntityFrameworkCore;
using EisntMvc.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EisntDbContext>(options => 
    options.UseMySQL("Server=localhost;Port=3306;Database=GestorClientes;User=root;Password=12345678;"));
builder.Services.AddScoped<IRepositorioProdutos, RepositorioProdutos>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo que a sessão ficará ativa
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Sessões, Autenticação e MVC
builder.Services.AddSession();

var app = builder.Build();

// Ativar Swagger apenas no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestao de Produtos API v1");
    });
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rota para API
app.MapControllers();

app.Run();
