using AspNetCore.Identity.Mongo;
using CorMon.Core.Domain;
using CorMon.Infrastructure.DataProviders;
using CorMon.IocConfig;
using CorMon.Web.Api.Services.Jwt;
using Microsoft.OpenApi.Models;
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

builder.Services.AddJwt(options =>
{
    builder.Configuration.GetSection("Jwt").Bind(options);

});

// Register the Swagger generator, defining one or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(builder.Configuration["Swagger:Version"], new OpenApiInfo { Title = builder.Configuration["Swagger:Title"], Version = builder.Configuration["Swagger:Version"] });
    var includeXmlComments = builder.Configuration["Swagger:IncludeXmlComments"].Split(",");
    foreach (var includeXmlComment in includeXmlComments)
        c.IncludeXmlComments(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, includeXmlComment));
});


builder.Services.AddControllers();

builder.Services.ConfigureIocContainer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
});


app.Services.InitialDatabase();
app.Services.SeedDatabase();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Help}/{action=Get_Api_Documentation_Url}/{id?}");
});
app.Run();
