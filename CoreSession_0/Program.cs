﻿using CoreSession_0.Models.ContextClasses;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NorthwindContext>(x => x.UseSqlServer().UseLazyLoadingProxies());

builder.Services.AddDistributedMemoryCache(); //Eger Session kompleks yapılarla calısmak icin Extension metodu eklenme durumuna maruz kalmıssa bu kod projenizdeki hafızayı dagıtık sistemde tutarak daha saglıklı bir çevre sunacaktır...

builder.Services.AddSession(
    x =>
    {
        x.IdleTimeout = TimeSpan.FromMinutes(3); //Kişinin bos durma süresi eger 1 dakikalık bir bos durma süresi olursa Session yok olsun...
        x.Cookie.HttpOnly = true;
        x.Cookie.IsEssential = true;
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=SignIn}/{id?}");

app.Run();
