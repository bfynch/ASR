using System;
using System.Text.RegularExpressions;

namespace WDT_Ass_1
{
    public class Menu
    {

        ASREngine eng = new ASREngine();

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
                    eng.GetAllRooms();
                    eng.ReturnToMenu();
                    MainMenu();
                    break;
                case "2":
                    eng.GeAllSlotsByDate();
                    eng.ReturnToMenu();
                    MainMenu();
                    break;
                case "3":
                    Console.Clear();
                    StaffMenu();
                    break;
                case "4":
                    Console.Clear();
                    StudentMenu();
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
                    eng.GetAllStaff();
                    eng.ReturnToMenu();
                    StaffMenu();
                    break;
                case "2":
                    eng.RoomsAvailable();
                    eng.ReturnToMenu();
                    StaffMenu();
                    break;
                case "3":
                    eng.CreateSlot();
                    eng.ReturnToMenu();
                    StaffMenu();
                    break;
                case "4":
                    eng.DeleteSlot();
                    eng.ReturnToMenu();
                    StaffMenu();
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

        public void StudentMenu()
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
                    eng.GetAllStudents();
                    eng.ReturnToMenu();
                    StudentMenu();
                    break;
                case "2":
                    eng.GetFreeSlots();
                    eng.ReturnToMenu();
                    StudentMenu();
                    break;
                case "3":
                    eng.MakeBooking();
                    eng.ReturnToMenu();
                    StudentMenu();
                    break;
                case "4":
                    eng.CancelBooking();
                    eng.ReturnToMenu();
                    StudentMenu();
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
    }
}
