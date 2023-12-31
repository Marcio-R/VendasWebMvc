﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VendasWebMvc.Data;
using VendasWebMvc.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VendasWebMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VendasWebMvcContext") ?? throw new InvalidOperationException("Connection string 'VendasWebMvcContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<VendedorService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<RegistrosDeVendasService>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
