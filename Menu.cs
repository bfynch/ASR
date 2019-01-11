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
                    Console.WriteLine("Press any key to return to main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                    break;
                case "2":
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
                    Console.WriteLine("Press any key to return to the staff menu...");
                    Console.ReadKey();
                    Console.Clear();
                    StaffMenu();
                    break;
                case "2":
                    break;
                case "3":
                    Console.Clear();
                    CreateSlot();
                    break;
                case "4":
                    eng.DeleteSlot();
                    Console.Clear();
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
                    Console.WriteLine("Press any key to return to the student menu...");
                    Console.ReadKey();
                    Console.Clear();
                    StudentMenu();
                    break;
                case "2":
                    eng.GetFreeSlots();
                    Console.WriteLine("Press any key to return to the student menu...");
                    Console.ReadKey();
                    Console.Clear();
                    StudentMenu();
                    break;
                case "3":
                    Console.Clear();
                    MakeBooking();
                    break;
                case "4":
                    Console.Clear();
                    CancelBooking();
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

        public void MakeBooking()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Make a Booking");
            Console.WriteLine("Enter room ID: ");
            var roomid = Console.ReadLine();
            Console.WriteLine("Enter date for slot (yyyy-dd-mm): ");
            var date = Console.ReadLine();
            Console.WriteLine("Enter time for slot (hh:mm): ");
            var time = Console.ReadLine();
            Console.WriteLine("Enter Student ID: ");
            var studid = Console.ReadLine();
            string starttime = date + " " + time;
            if (eng.StudentIDCheck(studid) && ValidTime(time))
            {
                //User has input correct information and is prompted with a message. Console is then cleared and user
                //is redirected to the main menu after 3 seconds.
                eng.BookSlot(studid, starttime, roomid);
                Console.WriteLine("Slot canceled successfully. Redirecting...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StudentMenu();
            }
            else
            {
                //User has input invalid data and is prompted with a message.
                //System waits for 3 seconds and then clears the console and returns to the staff menu
                Console.WriteLine("Invalid information submitted. Try again.");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StudentMenu();
            }
        }

        public void CancelBooking()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Delete a Booking");
            Console.WriteLine("Enter room ID: ");
            var roomid = Console.ReadLine();
            Console.WriteLine("Enter date for slot (yyyy-dd-mm): ");
            var date = Console.ReadLine();
            Console.WriteLine("Enter time for slot (hh:mm): ");
            var time = Console.ReadLine();
            string starttime = date + " " + time;
            if (ValidTime(time))
            {
                //User has input correct information and is prompted with a message. Console is then cleared and user
                //is redirected to the main menu after 3 seconds.
                eng.CancelBooking(starttime, roomid);
                Console.WriteLine("Slot created successfully. Redirecting...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StudentMenu();
            }
            else
            {
                //User has input invalid data and is prompted with a message.
                //System waits for 3 seconds and then clears the console and returns to the staff menu
                Console.WriteLine("Invalid information submitted. Try again.");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StudentMenu();
            }
        }

        public void CreateSlot()
        {
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Create a new slot");
            Console.WriteLine("Enter Room ID: ");
            var roomid = Console.ReadLine();
            Console.WriteLine("Enter date for slot (mm-dd-yy): ");
            var date = Console.ReadLine();
            Console.WriteLine("Enter time for slot (hh:mm): ");
            var time = Console.ReadLine();
            Console.WriteLine("Enter Staff ID: ");
            var staffid = Console.ReadLine();
            string starttime = date + " " + time;
            if (staffid.Length == 6 && staffid.StartsWith('e') && Regex.IsMatch(staffid.Substring(1), @"^\d+$") && ValidTime(time))
            {
                //User has input correct information and is prompted with a message. Console is then cleared and user
                //is redirected to the main menu after 3 seconds.
                eng.CreateSlot(roomid, starttime, staffid);
                Console.WriteLine("Slot created successfully. Redirecting...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StaffMenu();
            }
            else
            {
                //User has input invalid data and is prompted with a message.
                //System waits for 3 seconds and then clears the console and returns to the staff menu
                Console.WriteLine("Invalid information submitted. Try again.");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StaffMenu();
            }
        }
    
        public bool ValidTime(string time)
        {
            try
            {   
                //creates ints for hour and minutes from substrings of the user input
                int hour = Int32.Parse(time.Substring(0, 2));
                int min = Int32.Parse(time.Substring(3, 2));
                //checks whether the time input from the user is valid
                return time.Length == 5 && time.IndexOf(':') == 2 && hour <= 24 && hour >= 0 && min == 0;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
