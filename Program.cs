using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.XPath;

string? readResult;
string menuSelection = "";




List<XmlNode> nodeList = new List<XmlNode>();

do
{
    Console.Clear();
    Console.WriteLine("Welcome to the My Prefect workout app. Your main menu options are:");
    Console.WriteLine(" 1. Calisthenic Workout");
    Console.WriteLine(" 2. Weight Workout");
    Console.WriteLine(" 3. Cardio Workout");
    Console.WriteLine(" 4. Custom workout");
    Console.WriteLine(" 5. Add New workout");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    // switch-case to process the selected menu option
    switch (menuSelection)
    {
        case "1":
            XmlDocument calDoc = new XmlDocument();
            calDoc.Load(@"calData.xml");
            XmlNodeList calNodes = calDoc.DocumentElement.SelectNodes("/workouts/Workout");

            foreach (XmlNode node in calNodes)
            {
                if (node.Attributes["category"].Value == "calisthenics")
                {
                    nodeList.Add(node);
                }
            }
            Random rng = new Random();
            int n = nodeList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                XmlNode value = nodeList[k];
                nodeList[k] = nodeList[n];
                nodeList[n] = value;
            }

            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine("Name: " + node["name"].InnerText + " Sets: " + node["sets"].InnerText + " Rest: " + node["rest"].InnerText);
            }
            nodeList = new List<XmlNode>();
            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        case "2":
            XmlDocument weightDoc = new XmlDocument();
            weightDoc.Load(@"weightData.xml");
            XmlNodeList weightNodes = weightDoc.DocumentElement.SelectNodes("/workouts/Workout");
            // string? readResult2;
            // string menuSelection2 = "";
            // Console.WriteLine("What would you like to focus on");
            // Console.WriteLine("1. Full body");
            // Console.WriteLine("2. Upper body");
            // Console.WriteLine("3. Lower body");
            // Console.WriteLine();
            // Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

            // readResult2 = Console.ReadLine();
            // if (readResult2 != null)
            // {
            //     menuSelection2 = readResult2.ToLower();
            // }
            foreach (XmlNode node in weightNodes)
            {
                if (node.Attributes["category"].Value == "weight")
                {
                    nodeList.Add(node);
                }
            }
            Random rng2 = new Random();
            int n2 = nodeList.Count;
            while (n2 > 1)
            {
                n2--;
                int k = rng2.Next(n2 + 1);
                XmlNode value = nodeList[k];
                nodeList[k] = nodeList[n2];
                nodeList[n2] = value;
            }

            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine("Name: " + node["name"].InnerText + " Sets: " + node["sets"].InnerText + " Rest: " + node["rest"].InnerText);
            }
            nodeList = new List<XmlNode>();
            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;
        case "3":
            XmlDocument carDoc = new XmlDocument();
            carDoc.Load(@"carData.xml");
            XmlNodeList carNodes = carDoc.DocumentElement.SelectNodes("/workouts/Workout");

            foreach (XmlNode node in carNodes)
            {
                if (node.Attributes["category"].Value == "cardio")
                {
                    nodeList.Add(node);
                }
            }
            Random rng3 = new Random();
            int n3 = nodeList.Count;
            while (n3 > 1)
            {
                n3--;
                int k = rng3.Next(n3 + 1);
                XmlNode value = nodeList[k];
                nodeList[k] = nodeList[n3];
                nodeList[n3] = value;
            }

            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine("Name: " + node["name"].InnerText + " Sets: " + node["sets"].InnerText + " Rest: " + node["rest"].InnerText);
            }
            nodeList = new List<XmlNode>();
            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        default:
            break;
    }
} while (menuSelection != "exit");