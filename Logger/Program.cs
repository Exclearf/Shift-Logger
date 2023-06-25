using ShiftsLogger.Models;
using ShiftsLogger.Services;
using System.Diagnostics;
using System.Xml.Linq;

namespace ShiftsLogger
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ShiftsService.RunAsync().Wait();
            while (true)
            {
                ShowMenu();

                switch (Console.ReadLine())
                {
                    case "1":
                        ShiftModel? createdShift = ShiftManage.CreateShift();
                        ShiftsService.CreateShiftAsync(createdShift).Wait();
                        PrintLine();
                        Console.WriteLine("New Shift created!");
                        PressAnyKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        ShiftManage.ShowShifts().Wait();
                        PressAnyKey();
                        break;
                    case "3":
                        Console.Clear();
                        ShiftManage.ShowShifts().Wait();
                        Console.WriteLine("Type the ID of the Shift...");
                        Console.Write("Your input (-1 to exit): ");
                        {
                            int choice = Convert.ToInt32(Console.ReadLine());
                            if (choice == -1)
                                break;
                            Console.Clear();
                            ShiftManage.UpdateShift(choice).Wait();
                        }
                        PrintLine();
                        PressAnyKey();
                        break;
                    case "4":
                        Console.Clear();
                        ShiftManage.ShowShifts().Wait();
                        Console.WriteLine("Type the ID of the Shift...");
                        Console.Write("Your input (-1 to exit): ");
                        {
                            var choiceList = Console.ReadLine();
                            if (choiceList == "" || Convert.ToInt32(choiceList.Split( ).First()) == -1)
                                break;
                            Console.Clear();
                            foreach (var choice in choiceList.Split(' ').ToList())
                            ShiftManage.DeleteShift(choice).Wait();
                        }
                        PrintLine();
                        PressAnyKey();
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        
        static void ShowMenu()
        {
            Console.Clear();
            PrintLine();
            Console.WriteLine($"1. Start a Shift");
            Console.WriteLine($"2. View all Shifts");
            Console.WriteLine($"3. Update a Shifts' name");
            Console.WriteLine($"4. Delete a Shift");
            Console.WriteLine($"-- Press any key to exit --");
            Console.Write("Input: ");
        }

        public static void PrintLine(int v = 28)
        {
            Console.WriteLine(new string('-', v));
        }

        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}
