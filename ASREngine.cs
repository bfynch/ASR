using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace WDT_Ass_1
{
    public class ASREngine
    {
        const string connectionString = "server=wdt2019.australiasoutheast.cloudapp.azure.com;uid=s3589828;database=s3589828;pwd=abc123";

        //Code from Week 3 2.1 Example
        public void GetAllRooms()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("select * from Room", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}",
                            reader["RoomID"]);
                    }
                }
            }
        }

        //Prints all of the staff members in the database.
        public void GetAllStaff()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Staff Members:");
                Console.WriteLine(String.Format("{0, -8} {1, -10} {2, -15}",
                "User ID", "Name", "Email Address"));

                connection.Open();

                var command = new SqlCommand("SELECT * FROM [s3589828].[dbo].[User] WHERE [UserID] LIKE 'e%'", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -8} {1, -10} {2, -15}",
                            reader["UserID"], reader["Name"], reader["Email"]));
                    }
                }
            }
        }

        // Method called by Menu.MakeBooking() to add a student to a slot.
        public void BookSlot(string studentid, string starttime, string roomid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"update [Slot] set BookedInStudentID = '{studentid}' where RoomID = '{roomid}' and StartTime like '{starttime}%'";

                var updates = command.ExecuteNonQuery();
            }
        }

        //Method called by Menu.CancelBooking() to remove a student from a slot by setting the BookedInStudentId to null.
        public void CancelBooking(string starttime, string roomid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"update [Slot] set BookedInStudentID = Null where RoomID = '{roomid}' and StartTime like '{starttime}%'";

                var updates = command.ExecuteNonQuery();
            }
        }

        //Method called by DeleteSlot() to delete a slot based on parameters from user input
        public void SlotToDelete(string starttime, string roomid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"delete from [Slot] where RoomID = '{roomid}' and StartTime like '{starttime}%'";
                var updates = command.ExecuteNonQuery();
            }
        }

        public void DeleteSlot()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("Delete a slot:");
                    Console.WriteLine("Enter room ID: ");
                    var roomid = Console.ReadLine();
                    Console.WriteLine("Enter date for slot (yyyy-mm-dd): ");
                    var date = Console.ReadLine();
                    Console.WriteLine("Enter time for slot (hh:mm): ");
                    var time = Console.ReadLine();
                    string starttime = date + " " + time;
                    connection.Open();

                    var command = new SqlCommand($"select BookedInStudentID from [Slot] where RoomID = '{roomid}' and StartTime like '{starttime}%'", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.IsDBNull(reader["BookedInStudentID"]) && reader.HasRows)
                            {
                                SlotToDelete(starttime, roomid);
                                Console.WriteLine("Slot deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("An error has occurred.");
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void InsertSlot(string roomid, string starttime, string staffid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"insert into [Slot] (RoomID, StartTime, StaffID) values ('{roomid}', '{starttime}', '{staffid}');";

                var updates = command.ExecuteNonQuery();
            }
        }

        //checks how many slots have been made by a staff member on a certain date
        //to prevent a staff member from making more than 4 slots per day
        public int UserBookingsCount(string date, string id, string user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[Slot] where StartTime like '{date}%' and {user} = '{id}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count;
            }
        }

        public void RoomsAvailable()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter date: (yyyy-mm-dd)");
            var date = Console.ReadLine();
            Console.WriteLine($"Rooms available on {date}.");

            if (RoomBookingsCount(date, "A") < 2) Console.WriteLine("A");
            if (RoomBookingsCount(date, "B") < 2) Console.WriteLine("B");
            if (RoomBookingsCount(date, "C") < 2) Console.WriteLine("C");
            if (RoomBookingsCount(date, "D") < 2) Console.WriteLine("D");
        }

        public int RoomBookingsCount(string date, string roomid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[Slot] where StartTime like '{date}%' and RoomID = '{roomid}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count;
            }
        }

        //checks whether the student exists in the database and returns true if they do
        public bool StudentIDCheck(string studentid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[User] where UserID = '{studentid}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count > 0;
            }
        }

        public bool RoomIDCheck(string roomid)
        {
            return Regex.IsMatch(roomid, "[A-D]");
        }

        //checks whether the staff member exists in the database and returns true if they do
        public bool StaffIDCheck(string staffid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[User] where UserID like 'e%' and UserID = '{staffid}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count > 0;
            }
        }

        //prints all of the slots that aren't booked by a student
        public void GetFreeSlots()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Available Slots:");
                Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -8}",
                "Room", "Time and Date (dd/mm/yy)", "Staff Member"));
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [s3589828].[dbo].[Slot] where BookedInStudentID is NULL;", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -8}",
                            reader["RoomID"], reader["StartTime"], reader["StaffID"]));
                    }
                }
            }
        }

        public void GetAllStudents()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Students:");
                Console.WriteLine(String.Format("{0, -8} {1, -10} {2, -15}",
                "User ID", "Name", "Email Address"));
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [s3589828].[dbo].[User] WHERE [UserID] LIKE 's%'", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -8} {1, -10} {2, -15}",
                            reader["UserID"], reader["Name"], reader["Email"]));
                    }
                }
            }
        }

        //Called after most menu items to clear the console and prompt the user to return them to the menu. 
        public void ReturnToMenu()
        {
            Console.Write("Press any key to return to the menu...");
            Console.ReadKey();
            Console.Clear();
        }

        //User inputs a date and all of the slots for that date are printed from the db.
        public void GeAllSlotsByDate()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Enter date: (yyyy-mm-dd)");
                var date = Console.ReadLine();
                Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -8} {3, -8}",
                "Room", "Time and Date (dd/mm/yy)", "Staff Member", "Student"));
                connection.Open();

                var command = new SqlCommand($"SELECT * FROM [s3589828].[dbo].[Slot] where StartTime like '{date}%';", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -8} {3, -8}",
                            reader["RoomID"], reader["StartTime"], reader["StaffID"], reader["BookedInStudentID"]));
                    }
                }
            }
        }

        public void MakeBooking()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Make a Booking");
            Console.WriteLine("Enter room ID: ");
            var roomid = Console.ReadLine();
            Console.WriteLine("Enter date for slot (yyyy-mm-dd): ");
            var date = Console.ReadLine();
            Console.WriteLine("Enter time for slot (hh:mm): ");
            var time = Console.ReadLine();
            Console.WriteLine("Enter Student ID: ");
            var studid = Console.ReadLine();
            string starttime = date + " " + time;
            if (StudentIDCheck(studid) && ValidTime(time) && RoomIDCheck(roomid) && UserBookingsCount(date, studid, "BookedInStudentID") == 0)
            {
                //User has input correct information and is prompted with a message.
                BookSlot(studid, starttime, roomid);
                Console.WriteLine("Slot booked successfully. ");
            }
            else if (UserBookingsCount(date, studid, "BookedInStudentID") == 1)
            {
                Console.WriteLine($"Student {studid} has exceeded the max amount of bookings per day. ");
            }
            else
            {
                //User has input invalid data and is prompted with a message.
                Console.WriteLine("Invalid information submitted. ");
            }
        }

        public void CancelBooking()
        {
            try
            {
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Delete a Booking");
                Console.WriteLine("Enter room ID: ");
                var roomid = Console.ReadLine();
                Console.WriteLine("Enter date for slot (yyyy-mm-dd): ");
                var date = Console.ReadLine();
                Console.WriteLine("Enter time for slot (hh:mm): ");
                var time = Console.ReadLine();
                string starttime = date + " " + time;
                if (ValidTime(time) && RoomIDCheck(roomid))
                {
                    //User has input correct information and is prompted with a message.
                    CancelBooking(starttime, roomid);
                    Console.WriteLine("Booking canceled successfully. ");
                }
                else
                {
                    //User has input invalid data and is prompted with a message.
                    Console.WriteLine("Invalid information submitted. ");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An unexpected error occurred.");
            }
        }

        public void CreateSlot()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Create a new slot");
                Console.WriteLine("Enter Room ID: ");
                var roomid = Console.ReadLine();
                Console.WriteLine("Enter date for slot (yyyy-mm-dd): ");
                var date = Console.ReadLine();
                Console.WriteLine("Enter time for slot (hh:mm): ");
                var time = Console.ReadLine();
                Console.WriteLine("Enter Staff ID: ");
                var staffid = Console.ReadLine();
                string starttime = date + " " + time;
                if (StaffIDCheck(staffid) && ValidTime(time) && RoomIDCheck(roomid) && UserBookingsCount(date, staffid, "StaffID") < 4 && RoomBookingsCount(date, roomid) < 2)
                {
                    //User has input correct information and is prompted with a message.
                    InsertSlot(roomid, starttime, staffid);
                    Console.WriteLine("Slot created successfully. ");
                }
                else if (UserBookingsCount(date, staffid, "StaffID") == 4)
                {
                    Console.WriteLine($"Staff member {staffid} has exceeded the max amount of slots per day");
                }
                else if (RoomBookingsCount(date, roomid) == 2)
                {
                    Console.WriteLine($"Room {roomid} has exceeded the max amount of bookings per day. ");
                }
                else Console.WriteLine("Invalid information submitted. "); 
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
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
                return time.Length == 5 && time.IndexOf(':') == 2 && hour <= 14 && hour >= 9 && min == 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}