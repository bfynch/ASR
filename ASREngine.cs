using System;
using System.Data.SqlClient;

namespace WDT_Ass_1
{
    public class ASREngine
    {
        SQLConnector sql = SQLConnector.GetInstance();
        private SQLQueries _sqlqueries = new SQLQueries();
        private MiscUtilities _misc = new MiscUtilities();

        //Code from Week 3 2.1 Example
        //Queries the database for all rooms and prints them to the console.
        public void GetAllRooms()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Rooms: ");
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("select * from Room", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0, 3}",
                            reader["RoomID"]);
                    }
                }
            }
        }

        //Queries the database and prints all of the staff members.
        public void GetAllStaff()
        {
            using (var connection = sql.GetConnection())
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Staff Members:");
                Console.WriteLine(String.Format("{0, -8} {1, -15} {2, -20}",
                "User ID", "Name", "Email Address"));
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [s3589828].[dbo].[User] WHERE [UserID] LIKE 'e%'", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -8} {1, -15} {2, -20}",
                            reader["UserID"], reader["Name"], reader["Email"]));
                    }
                }
            }
        }

        // Prompts user to enter Room ID, a date, and time. If the the data corresponds
        // with a row in the database and the BookedInStudentId is null the slot is deleted
        public void DeleteSlot()
        {
            using (var connection = sql.GetConnection())
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
                    var starttime = date + " " + time;
                    connection.Open();
                    var command = new SqlCommand($"select BookedInStudentID from [Slot] where RoomID = '{roomid}' and StartTime like '{starttime}%'", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.IsDBNull(reader["BookedInStudentID"]) && reader.HasRows)
                            {
                                _sqlqueries.SlotToDelete(starttime, roomid);
                                Console.WriteLine("Slot deleted successfully.");
                            }
                            else if(!Convert.IsDBNull(reader["BookedInStudentID"]))
                            {
                                Console.WriteLine("Unable to delete slot. A student is currently booked for this slot.");
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

        // User enters a date and all rooms that have less than 2 bookings on that date are displayed.
        public void RoomsAvailable()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter date: (yyyy-mm-dd)");
            var date = Console.ReadLine();
            Console.WriteLine($"Rooms available on {date}.");

            if (_sqlqueries.RoomBookingsCount(date, "A") < 2) Console.WriteLine("A");
            if (_sqlqueries.RoomBookingsCount(date, "B") < 2) Console.WriteLine("B");
            if (_sqlqueries.RoomBookingsCount(date, "C") < 2) Console.WriteLine("C");
            if (_sqlqueries.RoomBookingsCount(date, "D") < 2) Console.WriteLine("D");
        }

        //Queries the database and prints all of the students' information.
        public void GetAllStudents()
        {
            using (var connection = sql.GetConnection())
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

        // User inputs a date and StaffID and the available slots for that day are printed.
        public void StaffAvailability()
        {
            using (var connection = sql.GetConnection())
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Staff Availability: ");
                Console.WriteLine("Enter date: (yyyy-mm-dd)");
                var date = Console.ReadLine();
                Console.WriteLine("Enter Staff ID: ");
                var staffid = Console.ReadLine();
                Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -15}",
                "Room", "Time and Date (dd/mm/yy)", "Staff Member"));
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM [s3589828].[dbo].[Slot] where StaffID = '{staffid}' and BookedInStudentID is Null and StartTime like '{date}%';", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -15}",
                            reader["RoomID"], reader["StartTime"], reader["StaffID"]));
                    }
                }
            }
        }

        // User inputs date, and StaffID and all booked slots for that day are printed.
        public void ViewBookings()
        {
            using (var connection = sql.GetConnection())
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("View Bookings");
                Console.WriteLine("Enter date: (yyyy-mm-dd)");
                var date = Console.ReadLine();
                Console.WriteLine("Enter Staff ID: ");
                var staffid = Console.ReadLine();
                Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -15}",
                "Room", "Time and Date (dd/mm/yy)", "Student ID"));
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM [s3589828].[dbo].[Slot] where StaffID = '{staffid}' and BookedInStudentID is not Null and StartTime like '{date}%';", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -15}",
                            reader["RoomID"], reader["StartTime"], reader["BookedInStudentID"]));
                    }
                }
            }
        }

        //Queries the database and prints all slots; booked or otherwise.
        public void GetAllSlots()
        {
            using (var connection = sql.GetConnection())
            {
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("All Slots");
                Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -15} {3, -15}",
                "Room", "Time and Date (dd/mm/yy)", "Staff Member", "Student"));
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM [s3589828].[dbo].[Slot];", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0, -6} {1, -25} {2, -15} {3, -15}",
                            reader["RoomID"], reader["StartTime"], reader["StaffID"], reader["BookedInStudentID"]));
                    }
                }
            }
        }

        // Prompts user to input Room ID, a date, a time, and Student ID. If the Student ID
        // exists in the database, and the time is valid, and the room id is valid, and 
        // the student has no other bookings for that day, then the student id is added
        // to the BookedInStudentID for that row. 
        public void MakeBooking()
        {
            using (var connection = sql.GetConnection())
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
                var starttime = date + " " + time;
                connection.Open();
                var command = new SqlCommand($"select * from [Slot] where RoomID = '{roomid}' and StartTime like '{starttime}%'", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        if (_sqlqueries.StudentIDCheck(studid) && _misc.ValidTime(time) && _misc.RoomIDCheck(roomid) &&
                         _sqlqueries.UserBookingsCount(date, studid, "BookedInStudentID") == 0 && Convert.IsDBNull(reader["BookedInStudentID"]))
                        {
                            //User has input correct information and is prompted with a message.
                            _sqlqueries.BookSlot(studid, starttime, roomid);
                            Console.WriteLine("Slot booked successfully. ");
                        }
                        else if (_sqlqueries.UserBookingsCount(date, studid, "BookedInStudentID") == 1)
                        {
                            Console.WriteLine($"Unable to make Booking. Student {studid} has exceeded the max amount of bookings per day. ");
                        }
                        else if(!Convert.IsDBNull(reader["BookedInStudentID"]))
                        {
                            Console.WriteLine("Unable to book slot. Another student has already booked this slot.");
                        }
                        else
                        {
                            //User has input invalid data and is prompted with a message.
                            Console.WriteLine("Invalid information submitted. ");
                        }
                }
            }
        }

        // Prompts user for room ID, date, and time for a booking. If the time is valid,
        // and the room id is valid, and a booking is found, and the BookedInStudentID
        // isnt null, then the booking is cancelled by setting BookedInStudentID to null.
        public void CancelBooking()
        {
            using (var connection = sql.GetConnection())
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("Delete a Booking");
                    Console.WriteLine("Enter room ID: ");
                    var roomid = Console.ReadLine();
                    Console.WriteLine("Enter date for slot (yyyy-mm-dd): ");
                    var date = Console.ReadLine();
                    Console.WriteLine("Enter time for slot (hh:mm): ");
                    var time = Console.ReadLine();
                    var starttime = date + " " + time;
                    connection.Open();
                    var command = new SqlCommand($"select * from [Slot] where RoomID = '{roomid}' and StartTime like '{starttime}%'", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            if (_misc.ValidTime(time) && _misc.RoomIDCheck(roomid) && reader.HasRows && !Convert.IsDBNull(reader["BookedInStudentID"]))
                            {
                                //User has input correct information and is prompted with a message.
                                _sqlqueries.CancelBooking(starttime, roomid);
                                Console.WriteLine("Booking canceled successfully. ");
                            }
                            else if (Convert.IsDBNull(reader["BookedInStudentID"]))
                            { 
                                Console.WriteLine("There is not a student currently booked into this slot. ");
                            }
                            else
                            {
                                //User has input invalid data and is prompted with a message.
                                Console.WriteLine("Invalid information submitted. ");
                            }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("An unexpected error occurred.");
                }
            }
        }

        // Prompts for user input for Room ID, Date, Time, and StaffID. If the staff ID
        // exists in the database, and time is correct format, and the room ID is valid,
        // and the staff member has less than 4 bookings on that date, and the room is 
        // booked for less than 2 slots on that date, then the data is inserted to the
        // database. Otherwise exceptions are caught and other errors are displayed.
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
                var starttime = date + " " + time;
                if (_sqlqueries.StaffIDCheck(staffid) && _misc.ValidTime(time) && _misc.RoomIDCheck(roomid) && _sqlqueries.UserBookingsCount(date, staffid, "StaffID") < 4 && _sqlqueries.RoomBookingsCount(date, roomid) < 2)
                {
                    //User has input correct information and is prompted with a message.
                    _sqlqueries.InsertSlot(roomid, starttime, staffid);
                    Console.WriteLine("Slot created successfully. ");
                }
                else if (_sqlqueries.UserBookingsCount(date, staffid, "StaffID") == 4)
                {
                    Console.WriteLine($"Staff member {staffid} has exceeded the max amount of slots per day");
                }
                else if (_sqlqueries.RoomBookingsCount(date, roomid) == 2)
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


    }
}