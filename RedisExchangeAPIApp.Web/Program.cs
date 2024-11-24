using RedisExchangeAPIApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// RedisService'yi DI konteynerine ekle
builder.Services.AddSingleton<RedisService>();
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

// Redis baðlantýsýný baþlat
using (var scope = app.Services.CreateScope())
{
    var redisService = scope.ServiceProvider.GetRequiredService<RedisService>();
    redisService.Connect(); // Redis sunucusuna baðlan
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
