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
                string createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL
            );";

                // Create Courses table
                string createCoursesTable = @"
            CREATE TABLE IF NOT EXISTS Courses (
                CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                CourseName TEXT NOT NULL
            );";

                // Create Subjects table
                string createSubjectsTable = @"
            CREATE TABLE IF NOT EXISTS Subjects (
                SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectName TEXT NOT NULL,
                CourseID INTEGER,
                FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
            );";

                // Create Students table
                string createStudentsTable = @"
            CREATE TABLE IF NOT EXISTS Students (
                StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                CourseID INTEGER,
                FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
            );";

                // Create Exams table
                string createExamsTable = @"
            CREATE TABLE IF NOT EXISTS Exams (
                ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                ExamName TEXT NOT NULL,
                SubjectID INTEGER,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

                // Create Marks table
                string createMarksTable = @"
            CREATE TABLE IF NOT EXISTS Marks (
                MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentID INTEGER,
                ExamID INTEGER,
                Score INTEGER,
                FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
            );";

                // Create Rooms table
                string createRoomsTable = @"
            CREATE TABLE IF NOT EXISTS Rooms (
                RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomName TEXT NOT NULL,
                RoomType TEXT NOT NULL
            );";

                // Create TimeSlots table
                string createTimeSlotsTable = @"
            CREATE TABLE IF NOT EXISTS TimeSlots (
                TimeSlotID INTEGER PRIMARY KEY AUTOINCREMENT,
                TimeSlot TEXT NOT NULL
            );";

                // Create Timetables table
                string createTimetablesTable = @"
            CREATE TABLE IF NOT EXISTS Timetables (
                TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectID INTEGER,
                TimeSlotID INTEGER,
                RoomID INTEGER,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                FOREIGN KEY (TimeSlotID) REFERENCES TimeSlots(TimeSlotID),
                FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
            );";

                // Create student_subjects table
                string createStudentSubjectsTable = @"
            CREATE TABLE IF NOT EXISTS student_subjects (
                StudentSubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentID INTEGER,
                SubjectID INTEGER,
                FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

                // Create lecturer_subjects table
                string createLecturerSubjectsTable = @"
            CREATE TABLE IF NOT EXISTS lecturer_subjects (
                LecturerSubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                LecturerID INTEGER,
                SubjectID INTEGER,
                FOREIGN KEY (LecturerID) REFERENCES Users(UserID),
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

              

                // Execute the SQL commands to create tables

                //sqlitecommand cmd = new sqlitecommand(createuserstable);
                //cmd.executenonquery();

                //sqlitecommand cmd = new sqlitecommand(createcoursestable);
                //cmd.executenonquery();


                ExecuteNonQuery(dbconn, createUsersTable);
                ExecuteNonQuery(dbconn, createCoursesTable);
                ExecuteNonQuery(dbconn, createSubjectsTable);
                ExecuteNonQuery(dbconn, createStudentsTable);
                ExecuteNonQuery(dbconn, createExamsTable);
                ExecuteNonQuery(dbconn, createMarksTable);
                ExecuteNonQuery(dbconn, createRoomsTable);
                ExecuteNonQuery(dbconn, createTimeSlotsTable);
                ExecuteNonQuery(dbconn, createTimetablesTable);
                ExecuteNonQuery(dbconn, createStudentSubjectsTable);
                ExecuteNonQuery(dbconn, createLecturerSubjectsTable);
                
            }
        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string commandText)
        {
            using (var cmd = new SQLiteCommand(commandText, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
