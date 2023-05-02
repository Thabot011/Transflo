using Microsoft.EntityFrameworkCore;
using Transflo.Domain.Entities;

namespace Transflo.Persistence.DBContext
{
    public class TransfloDBContext : DbContext
    {
        public virtual DbSet<Driver> Drivers { get; set; }
        public TransfloDBContext(DbContextOptions<TransfloDBContext> options) : base(options)
        {

        }

        public TransfloDBContext()
        {

        }
    }
}
