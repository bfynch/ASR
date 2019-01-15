using System;
namespace WDT_Ass_1
{
    public class SQLQueries
    {
        SQLConnector sql = SQLConnector.GetInstance();

        //Method called by DeleteSlot() to delete a slot. Takes the parameters StartTime, and RoomID.
        public void SlotToDelete(string starttime, string roomid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"delete from [Slot] where RoomID = '{roomid}' and StartTime like '{starttime}%'";
                command.ExecuteNonQuery();
            }
        }

        // Called by CreateSlot() to insert a new row into the database. 
        // It takes parameters RoomID, StartTime, and StaffID
        public void InsertSlot(string roomid, string starttime, string staffid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"insert into [Slot] (RoomID, StartTime, StaffID) values ('{roomid}', '{starttime}', '{staffid}');";
                command.ExecuteNonQuery();
            }
        }

        // Method called by Menu.MakeBooking() to add a student to a slot.
        // Takes parameters StudentID, StartTime, and RoomID
        public void BookSlot(string studentid, string starttime, string roomid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"update [Slot] set BookedInStudentID = '{studentid}' where RoomID = '{roomid}' and StartTime like '{starttime}%'";
                command.ExecuteNonQuery();
            }
        }

        // Method called by CancelBooking() to remove a student from a slot by
        // setting the BookedInStudentId to null. Takes the parameters StartTime, and RoomID.
        public void CancelBooking(string starttime, string roomid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"update [Slot] set BookedInStudentID = Null where RoomID = '{roomid}' and StartTime like '{starttime}%'";
                command.ExecuteNonQuery();
            }
        }

        //checks how many slots have been made by a staff member on a certain date
        //to prevent a staff member from making more than 4 slots per day
        public int UserBookingsCount(string date, string id, string user)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[Slot] where StartTime like '{date}%' and {user} = '{id}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count;
            }
        }

        // Queries the database to check whether a staff member exists and returns true if they do.
        // Takes parameter StaffID.
        public bool StaffIDCheck(string staffid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[User] where UserID like 'e%' and UserID = '{staffid}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count > 0;
            }
        }

        // Queries the database to check whether a student exists and returns true if they do.
        // Takes parameter StudentID. 
        public bool StudentIDCheck(string studentid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[User] where UserID = '{studentid}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count > 0;
            }
        }

        // Queries the database to count how many times a room is booked for a certain day.
        // Takes parameters Date and RoomID. Returns int count.
        public int RoomBookingsCount(string date, string roomid)
        {
            using (var connection = sql.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select count(*) from [s3589828].[dbo].[Slot] where StartTime like '{date}%' and RoomID = '{roomid}'";
                Int32 count = (Int32)command.ExecuteScalar();
                return count;
            }
        }
    }
}
