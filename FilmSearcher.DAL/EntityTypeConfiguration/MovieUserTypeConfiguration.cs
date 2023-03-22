using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FilmSearcher.DAL.EntityTypeConfiguration
{
    public class MovieUserTypeConfiguration : IEntityTypeConfiguration<MovieUser>
    {
        public void Configure(EntityTypeBuilder<MovieUser> builder)
        {
            builder.HasKey(mu => new
            {
                mu.UserId,
                mu.MovieId
            });

            builder.HasOne(mu => mu.Movie).WithMany(m => m.MoviesUsers).HasForeignKey(mu => mu.MovieId);
            builder.HasOne(mu => mu.User).WithMany(u => u.MoviesUsers).HasForeignKey(mu => mu.UserId);
        }
    }
}