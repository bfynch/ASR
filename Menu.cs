using System;
namespace WDT_Ass_1
{
    public class Menu
    {
        public Menu()
        {
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void MainMenu()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. List Rooms");
            Console.WriteLine("2. List Slots");
            Console.WriteLine("3. Staff Menu");
            Console.WriteLine("4. Student Menu");
            Console.WriteLine("5. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    Console.Clear();
                    StaffMenu();
                    break;
                case "4": 
                    break;
                case "5": 
                    Exit(); 
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Try again.");
                    MainMenu();
                    break;
            }
        }

        public void StaffMenu()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Staff Menu:");
            Console.WriteLine("1. List Staff");
            Console.WriteLine("2. Room Availability");
            Console.WriteLine("3. Create Slot");
            Console.WriteLine("4. Remove Slot");
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
                    CreateSlot();
                    break;
                case "4":
                    break;
                case "5":
                    Console.Clear();
                    MainMenu();
                    break;
                case "6":
                    Exit();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Try again.");
                    MainMenu();
                    break;
            }
        }

        public void CreateSlot()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Create a new slot");
            Console.WriteLine("Enter room name: ");
            var roomname = Console.ReadLine();
            Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
            var date = Console.ReadLine();
            Console.WriteLine("Enter time for slot (hh:mm): ");
            var time = Console.ReadLine();
            Console.WriteLine("Enter Staff ID: ");
            var staffid = Console.ReadLine();
        }
    }
}
