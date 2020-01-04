using System;
using System.Collections.Generic;
using System.Text;
using Management.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Management.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Article { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<Answer> Answer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Article>().HasKey(i => i.Id);
            builder.Entity<Article>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Survey>().HasKey(i => i.Id);
            builder.Entity<Survey>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Survey>()
                .HasOne(e => e.Article)
                .WithMany(c => c.SurveyList)
                .HasForeignKey(f => f.ArticleId);


            builder.Entity<Answer>().HasKey(i => i.Id);
            builder.Entity<Answer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Answer>()
                .HasOne(e => e.Survey)
                .WithMany(c => c.AnswerList)
                .HasForeignKey(f => f.SurveyId);
        }
    }
}
