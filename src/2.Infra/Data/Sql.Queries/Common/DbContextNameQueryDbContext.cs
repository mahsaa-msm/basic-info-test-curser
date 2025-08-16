using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Queries;

namespace Master.Data.Infra.Data.Sql.Queries.Common;

public class DbContextNameQueryDbContext : BaseQueryDbContext
{
    public DbContextNameQueryDbContext(DbContextOptions<DbContextNameQueryDbContext> options) : base(options)
    {
    }
}