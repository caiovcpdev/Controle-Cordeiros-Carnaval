using ControleCordeirosCarnaval.Data;
using ControleCordeirosCarnaval.HttpClient;
using ControleCordeirosCarnaval.HttpClient.Interfaces;
using ControleCordeirosCarnaval.HttpClient.Refit;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IWebApiCordeiroIntegracao, WebApiCordeiroIntegracao>();

builder.Services.AddRefitClient<IWebApiCordeiroIntegracaoRefit>()
.ConfigureHttpClient(x => {
    x.BaseAddress = new Uri("https://localhost:7025/");
});

builder.Services.AddDbContext<AppDBContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
