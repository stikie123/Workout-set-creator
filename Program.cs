using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;

string? readResult;
string menuSelection = "";
int userChoice = 0;

// preferences doc
XmlDocument preferenceDoc = new XmlDocument();
preferenceDoc.Load(@"myPreferences.xml");
XmlNodeList preferenceNodes = preferenceDoc.DocumentElement.SelectNodes("/root/preferences");
// list of nodes
List<XmlNode> nodeList = new List<XmlNode>();

do
{
    Console.WriteLine("Welcome to the My workout generator app. Your main menu options are:");
    Console.WriteLine("1. Your workouts");
    Console.WriteLine("2. Professional workouts");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
    readResult = Console.ReadLine();
    switch (readResult)
    {
        case "1":
            Console.Clear();
            Console.WriteLine(" 1. Quick workout");
            Console.WriteLine(" 2. Add New workout(under construction)");
            Console.WriteLine(" 3. List your workouts(under construction)");
            Console.WriteLine(" 4. Create new category(Under construction)");
            Console.WriteLine(" 5. Delete a workout or category(under construction)");
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
                //Quick workouts 
                case "1":
                    // Get file names in category folder
                    string fileName = "";
                    string[] categoryFilePaths = Directory.GetFiles(@"categoryFiles", "*.xml");
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
                        XmlDocument Doc = new XmlDocument();
                        string fileName1 = $"{Path.GetFileNameWithoutExtension(categoryFilePaths[userChoice - 1])}.xml";
                        string filePath = Path.Combine(@"categoryFiles", fileName1);
                        Doc.Load(filePath);
                        // Get all Workout nodes from XML file
                        XmlNodeList Nodes = Doc.DocumentElement.SelectNodes("/workouts/Workout");
                        // Prompt user to choose focus area

                        bool selectingFocusAreas = true;
                        while (selectingFocusAreas)
                        {
                            string? readResult1;
                            string menuSelection1 = "";
                            Console.WriteLine("What would you like to focus on");
                            Console.WriteLine(" 1. Full body");
                            Console.WriteLine(" 2. Upper body");
                            Console.WriteLine(" 3. Lower body");
                            Console.WriteLine(" 4. Arms");
                            Console.WriteLine(" 5. Legs");
                            Console.WriteLine(" 6. Shoulders");
                            Console.WriteLine(" 7. Back");
                            Console.WriteLine(" 8. Abs");
                            Console.WriteLine(" 9. Pecs");
                            Console.WriteLine(" 10. Done selecting focus areas");
                            Console.WriteLine();
                            Console.WriteLine("Enter your selection number");
                            // Get user's focus area choice
                            readResult1 = Console.ReadLine();
                            if (readResult1 != null)
                            {
                                menuSelection1 = readResult1.ToLower();
                            }
                            // Process user's focus area choice using a switch statement
                            switch (menuSelection1)
                            {
                                case "1":
                                    focusAttributeValue.Add("arms");
                                    focusAttributeValue.Add("full body");
                                    focusAttributeValue.Add("shoulders");
                                    focusAttributeValue.Add("back");
                                    focusAttributeValue.Add("pecs");
                                    focusAttributeValue.Add("abs");
                                    focusAttributeValue.Add("legs");
                                    break;
                                case "2":
                                    focusAttributeValue.Add("arms");
                                    focusAttributeValue.Add("upper body");
                                    focusAttributeValue.Add("shoulders");
                                    focusAttributeValue.Add("back");
                                    focusAttributeValue.Add("pecs");
                                    focusAttributeValue.Add("abs");
                                    break;
                                case "3":
                                    focusAttributeValue.Add("lower body");
                                    focusAttributeValue.Add("legs");
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
                                    focusAttributeValue.Add("pecs");
                                    break;

                                case "10":
                                    selectingFocusAreas = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        // asks user 
                        string tagsAttributeName1 = "tags";
                        Console.WriteLine("Enter your skill level:");
                        Console.WriteLine(" 1. Beginner");
                        Console.WriteLine(" 2. Intermediate");
                        Console.WriteLine(" 3. Pro");
                        string tagsAttributeValue1 = Console.ReadLine();
                        switch (tagsAttributeValue1)
                        {
                            case "1":
                                tagsAttributeValue1 = "beginner";
                                break;

                            case "2":
                                tagsAttributeValue1 = "intermediate";
                                break;
                            case "3":
                                tagsAttributeValue1 = "pro";
                                break;
                            default:
                                break;
                        }
                        var totalTime = 0;

                        // Add nodes that match req to nodeList
                        foreach (XmlNode node in Nodes)
                        {
                            if (focusAttributeValue.Any(focus => node.Attributes["focus"].Value.Contains(focus)) && tagsAttributeValue1 == node.Attributes["tags"].InnerText)
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
                        if (tagsAttributeValue1 == "beginner")
                        {
                            exerciseTime = 1800;
                        }
                        else if (tagsAttributeValue1 == "intermediate")
                        {
                            exerciseTime = 3600;
                        }
                        else if (tagsAttributeValue1 == "pro")
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
                case "4":
                    string tags = "";
                    string focus = "";
                    string name = "";
                    string explanation = "";
                    string sets = "";
                    string rest = "";
                    string time = "";
                    string link = "";
                    string didCancel = "";
                    //checks if you want to cancel
                    do
                    {
                        Console.WriteLine("\n\r*Which type of workout would you like to add?");
                        Console.WriteLine(" 1. Calisthenics");
                        Console.WriteLine(" 2. Weight");
                        Console.WriteLine(" 3. Cardio");
                        string fileSelection = Console.ReadLine();
                        if (fileSelection == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }
                        string selectedFile = "";
                        string category = "";
                        switch (fileSelection)
                        {
                            case "1":
                                selectedFile = @"calData.xml";
                                category = "calisthenics";
                                break;

                            case "2":
                                selectedFile = @"weightData.xml";
                                category = "weight";
                                break;
                            case "3":
                                selectedFile = @"carData.xml";
                                category = "cardio";
                                break;
                            default:
                                break;
                        }
                        //adddd cases
                        Console.WriteLine("*Enter the workout tags (e.g. beginner): ");
                        tags = Console.ReadLine();
                        if (tags == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }
                        // adddd cases
                        Console.WriteLine("*Enter the workout focus (e.g. shoulders): ");
                        focus = Console.ReadLine();
                        if (focus == "cancel")
                        {
                            didCancel = "true";
                        }

                        Console.WriteLine("*Enter the workout name (e.g. 3km Jog): ");
                        name = Console.ReadLine();
                        if (name == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }

                        Console.WriteLine("*Enter the workout explanation: ");
                        explanation = Console.ReadLine();
                        if (explanation == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }

                        Console.WriteLine("*Enter the number of sets and reps (e.g. 1x1): ");
                        sets = Console.ReadLine();
                        if (sets == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }

                        Console.WriteLine("*Enter the rest time in seconds (e.g. 180): ");
                        rest = Console.ReadLine();
                        if (rest == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }

                        Console.WriteLine("*Enter the workout time: ");
                        time = Console.ReadLine();
                        if (time == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }

                        Console.WriteLine("Enter a link for the workout (e.g. https://www.example.com/workout): ");
                        link = Console.ReadLine();
                        if (link == "cancel")
                        {
                            didCancel = "true";
                            break;
                        }

                        // Check if any input is null or empty
                        if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(tags) || string.IsNullOrEmpty(focus) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(explanation) || string.IsNullOrEmpty(sets) || string.IsNullOrEmpty(rest) || string.IsNullOrEmpty(time))
                        {
                            Console.WriteLine("Error: All fields are required. Please enter values for all fields.*");
                        }
                        else
                        {


                            XDocument doc5 = XDocument.Load(selectedFile);

                            // Create a new Workout element
                            XElement workout = new XElement("Workout",
                                new XAttribute("category", category),
                                new XAttribute("tags", tags),
                                new XAttribute("focus", focus),
                                new XElement("name", name),
                                new XElement("explanation", explanation),
                                new XElement("sets", sets),
                                new XElement("rest", rest),
                                new XElement("time", time),
                                new XElement("link", link)
                            );

                            // Add the new Workout element to the workouts element
                            doc5.Element("workouts").Add(workout);

                            // Save the updated XML document
                            doc5.Save(selectedFile);
                        }
                        Console.WriteLine("\n\rPress the Enter key to continue");
                        readResult = Console.ReadLine();
                    } while (didCancel != "true");
                    break;

                default:
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
                        XmlDocument doc = new XmlDocument();
                        doc.Load(chosenXmlFilePath);
                        // Get all Workout nodes
                        XmlNodeList workoutNodes = doc.DocumentElement.SelectNodes("/workouts/Workout");

                        // Display workouts and their information
                        foreach (XmlNode workoutNode in workoutNodes)
                        {
                            string category = workoutNode.Attributes["category"].Value;
                            string name = workoutNode.SelectSingleNode("name").InnerText;
                            string explanation = workoutNode.SelectSingleNode("explanation").InnerText;
                            string sets = workoutNode.SelectSingleNode("sets").InnerText;
                            string rest = workoutNode.SelectSingleNode("rest").InnerText;
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