using System;

namespace WDT_Ass_1
{
    public class Menu
    {
        private ASREngine _eng = new ASREngine();
        private MiscUtilities _misc = new MiscUtilities();

        // Prints the main menu. Users are able to navigate to staff menu, student menu,
        // or call other functions based on user input
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
                    _eng.GetAllRooms();
                    _misc.ReturnToMenu();
                    MainMenu();
                    break;
                case "2":
                    _eng.GetAllSlots();
                    _misc.ReturnToMenu();
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

        // Prints menu for staff members.
        public void StaffMenu()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Staff Menu:");
            Console.WriteLine("1. List Staff");
            Console.WriteLine("2. View Bookings");
            Console.WriteLine("3. Room Availability");
            Console.WriteLine("4. Create Slot");
            Console.WriteLine("5. Remove Slot");
            Console.WriteLine("6. Main Menu");
            Console.WriteLine("7. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _eng.GetAllStaff();
                    _misc.ReturnToMenu();
                    StaffMenu();
                    break;
                case "2":
                    _eng.ViewBookings();
                    _misc.ReturnToMenu();
                    StaffMenu();
                    break;
                case "3":
                    _eng.RoomsAvailable();
                    _misc.ReturnToMenu();
                    StaffMenu();
                    break;
                case "4":
                    _eng.CreateSlot();
                    _misc.ReturnToMenu();
                    StaffMenu();
                    break;
                case "5":
                    _eng.DeleteSlot();
                    _misc.ReturnToMenu();
                    StaffMenu();
                    break;
                case "6":
                    Console.Clear();
                    MainMenu();
                    break;
                case "7":
                    Exit();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Try again.");
                    MainMenu();
                    break;
            }
        }

        // Prints menu for students.
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
                    _eng.GetAllStudents();
                    _misc.ReturnToMenu();
                    StudentMenu();
                    break;
                case "2":
                    _eng.StaffAvailability();
                    _misc.ReturnToMenu();
                    StudentMenu();
                    break;
                case "3":
                    _eng.MakeBooking();
                    _misc.ReturnToMenu();
                    StudentMenu();
                    break;
                case "4":
                    _eng.CancelBooking();
                    _misc.ReturnToMenu();
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

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
