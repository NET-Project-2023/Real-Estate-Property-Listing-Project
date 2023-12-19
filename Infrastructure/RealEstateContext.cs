﻿using Microsoft.EntityFrameworkCore;
using Real_estate.Domain.Entities;

namespace Infrastructure
{
    public class RealEstateContext: DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options) { }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Listing> Listings { get; set; }

        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) 
        //{ 
        //    foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>()) 
        //    { 
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
        //            entry.Entity.CreatedDate = DateTime.UtcNow;
        //        }
        //        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
        //        {
        //            entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
        //            entry.Entity.LastModifiedDate = DateTime.UtcNow;
        //        }
        //    }
        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}
            
    }
}
