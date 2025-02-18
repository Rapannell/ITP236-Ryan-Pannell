using System;
using System.Collections.Generic;
using System.Linq;

namespace EDI_RyanPannell
{
    delegate string PersonOp(string input);

    class Program
    {
        static void Main(string[] args)
        {
            // Create objects
            Student student = new Student { FirstName = "john", LastName = "doe", Age = 20, Email = "john.doe@example.com", Major = "Computer Science" };
            Teacher teacher = new Teacher { FirstName = "jane", LastName = "smith", Age = 35, Email = "jane.smith@example.com", Department = "Mathematics" };
            Staff staff = new Staff { FirstName = "alice", LastName = "johnson", Age = 40, Email = "alice.johnson@example.com", Position = "Administrator" };

            // Define delegates
            PersonOp toLower = input => input.ToLower();
            PersonOp toProper = input => input.ProperName();

            // List of people
            List<IPerson> people = new List<IPerson> { student, teacher, staff };

            // Iterate through the list
            foreach (var person in people)
            {
                // Convert names using delegates
                person.FirstName = toProper(toLower(person.FirstName));
                person.LastName = toProper(toLower(person.LastName));

                // Display information
                person.Display();
            }
        }
    }
}
