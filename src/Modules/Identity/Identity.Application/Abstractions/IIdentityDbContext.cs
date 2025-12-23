using Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Abstractions;

public interface IIdentityDbContext
{
    DbSet<User> Users { get; }
}
