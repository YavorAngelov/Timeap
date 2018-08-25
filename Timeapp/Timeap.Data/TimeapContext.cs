using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timeap.Models;

namespace Timeap.Web.Data
{
    public class TimeapContext : IdentityDbContext<User>
    {
        public TimeapContext(DbContextOptions<TimeapContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<UsersTeams> UsersTeams { get; set; }

        public DbSet<Solution> Solutions { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<StatusType> StatusTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>()
                .Property(p => p.Progress)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Project>()
                .HasOne(project => project.Team)
                .WithOne(team => team.Project)
                .HasForeignKey<Team>(team => team.ProjectId);

            builder.Entity<Status>()
               .HasOne(status => status.Solution)
               .WithOne(solution => solution.Status)
               .HasForeignKey<Solution>(solution => solution.StatusId);

            builder.Entity<User>()
                .HasMany(user => user.Projects)
                .WithOne(project => project.Client)
                .HasForeignKey(project => project.ClientId);

            builder.Entity<Team>()
                .HasMany(team => team.Solutions)
                .WithOne(solution => solution.Team)
                .HasForeignKey(solution => solution.TeamId);

            builder.Entity<StatusType>()
                .HasMany(statusType => statusType.Statuses)
                .WithOne(status => status.StatusType)
                .HasForeignKey(status => status.StatusTypeId);

            builder.Entity<Team>()
                .HasMany(team => team.Members)
                .WithOne(member => member.Team)
                .HasForeignKey(member => member.TeamId);

            builder.Entity<User>()
                .HasMany(user => user.Teams)
                .WithOne(team => team.Member)
                .HasForeignKey(team => team.MemberId);

            builder.Entity<UsersTeams>()
                .HasKey(ut => new { ut.MemberId, ut.TeamId });

            base.OnModelCreating(builder);
        }
    }
}
