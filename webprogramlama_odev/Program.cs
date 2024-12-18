using webprogramlama_odev.Data;
using webprogramlama_odev.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static webprogramlama_odev.Helper.MailHelper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IMailHelper, MailHelper>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Giri� sayfas�
        options.AccessDeniedPath = "/Login"; // Eri�im reddedildi�inde
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));

var app = builder.Build();


// Veritaban�n� olu�turun veya migration'lar� uygulay�n
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();

    // Migration'lar� uygulay�n
    context.Database.Migrate();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anasayfa}/{action=Anasayfa}/{id?}");

app.Run();
