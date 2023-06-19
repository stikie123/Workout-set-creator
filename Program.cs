using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

string? readResult;
string menuSelection = "";
int userChoice = 0;
string filePath = "";
string fileName = "";
string[] categoryFilePaths = Directory.GetFiles(@"categoryFiles", "*.xml");
string tagsAttributeValue = "";
XmlDocument doc = new XmlDocument();
string tags, name, sets, explanation, rest, time, link = "";

// List of nodes to store workout information
List<XmlNode> nodeList = new List<XmlNode>();

do
{
    Console.Clear();
    Console.WriteLine("Welcome to the My workout generator app. Your main menu options are:");
    Console.WriteLine("1. Your workouts");
    Console.WriteLine("2. Professional workouts");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }
    switch (readResult)
    {
        case "1":
            Console.Clear();
            Console.WriteLine(" 1. Quick workout");
            Console.WriteLine(" 2. Add New workout");
            Console.WriteLine(" 3. List/Update your workouts");
            Console.WriteLine(" 4. Create new category");
            Console.WriteLine(" 5. Delete a workout or category");
            Console.WriteLine();
            Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

            readResult = Console.ReadLine();
            if (readResult != null)
            {
                menuSelection = readResult;
            }
            else
            {
                break;
            }

            // switch-case to process the selected menu option
            switch (menuSelection)
            {
                //Quick workouts 
                case "1":
                    // Get file names in category folder
                    categoryFilePaths = Directory.GetFiles(@"categoryFiles", "*.xml");

                    // Prompt user to pick a workout category
                    Console.WriteLine("Pick what kind of workout you want to have.");
                    for (int i = 0; i < categoryFilePaths.Length; i++)
                    {
                        fileName = Path.GetFileNameWithoutExtension(categoryFilePaths[i]);
                        Console.WriteLine(" {0}: {1} workout", i + 1, fileName);
                    }

                    // Get user's choice
                    userChoice = Convert.ToInt32(Console.ReadLine());

                    // Check if user's choice is valid
                    if (userChoice > 0 && userChoice <= categoryFilePaths.Length)
                    {
                        // Create list to store focus attribute values
                        List<string> focusAttributeValue = new List<string>();

                        // Load XML file based on user's choice
                        fileName = $"{Path.GetFileNameWithoutExtension(categoryFilePaths[userChoice - 1])}.xml";
                        filePath = Path.Combine(@"categoryFiles", fileName);
                        doc.Load(filePath);

                        // Get all Workout nodes from XML file
                        XmlNodeList Nodes = doc.DocumentElement.SelectNodes("/workouts/Workout");

                        // Prompt user to choose focus area
                        bool selectingFocusAreas = true;
                        while (selectingFocusAreas)
                        {
                            Console.WriteLine("What would you like to focus on");
                            Console.WriteLine(" 1. Full body");
                            Console.WriteLine(" 2. Upper body");
                            Console.WriteLine(" 3. Lower body");
                            Console.WriteLine(" 4. Arms");
                            Console.WriteLine(" 5. Legs");
                            Console.WriteLine(" 6. Shoulders");
                            Console.WriteLine(" 7. Back");
                            Console.WriteLine(" 8. Abs");
                            Console.WriteLine(" 9. chest");
                            Console.WriteLine(" 10. Done selecting focus areas");
                            Console.WriteLine();
                            Console.WriteLine("Enter your selection number");

                            // Get user's focus area choice
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                menuSelection = readResult.ToLower();
                            }
                            // Process user's focus area choice using a switch statement
                            switch (menuSelection)
                            {
                                case "1":
                                    focusAttributeValue.Add("arms");
                                    focusAttributeValue.Add("full body");
                                    focusAttributeValue.Add("shoulders");
                                    focusAttributeValue.Add("back");
                                    focusAttributeValue.Add("chest");
                                    focusAttributeValue.Add("abs");
                                    focusAttributeValue.Add("legs");
                                    selectingFocusAreas = false;
                                    break;
                                case "2":
                                    focusAttributeValue.Add("arms");
                                    focusAttributeValue.Add("upper body");
                                    focusAttributeValue.Add("shoulders");
                                    focusAttributeValue.Add("back");
                                    focusAttributeValue.Add("chest");
                                    focusAttributeValue.Add("abs");
                                    selectingFocusAreas = false;
                                    break;
                                case "3":
                                    focusAttributeValue.Add("lower body");
                                    focusAttributeValue.Add("legs");
                                    selectingFocusAreas = false;
                                    break;
                                case "4":
                                    focusAttributeValue.Add("arms");
                                    break;

                                case "5":
                                    focusAttributeValue.Add("legs");
                                    break;

                                case "6":
                                    focusAttributeValue.Add("shoulders");
                                    break;

                                case "7":
                                    focusAttributeValue.Add("back");
                                    break;

                                case "8":
                                    focusAttributeValue.Add("abs");
                                    break;

                                case "9":
                                    focusAttributeValue.Add("chest");
                                    break;

                                case "10":
                                    selectingFocusAreas = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        // asks user 
                        Console.WriteLine("Enter your skill level:");
                        Console.WriteLine(" 1. Beginner");
                        Console.WriteLine(" 2. Intermediate");
                        Console.WriteLine(" 3. Pro");
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            tagsAttributeValue = readResult;
                            menuSelection = readResult.ToLower();
                        }
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
                        var totalTime = 0;

                        // Add nodes that match req to nodeList
                        foreach (XmlNode node in Nodes)
                        {
                            if (focusAttributeValue.Any(focus => node.Attributes["focus"].Value.Contains(focus)) && tagsAttributeValue == node.Attributes["tags"].InnerText)
                            {
                                nodeList.Add(node);
                            }
                        }

                        // Randomize order of nodes in nodeList
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
                        int exerciseTime = 0;
                        if (tagsAttributeValue == "beginner")
                        {
                            exerciseTime = 1800;
                        }
                        else if (tagsAttributeValue == "intermediate")
                        {
                            exerciseTime = 3600;
                        }
                        else if (tagsAttributeValue == "pro")
                        {
                            exerciseTime = 5400;
                        }

                        // Count time attributes values until totalTime limit is reached and remove rest
                        int i = 0;
                        while (totalTime < exerciseTime + 1 && i < nodeList.Count)
                        {
                            totalTime += int.Parse(nodeList[i]["time"].InnerText);
                            if (totalTime >= exerciseTime)
                            {
                                nodeList.RemoveRange(i, nodeList.Count - i);
                            }
                            i++;
                        }
                        Console.Clear();
                        // Print out randomized list of workouts
                        foreach (XmlNode node in nodeList)
                        {
                            Console.WriteLine("Workout: {0}", node["name"].InnerText);
                            Console.WriteLine("  focus: {0}", node.Attributes["focus"].InnerText);
                            Console.WriteLine("  Explanation: {0}", node["explanation"].InnerText);
                            Console.WriteLine("  Sets: {0}", node["sets"].InnerText);
                            Console.WriteLine("  Rest: {0}", node["rest"].InnerText);
                            Console.WriteLine("  Example link: {0}", node["exampleLink"].InnerText);
                        }
                        // prints total time of exercise
                        Console.WriteLine(totalTime / 60 + "min in Total");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                    nodeList = new List<XmlNode>();
                    Console.WriteLine("\n\rPress the Enter key to continue");
                    readResult = Console.ReadLine();
                    break;

                //Add new workout
                case "2":

                    Console.WriteLine("\n\r*Which type of workout would you like to add?");
                    for (int i = 0; i < categoryFilePaths.Length; i++)
                    {
                        fileName = Path.GetFileNameWithoutExtension(categoryFilePaths[i]);
                        Console.WriteLine(" {0}: {1} workout", i + 1, fileName);
                    }
                    readResult = Console.ReadLine();
                    Console.WriteLine(readResult);
                    if (readResult != null)
                    {
                        // Get user's choice
                        userChoice = Convert.ToInt32(readResult);
                    }

                    // Check if user's choice is valid
                    if (userChoice > 0 && userChoice <= categoryFilePaths.Length)
                    {

                        // Load XML file based on user's choice
                        doc = new XmlDocument();
                        fileName = $"{Path.GetFileNameWithoutExtension(categoryFilePaths[userChoice - 1])}.xml";
                        filePath = Path.Combine(@"categoryFiles", fileName);
                        doc.Load(filePath);
                    }
                    Console.WriteLine("*Enter the workout name (e.g. 3km Jog): ");
                    readResult = Console.ReadLine();
                    // Get user's choice
                    name = readResult;
                    Console.WriteLine("*Pick the workout tag ");
                    Console.WriteLine(" 1. Beginner");
                    Console.WriteLine(" 2. intermediate");
                    Console.WriteLine(" 3. Pro");
                    readResult = Console.ReadLine();
                    tags = readResult;
                    if (readResult != null)
                    {
                        // Get user's choice

                        menuSelection = readResult.ToLower();
                        switch (tags)
                        {
                            case "1":
                                tags = "beginner";
                                break;
                            case "2":
                                tags = "intermediate";
                                break;
                            case "3":
                                tags = "pro";
                                break;
                            default:
                                break;
                        }
                    }

                    Console.WriteLine("*Enter the workout focus (e.g. shoulders): ");
                    Console.WriteLine("List of workout focuses:");
                    Console.WriteLine(" 1. Full body");
                    Console.WriteLine(" 2. Upper body");
                    Console.WriteLine(" 3. Lower body");
                    Console.WriteLine(" 4. Arms");
                    Console.WriteLine(" 5. Legs");
                    Console.WriteLine(" 6. Shoulders");
                    Console.WriteLine(" 7. Back");
                    Console.WriteLine(" 8. Abs");
                    Console.WriteLine(" 9. chest");
                    Console.WriteLine(" 10. Done selecting focus areas");
                    Console.WriteLine();
                    Console.WriteLine("Enter your selection number");
                    readResult = Console.ReadLine();
                    // Create list to store focus attribute values
                    List<string> focusAttributeValue2 = new List<string>();
                    // Process user's focus area choice using a switch statement
                    if (readResult != null)
                    {
                        bool selectingFocusAreas = true;
                        switch (readResult)
                        {
                            case "1":
                                focusAttributeValue2.Add("arms");
                                focusAttributeValue2.Add("full body");
                                focusAttributeValue2.Add("shoulders");
                                focusAttributeValue2.Add("back");
                                focusAttributeValue2.Add("chest");
                                focusAttributeValue2.Add("abs");
                                focusAttributeValue2.Add("legs");
                                selectingFocusAreas = false;
                                break;
                            case "2":
                                focusAttributeValue2.Add("arms");
                                focusAttributeValue2.Add("upper body");
                                focusAttributeValue2.Add("shoulders");
                                focusAttributeValue2.Add("back");
                                focusAttributeValue2.Add("chest");
                                focusAttributeValue2.Add("abs");
                                selectingFocusAreas = false;
                                break;
                            case "3":
                                focusAttributeValue2.Add("lower body");
                                focusAttributeValue2.Add("legs");
                                selectingFocusAreas = false;
                                break;
                            case "4":
                                focusAttributeValue2.Add("arms");
                                break;

                            case "5":
                                focusAttributeValue2.Add("legs");
                                break;

                            case "6":
                                focusAttributeValue2.Add("shoulders");
                                break;

                            case "7":
                                focusAttributeValue2.Add("back");
                                break;

                            case "8":
                                focusAttributeValue2.Add("abs");
                                break;

                            case "9":
                                focusAttributeValue2.Add("chest");
                                break;

                            case "10":
                                selectingFocusAreas = false;
                                break;
                            default:
                                break;
                        }
                        menuSelection = readResult.ToLower();
                    }


                    Console.WriteLine("*Enter the workout explanation: ");
                    readResult = Console.ReadLine();
                    // Get user's choice
                    explanation = readResult;


                    Console.WriteLine("*Enter the number of sets and reps (e.g. 1x1): ");
                    readResult = Console.ReadLine();
                    // Get user's choice
                    sets = readResult;

                    Console.WriteLine("*Enter the rest time in seconds (e.g. 180): ");
                    readResult = Console.ReadLine();
                    // Get user's choice
                    rest = readResult;

                    Console.WriteLine("*Enter the time it takes to complete workout: ");
                    readResult = Console.ReadLine();
                    // Get user's choice
                    time = readResult;


                    Console.WriteLine("Enter a link for the workout (e.g. https://www.example.com/workout): ");
                    readResult = Console.ReadLine();
                    // Get user's choice
                    link = readResult;

                    // Check if any input is null or empty
                    if (string.IsNullOrEmpty(tags) || focusAttributeValue2.Count == 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(explanation) || string.IsNullOrEmpty(sets) || string.IsNullOrEmpty(rest) || string.IsNullOrEmpty(time))
                    {
                        Console.WriteLine("Error: All *fields are required. Please enter values for all fields.*");
                    }
                    else
                    {
                        Console.WriteLine("Press enter to save or type anything to cancel");
                        readResult = Console.ReadLine();
                        if (readResult == "cancel")
                        {
                            break;
                        }
                        else if (readResult == "")
                        {
                            try
                            {
                                doc.Load(filePath);
                                // Create a new element for the workout
                                XmlElement workoutElement = doc.CreateElement("Workout");

                                // Set the attributes
                                workoutElement.SetAttribute("tags", tags);
                                // Convert the list of strings to a single string separated by commas
                                string focusString = string.Join(" ", focusAttributeValue2);
                                workoutElement.SetAttribute("focus", focusString);

                                // Create and append child elements
                                XmlElement nameElement = doc.CreateElement("name");
                                nameElement.InnerText = name;
                                workoutElement.AppendChild(nameElement);

                                XmlElement explanationElement = doc.CreateElement("explanation");
                                explanationElement.InnerText = explanation;
                                workoutElement.AppendChild(explanationElement);

                                XmlElement setsElement = doc.CreateElement("sets");
                                setsElement.InnerText = sets;
                                workoutElement.AppendChild(setsElement);

                                XmlElement restElement = doc.CreateElement("rest");
                                restElement.InnerText = rest;
                                workoutElement.AppendChild(restElement);

                                XmlElement timeElement = doc.CreateElement("time");
                                timeElement.InnerText = time;
                                workoutElement.AppendChild(timeElement);

                                XmlElement exampleLinkElement = doc.CreateElement("exampleLink");
                                exampleLinkElement.InnerText = link;
                                workoutElement.AppendChild(exampleLinkElement);

                                // Get the root element of the XML document
                                XmlElement workoutsElement = doc.DocumentElement;

                                // Append the new workout element to the workouts element
                                workoutsElement?.AppendChild(workoutElement);


                                // Save the updated XML document
                                doc.Save(filePath);
                                // Log success message
                                Console.WriteLine("XML file saved successfully.");
                            }
                            catch (Exception ex)
                            {
                                // Log error message if save operation failed
                                Console.WriteLine("Error occurred while saving XML file: " + ex.Message);
                            }
                        }
                    }
                    Console.WriteLine("\n\rPress the Enter key to continue");
                    readResult = Console.ReadLine();
                    Console.Clear();
                    break;

                // List/update Your Workouts (Under Construction)
                case "3":
                    Console.WriteLine("List or update a workouts");
                    Console.WriteLine(" 1. List workouts");
                    Console.WriteLine(" 2. Update workout");
                    readResult = Console.ReadLine();
                    if (readResult == null)
                    {
                        break;
                    }
                    else if (readResult == "1")
                    {
                        Console.WriteLine("Pick what you want to see");
                        Console.WriteLine(" 1. List all Workouts");
                        Console.WriteLine(" 2. List from category");
                        readResult = Console.ReadLine();
                        if (readResult == "1")
                        {
                            try
                            {
                                // check all xml files in the directory

                                foreach (string file in categoryFilePaths)
                                {
                                    // Load the XML document
                                    doc = new XmlDocument();
                                    doc.Load(file);

                                    // Get all the elements in the XML document
                                    XmlNodeList elements = doc.DocumentElement.SelectNodes("/workouts/Workout");
                                    if (elements == null)
                                    {
                                        Console.WriteLine("empty");
                                    }

                                    foreach (XmlNode element in elements)
                                    {
                                        // Extract workout attributes and elements
                                        tags = element.Attributes["tags"]?.Value;
                                        string focus = element.Attributes["focus"]?.Value;

                                        XmlNode nameElement = element.SelectSingleNode("name");
                                        name = nameElement?.InnerText;

                                        XmlNode explanationElement = element.SelectSingleNode("explanation");
                                        explanation = explanationElement?.InnerText;

                                        XmlNode setsElement = element.SelectSingleNode("sets");
                                        sets = setsElement?.InnerText;

                                        XmlNode restElement = element.SelectSingleNode("rest");
                                        rest = restElement?.InnerText;

                                        XmlNode timeElement = element.SelectSingleNode("time");
                                        time = timeElement?.InnerText;

                                        XmlNode linkElement = element.SelectSingleNode("exampleLink");
                                        link = linkElement?.InnerText;

                                        // Process and display the workout information
                                        Console.WriteLine("Workout:");
                                        Console.WriteLine("Tags: " + tags);
                                        Console.WriteLine("Focus: " + focus);
                                        Console.WriteLine("Name: " + name);
                                        Console.WriteLine("Explanation: " + explanation);
                                        Console.WriteLine("Sets: " + sets);
                                        Console.WriteLine("Rest: " + rest);
                                        Console.WriteLine("Time: " + time);
                                        Console.WriteLine("Link: " + link);
                                        Console.WriteLine();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error occurred while processing XML files: " + ex.Message);
                            }
                        }
                        else if (readResult == "2")
                        {
                            try
                            {
                                // check all xml files in the directory
                                Console.WriteLine("\n\r*Which type of workout would you like to list?");
                                for (int i = 0; i < categoryFilePaths.Length; i++)
                                {
                                    fileName = Path.GetFileNameWithoutExtension(categoryFilePaths[i]);
                                    Console.WriteLine(" {0}: {1} workout", i + 1, fileName);
                                }
                                readResult = Console.ReadLine();
                                if (readResult != null)
                                {
                                    // Get user's choice
                                    userChoice = Convert.ToInt32(readResult);
                                }
                                // Check if user's choice is valid
                                if (userChoice > 0 && userChoice <= categoryFilePaths.Length)
                                {

                                    // Load XML file based on user's choice
                                    doc = new XmlDocument();
                                    fileName = $"{Path.GetFileNameWithoutExtension(categoryFilePaths[userChoice - 1])}.xml";
                                    filePath = Path.Combine(@"categoryFiles", fileName);
                                    doc.Load(filePath);
                                    XmlNodeList elements = doc.DocumentElement.SelectNodes("/workouts/Workout");
                                    if (elements == null)
                                    {
                                        Console.WriteLine("empty");
                                    }

                                    foreach (XmlNode element in elements)
                                    {
                                        // Extract workout attributes and elements
                                        tags = element.Attributes["tags"]?.Value;
                                        string focus = element.Attributes["focus"]?.Value;

                                        XmlNode nameElement = element.SelectSingleNode("name");
                                        name = nameElement?.InnerText;

                                        XmlNode explanationElement = element.SelectSingleNode("explanation");
                                        explanation = explanationElement?.InnerText;

                                        XmlNode setsElement = element.SelectSingleNode("sets");
                                        sets = setsElement?.InnerText;

                                        XmlNode restElement = element.SelectSingleNode("rest");
                                        rest = restElement?.InnerText;

                                        XmlNode timeElement = element.SelectSingleNode("time");
                                        time = timeElement?.InnerText;

                                        XmlNode linkElement = element.SelectSingleNode("exampleLink");
                                        link = linkElement?.InnerText;

                                        // Process and display the workout information
                                        Console.WriteLine("Workout:");
                                        Console.WriteLine("Tags: " + tags);
                                        Console.WriteLine("Focus: " + focus);
                                        Console.WriteLine("Name: " + name);
                                        Console.WriteLine("Explanation: " + explanation);
                                        Console.WriteLine("Sets: " + sets);
                                        Console.WriteLine("Rest: " + rest);
                                        Console.WriteLine("Time: " + time);
                                        Console.WriteLine("Link: " + link);
                                        Console.WriteLine();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error occurred while processing XML files: " + ex.Message);
                            }
                            Console.ReadLine();
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (readResult == "2")
                    {
                        try
                        {
                            // check all xml files in the directory
                            Console.WriteLine("\n\r*Which type of workout would you like to list?");
                            for (int i = 0; i < categoryFilePaths.Length; i++)
                            {
                                fileName = Path.GetFileNameWithoutExtension(categoryFilePaths[i]);
                                Console.WriteLine(" {0}: {1} workout", i + 1, fileName);
                            }
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                // Get user's choice
                                userChoice = Convert.ToInt32(readResult);
                            }
                            // Check if user's choice is valid
                            if (userChoice > 0 && userChoice <= categoryFilePaths.Length)
                            {

                                // Load XML file based on user's choice
                                doc = new XmlDocument();
                                fileName = $"{Path.GetFileNameWithoutExtension(categoryFilePaths[userChoice - 1])}.xml";
                                filePath = Path.Combine(@"categoryFiles", fileName);
                                doc.Load(filePath);
                                XmlNodeList elements = doc.DocumentElement.SelectNodes("/workouts/Workout");
                                if (elements == null)
                                {
                                    Console.WriteLine("empty");
                                }

                                foreach (XmlNode element in elements)
                                {
                                    // Extract workout attributes and elements
                                    tags = element.Attributes["tags"]?.Value;
                                    string focus = element.Attributes["focus"]?.Value;

                                    XmlNode nameElement = element.SelectSingleNode("name");
                                    name = nameElement?.InnerText;

                                    XmlNode explanationElement = element.SelectSingleNode("explanation");
                                    explanation = explanationElement?.InnerText;

                                    XmlNode setsElement = element.SelectSingleNode("sets");
                                    sets = setsElement?.InnerText;

                                    XmlNode restElement = element.SelectSingleNode("rest");
                                    rest = restElement?.InnerText;

                                    XmlNode timeElement = element.SelectSingleNode("time");
                                    time = timeElement?.InnerText;

                                    XmlNode linkElement = element.SelectSingleNode("exampleLink");
                                    link = linkElement?.InnerText;

                                    // Process and display the workout information
                                    Console.WriteLine("Workout:");
                                    Console.WriteLine("Tags: " + tags);
                                    Console.WriteLine("Focus: " + focus);
                                    Console.WriteLine("Name: " + name);
                                    Console.WriteLine("Explanation: " + explanation);
                                    Console.WriteLine("Sets: " + sets);
                                    Console.WriteLine("Rest: " + rest);
                                    Console.WriteLine("Time: " + time);
                                    Console.WriteLine("Link: " + link);
                                    Console.WriteLine();
                                }
                                // Ask the user to pick a workout by name
                                Console.WriteLine("Which workout would you like to update?");
                                string workoutName = Console.ReadLine();

                                // Find the workout node by name
                                XmlNode workoutNode = doc.DocumentElement.SelectSingleNode($"/workouts/Workout[name='{workoutName}']");

                                // Check if the workout node exists
                                if (workoutNode != null)
                                {
                                    // Ask the user to pick an info field to update
                                    Console.WriteLine("Which info field would you like to update?");
                                    Console.WriteLine("1: Tags");
                                    Console.WriteLine("2: Focus");
                                    Console.WriteLine("3: Name");
                                    Console.WriteLine("4: Explanation");
                                    Console.WriteLine("5: Sets");
                                    Console.WriteLine("6: Rest");
                                    Console.WriteLine("7: Time");
                                    Console.WriteLine("8: Link");
                                    readResult = Console.ReadLine();
                                    if (readResult != null)
                                    {
                                        // Get user's choice
                                        userChoice = Convert.ToInt32(readResult);
                                    }
                                    // Check if user's choice is valid
                                    if (userChoice > 0 && userChoice <= 8)
                                    {
                                        // Get the name of the info field based on user's choice
                                        string infoField = "";
                                        switch (userChoice)
                                        {
                                            case 1:
                                                infoField = "tags";
                                                break;
                                            case 2:
                                                infoField = "focus";
                                                break;
                                            case 3:
                                                infoField = "name";
                                                break;
                                            case 4:
                                                infoField = "explanation";
                                                break;
                                            case 5:
                                                infoField = "sets";
                                                break;
                                            case 6:
                                                infoField = "rest";
                                                break;
                                            case 7:
                                                infoField = "time";
                                                break;
                                            case 8:
                                                infoField = "link";
                                                break;
                                        }
                                        // Ask the user to enter the new value for the info field
                                        Console.WriteLine($"What is the new value for {infoField}?");
                                        string newValue = "";
                                        if (infoField == "tags")
                                        {
                                            // Ask the user to pick a tag
                                            Console.WriteLine("1: Beginner");
                                            Console.WriteLine("2: Intermediate");
                                            Console.WriteLine("3: Pro");
                                            readResult = Console.ReadLine();
                                            if (readResult != null)
                                            {
                                                // Get user's choice
                                                userChoice = Convert.ToInt32(readResult);
                                            }
                                            // Check if user's choice is valid
                                            if (userChoice > 0 && userChoice <= 3)
                                            {
                                                // Get the name of the tag based on user's choice
                                                string tag = "";
                                                switch (userChoice)
                                                {
                                                    case 1:
                                                        tag = "Beginner";
                                                        break;
                                                    case 2:
                                                        tag = "Intermediate";
                                                        break;
                                                    case 3:
                                                        tag = "Pro";
                                                        break;
                                                }
                                                // Set infoField to the chosen tag
                                                newValue = tag;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid choice.");
                                            }
                                        }
                                        else if (infoField == "focus")
                                        {
                                            // Ask the user to pick one or more focuses
                                            Console.WriteLine("Which focus(es) would you like to choose?");
                                            Console.WriteLine("1: Full body");
                                            Console.WriteLine("2: Upper body");
                                            Console.WriteLine("3: Lower body");
                                            Console.WriteLine("4: Arms");
                                            Console.WriteLine("5: Legs");
                                            Console.WriteLine("6: Shoulders");
                                            Console.WriteLine("7: Back");
                                            Console.WriteLine("8: Abs");
                                            Console.WriteLine("9: Chest");
                                            Console.WriteLine("Enter your choices separated by commas (e.g. 1,3,5)");
                                            readResult = Console.ReadLine();
                                            int[] userChoices = { };
                                            if (readResult != null)
                                            {
                                                // Get user's choices as an array of integers
                                                userChoices = Array.ConvertAll(readResult.Split(','), int.Parse);
                                            }
                                            // Check if user's choices are valid
                                            if (userChoices.All(x => x > 0 && x <= 9))
                                            {
                                                // Get the names of the focuses based on user's choices
                                                List<string> focuses = new List<string>();
                                                foreach (int choice in userChoices)
                                                {
                                                    switch (choice)
                                                    {
                                                        case 1:
                                                            focuses.Add("Full body");
                                                            break;
                                                        case 2:
                                                            focuses.Add("Upper body");
                                                            break;
                                                        case 3:
                                                            focuses.Add("Lower body");
                                                            break;
                                                        case 4:
                                                            focuses.Add("Arms");
                                                            break;
                                                        case 5:
                                                            focuses.Add("Legs");
                                                            break;
                                                        case 6:
                                                            focuses.Add("Shoulders");
                                                            break;
                                                        case 7:
                                                            focuses.Add("Back");
                                                            break;
                                                        case 8:
                                                            focuses.Add("Abs");
                                                            break;
                                                        case 9:
                                                            focuses.Add("Chest");
                                                            break;
                                                    }
                                                }
                                                // Set newValue to the chosen focuses
                                                // If Upper body is chosen, set newValue to every option
                                                if (focuses.Contains("Full body"))
                                                {
                                                    newValue = "Full body, Upper body, Lower body, Arms, Legs, Shoulders, Back, Abs, Chest";
                                                }
                                                else if (focuses.Contains("Upper body"))
                                                {
                                                    newValue = "Upper body, Arms, Shoulders, Back, Abs, Chest";
                                                }
                                                // If Lower body is chosen, set newValue to Legs and Lower body
                                                else if (focuses.Contains("Lower body"))
                                                {
                                                    newValue = "Legs, Lower body";
                                                }
                                                // Otherwise, set newValue to the chosen focuses separated by commas
                                                else
                                                {
                                                    newValue = string.Join(", ", focuses);
                                                }
                                                // Console log the chosen focuses and newValue
                                                Console.WriteLine($"You have chosen {string.Join(", ", focuses)} as your focus(es).");
                                                Console.WriteLine($"newValue is now {newValue}.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid choice(s).");
                                            }
                                        }
                                        else { newValue = Console.ReadLine(); }
                                        // Check if the info field is an attribute or an element
                                        if (infoField == "tags" || infoField == "focus")
                                        {
                                            // Update the attribute value
                                            workoutNode.Attributes[infoField].Value = newValue;
                                        }
                                        else
                                        {
                                            // Update the element value
                                            workoutNode.SelectSingleNode(infoField).InnerText = newValue;
                                        }
                                        // Save the changes to the XML file
                                        doc.Save(filePath);
                                        Console.WriteLine($"The {infoField} of {workoutName} has been updated to {newValue}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid choice.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No such workout found.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error occurred while processing XML files: " + ex.Message);
                        }
                        Console.ReadLine();

                    }
                    break;

                // Create New Category (Under Construction)
                case "4":
                    Console.WriteLine("Type the name of the new Category");
                    readResult = Console.ReadLine();
                    try
                    {
                        fileName = $"{readResult}.xml";
                        string categoryFilePath = @"categoryFiles";
                        filePath = Path.Combine(categoryFilePath, fileName);
                        doc = new XmlDocument();
                        XmlNode rootNode = doc.CreateElement("workouts");
                        doc.AppendChild(rootNode);
                        doc.Save(filePath);
                        Console.WriteLine("Saved Successfully-- Restart application to see changes");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error occurred while processing XML files: " + ex.Message);
                        throw;
                    }

                    break;

                // Delete a Workout or Category (Under Construction)
                case "5":
                    Console.WriteLine("What do you want to delete.");
                    Console.WriteLine(" 1. Delete Workout.");
                    Console.WriteLine(" 2. Delete Category.");
                    readResult = Console.ReadLine();
                    if (readResult == "1")
                    {
                        XmlNodeList? elements = null;
                        try
                        {
                            // check all xml files in the directory
                            Console.WriteLine("\n\r*Which type of workout would you like to delete?");
                            for (int i = 0; i < categoryFilePaths.Length; i++)
                            {
                                fileName = Path.GetFileNameWithoutExtension(categoryFilePaths[i]);
                                Console.WriteLine(" {0}: {1} workout", i + 1, fileName);
                            }
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                // Get user's choice
                                userChoice = Convert.ToInt32(readResult);
                            }
                            // Check if user's choice is valid
                            if (userChoice > 0 && userChoice <= categoryFilePaths.Length)
                            {

                                // Load XML file based on user's choice
                                doc = new XmlDocument();
                                fileName = $"{Path.GetFileNameWithoutExtension(categoryFilePaths[userChoice - 1])}.xml";
                                filePath = Path.Combine(@"categoryFiles", fileName);
                                doc.Load(filePath);
                                elements = doc.DocumentElement.SelectNodes("/workouts/Workout");
                                if (elements == null)
                                {
                                    Console.WriteLine("empty");
                                }
                                Console.WriteLine("\n\r*Which workouts would you like to delete? (Enter numbers separated by commas)");
                                for (int i = 0; i < elements.Count; i++)
                                {
                                    Console.WriteLine(" {0}: {1}", i + 1, elements[i].InnerText);
                                }
                            }

                            // Get user's choices
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                // Split user's choices into an array of integers
                                int[] userChoices = readResult.Split(',').Select(int.Parse).ToArray();

                                // Delete selected elements
                                foreach (int choice in userChoices)
                                {
                                    if (choice > 0 && choice <= elements.Count)
                                    {
                                        XmlNode nodeToDelete = elements[choice - 1];
                                        nodeToDelete.ParentNode.RemoveChild(nodeToDelete);
                                    }
                                }
                                Console.WriteLine("Press enter to save or press any key to cancel");
                                readResult = Console.ReadLine();
                                if (readResult == null)
                                {
                                    // Save changes to XML file
                                    doc.Save(filePath);
                                }
                                else
                                {
                                    Console.WriteLine("Canceled");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error occurred while processing XML files: " + ex.Message);
                        }
                        Console.ReadLine();
                    }
                    else if (readResult == "2")
                    {
                        try
                        {
                            // check all xml files in the directory
                            Console.WriteLine("\n\r*Which category would you like to delete?");
                            for (int i = 0; i < categoryFilePaths.Length; i++)
                            {
                                fileName = Path.GetFileNameWithoutExtension(categoryFilePaths[i]);
                                Console.WriteLine(" {0}: {1} workout", i + 1, fileName);
                            }
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                // Get user's choice
                                userChoice = Convert.ToInt32(readResult);
                            }
                            // Check if user's choice is valid
                            if (userChoice > 0 && userChoice <= categoryFilePaths.Length)
                            {
                                filePath = categoryFilePaths[userChoice - 1];
                                // Delete the file if it exists
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                    Console.WriteLine("The file {0} has been deleted.", filePath);
                                }
                                else
                                {
                                    Console.WriteLine("The file {0} does not exist.", filePath);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error occurred while processing XML files: " + ex.Message);
                        }
                    }
                    else { break; }
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
            break;
        case "2":
            // Get file names in category folder
            string folderName = "";
            string[] sportCategoryFolderDirectorys = Directory.GetDirectories(@"proFiles");
            // Prompt user to pick a workout category
            Console.WriteLine("Pick what kind of workout you want to have.");
            for (int i = 0; i < sportCategoryFolderDirectorys.Length; i++)
            {
                folderName = Path.GetFileName(sportCategoryFolderDirectorys[i]);
                Console.WriteLine(" {0}: {1} workouts", i + 1, folderName);
            }
            // Get user's choice
            userChoice = Convert.ToInt32(Console.ReadLine());
            // Check if user's choice is valid
            if (userChoice > 0 && userChoice <= sportCategoryFolderDirectorys.Length)
            {
                // Get the path of the chosen subdirectory
                string chosenSubdirectoryPath = sportCategoryFolderDirectorys[userChoice - 1];
                // Get the names of subdirectories within the chosen subdirectory
                string[] subSubdirectories = Directory.GetDirectories(chosenSubdirectoryPath);
                // Prompt user to pick a subcategory
                Console.WriteLine("Pick a subcategory:");
                for (int i = 0; i < subSubdirectories.Length; i++)
                {
                    string subSubdirectoryName = Path.GetFileName(subSubdirectories[i]);
                    Console.WriteLine(" {0}: {1}", i + 1, subSubdirectoryName);
                }
                // Get user's choice
                int subcategoryChoice = Convert.ToInt32(Console.ReadLine());
                // Check if user's choice is valid
                if (subcategoryChoice > 0 && subcategoryChoice <= subSubdirectories.Length)
                {
                    // Get the path of the chosen subsubcategory
                    string chosenSubSubdirectoryPath = subSubdirectories[subcategoryChoice - 1];
                    // Get the names of XML files within the chosen subsubcategory
                    string[] xmlFiles = Directory.GetFiles(chosenSubSubdirectoryPath, "*.xml");
                    // Prompt user to pick an XML file
                    Console.WriteLine("Pick an XML file:");
                    for (int i = 0; i < xmlFiles.Length; i++)
                    {
                        string xmlFileName = Path.GetFileNameWithoutExtension(xmlFiles[i]);
                        Console.WriteLine(" {0}: {1}", i + 1, xmlFileName);
                    }
                    // Get user's choice
                    int xmlFileChoice = Convert.ToInt32(Console.ReadLine());
                    // Check if user's choice is valid
                    if (xmlFileChoice > 0 && xmlFileChoice <= xmlFiles.Length)
                    {
                        // Get the path of the chosen XML file
                        string chosenXmlFilePath = xmlFiles[xmlFileChoice - 1];
                        // Load the chosen XML file
                        doc = new XmlDocument();
                        doc.Load(chosenXmlFilePath);
                        // Get all Workout nodes
                        XmlNodeList workoutNodes = doc.DocumentElement.SelectNodes("/workouts/Workout");

                        // Display workouts and their information
                        foreach (XmlNode workoutNode in workoutNodes)
                        {
                            string category = workoutNode.Attributes["category"].Value;
                            name = workoutNode.SelectSingleNode("name").InnerText;
                            explanation = workoutNode.SelectSingleNode("explanation").InnerText;
                            sets = workoutNode.SelectSingleNode("sets").InnerText;
                            rest = workoutNode.SelectSingleNode("rest").InnerText;
                            string exampleLink = workoutNode.SelectSingleNode("exampleLink").InnerText;

                            Console.WriteLine("Workout: {0}", name);
                            Console.WriteLine("  Category: {0}", category);
                            Console.WriteLine("  Explanation: {0}", explanation);
                            Console.WriteLine("  Sets: {0}", sets);
                            Console.WriteLine("  Rest: {0}", rest);
                            Console.WriteLine("  Example link: {0}", exampleLink);
                        }
                    }
                }
            }
            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();
            break;
        default:
            break;
    }
} while (menuSelection != "exit");