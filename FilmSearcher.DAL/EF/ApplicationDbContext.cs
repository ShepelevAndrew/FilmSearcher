using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<MovieUser> MoviesUsers { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserTypeConfiguration());
            builder.ApplyConfiguration(new ActorTypeConfiguration());
            builder.ApplyConfiguration(new ProducerTypeConfiguration());
            builder.ApplyConfiguration(new CinemaTypeConfiguration());
            builder.ApplyConfiguration(new MovieTypeConfiguration());
            builder.ApplyConfiguration(new ActorMovieTypeConfiguration());
            builder.ApplyConfiguration(new MovieUserTypeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
