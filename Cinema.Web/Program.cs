using Cinema.DataAccess;
using Cinema.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddWebAutoMapper();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
    var imageSource = app.Configuration.GetSection("SeedSettings").GetValue<string>("ImageSource");
    if (string.IsNullOrEmpty(imageSource))
    {
        Console.Error.WriteLine("Missing configuration Seed:ImageSource");
        Environment.Exit(1);
    }
    DbInitializer.Initialize(context, imageSource);
}

app.Run();
