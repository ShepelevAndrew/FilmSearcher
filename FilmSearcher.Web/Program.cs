using FilmSearcher.BLL.Services.Implementations;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
);
builder.Services.AddScoped<ICrudService<Actor>, ActorService>();
builder.Services.AddScoped<ICrudService<Cinema>, CinemaService>();
builder.Services.AddScoped<ICrudService<Movie>, MovieService>();
builder.Services.AddScoped<ICrudService<Producer>, ProducerService>();

builder.Services.AddScoped<ISearchService<Actor>, SearchActorService>();
builder.Services.AddScoped<ISearchService<Movie>, SearchMovieService>();
builder.Services.AddControllersWithViews();

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
