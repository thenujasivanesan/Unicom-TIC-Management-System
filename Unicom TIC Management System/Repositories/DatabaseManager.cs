using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Repositories
{
    internal class DatabaseManager
    {
        public static void InitializeDatabase()
        {
            using(var dbconn = dbConfig.GetConnection()) 
            {
             
                // Create Users table
                string UsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL
            );";

                // Create Courses table
                string CoursesTable = @"
            CREATE TABLE IF NOT EXISTS Courses (
                CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                CourseName TEXT NOT NULL
            );";

                // Create Subjects table
                string SubjectsTable = @"
            CREATE TABLE IF NOT EXISTS Subjects (
                SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectName TEXT NOT NULL,
                CourseID INTEGER,
                LecturerId INTEGER,
                FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
            );";

                // Create Students table
                string StudentsTable = @"
            CREATE TABLE IF NOT EXISTS Students (
                StudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER UNIQUE NOT NULL,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                Gender TEXT NOT NULL,
                DateOfBirth TEXT NOT NULL,
                Contact TEXT,
                Email TEXT,
                Address TEXT,
                CourseId INTEGER NOT NULL,

                FOREIGN KEY (UserId) REFERENCES Users(UserId),
                FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
            );";

                // Create Exams table
                string ExamsTable = @"
            CREATE TABLE IF NOT EXISTS Exams (
                ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                ExamName TEXT NOT NULL,
                SubjectID INTEGER,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

                // Create Marks table
                string MarksTable = @"
            CREATE TABLE IF NOT EXISTS Marks (
                MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentID INTEGER,
                ExamID INTEGER,
                Score INTEGER,
                FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
            );";

                // Create Rooms table
                string RoomsTable = @"
            CREATE TABLE IF NOT EXISTS Rooms (
                RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomName TEXT NOT NULL,
                RoomType TEXT NOT NULL
            );";

                // Create TimeSlots table
                string TimeSlotsTable = @"
            CREATE TABLE IF NOT EXISTS TimeSlots (
                TimeSlotID INTEGER PRIMARY KEY AUTOINCREMENT,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL
            );";

                // Create Timetables table
                string TimetablesTable = @"
            CREATE TABLE IF NOT EXISTS Timetables (
                TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectID INTEGER NOT NULL,
                RoomID INTEGER NOT NULL,
                TimeSlot TEXT NOT NULL,
                Date TEXT NOT NULL,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
            );";

                // Create student_subjects table
                string StudentSubjectsTable = @"
            CREATE TABLE IF NOT EXISTS student_subjects (
                StudentSubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentID INTEGER,
                SubjectID INTEGER,
                FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

                // Create lecturer_subjects table
                string LecturerSubjectsTable = @"
            CREATE TABLE IF NOT EXISTS lecturer_subjects (
                LecturerSubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                LecturerID INTEGER,
                SubjectID INTEGER,
                FOREIGN KEY (LecturerID) REFERENCES Users(UserID),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

                // Create Lecturers table
                string LecturersTable = @"
            CREATE TABLE IF NOT EXISTS Lecturers (
                LecturerId INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER UNIQUE,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                Contact TEXT,
                Email TEXT,
                Address TEXT,
                FOREIGN KEY (UserId) REFERENCES Users(UserId)
            );";
                            





                // Execute the SQL commands to create tables

                //sqlitecommand cmd = new sqlitecommand(createuserstable);
                //cmd.executenonquery();

                //sqlitecommand cmd = new sqlitecommand(createcoursestable);
                //cmd.executenonquery();


                ExecuteNonQuery(dbconn, UsersTable);
                ExecuteNonQuery(dbconn, CoursesTable);
                ExecuteNonQuery(dbconn, SubjectsTable);
                ExecuteNonQuery(dbconn, StudentsTable);
                ExecuteNonQuery(dbconn, ExamsTable);
                ExecuteNonQuery(dbconn, MarksTable);
                ExecuteNonQuery(dbconn, RoomsTable);
                ExecuteNonQuery(dbconn, TimeSlotsTable);
                ExecuteNonQuery(dbconn, TimetablesTable);
                ExecuteNonQuery(dbconn, StudentSubjectsTable);
                ExecuteNonQuery(dbconn, LecturerSubjectsTable);
                ExecuteNonQuery(dbconn, LecturersTable);

            }
        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string commandText)
        {
            using (var cmd = new SQLiteCommand(commandText, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }


        /*
        public static bool IsAdminRegistered()
        {
            using (var conn = dbConfig.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin'";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;   
                }
            }
        } */


    }
}
       
