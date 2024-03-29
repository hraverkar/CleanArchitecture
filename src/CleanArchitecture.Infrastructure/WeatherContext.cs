﻿using CleanArchitecture.Core.Weather.Entities;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.Extensions.Hosting;
using CleanArchitecture.Core.CarCompanies.Entities;
using CleanArchitecture.Core.Task.Entities;
using CleanArchitecture.Core.Task_Details.Task_Status_Entities;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;
using CleanArchitecture.Core.Email_Notification.Entities;
using CleanArchitecture.Core.Projects.Entities;

namespace CleanArchitecture.Infrastructure
{
    public sealed class WeatherContext : DbContext
    {
        private static readonly ILoggerFactory DebugLoggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
        private readonly IHostEnvironment? _env;

        public WeatherContext(DbContextOptions<WeatherContext> options,
            IHostEnvironment? env) : base(options)
        {
            _env = env;
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<Project> Projects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_env != null && _env.IsDevelopment())
            {
                // used to print activity when debugging
                optionsBuilder.UseLoggerFactory(DebugLoggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeatherForecastConfiguration).Assembly);
            var aggregateTypes = modelBuilder.Model
                                             .GetEntityTypes()
                                             .Select(e => e.ClrType)
                                             .Where(e => !e.IsAbstract && e.IsAssignableTo(typeof(AggregateRoot)));

            foreach (var type in aggregateTypes)
            {
                var aggregateBuild = modelBuilder.Entity(type);
                aggregateBuild.Ignore(nameof(AggregateRoot.DomainEvents));
                aggregateBuild.Property(nameof(AggregateRoot.Id)).ValueGeneratedNever();
            }
        }
    }
}
