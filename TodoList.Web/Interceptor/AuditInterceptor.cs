using Domain.Models;
using Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Web.Interceptor;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;

    public AuditInterceptor(ICurrentUser currentUser)
    {
        _currentUser= currentUser;
    }
    
//    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
//        CancellationToken cancellationToken = new CancellationToken())
//    {
//        if (eventData.Context is null) return base.SavingChangesAsync(eventData, result, cancellationToken);
//
//        var entries = eventData.Context.ChangeTracker.Entries<BaseAuditableEntity<ApplicationUser>>();
//
//        foreach (var entry in entries )
//        {
//            if (entry.State == EntityState.Added)
//            {
//                entry.Entity.DateCreated = DateTime.UtcNow;
//            }
//        }
//
//        return base.SavingChangesAsync(eventData, result, cancellationToken);
//    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        Console.Write("_%Q%%%%%%%%%%%%%%%%%%%%%%%%%%%"); 
        var context = eventData.Context;
        var entries = context.ChangeTracker.Entries<BaseAuditableEntity<ApplicationUser>>();

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;
            var user = _currentUser.GetUserId() ?? "system";

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedById = user;
                entry.Entity.DateCreated = now;
                entry.Entity.LastModifiedBy = user;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = user;
                entry.Entity.DateLastModified = now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}