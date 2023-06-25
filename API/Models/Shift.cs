using System.ComponentModel.DataAnnotations;

namespace ShiftsAPI.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ShiftStart { get; set; }

        [DataType(DataType.Date)]
        public DateTime ShiftEnd { get; set; }
    }
}
