using Frock_backend.Shared.Domain.Repositories;
using Frock_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
namespace Frock_backend.Shared.Infrastructure.Persistence.EFC.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
