using ChatGPT_UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddHttpClient();

builder.Services.AddMvc();
builder.Services.AddScoped<ApiService>();

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

app.Run();
