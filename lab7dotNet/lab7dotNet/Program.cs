using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace lab7dotNet
{
    public class Department(int id, string name)
    {
        public int Id { get; set; } = id;
        public String Name { get; set; } = name;

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics(int id, int index, string name, Gender gender, bool active,
        int departmentId, List<string> topics)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;

        public List<string> Topics { get; set; } = topics;

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public class Student(int id, int index, string name, Gender gender, bool active,
        int departmentId, List<int> topics)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;

        public List<int> Topics { get; set; } = topics;

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }

        public int CountTopics()
        {
            return Topics.Count();
        }

        public bool HasTopic(int topicId)
        {
            return Topics.Contains(topicId);
        }
    }

    public class StudentTwo(int id, int index, string name, Gender gender, bool active,
        int departmentId)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;

        public override string ToString()
        {
            return $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}";
        }
    }

    public class StudentToTopic
    {
        public int StudentId { get; set; }
        public int TopicId { get; set; }

        public StudentToTopic(int studentId, int topicId)
        {
            StudentId = studentId;
            TopicId = topicId;
        }
        public override string ToString()
        {
            return $"student: {StudentId,2}), topic: {TopicId,2}";
        }

    }



    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Id,2}), {Name,15}";
        }

        public static double Multiply(double d1, double d2)
        {
            return d1 * d2;
        }

    }

    public static class Generator
    {
        public static int[] GenerateIntsEasy()
        {
            return [5, 3, 9, 7, 1, 2, 6, 7, 8];
        }

        public static int[] GenerateIntsMany()
        {
            var result = new int[10000];
            Random random = new();
            for (int i = 0; i < result.Length; i++)
                result[i] = random.Next(1000);
            return result;
        }

        public static List<string> GenerateNamesEasy()
        {
            return [
                "Nowak",
                "Kowalski",
                "Schmidt",
                "Newman",
                "Bandingo",
                "Miniwiliger",
                "Showner",
                "Neumann",
                "Rocky",
                "Bruno"
            ];
        }
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return [
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    ["C#","PHP","algorithms"]),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    ["C#","C++","fuzzy logic"]),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    ["Basic","Java"]),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    ["JavaScript","neural networks"]),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    ["Java","C#"]),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    ["algorithms","web programming"]),
            new StudentWithTopics(11,22345,"Nowak", Gender.Female,true,2,
                    ["C#","PHP","web programming"]),
            new StudentWithTopics(12, 23235, "Nowak", Gender.Male, true,1,
                    ["C#","C++","fuzzy logic"]),
            new StudentWithTopics(13, 23444, "Showner", Gender.Male, false,2,
                    ["Basic","C#"]),
            new StudentWithTopics(14, 24000, "Neumann", Gender.Female, false,3,
                    ["JavaScript","neural networks"]),
            new StudentWithTopics(15, 24001, "Rocky", Gender.Male, true,2,
                    ["fuzzy logic","C#"]),
            new StudentWithTopics(16, 24100, "Bruno", Gender.Female, false,2,
                    ["algorithms","web programming"]),
            ];
        }

        public static List<Department> GenerateDepartmentsEasy()
        {
            return [
            new Department(1,"Computer Science"),
            new Department(2,"Electronics"),
            new Department(3,"Mathematics"),
            new Department(4,"Mechanics")
            ];
        }

        public static List<Topic> GenerateTopics()
        {
            var topics = Generator.GenerateStudentsWithTopicsEasy()
                           .SelectMany(s => s.Topics)
                           .Distinct()
                           .Select((topic, index) => new Topic(index + 1, topic));
            return topics.ToList();
        }

    }

    class Program
    {
        //Zadanie 1 - grupowanie studentów
        public static void GroupStudents(int n)
        {
            //sort
            var resStud = Generator.GenerateStudentsWithTopicsEasy()
                          .OrderBy(s => s.Name)
                          .ThenBy(s => s.Index);
            //group
            var groupedStudents = resStud
                                  .Select((student, index) => new { student, GroupIndex = index / n }) // indeks grupy
                                  .GroupBy(x => x.GroupIndex, x => x.student);

            foreach (var group in groupedStudents)
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach(s => Console.WriteLine(" " + s));
            }
        }

        //Zadanie 2a - posortować tematy (Topics) występujące wśród studentów wg częstośc występowania wśród wszystkich studentów
      

        public static void MostCommonTopicInCollection(IEnumerable<StudentWithTopics> students)
        {
            var resTopics = students
                            .SelectMany(s => s.Topics)
                            .GroupBy(topic => topic)
                            .Select(group => new { Topic = group.Key, Count = group.Count() })
                            .OrderByDescending(x => x.Count)
                            .ThenBy(x => x.Topic);
            foreach (var topic in resTopics)
            {
                Console.WriteLine($"{topic.Topic}: {topic.Count} times");
            }
        }

        public static void MostCommonTopics()
        {
            MostCommonTopicInCollection(Generator.GenerateStudentsWithTopicsEasy());
        }

        //Zadanie 2b - z podzialem na plec

        public static void MostCommonTopicByGender()
        {
            var topicsByGender = Generator.GenerateStudentsWithTopicsEasy()
                .GroupBy(s => s.Gender)
                .Select(genderGroup => new
                {
                    Gender = genderGroup.Key,
                    Topics = genderGroup
                        .SelectMany(s => s.Topics)
                        .GroupBy(topic => topic)
                        .Select(group => new { Topic = group.Key, Count = group.Count() })
                        .OrderByDescending(x => x.Count)
                        .ThenBy(x => x.Topic)
                });

            foreach (var genderGroup in topicsByGender)
            {
                Console.WriteLine($"Gender: {genderGroup.Gender}");
                foreach (var topic in genderGroup.Topics)
                {
                    Console.WriteLine($"  {topic.Topic}: {topic.Count} times");
                }
            }
        }


        //Zadanie 3ab - Stworzyć nową klasę Student, która różni się tym od klasy StudentWithTopics, że tematy są pamiętane jako lista identyfikatorow tematow
        public static void StudentsWithTopicsToStudents()
        {
            var topics = Generator.GenerateTopics();
            foreach (var topic in topics)
            {
                Console.WriteLine(topic);
            }
            Console.WriteLine();

            var topicMap = topics.ToDictionary(t => t.Name, t => t.Id);

            var students = Generator.GenerateStudentsWithTopicsEasy()
                .Select(s => new Student
                (
                    s.Id,
                    s.Index,
                    s.Name,
                    s.Gender,
                    s.Active,
                    s.DepartmentId,
                    s.Topics.Select(t => topicMap[t]).ToList()
                ));
            Console.WriteLine("Studenci z id kursów");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }


        //Zadanie 3c - jak w bazie relacyjnej - odzielenie 
        public static void StudentsWithoutTopics()
        {
            var topics = Generator.GenerateTopics();

            var topicMap = topics.ToDictionary(t => t.Name, t => t.Id);

            var studentsWithTopics = Generator.GenerateStudentsWithTopicsEasy();

            var students = studentsWithTopics
                .Select(s => new StudentTwo
                (
                    s.Id,
                    s.Index,
                    s.Name,
                    s.Gender,
                    s.Active,
                    s.DepartmentId
                ));

            List<StudentToTopic> studentsToTopic = studentsWithTopics
                .SelectMany(s => s.Topics.Select(t => new StudentToTopic
                (
                    s.Id,
                    topicMap[t]
                ))).ToList();


            Console.WriteLine("Students");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine("\nStudents To Topic");
            foreach (var st in studentsToTopic)
            {
                Console.WriteLine(st);
            }
        }

        //Zadanie 4
        public static void ShowReflection()
        {
            //object topic = new Topic(21, "dotNet");
            Type topicType = Type.GetType("lab7dotNet.Topic");
            
            object[] topicArgs = { 21, "dotNet" };
            object topic = Activator.CreateInstance(topicType,topicArgs);
            

            MethodInfo methodInfo = topic.GetType().GetMethod("Multiply");
            object result = methodInfo.Invoke(topic, new object[] {2.1,5.4});
            Console.WriteLine(result);
        }




        static void Main(string[] args)
        {
            Console.WriteLine("-------------Zad 1---------------\n");
            GroupStudents(5);

            Console.WriteLine("\n\n-------------Zad 2a---------------\n");
            MostCommonTopics();

            Console.WriteLine("\n\n-------------Zad 2b---------------\n");
            MostCommonTopicByGender();

            Console.WriteLine("\n\n-------------Zad 3---------------\n");
            StudentsWithTopicsToStudents();

            Console.WriteLine("\n\n-------------Zad 3c---------------\n");
            StudentsWithoutTopics();

            Console.WriteLine("\n\n-------------Zad 4---------------\n");
            ShowReflection();
        }
    }
}