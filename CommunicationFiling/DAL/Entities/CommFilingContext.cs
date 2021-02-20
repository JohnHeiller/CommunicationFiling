using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationFiling.DAL.Entities
{
    public class CommFilingContext : DbContext
    {
        public CommFilingContext(DbContextOptions<CommFilingContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<RoleAction> RolesActions { get; set; }
        public DbSet<CorrespondenceType> CorrespondenceTypes { get; set; }
        public DbSet<Filing> Filings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserRole>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserRole>().HasOne(o => o.Role)
                .WithMany()
                .HasForeignKey(s => s.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserRole>().HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audit>().HasOne(o => o.CreationUser)
                .WithMany()
                .HasForeignKey(s => s.CreationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Audit>().HasOne(o => o.ModificationUser)
                .WithMany()
                .HasForeignKey(s => s.ModificationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Action>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CorrespondenceType>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Filing>().HasOne(o => o.AddresseeUser)
                .WithMany()
                .HasForeignKey(s => s.AddresseeUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Filing>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Filing>().HasOne(o => o.CorrespondenceType)
                .WithMany()
                .HasForeignKey(s => s.CorrespondenceTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Filing>().HasOne(o => o.SenderUser)
                .WithMany()
                .HasForeignKey(s => s.SenderUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RoleAction>().HasOne(o => o.Action)
                .WithMany()
                .HasForeignKey(s => s.ActionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RoleAction>().HasOne(o => o.Role)
                .WithMany()
                .HasForeignKey(s => s.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RoleAction>().HasOne(o => o.Audit)
                .WithMany()
                .HasForeignKey(s => s.AuditId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
