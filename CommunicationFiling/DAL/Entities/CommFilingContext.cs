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
    }
}
