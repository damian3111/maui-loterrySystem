using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SystemLosowania.Models
{
    internal class StudentRepository
    {

        public static List<Student> students = new List<Student>()
        {

            //  new Product { ProductId = 1, Name = "asd6", Color=false, Quantity=1},
            // new Product { ProductId = 2, Name = "asd23", Color = false, Quantity = 2, CategoryId = 1},
            //new Product { ProductId= 3, Name = "asd43" , Color = false, Quantity = 3, CategoryId = 1},
            //new Product { ProductId= 4, Name = "asd65" , Color = false, Quantity = 4, CategoryId = 2},    
            //new Product { ProductId= 5, Name = "asd14", Color = false, Quantity = 5, CategoryId = 2}
        };

        public static List<Student> getStudents(string studentClass)
        {
            return students.Where(s => s.Class == studentClass).ToList();
        }


        public static Student getStudentById(string studentId)
        {
            return students.FirstOrDefault(s => s.Id == studentId);
        }


        public static void AddStudent(Student student)
        {

            students.Add(student);
            SaveStudentToXml(student);
        }

        public static List<Student> ReadStudentsFromXml()
        {
            students.Clear();
            //if (students.Count() != 0)
            //{
            //    return students;
            //}

            try
            {
                var path = FileSystem.Current.AppDataDirectory;
                var filePath = Path.Combine(path, "students.xml");
                XDocument xdoc = XDocument.Load(filePath);

                var studentElements = xdoc.Root.Elements("Student");

                foreach (var studentElement in studentElements)
                {
                    Student student = new Student()
                    {
                        Id = studentElement.Element("Id").Value,
                        Firstname = studentElement.Element("Firstname").Value,
                        Lastname = studentElement.Element("Lastname").Value,
                        Class = studentElement.Element("Class").Value
                    };

                    students.Add(student);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML file: {ex.Message}");
            }

            return students;
        }

        private static void SaveStudentToXml(Student student)
        {

            var path = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(path, "students.xml");
            try
            {
                XDocument xdoc;

                if (File.Exists(filePath))
                {
                    xdoc = XDocument.Load(filePath);
                }
                else
                {
                    xdoc = new XDocument(new XElement("Students"));
                }

                XElement categoryElement = new XElement("Student",
                    new XElement("Id", student.Id),
                    new XElement("Firstname", student.Firstname),
                    new XElement("Lastname", student.Lastname),
                    new XElement("Class", student.Class)
                );

                xdoc.Root.Add(categoryElement);

                xdoc.Save(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving XML file: {ex.Message}");
            }
        }

        public static void RemoveProductFromXml(string studentId)
        {
            var path = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(path, "students.xml");

            try
            {
                XDocument xdoc = XDocument.Load(filePath);

                XElement productElementToRemove = xdoc.Root.Elements("Student")
                    .FirstOrDefault(e => e.Element("Id")?.Value == studentId);

                if (productElementToRemove != null)
                {
                    productElementToRemove.Remove();
                }

                xdoc.Save(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing category from XML file: {ex.Message}");
            }
        }
    }
}

