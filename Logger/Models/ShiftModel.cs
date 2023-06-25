using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLogger.Models
{
    public class ShiftModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ShiftStart { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ShiftEnd { get; set; }
    }
}
