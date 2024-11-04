using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ItemManage.Data;
using ItemManage.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ItemManageContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found."))
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10); // ����Session�Ŀ��г�ʱʱ��
    options.Cookie.HttpOnly = true;      // ����Cookie HttpOnly���ԣ���߰�ȫ��
    options.Cookie.IsEssential = true;      // ���ΪEssential���Ա��ڲ�����Ǳ�ҪCookie�������ʹ��
    options.Cookie.Name = "Session"; // �Զ���Session Cookie������

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
