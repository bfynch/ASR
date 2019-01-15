using System;
using System.Text.RegularExpressions;
namespace WDT_Ass_1
{
    public class MiscUtilities
    {
        // Checks whether the user has input a correct time format. Returns true
        // or catches an exception and prints an error message.
        public bool ValidTime(string time)
        {
            try
            {
                //creates ints for hour and minutes from substrings of the user input
                var hour = Int32.Parse(time.Substring(0, 2));
                var min = Int32.Parse(time.Substring(3, 2));
                //checks whether the time input from the user is valid
                return time.Length == 5 && time.IndexOf(':') == 2 && hour <= 14 && hour >= 9 && min == 0;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid time format.");
            }
            return false;
        }
       
        //Called after most menu items to clear the console and prompt the user to return them to the menu. 
        public void ReturnToMenu()
        {
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
            Console.Clear();
        }

        // Method used for bookings to check whether the user has input a valid RoomID.
        // Returns true if the RoomID is valid.
        public bool RoomIDCheck(string roomid)
        {
            return Regex.IsMatch(roomid, "[A-D]");
        }
    }
}
