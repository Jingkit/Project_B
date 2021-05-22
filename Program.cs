using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string theUser = ""; // User is NIET ingelogd
            bool stopProgram = false;
            while (stopProgram == false)
            {
                Console.WriteLine("1. Log in/Register account\n2. Reserve table/Cancel Reservation\n3. Menu and dishes\n4. Account\n5. Admin\n6. Information about the Restaurant\n7. Exit\n");
                string x = Console.ReadLine();
                if (x == "1")
                {
                    Console.WriteLine("Press a numberkey to choose option");
                    Console.WriteLine("1: Login");
                    Console.WriteLine("2: Register new account");
                    Console.WriteLine("0: Back");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        var userTuple = inputUsernamePassword();
                        while (!Login(userTuple))
                        {
                            Console.WriteLine("Enter r to rety or q to quit");
                            string quit = Console.ReadLine();
                            if (quit == "r")
                            {
                                userTuple = inputUsernamePassword();
                            }
                            if (quit == "q")
                            {
                                break;
                            }
                        }
                        if (Login(userTuple))
                        {
                            Console.WriteLine("Login succesful\n");
                            theUser = userTuple.Item1;
                            continue;
                        }
                        else
                        {
                            theUser = "";
                        }

                    }
                    if (choice == "2")
                    {
                        var userTuple = inputUsernamePassword();
                        Register(userTuple);
                        continue;
                    }
                    if (choice == "0")
                    {
                        continue;
                    }
                } // Login
                if (x == "3") // Current Menu
                {
                    CurrentMenuPage();
                    continue;
                } // Menu
                if (x == "6") // Info
                {
                    Info();
                } // Information about the restaurant
                if (x == "4") // Account 
                {
                    if (theUser == "")
                    {
                        Console.WriteLine("You are not logged in\n");
                    }
                    else
                    {
                        Console.WriteLine("1. Add/edit personal information\n2. Back\n");
                        string y = Console.ReadLine();
                        if (y == "1")
                        {
                            Console.Write("First Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Last Name: ");
                            string surname = Console.ReadLine();
                            Console.Write("Email: ");
                            string mail = Console.ReadLine();
                            Console.Write("Phone Number: ");
                            string number = Console.ReadLine();

                            json createFile = new json()
                            {
                                firstname = name,
                                lastname = surname,
                                email = mail,
                                phone = number
                            };
                            string text = JsonConvert.SerializeObject(createFile);
                            File.WriteAllText($"{theUser}_info.json", text);
                            Console.WriteLine("Information saved succesfully\n");
                        }

                        if (y == "2")
                        {
                            continue;
                        }

                    }

                } // Account
                if (x == "7")
                {
                    stopProgram = true;
                } // Exit
                if (x == "2")
                {

                    Console.WriteLine("1. Reserve Table\n2. Cancel Reservation");
                    var option = Console.ReadLine();
                    if(option == "1")
                    {
                        if (theUser != "") // User is WEL ingelogd
                        {                      
                            string info;
                            bool userNoInfo = true;
                            try
                            {
                                info = File.ReadAllText($"{theUser}_info.json");
                                userNoInfo = false;
                            }
                            catch (FileNotFoundException)
                            {
                                userNoInfo = true;
                            }

                            if (userNoInfo == false)
                            {
                                Console.Write("\nConfirm Personal Information\n\n");
                                dynamic infoDict = JObject.Parse(File.ReadAllText($"{theUser}_info.json"));
                                string firstname = infoDict.firstname;
                                string surName = infoDict.lastname;
                                string mail = infoDict.email;
                                string phone = infoDict.phone;
                                UserInfo reserveInfo = new UserInfo(firstname, surName, mail, phone);
                                Console.WriteLine($"First name: \t{reserveInfo.firstname}\nLast name: \t{reserveInfo.lastname}\nEmail: \t\t{reserveInfo.email}\nPhone number: \t{reserveInfo.phone}\n");
                                Console.WriteLine("1. Confirm\n2. Cancel\n");
                                string input = Console.ReadLine();
                                if (input == "1")
                                {                            
                                    ReserveTable(reserveInfo.firstname);
                                    SentEmail(reserveInfo);
                                }
                                if (input == "2")
                                {
                                    continue;
                                }
                            }
                            if (userNoInfo == true)
                            {
                                Console.WriteLine("\nEnter Personal Information\n\n");
                                Console.Write("First name:\t");
                                string firstname = Console.ReadLine();
                                Console.Write("Last name:\t");
                                string lastname = Console.ReadLine();
                                Console.Write("Email:\t\t");
                                string email = Console.ReadLine();
                                Console.Write("Phone number:\t");
                                string phoneNumber = Console.ReadLine();
                                Console.WriteLine("\n\n");
                                UserInfo reserveInfo = new UserInfo(firstname, lastname, email, phoneNumber);
                                Console.WriteLine($"First name: \t{reserveInfo.firstname}\nLast name: \t{reserveInfo.lastname}\nEmail: \t\t{reserveInfo.email}\nPhone number: \t{reserveInfo.phone}\n");
                                Console.WriteLine("1. Confirm\n2. Cancel\n");
                                string input = Console.ReadLine();
                                if (input == "1")
                                {                            
                                    ReserveTable(reserveInfo.firstname);
                                    SentEmail(reserveInfo);
                                }
                                if (input == "2")
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nEnter Personal Information\n\n");
                            Console.Write("First name:\t");
                            string firstname = Console.ReadLine();
                            Console.Write("Last name:\t");
                            string lastname = Console.ReadLine();
                            Console.Write("Email:\t\t");
                            string email = Console.ReadLine();
                            Console.Write("Phone number:\t");
                            string phoneNumber = Console.ReadLine();
                            Console.WriteLine("\n\n");
                            UserInfo reserveInfo = new UserInfo(firstname, lastname, email, phoneNumber);
                            Console.WriteLine($"First name: \t{reserveInfo.firstname}\nLast name: \t{reserveInfo.lastname}\nEmail: \t\t{reserveInfo.email}\nPhone number: \t{reserveInfo.phone}\n");
                            Console.WriteLine("1. Confirm\n2. Cancel\n");
                            string input = Console.ReadLine();
                            if (input == "1")
                            {
                                ReserveTable(reserveInfo.firstname);
                                SentEmail(reserveInfo);
                            }
                            if (input == "2")
                            {
                                continue;
                            }
                    }

                    }
                    else if(option == "2")
                    {
                        string holder = "";
                        ReservationData tableData = null;
                        Console.WriteLine("Enter the information of your current reservation\n1. This week\n2. Next week");
                        string week = Console.ReadLine();

                        if (week == "1") // Week 1
                        {
                            holder = File.ReadAllText("This Week Table Info.json");
                            tableData = JsonConvert.DeserializeObject<ReservationData>(holder);

                        }
                        else if (week == "2") // Week 2
                        {
                            holder = File.ReadAllText("Next Week Table Info.json");
                            tableData = JsonConvert.DeserializeObject<ReservationData>(holder);
                        }

                        Console.WriteLine("Enter the day of your reservation\n1. Tuesday\n2. Wednesday\n3. Thursday\n4. Friday\n5. Saturday\n6. Sunday");
                        var day = Console.ReadLine();
                        Console.WriteLine("Enter the time of your reservation\n1. 16:00\n2. 18:00\n3. 20:00");
                        var time = Console.ReadLine();

                        var reservationDay = chosenDayTime(day, time, tableData); 
                        var tableAvailability = SetupTable(reservationDay); 

                        Console.Write("Enter the name you have reserved the table on: ");
                        var firstname = Console.ReadLine();
                        string table = null;
                        bool changeTable = false;
                        foreach (var value in tableAvailability)
                        {
                            if(value.Value == firstname)
                            {
                                table = value.Key;
                                changeTable = true;
                            }
                        }
                        if (changeTable)
                        {
                            tableAvailability[table] = "";
                            Console.WriteLine("Reservation cancelled successfully");
                        }
                        else
                        {
                            Console.WriteLine($"No table reserved on {firstname}");
                        }

                        //saves  the updated file
                        EditTableData(day, time, tableAvailability, tableData); // Edits the ReservationData class
                        var file = JsonConvert.SerializeObject(tableData, Formatting.Indented); // Converst ReservationData class into json
                        if (week == "1")
                        {
                            File.WriteAllText("This Week Table Info.json", file);
                        }
                        else if (week == "2")
                        {
                            File.WriteAllText("Next Week Table Info.json", file);
                        }
                    }
                } // Reserve table/Cancel
                if (x == "5")
                {
                    Console.Write("Enter Password:\t");
                    var password = Console.ReadLine();
                    if(password == "Admin")
                    {
                        Console.WriteLine("1. Change week\n2. Add dishes\n3. Back");
                        var input = Console.ReadLine();
                        if (input == "1")
                        {
                            var thisweek = File.ReadAllText("Next Week Table Info.json");
                            File.WriteAllText("This Week Table Info.json", thisweek);
                            var nextweek = File.ReadAllText("Default Table Info.json");
                            File.WriteAllText("Next Week Table Info.json", nextweek);
                        }
                        else if(input == "2")
                        {
                            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        }
                        else if(input == "3")
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong password");
                    }
                } // Admin
            }


        }
        static void CurrentMenuPage()
        {
            var json = File.ReadAllText("details.json");
            dynamic stuff = JsonConvert.DeserializeObject(json);
            foreach (var s in stuff)
            {
                Console.WriteLine(s.Number + s.Dot + s.Name);
            }
            Console.WriteLine("Press s for the option to view dish details or to sort dishes");
            string choise = Console.ReadLine();
            if (choise == "s")
            {
                SortingMenu();
            }

        }
        static void SortingMenu()
        {
            var json = File.ReadAllText("details.json");
            dynamic stuff = JsonConvert.DeserializeObject(json);

            Console.WriteLine("\nPlease press the number of the dish you want to know more information about.\n" +
                "Or press 'v' to sort for vegetarian dishes, 'g' for glutenfree, 'h' for halal food, s for spicy and \n'0' to go back to the Main menu, " +
                "follow that up by pressing Enter.");
            var dish = Console.ReadLine();
            var n = dish;
            List<string> list = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "0", "v", "g", "h", "b", "s" };
            while(dish != "0")
            {
                if (!list.Contains(n))
                {
                    Console.WriteLine("There is no such dish or command");
                }
                else if (dish == "v")
                {
                    Console.WriteLine("These are the vegetarian options:\n");
                    foreach (var s in stuff)
                    {
                        if (s.Veggie == true)
                        {
                            string vegdishes = s.Number + s.Dot + s.Name + s.Price;
                            Console.WriteLine(vegdishes);
                        }
                    }
                }
                else if (dish == "s")
                {
                    Console.WriteLine("These are the spicy options:\n");
                    foreach (var s in stuff)
                    {
                        if (s.Spicy == true)
                        {
                            string spicedishes = s.Number + s.Dot + s.Name;
                            Console.WriteLine(spicedishes);
                        }
                    }
                }
                else if (dish == "g")
                {
                    Console.WriteLine("These are the glutenfree options:\n");
                    foreach (var s in stuff)
                    {
                        if (s.GlutenFree == true)
                        {
                            string glfrdishes = s.Number + s.Dot + s.Name;
                            Console.WriteLine(glfrdishes);
                        }
                    }
                }
                else if (dish == "h")
                {
                    Console.WriteLine("These are the Halal options:\n");
                    foreach (var s in stuff)
                    {
                        if (s.Halal == true)
                        {
                            string halaldishes = s.Number + s.Dot + s.Name;
                            Console.WriteLine(halaldishes);
                        }
                    }
                }
                else if (dish == "b")// if b is ur inpu, there will be a list of the dishes that are both vegetarian and glutenfree
                {
                    Console.WriteLine("These are the vegeterian glutenfree options:\n");
                    foreach (var s in stuff)
                    {
                        if (s.GlutenFree == true && s.Veggie == true)
                        {
                            string bothdish = s.Number + s.Dot + s.Name;
                            Console.WriteLine(bothdish);
                        }
                    }
                }
                else if (dish == n && dish != "0")// n is the number you input, and here it will show the details for the specific dishes 
                {
                    foreach (var s in stuff)
                    {
                        if (s.Number == n)
                        {
                            Console.WriteLine("\n" + s.Name + s.Dot + s.Price + "\n" + s.Ingredients);
                        }
                    }
                }
                Console.WriteLine("\nPlease press the number of the dish you want to know more information about.\n" +
                                  "Or press 'v' to sort for vegetarian dishes, 'g' for glutenfree, 'h' for halal food, s for spicy and \n'0' to go back to the Main menu");
                dish = Console.ReadLine();
                n = dish;
            }

        }
        static void Info()
        {
            Console.WriteLine("_________________________\n\nOpening Hours\n\nMonday:     CLOSED\nTuesday:    16:00-22:00\n" +
            "Wednesday:  16:00-22:00\nThursday:   16:00-22:00\nFriday:     16:00-22:00\nSatuday:    16:00-22:00\nSunday:     " +
            "16:00-22:00\n_________________________\n\nContact Us\n\nPhone Number: 071-5119113\nEmail:\tRestaurant@hr.nl\n_________________________\n");
            Console.WriteLine("0. Previous Page");
            string x = Console.ReadLine();
            if (x == "0")
            {

            }
            else
            {
                Info();
            }
        }
        static void Exit()
        {
            Console.WriteLine("Exit");
        }

        public static void Register(Tuple<string, string> username) // Function to register an account using a tuple that consists of a username and password.
        {
            using (StreamWriter text = new StreamWriter(File.Create($"{username.Item1}.json")))
            {
                text.WriteLine(username.Item1);
                text.WriteLine(username.Item2);
                text.Close();
            }

            Console.WriteLine("Account succesfuly made");
        }

        public static bool Login(Tuple<string, string> username) // Function to login using a tuple that consists of a username and password.
        {
            string usernamecheck, passwordcheck = string.Empty;
            try
            {
                using (StreamReader acc = new StreamReader(File.Open($"{username.Item1}.json", FileMode.Open)))
                {
                    usernamecheck = acc.ReadLine();
                    passwordcheck = acc.ReadLine();
                    acc.Close();

                    if (usernamecheck == username.Item1 && passwordcheck == username.Item2)
                    {
                        return true;
                    }

                    else
                    {
                        Console.WriteLine("Incorrect username or password");
                        Console.WriteLine(" ");
                        return false;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.Write("Account does not exist");
                Console.WriteLine(" ");
                return false;
            }
        }

        public static Tuple<string, string> inputUsernamePassword()
        {
            Console.Write("Enter a username: ");
            string username = Console.ReadLine();
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();

            return Tuple.Create(username, password);
        }

        public static void AddInfo()
        {

        }
        public static Dictionary<string, string> SetupTable(Table data)
        {
            var result = new Dictionary<string, string>();
            result["Table 1"] = data.Table1;
            result["Table 2"] = data.Table2;
            result["Table 3"] = data.Table3;
            result["Table 4"] = data.Table4;
            result["Table 5"] = data.Table5;
            result["Table 6"] = data.Table6;
            result["Table 7"] = data.Table7;
            result["Table 8"] = data.Table8;
            result["Table 9"] = data.Table9;
            result["Table 10"] = data.Table10;
            return result;
        }
        public static Table chosenDayTime(string day, string time, ReservationData table)
        {
            if (day == "1")
            {
                if (time == "1")
                {
                    return table.Tuesday.Four;
                }
                else if (time == "2")
                {
                    return table.Tuesday.Six;
                }
                else if (time == "3")
                {
                    return table.Tuesday.Eight;
                }
            }
            if (day == "2")
            {
                if (time == "1")
                {
                    return table.Wednesday.Four;
                }
                else if (time == "2")
                {
                    return table.Wednesday.Six;
                }
                else if (time == "3")
                {
                    return table.Wednesday.Eight;
                }
            }
            if (day == "3")
            {
                if (time == "1")
                {
                    return table.Thursday.Four;
                }
                else if (time == "2")
                {
                    return table.Thursday.Six;
                }
                else if (time == "3")
                {
                    return table.Thursday.Eight;
                }
            }
            if (day == "4")
            {
                if (time == "1")
                {
                    return table.Friday.Four;
                }
                else if (time == "2")
                {
                    return table.Friday.Six;
                }
                else if (time == "3")
                {
                    return table.Friday.Eight;
                }
            }
            if (day == "5")
            {
                if (time == "1")
                {
                    return table.Saturday.Four;
                }
                else if (time == "2")
                {
                    return table.Saturday.Six;
                }
                else if (time == "3")
                {
                    return table.Saturday.Eight;
                }
            }
            if (day == "6")
            {
                if (time == "1")
                {
                    return table.Sunday.Four;
                }
                else if (time == "2")
                {
                    return table.Sunday.Six;
                }
                else if (time == "3")
                {
                    return table.Sunday.Eight;
                }
            }
            return null;
        }

        public static string ConfirmationText(string week, string day, string time)
        {
            if (week == "1")
            {
                if (day == "1")
                {
                    if (time == "1")
                    {
                        return "This weeks Tuesday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "This weeks Tuesday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "This weeks Tuesday at 20:00";
                    }
                }
                if (day == "2")
                {
                    if (time == "1")
                    {
                        return "This weeks Wednesday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "This weeks Wednesday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "This weeks Wednesday at 20:00";
                    }
                }
                if (day == "3")
                {
                    if (time == "1")
                    {
                        return "This weeks Thursday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "This weeks Thursday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "This weeks Thursday at 20:00";
                    }
                }
                if (day == "4")
                {
                    if (time == "1")
                    {
                        return "This weeks Friday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "This weeks Friday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "This weeks Friday at 20:00";
                    }
                }
                if (day == "5")
                {
                    if (time == "1")
                    {
                        return "This weeks Saturday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "This weeks Saturday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "This weeks Saturday at 20:00";
                    }
                }
                if (day == "6")
                {
                    if (time == "1")
                    {
                        return "This weeks Sunday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "This weeks Sunday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "This weeks Sunday at 20:00";
                    }
                }
            }
            if (week == "2")
            {
                if (day == "1")
                {
                    if (time == "1")
                    {
                        return "Next weeks Tuesday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "Next weeks Tuesday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "Next weeks Tuesday at 20:00";
                    }
                }
                if (day == "2")
                {
                    if (time == "1")
                    {
                        return "Next weeks Wednesday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "Next weeks Wednesday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "Next weeks Wednesday at 20:00";
                    }
                }
                if (day == "3")
                {
                    if (time == "1")
                    {
                        return "Next weeks Thursday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "Next weeks Thursday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "Next weeks Thursday at 20:00";
                    }
                }
                if (day == "4")
                {
                    if (time == "1")
                    {
                        return "Next weeks Friday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "Next weeks Friday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "Next weeks Friday at 20:00";
                    }
                }
                if (day == "5")
                {
                    if (time == "1")
                    {
                        return "Next weeks Saturday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "Next weeks Saturday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "Next weeks Saturday at 20:00";
                    }
                }
                if (day == "6")
                {
                    if (time == "1")
                    {
                        return "Next weeks Sunday at 16:00";
                    }
                    if (time == "2")
                    {
                        return "Next weeks Sunday at 18:00";
                    }
                    if (time == "3")
                    {
                        return "Next weeks Sunday at 20:00";
                    }
                }
            }
            return null;
        }

        public static void EditTableData(string day, string time, Dictionary<string, string> dict, ReservationData tableinfo)
        {
            if (day == "1")
            {
                if (time == "1")
                {
                    tableinfo.Tuesday.Four = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "2")
                {
                    tableinfo.Tuesday.Six = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "3")
                {
                    tableinfo.Tuesday.Eight = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
            }
            if (day == "2")
            {
                if (time == "1")
                {
                    tableinfo.Wednesday.Four = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "2")
                {
                    tableinfo.Wednesday.Six = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "3")
                {
                    tableinfo.Wednesday.Eight = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
            }
            if (day == "3")
            {
                if (time == "1")
                {
                    tableinfo.Thursday.Four = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "2")
                {
                    tableinfo.Thursday.Six = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "3")
                {
                    tableinfo.Thursday.Eight = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
            }
            if (day == "4")
            {
                if (time == "1")
                {
                    tableinfo.Friday.Four = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "2")
                {
                    tableinfo.Friday.Six = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "3")
                {
                    tableinfo.Friday.Eight = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
            }
            if (day == "5")
            {
                if (time == "1")
                {
                    tableinfo.Saturday.Four = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "2")
                {
                    tableinfo.Saturday.Six = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "3")
                {
                    tableinfo.Saturday.Eight = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
            }
            if (day == "6")
            {
                if (time == "1")
                {
                    tableinfo.Sunday.Four = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "2")
                {
                    tableinfo.Sunday.Six = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
                else if (time == "3")
                {
                    tableinfo.Sunday.Eight = new Table()
                    {
                        Table1 = dict["Table 1"],
                        Table2 = dict["Table 2"],
                        Table3 = dict["Table 3"],
                        Table4 = dict["Table 4"],
                        Table5 = dict["Table 5"],
                        Table6 = dict["Table 6"],
                        Table7 = dict["Table 7"],
                        Table8 = dict["Table 8"],
                        Table9 = dict["Table 9"],
                        Table10 = dict["Table 10"]
                    };
                }
            }

        }

        public static void ReserveTable(string theUser) // theUser == the users first name on which the table will be reserved
        {
            string holder = "";
            string peopleAmount = "";
            int amount = 0;
            ReservationData tableData = null;
            Console.WriteLine("Reserve Table\n1. This week\n2. Next week\n");
            string week = Console.ReadLine();
            if (week == "1") // Week 1
            {
                holder = File.ReadAllText("This Week Table Info.json");
                tableData = JsonConvert.DeserializeObject<ReservationData>(holder);

            }
            else if (week == "2") // Week 2
            {
                holder = File.ReadAllText("Next Week Table Info.json");
                tableData = JsonConvert.DeserializeObject<ReservationData>(holder);
            }
            Console.WriteLine("Pick a day\nMonday CLOSED\n1. Tuesday\n2. Wednesday\n3. Thursday\n4. Friday\n5. Saturday\n6. Sunday");

            string day = Console.ReadLine();

            Console.WriteLine("Pick a time\n1. 16:00\n2. 18:00\n3. 20:00");

            string time = Console.ReadLine();
            Console.WriteLine("\n___________________________________\n\n" +
                "Every table has 4 seats, to make an reservation of more than 4 people,\nplease contact us via email or phone\n___________________________________\n");
            Console.Write("Enter amount of people: ");
            peopleAmount = Console.ReadLine();
            if(peopleAmount == "1")
            {
                amount = 1;
            }
            else if(peopleAmount == "2")
            {
                amount = 2;
            }
            else if (peopleAmount == "3")
            {
                amount = 3;
            }
            else if (peopleAmount == "4")
            {
                amount = 4;
            }
            else
            {
                Console.WriteLine("Please retry and enter a number between 0 and 4");
                return;
            }
            var reservationDay = chosenDayTime(day, time, tableData); // Returns a Table class on the specific day and time
            var tableAvailability = SetupTable(reservationDay); // Returns a dictionary with the table as key and the values for the availability of a table as value

            Console.WriteLine("Pick an available table number\n"); 

            foreach (var table in tableAvailability) // Prints the available tables
            {
                string available;
                if (table.Value != "")
                {
                    available = " is not available";
                }
                else
                {
                    available = " is available";
                }
                Console.WriteLine(table.Key + available);
            }

            string pick = Console.ReadLine();
            string confirmation = ConfirmationText(week, day, time);

            Console.WriteLine($"\nConfirm Reservation\n\nYou want to reservere a table on:\n" + confirmation + $"\nTable {pick}\nEstimated Costs: ${amount * 20}\n\n1. Confirm\n2. Cancel\n");
            confirmation = Console.ReadLine();

            if (confirmation == "1")
            {
                if (tableAvailability["Table " + pick] == "")
                {
                    tableAvailability["Table " + pick] = theUser; 

                }
                else
                {
                    Console.WriteLine("This table is not available.");
                }
            }
            else
            {
                Console.WriteLine("You have cancelled the reservation");
            }

            EditTableData(day, time, tableAvailability, tableData); // Edits the ReservationData class
            var file = JsonConvert.SerializeObject(tableData, Formatting.Indented); // Converst ReservationData class into json
            if (week == "1")
            {
                File.WriteAllText("This Week Table Info.json", file);
            }
            else if (week == "2")
            {
                File.WriteAllText("Next Week Table Info.json", file);
            }
        }
        public static void SentEmail(UserInfo reserveInfo)
        {
            Random generator = new Random();
            int randomNumber = generator.Next(100000, 999999);
            string read = File.ReadAllText("allCodeNumbers.json");
            dynamic deserialize = JObject.Parse(read);
            List<int> allCodes = new List<int>();
            foreach (var number in deserialize.reservationCode)
            {
                allCodes.Add((int)number);
            }

            while (allCodes.Contains(randomNumber))
            {
                randomNumber = generator.Next(100000, 999999);
            }

            allCodes.Add(randomNumber);

            Code code = new Code { reservationCode = allCodes };
            string json = JsonConvert.SerializeObject(code, Formatting.Indented);
            File.WriteAllText("allCodeNumbers.json", json);
            SmtpClient sentMail = new SmtpClient("smtp-mail.outlook.com", 587);
            sentMail.EnableSsl = true;
            sentMail.UseDefaultCredentials = false;
            sentMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            sentMail.Credentials = new NetworkCredential("1003967@hr.nl", "b2b256d1");

            try
            {
                sentMail.Send("1003967@hr.nl", $"{reserveInfo.email}", "Reservation code", $"Thank you for making a reservation at our restaurant!\n\nYour Reservation Code is:\t{randomNumber}");
                Console.WriteLine("Email sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine($"Reservation successful!\n\nAn email with the reservation code has been sent to {reserveInfo.email}.\n\nPress enter to return.");
            Console.ReadLine();
        }
    }
}