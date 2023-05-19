
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ChatGPT_UI;
using ChatGPT_UI.Services;
using ChatGPT_UI.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = configBuilder.Build();

        // Add services to the container.
        builder.Services
            .AddControllersWithViews()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        builder.Services.AddHttpClient();
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddMvc();
        builder.Services.AddScoped<IChatApiService,ChatService>();
        builder.Services.AddScoped<IChatContextService,ChatContextService>();
        builder.Services.AddScoped<ITextApiService,TextService>();
        builder.Services.AddScoped<ITextContextService,TextContextService>();
        builder.Services.AddScoped<IImageApiService,ImagesService>();
        builder.Services.AddScoped<IImageContextService, ImageContextService>();    

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=ChatWebAPI}/{action=Index}/{id?}");

/*        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=ChatWebAPI}/{action=Index}/{id?}");*/

        app.Run();
    }
}