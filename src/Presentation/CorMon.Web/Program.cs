using AspNetCore.Identity.Mongo;
using CorMon.Core.Domain;
using CorMon.Infrastructure.DataProviders;
using CorMon.IocConfig;
using RedisCache.Core;

var builder = WebApplication.CreateBuilder(args);

#region Caching

builder.Services.AddRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:Connection"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"];
});


#endregion

#region Identity

builder.Services.AddIdentityMongoDbProvider<User, Role>(identityOptions =>
{
    identityOptions.Password.RequiredLength = 6;
    identityOptions.Password.RequireLowercase = false;
    identityOptions.Password.RequireUppercase = false;
    identityOptions.Password.RequireNonAlphanumeric = false;
    identityOptions.Password.RequireDigit = false;
}, mongoIdentityOptions => {
    mongoIdentityOptions.ConnectionString = builder.Configuration["Mongo:Connection"] + "/" + builder.Configuration["Mongo:Database"];
});

#endregion


builder.Services.AddControllersWithViews();

builder.Services.ConfigureIocContainer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.Services.InitialDatabase();
app.Services.SeedDatabase();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
