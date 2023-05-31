﻿using System;
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

        case "4":

            string attributeName = "category";
            Console.WriteLine("Enter the category you want to search for:");
            Console.WriteLine(" 1. Calisthenics");
            Console.WriteLine(" 2. Weight");
            Console.WriteLine(" 3. Cardio");
            string attributeValue = Console.ReadLine();
            switch (attributeValue)
            {
                case "1":
                    attributeValue = "calisthenics";
                    break;

                case "2":
                    attributeValue = "weight";
                    break;
                case "3":
                    attributeValue = "cardio";
                    break;
                default:
                    break;
            }

            string focusAttributeName = "focus";
            List<string> focusAttributeValues = new List<string>();
            bool selectingFocusAreas = true;
            while (selectingFocusAreas)
            {
                Console.WriteLine("Enter the focus you want to work on:(choose one at a time)");
                Console.WriteLine(" 1. Arms");
                Console.WriteLine(" 2. Legs");
                Console.WriteLine(" 3. Shoulders");
                Console.WriteLine(" 4. Back");
                Console.WriteLine(" 5. Abs");
                Console.WriteLine(" 6. Pecs");
                Console.WriteLine(" 7. Done selecting focus areas");
                string focusAttributeValue = Console.ReadLine();
                switch (focusAttributeValue)
                {
                    case "1":
                        focusAttributeValues.Add("arms");
                        break;

                    case "2":
                        focusAttributeValues.Add("legs");
                        break;

                    case "3":
                        focusAttributeValues.Add("shoulders");
                        break;

                    case "4":
                        focusAttributeValues.Add("back");
                        break;

                    case "5":
                        focusAttributeValues.Add("abs");
                        break;

                    case "6":
                        focusAttributeValues.Add("pecs");
                        break;

                    case "7":
                        selectingFocusAreas = false;
                        break;

                    default:
                        break;
                }
            }
            string tagsAttributeName = "tags";
            Console.WriteLine("Enter your skill level:");
            Console.WriteLine(" 1. Beginner");
            Console.WriteLine(" 2. Intermediate");
            Console.WriteLine(" 3. Pro");
            string tagsAttributeValue = Console.ReadLine();
            switch (tagsAttributeValue)
            {
                case "1":
                    tagsAttributeValue = "beginner";
                    break;

                case "2":
                    tagsAttributeValue = "intermediate";
                    break;
                case "3":
                    tagsAttributeValue = "pro";
                    break;
                default:
                    break;
            }

            List<string> xmlFiles = new List<string> { @"calData.xml", @"weightData.xml", @"carData.xml" };
            foreach (string xmlFile in xmlFiles)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFile);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/workouts/Workout");

                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes[attributeName].Value == attributeValue && focusAttributeValues.Any(focus => node.Attributes[focusAttributeName].Value.Contains(focus)) && node.Attributes[tagsAttributeName].Value == tagsAttributeValue)
                    {
                        nodeList.Add(node);
                    }
                }
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