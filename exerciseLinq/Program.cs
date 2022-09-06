using System;
using System.Collections.Generic;
using System.Linq;

namespace exerciseLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Za linq se potrebni tri raboti
            //1.data source
            //2.query expresion
            //3.izvrsuvanje/ koe moze da bide i odlozeno

            //ZADACA 1
            //List<string> firstList = new List<string>() { "hello", "hi", "good evening", "good day", "good morning", "goodbye" };
            //List<string> secondList = new List<string>() { "whatsup", "how are you", "hello", "bye", "hi" };
            ////1.1
            //List<string> filteredList1 = firstList.Where(el => el.Contains("good")).ToList();
            //Console.WriteLine("Find all elements of first list that contains 'good'");
            //foreach (var item in filteredList1)
            //{
            //    Console.WriteLine(item);
            //}

            ////1.2
            // List<string> filteredList2 =  firstList.Where(el => el.Contains('g') || el.Contains('h') && el.Length >= 2).ToList();
            //Console.WriteLine("Find all elements of first list that contains letter 'g' or contains letter 'h' and has more than or equal to 2 letters.");
            //foreach(var item in filteredList2)
            //{
            //    Console.WriteLine(item);
            //}

            ////1.3
            // List<string> intersection = firstList.Intersect(secondList).ToList();
            //Console.WriteLine("Find intersection of two lists.");
            //foreach (string item in intersection)
            //{
            //    Console.WriteLine(item);
            //}

            ////1.4
            //List<string> union =firstList.Union(secondList).ToList();
            //Console.WriteLine("List all values from the lists, without duplicates.");
            //foreach (string item in union)
            //{
            //    Console.WriteLine(item);
            //}

            //ZADACA 2

            List<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18, SubjectID = 1 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, SubjectID = 1 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, SubjectID = 2 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, SubjectID = 2 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 21, SubjectID = 3 }
            };

            List<Subject> subjectList = new List<Subject>() {
                new Subject(){ SubjectID = 1, SubjectName = "Physics"},
                new Subject(){ SubjectID = 2, SubjectName = "History"},
                new Subject(){ SubjectID = 3, SubjectName = "Mathematics"}
            };
            
            //2.1
            //Count all Students who are older than 15
            int countOfAllStudentsOlderThan15 =
               studentList.Where(t => t.Age > 15).Count();
            Console.WriteLine($"Number of students older than 15: {countOfAllStudentsOlderThan15}");

            //2.2
            //Count all Subjects that have subject name which contains 's' letter in their name.
            int countSubjectsWithNameContainingS =
               subjectList.Where(t => t.SubjectName.Contains('s')).Count();
            Console.WriteLine($"Number of subjects that have subject name which contains 's' letter in their name: {countSubjectsWithNameContainingS}");

            //2.3
            //Get all students that are studying subject with name Mathematics.
            Subject subjectMathematics = subjectList.Where(t => t.SubjectName == "Mathematics").FirstOrDefault();
            List<Student> listOfStudentsStudyingMathematics = studentList.Where(t => t.SubjectID == subjectMathematics.SubjectID).ToList();
            Console.WriteLine("All students that are studying subject with name Mathematics.");
            foreach (Student student in listOfStudentsStudyingMathematics)
            {
                Console.WriteLine($"ID:{student.StudentID} Name:{ student.StudentName} Age:{ student.Age}");
            }

            //2.4
            //Show all student names that are older than 18 years and they are studying subject which name is 'Physics'
            Subject subjectPhysics = subjectList.Where(t => t.SubjectName == "Physics").FirstOrDefault();
            List<Student> listOfStudentsOlderThan18StudyingPhysics = studentList.
                Where(t => t.Age > 18 && t.SubjectID == subjectPhysics.SubjectID).ToList();
            Console.WriteLine("Show all student names that are older than 18 years and they are studying subject which name is 'Physics'");
            foreach (Student student in listOfStudentsOlderThan18StudyingPhysics)
            {
                Console.WriteLine($"{student.StudentName}");
            }

            //2.5
            //Show all students with their id, name and age, that are older than 15 years, but not older than 25, and they are studying subject which name is 'History'.
            Subject subjectHistory = subjectList.Where(t => t.SubjectName == "History").FirstOrDefault();
            List<Student> listOfStudents = studentList.Where(t => t.Age > 15 && t.Age <= 25 && t.SubjectID == subjectHistory.SubjectID).ToList();
            Console.WriteLine("Students that are older than 15 years, but not older than 25, and they are studying subject which name is 'History'");
            foreach (Student student in listOfStudents)
            {
                Console.WriteLine($"ID:{student.StudentID} Name:{ student.StudentName} Age:{ student.Age}");
            }
            
            //2.6
            //Show all students with their id, name and age, 
            //whose name starts with R and they are not older than 30
            //ordered by student name, then by subject name.

            List<Student> listOfStudentsNotOlderThan30NameStartsWithR = studentList.Where(t => t.Age <= 30 && t.StudentName.
            StartsWith("R")).ToList();

            //ordered by student name
            List <Student> listOfStudentsOrderedByStudentName = listOfStudentsNotOlderThan30NameStartsWithR.
                OrderBy(t => t.StudentName).ToList();
            Console.WriteLine("Order by student name");
            foreach (Student student in listOfStudentsOrderedByStudentName)
            {
                Console.WriteLine($"ID:{student.StudentID} Name:{ student.StudentName} Age:{ student.Age}");
            }

            //ordered by subject name
            var studentSubject = listOfStudentsNotOlderThan30NameStartsWithR.
                Join(subjectList, st => st.SubjectID,su=>su.SubjectID,
                (st,su)=>new 
                {
                    StudentID = st.StudentID,
                    StudentName = st.StudentName,
                    Age = st.Age,
                    SubjectName = su.SubjectName,
                }).OrderBy(su=>su.SubjectName);

            Console.WriteLine("Order by subject name");
            foreach (var student in studentSubject)
            {
                Console.WriteLine($"ID:{student.StudentID} Name:{ student.StudentName} Age:{ student.Age} Subject name: {student.SubjectName} ");
            }

        }
    }
}
