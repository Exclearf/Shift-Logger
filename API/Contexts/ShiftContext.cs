using Microsoft.EntityFrameworkCore;
using ShiftsAPI.Models;

namespace ShiftsAPI.Contexts
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> opt) : base(opt) { }

        public DbSet<Shift> Shifts { get; set; } = null!;
    }
}
