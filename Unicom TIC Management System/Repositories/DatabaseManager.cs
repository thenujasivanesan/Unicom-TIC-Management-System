﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_TIC_Management_System.Repositories
{
    internal class DatabaseManager
    {
        // creating all necessary tables if they don't exist
        public static void InitializeDatabase()
        {
            using(var dbconn = dbConfig.GetConnection()) 
            {
             
                // Creating Users table
                string UsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL,
                Role TEXT NOT NULL
            );";

                // Creating Courses table
                string CoursesTable = @"
            CREATE TABLE IF NOT EXISTS Courses (
                CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                CourseName TEXT NOT NULL
            );";

                // Creating Subjects table
                string SubjectsTable = @"
            CREATE TABLE IF NOT EXISTS Subjects (
                SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                SubjectName TEXT NOT NULL,
                CourseID INTEGER,
                LecturerId INTEGER,
                FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
            );";

                // Creating Students table
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

                // Creating Exams table
                string ExamsTable = @"
            CREATE TABLE IF NOT EXISTS Exams (
                ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                ExamName TEXT NOT NULL,
                SubjectID INTEGER,
                FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
            );";

                // Creating Marks table
                string MarksTable = @"
            CREATE TABLE IF NOT EXISTS Marks (
                MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentID INTEGER NOT NULL,
                ExamID INTEGER NOT NULL,
                Score INTEGER NOT NULL,
                FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
            );";

                // Creating Rooms table
                string RoomsTable = @"
            CREATE TABLE IF NOT EXISTS Rooms (
                RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                RoomName TEXT NOT NULL,
                RoomType TEXT NOT NULL
            );";


                // Creating Timetables table
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

                
                // Creating Lecturers table
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

                // creating staff table
                string StaffTable = @"
            CREATE TABLE IF NOT EXISTS Staff (
                StaffId INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER UNIQUE,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                Gender TEXT,
                DateOfBirth TEXT,
                Contact TEXT,
                Email TEXT,
                Address TEXT,
                FOREIGN KEY (UserId) REFERENCES Users(UserId)
            );";


                ExecuteNonQuery(dbconn, UsersTable);
                ExecuteNonQuery(dbconn, CoursesTable);
                ExecuteNonQuery(dbconn, SubjectsTable);
                ExecuteNonQuery(dbconn, StudentsTable);
                ExecuteNonQuery(dbconn, ExamsTable);
                ExecuteNonQuery(dbconn, MarksTable);
                ExecuteNonQuery(dbconn, RoomsTable);
                ExecuteNonQuery(dbconn, TimetablesTable);
                ExecuteNonQuery(dbconn, LecturersTable);
                ExecuteNonQuery(dbconn, StaffTable);

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
       
