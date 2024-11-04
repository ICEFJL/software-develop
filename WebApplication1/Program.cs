using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle") ?? throw new InvalidOperationException("Connection string 'WebApplication1Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10); // 设置Session的空闲超时时间
    options.Cookie.HttpOnly = true;      // 设置Cookie HttpOnly属性，提高安全性
    options.Cookie.IsEssential = true;      // 标记为Essential，以便在不允许非必要Cookie的情况下使用
    options.Cookie.Name = "Session"; // 自定义Session Cookie的名称

});

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
