using System;
using System.Data.SqlClient;
using System.Text;

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
                command.CommandText = "update [Slot] set BookedInStudentID = '"+studentid+"' where RoomID = '"+roomid+"' and StartTime like '"+starttime+"%'";

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
                command.CommandText = "update [Slot] set BookedInStudentID = Null where RoomID = '" + roomid + "' and StartTime like '" + starttime + "%'";

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
                Console.Clear();
                Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Delete a slot:");
                Console.WriteLine("Enter room ID: ");
                var roomid = Console.ReadLine();
                Console.WriteLine("Enter date for slot (yyyy-dd-mm): ");
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
                        if (Convert.IsDBNull(reader["BookedInStudentID"]))
                        {
                            SlotToDelete(starttime, roomid);
                            Console.WriteLine("Slot created successfully. Redirecting...");
                        }
                        else Console.WriteLine("Error");
                    }
                }
            }
        }

        public void CreateSlot(string roomid, string starttime, string staffid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"insert into [Slot] (RoomID, StartTime, StaffID) values ('" + roomid + "', '" + starttime + "', '" + staffid + "');";

                var updates = command.ExecuteNonQuery();
            }
        }

        public int StaffBookings(string starttime, string staffid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select count(*) from [s3589828].[dbo].[Slot] where StartTime like '%" + starttime + "%'"; //and StaffID = '" + staffid + "'";
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
                command.CommandText = "select count(*) from [s3589828].[dbo].[User] where UserID like 's%' and UserID = '" + studentid + "'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count > 0;
            }
        }

        //checks whether the staff member exists in the database and returns true if they do
        public bool StaffIDCheck(string staffid)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select count(*) from [s3589828].[dbo].[User] where UserID like 'e%' and UserID = '" + staffid + "'";
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
                "Room", "Time", "Staff Member"));
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
    }
}