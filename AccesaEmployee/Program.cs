using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccesaEmployee
{
    class Program
    {
        private static object videogameRatings;

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
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            using (XmlWriter writer = XmlWriter.Create("temp.xml", settings))
            {
                writer.WriteStartDocument();
                officeManagement.WriteXml(writer);
                writer.WriteEndDocument();
<<<<<<< HEAD
            }

            using (StreamWriter file = File.CreateText(@"c:\employee.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                employee.WriteTo(writer);
=======
>>>>>>> e1ecf91d10f387c84cc4f7bdbd6015395509934d
            }

            Console.ReadLine();

        }
<<<<<<< HEAD

        public static string WriteFromObject()
        {
            //Create User object.  
            Employee emp = new Employee();

            //Create a stream to serialize the object to.  
            MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
            ser.WriteObject(ms, emp);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        // Deserialize a JSON stream to a User object.  
        public static Employee ReadToObject(string json)
        {
            Employee deserializedUser = new Employee();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as Employee;
            ms.Close();
            return deserializedUser;
        }

=======
        
>>>>>>> e1ecf91d10f387c84cc4f7bdbd6015395509934d
        private static void PopulateEmployeeList(OfficeManagement officeManagement)
        {
            var allInformation = File.ReadAllText(@"C:\Users\semida.lucaciu\Downloads\officeDB.txt");
            char[] arr = new char[] { '\r', '\n' };
            
            var employees = allInformation.Split(new string[] { nameof(Employee), "{", "}" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var record in employees)
            {
                var trimmedRecord = record.TrimStart(new char[] { '\r', '\n' });
                if (trimmedRecord.StartsWith(nameof(Project)) || trimmedRecord.Equals("\r\n")) continue;
                GetEmployeeFromText(trimmedRecord, officeManagement);
            }
            
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
