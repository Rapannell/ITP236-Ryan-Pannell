using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI_RyanPannell
{
    public interface IPerson
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        int Age { get; set; }
        string Email { get; set; }

        void Display();
    }

    public class Student : IPerson
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }

        public void Display()
        {
            Console.WriteLine($"Student: {FirstName} {LastName}, Age: {Age}, Email: {Email}, Major: {Major}");
        }
    }

    public class Teacher : IPerson
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public void Display()
        {
            Console.WriteLine($"Teacher: {FirstName} {LastName}, Age: {Age}, Email: {Email}, Department: {Department}");
        }
    }

    public class Staff : IPerson
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }

        public void Display()
        {
            Console.WriteLine($"Staff: {FirstName} {LastName}, Age: {Age}, Email: {Email}, Position: {Position}");
        }
    }
}
