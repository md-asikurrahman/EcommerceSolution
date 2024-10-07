using Serilog;
using Serilog.Events;
using ECommerceSolution.Service;
using Serilog.Formatting.Compact;
using ECommerceSolution.Infrastructure;
using ECommerceSolution.WEB.Areas.Admin.CommonMethods;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console(new CompactJsonFormatter())
.CreateLogger();

Log.Logger.Information("Logger is open");
builder.Services.AddInfrastructureDependency(builder.Configuration);
builder.Services.AddServiceDependency(builder.Configuration);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
   
});
builder.Services.AddAuthentication().AddCookie( config =>
{
    config.LoginPath = "/Account/Login";
    config.ExpireTimeSpan = TimeSpan.FromDays(3);
});
builder.Services.AddScoped<ImageUpload>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();


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
app.UseCookiePolicy();
app.UseRouting();
app.MapControllers();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{Id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllers();
});

app.Run();
