using ShiftsLogger.Models;
using ShiftsLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLogger.Services
{
    public class ShiftManage
    {
        private static ShiftModel shift { get; set; } = null!;


        public static ShiftModel CreateShift()
        {
            shift = new ShiftModel();
            Console.Clear();

            Console.WriteLine("Enter the name of the Shift (can be empty)");
            Console.Write("Input: ");
            var name = Console.ReadLine();
            shift.Name = name;

            Console.WriteLine("To start the Shift, press any key");
            Console.ReadKey();
            var start = StartShift();
            Console.WriteLine($"Shift started at {start}");

            Console.WriteLine(new string('-', 40));

            Console.WriteLine("To end the Shift, press any key");
            Console.ReadKey();
            var end = EndShift();
            Console.WriteLine($"Shift ended at {end}");

            return shift;
        }

        public static async Task<List<ShiftModel>?> ShowShifts()
        {
            var shiftList = await ShiftsService.GetShifts();
            Program.PrintLine();
            if (shiftList != null)
            {
                foreach (var shift in shiftList)
                {
                    ShiftsService.ShowShift(shift);
                    Program.PrintLine(); 
                }
            }
            return shiftList;
        }
        
        public static async Task UpdateShift(long Id)
        {
            Console.Clear();
            ShiftModel? shiftChosen = await ShiftsService.GetShiftAsyncById(Id);

            Console.WriteLine("Chosen shift: ");
            ShiftsService.ShowShift(shiftChosen);
            Program.PrintLine();
            
            Console.Write("New name: ");
            var newName = Console.ReadLine();
            shiftChosen.Name = newName;
            shiftChosen = await ShiftsService.UpdateShiftAsync(shiftChosen);
            Program.PrintLine();
            
            Console.WriteLine("Shift after changes: ");
            ShiftsService.ShowShift(shiftChosen);

            return;
        }

        public static async Task DeleteShift(string id)
        {
            if(Convert.ToInt32(id) < 1)
            {
                await Console.Out.WriteLineAsync("Incorrect ID!");
                return;
            }
            try
            {
                var response = await ShiftsService.DeleteShiftByIdAsync(Convert.ToInt32(id));

                Console.WriteLine($"Shift with ID={id} has been deleted!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error has occured. Deatils: ");
                Console.WriteLine(ex.ToString());
            }
            return;
        }

        private static DateTime StartShift()
        {
            shift.ShiftStart = DateTime.Now;
            return shift.ShiftStart;
        }

        private static DateTime EndShift()
        {
            shift.ShiftEnd = DateTime.Now;
            return shift.ShiftEnd;
        }

    }
}
