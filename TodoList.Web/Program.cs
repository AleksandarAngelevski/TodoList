using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Services;
using Repository.Data;
using Repository.HttpClient;
using Service.ApiSettings;
using Service.Interfaces;
using Service.Jobs;
using Web.Interceptor;
using Web.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Register Task Service
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

// Register the AuditInterceptor service
builder.Services.AddScoped<AuditInterceptor>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

// Api settings 
builder.Services.Configure <CatFactsApiSettings>(
    builder.Configuration.GetSection("CatFactsApi")
    );
builder.Services.AddHttpClient<ICatFactsApiClient,CatFactsApiClient>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<CatFactsApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);


});

// Register Background Services
builder.Services.AddHostedService<AuditFieldsBackgroundService>();


// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



// Open generic registration - maps IRepository<T> to BaseRepository<T> for any T at runtime.
// // e.g. IRepository<Task> → BaseRepository<Task>, IRepository<User> → BaseRepository<User>e
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register TaskMapper
builder.Services.AddScoped<TaskMapper>();


// Add Authentication
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

app.Run();