using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
    class Program
    {
        static void Main(string[] args)
        {
            OfficeManagement officeManagement = new OfficeManagement();
            // using (XmlReader reader = XmlReader.Create("Office.xml")) ;

            //POPULEAZA LISTA DE ANGAJATI SI PROIECTE SI APOI CHEAMA WRITEXML


            
            //var officeManagement = new OfficeManagement();
            /*officeManagement.DisplayAllProjects();

            officeManagement.DeleteEmployee(dev);
            officeManagement.DisplayAllEmployees();
            officeManagement.DisplayAllProjects();*/

            PopulateEmployeeList(officeManagement);
            officeManagement.DisplayAllEmployees();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(@"c:\temp\temp.xml", settings))
            {
                officeManagement.WriteXml(writer);
            }

            Console.ReadLine();

        }
        //private static void PopulateEmployeeList(OfficeManagement office)
        //{

        //    Employee emp = new Employee();
        //    emp.Name = "Ana";
        //    emp.Position = EmployeePosition.Intern;
        //    emp.Capacity = 8;



        //}
        private static void PopulateEmployeeList(OfficeManagement officeManagement)
        {
            var allInformation = File.ReadAllText(@"C:\Users\semida.lucaciu\Downloads\officeDB.txt");
            char[] arr = new char[] { '\r', '\n' };
            //var result = (from line in allInformation
            //              let trimmedLine = line.TrimStart(arr)
            //              select line).ToList();
            //GetEmployeeFromText(result, officeManagement);
            var employees = allInformation.Split(new string[] { nameof(Employee), "{", "}" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var record in employees)
            {
                var trimmedRecord = record.TrimStart(new char[] { '\r', '\n' });
                if (trimmedRecord.StartsWith(nameof(Project)) || trimmedRecord.Equals("\r\n")) continue;
                GetEmployeeFromText(trimmedRecord, officeManagement);
            }
            //var query =
            //    from n in employees
            //    where n.Contains("Name:")
            //    select n.ToUpper();
        }

        private static Employee GetEmployeeFromText(string info, OfficeManagement officeMangement)
        {
            if (string.IsNullOrEmpty(info)) return null;
            var lines = info
                    .Replace("\t", string.Empty)
                    .Trim()
                    .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!lines.Any()) return null;

            var name = GetPropertyValue(nameof(Employee.Name), lines);
            var position = GetPropertyValue(nameof(Employee.Position), lines);
            var capacity = GetPropertyValue(nameof(Employee.Capacity), lines);
            var hobbies = GetPropertyValue(nameof(Employee.Hobbies), lines).Split(',');

            var positionType = EmployeePosition.Intern;
            if (!Enum.TryParse(position, out positionType))
                Console.WriteLine($"Pentru {name} nu s-a putut stabili position type");

            return officeMangement.AddEmployee(name, positionType, Convert.ToSingle(capacity, CultureInfo.InvariantCulture), hobbies.ToList());
        }

        private static string GetPropertyValue(string propertyName, string[] values)
        {
            if (!values.Any()) return string.Empty;
            var adjustedProperty = propertyName + ":";
            return values.SingleOrDefault(x => x.StartsWith(adjustedProperty))
                .TrimStart(adjustedProperty.ToCharArray())
                .TrimEnd("\r\n".ToCharArray());
        }
    }
}
