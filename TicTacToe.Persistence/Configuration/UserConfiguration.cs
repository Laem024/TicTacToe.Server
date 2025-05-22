using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder
                .Property(u => u.Id)
                .HasColumnName("user_id")
                .UseIdentityColumn();

            builder
                .Property(u => u.Email)
                .HasColumnName("user_email")
                .HasMaxLength(320)
                .IsRequired();

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(u => u.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder
                .Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}