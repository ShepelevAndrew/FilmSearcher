using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FilmSearcher.DAL.EntityTypeConfiguration
{
    public class ActorMovieTypeConfiguration : IEntityTypeConfiguration<ActorMovie>
    {
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            builder.HasOne(m => m.Movie).WithMany(m => m.ActorsMovies).HasForeignKey(am => am.MovieId);
            builder.HasOne(m => m.Actor).WithMany(a => a.ActorsMovies).HasForeignKey(am => am.ActorId);
        }
    }
}