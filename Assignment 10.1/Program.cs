using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace Assignment_10._1
{

    [Serializable]
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

    }

    internal class Program
    {
        #region Formatting

        static void Introduction()
        {
            // HEADER
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Assignment 10.1");
            Console.WriteLine("Name: Isaac Jang\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n---------------PART 1---------------\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Transition()
        {
            // TRANSITION
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress any key to continue!\n");
            Console.ReadKey();
        }

        static void Part(int i)
        {
            // PART 2
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n---------------PART {i}---------------\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void ClosingMessage()
        {
            // CLOSING MESSAGE
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nHave a great day!");
        }
        #endregion
        static void Main(string[] args)
        {
            Introduction();
            /*Create any user defined class of your choice like Student, Customer etc.
             * Add 3 properties in it(of your choice). 
             * Serialize and deserialize the object of this class by Binary, XML, JSON format.
            */

            var address = new Address { StreetAddress = "123 Street", City = "Oceanside", State = "CA", ZipCode = 98058 };
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nAddress being Serailized: StreetAddress = 123 Street, City = Oceanside, State = CA, ZipCode = 98058\n");

            #region Binary
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSERIALIZE IN BINARY\n");
            Console.ForegroundColor = ConsoleColor.White;

            string filePathBinary = "BinaryAddress.txt";

            // Creating formatterBinary for binary
            IFormatter formatterBinary = new BinaryFormatter();

            // Serialize in Binary
            using (FileStream binaryFile = File.Create(filePathBinary))
            {
                formatterBinary.Serialize(binaryFile, address);
            }
            Console.WriteLine("Address serialized to binary format...\n");
            Console.ReadKey();

            //prints out the binary
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPrinted out in Binary\n");
            Console.ForegroundColor = ConsoleColor.White;

            byte[] fileContents = File.ReadAllBytes(filePathBinary);
            foreach (byte b in fileContents)
            {
                Console.Write(Convert.ToString(b, 2).PadLeft(8, '0') + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();


            // Deserialize from Binary
            Address addressFromBinary;
            using (FileStream binaryFile = File.OpenRead(filePathBinary))
            {
                addressFromBinary = (Address)formatterBinary.Deserialize(binaryFile);
            }

            // Output the deserialized object's properties
            Console.WriteLine("\nDeserialized Address From Binary:");
            Console.WriteLine($"Street: {addressFromBinary.StreetAddress}");
            Console.WriteLine($"City: {addressFromBinary.City}");
            Console.WriteLine($"State: {addressFromBinary.State}");
            Console.WriteLine($"Zip Code: {addressFromBinary.ZipCode}");
            #endregion

            Transition();

            #region XML
            // Serialize in XML
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSERIALIZE IN XML\n");
            Console.ForegroundColor = ConsoleColor.White;

            //File Name
            string filePathXML = "XMLAddress.xml";

            //XML Fomatter
            IFormatter formatterXML = new SoapFormatter();

            //Serializing 
            using (FileStream XMLFile = File.Create(filePathXML))
            {
                formatterXML.Serialize(XMLFile, address);
            }
            Console.WriteLine("Object serialized to XML format...\n");
            Console.ReadKey();

            //print XML
            string xmlContent = File.ReadAllText(filePathXML);
            Console.WriteLine("XML Content:");
            Console.WriteLine(xmlContent);
            Console.ReadKey();

            //Deserailize from XML
            Address addressFromXML = new Address();
            using (FileStream XMLFile = File.OpenRead(filePathXML))
            {
                addressFromXML = (Address)formatterXML.Deserialize(XMLFile);
            }

            //Output Deserialized Address from XML
            Console.WriteLine("\nDeserialized Address From XML:");
            Console.WriteLine($"Street: {addressFromXML.StreetAddress}");
            Console.WriteLine($"City: {addressFromXML.City}");
            Console.WriteLine($"State: {addressFromXML.State}");
            Console.WriteLine($"Zip Code: {addressFromXML.ZipCode}");

            #endregion

            Transition();

            #region JSON
            // Serialize in JSON
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSERIALIZE IN JSON\n");
            Console.ForegroundColor = ConsoleColor.White;

            //Serializing to Json
            var json = JsonConvert.SerializeObject(address);
            File.WriteAllText("JsonAddress.json", json);
            Console.WriteLine("Address serialized to Json format...\n");
            Console.ReadKey();


            // Deserialize from Json Format
            var jsonContent = File.ReadAllText("JsonAddress.json");
            Console.WriteLine("JSON Content:");
            Console.WriteLine(jsonContent);
            Console.ReadKey();

            var addressFromJson = JsonConvert.DeserializeObject<Address>(jsonContent);

            //output addressFromJson
            Console.WriteLine("\nDeserialized Address From Json:");
            Console.WriteLine($"Street: {addressFromJson.StreetAddress}");
            Console.WriteLine($"City: {addressFromJson.City}");
            Console.WriteLine($"State: {addressFromJson.State}");
            Console.WriteLine($"Zip Code: {addressFromJson.ZipCode}");

            #endregion
            ClosingMessage();
            Console.ReadKey();
        }
    }
}
