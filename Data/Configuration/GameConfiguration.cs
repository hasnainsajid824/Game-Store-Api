using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gamestoreapi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gamestoreapi.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(game => game.Price).HasPrecision(5,2);
        }
    }
}