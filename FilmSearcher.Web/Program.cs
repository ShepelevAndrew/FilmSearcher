using FilmSearcher.BLL.Helpers;
using FilmSearcher.BLL.Services.Implementations;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Implementations;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), 
                                    b => b.MigrationsAssembly("FilmSearcher.Web"))
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.AddScoped<IBaseRepository<User>, UserRepository>()
                .AddScoped<IBaseRepository<Actor>, ActorRepository>()
                .AddScoped<IBaseRepository<Cinema>, CinemaRepository>()
                .AddScoped<IBaseRepository<Movie>, MovieRepository>()
                .AddScoped<IBaseRepository<Producer>, ProducerRepository>()
                .AddScoped<IActorMovieRepository, ActorMovieRepository>();

builder.Services.AddScoped<ISearchService<Actor>, SearchActorService>()
                .AddScoped<ISearchService<Movie>, SearchMovieService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IMovieService, MovieService>()
                .AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
