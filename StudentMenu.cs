using System;
namespace WDT_Ass_1
{
    public class StudentMenu
    {
        
        public StudentMenu()
        {

        }

        public void MainMenu(Menu m)
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Student Menu:");
            Console.WriteLine("1. List Students");
            Console.WriteLine("2. Staff Availability");
            Console.WriteLine("3. Make Booking");
            Console.WriteLine("4. Cancel Booking");
            Console.WriteLine("5. Main Menu");
            Console.WriteLine("6. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    Console.Clear();
                    m.CreateSlot();
                    break;
                case "4":
                    break;
                case "5":
                    Console.Clear();
                    m.MainMenu();
                    break;
                case "6":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }

    }
}
