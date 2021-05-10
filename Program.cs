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
            string theUser = "";
            bool stopProgram = false;
            while (stopProgram == false)
            {
                Console.WriteLine("1. Log in\n2. Reserve table\n3. Current Menu\n4. Future Menu\n5. Information about the Restaurant\n6. Account\n7. Exit\n");
                string x = Console.ReadLine();
                if (x == "1") // Login 
                {
                    LogIn();
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
                }
                if (x == "3") // Current Menu
                {
                    CurrentMenuPage();
                    continue;
                }
                if (x == "4") // FutureMenu
                {
                    FutureMenu();
                }
                if (x == "5") // Info
                {
                    Info();
                }
                if (x == "6") // Account 
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

                }
                if (x == "7")
                {
                    stopProgram = true;
                }
                if (x == "2")
                {
                    if (theUser != "")
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
                            if (input == "2")
                            {
                                continue;
                            }
                        }
                        if (userNoInfo == true)
                        {
                            Console.WriteLine("Here comes the reservation system");
                            continue;
                        }
                    }
                    else
                    {
                        int seats = 30;
                        Console.WriteLine("Day:\n1. Tuesday\n2. Wednesday\n3. Thursday\n4. Friday\n5. Saturday\n6. Sunday");
                        string day = Console.ReadLine();
                        Console.WriteLine("Time:");
                        if (day == "1" || day == "2" || day == "3")
                        {
                            Console.WriteLine("(between 16:00 and 19:00)");
                        }
                        else
                        {
                            Console.WriteLine("(between 16:00 and 20:00)");
                        }
                        day = day == "1" ? "Tuesday" : day == "2" ? "Wednesday" : day == "3" ? "Thursday" : day == "4" ? "Friday" : day == "5" ? "Saturday" : "Sunday";
                        string time = Console.ReadLine();
                        Console.WriteLine("Amount of people:");
                        string people = Console.ReadLine();
                        seats -= Int32.Parse(people);
                        Console.WriteLine(seats);
                    }
                }
            }


        }
        static void LogIn()
        {
            Console.WriteLine("Press a numberkey to choose option");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register new account");
            Console.WriteLine("0: Back");
        }
        static void CurrentMenuPage()
        {
            var json = File.ReadAllText("details.json");
            dynamic stuff = JsonConvert.DeserializeObject(json);
            foreach (var s in stuff)
            {
                Console.WriteLine(s.Number + s.Dot + s.Name);
            }
            Console.WriteLine("Press s for the sorting page");
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
                Console.WriteLine("These are the vegetarian options:\n");
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
                    Console.WriteLine(s.Number + s.Dot + s.Name);
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
            else if (dish == "0")
            {
                Console.WriteLine("\n\n");
            }

        }
        static void FutureMenu()
        {

            //Welcome message
            Console.WriteLine("***Welcome to the future menu page!***\n");
            Console.WriteLine("Here you can find dishes that are to be added... \n\n");

            //New dishes content 
            //Primers   
            Console.WriteLine("Appetizer");
            string[] appetizerDishes = { "1. Bruschetta", "2. Insalata con mozzarella e avocado", "3. Grissini con prosciutto crudo e parmigiano" };
            List<string> appetizerDishesList = new List<string>(appetizerDishes);
            foreach (string x in appetizerDishes)
                Console.WriteLine(x);


            //Main dishes
            Console.WriteLine("\nMain dishes");
            string[] mainDishes = { "4. Risotto ai funghi", "5. Melanzane alla Parmigiana", "6. Bistecca alla Fiorentina" };
            List<string> mainDishesList = new List<string>(mainDishes);
            foreach (string x in mainDishes)
                Console.WriteLine(x);


            //Desserts
            Console.WriteLine("\nDesserts");
            string[] desserts = { "7. Tiramisu", "8. Gelato alla frutta" };
            List<string> dessertsList = new List<string>(desserts);
            foreach (string x in desserts)
                Console.WriteLine(x);


            //Drinks
            Console.WriteLine("\nDrinks");
            string[] drinks = { "9. Limoncello", "10. Disaronno", "11. Sambuca" };
            List<string> drinksList = new List<string>(drinks);
            foreach (string x in drinks)
                Console.WriteLine(x);
            {

            }


            //View dishes
            Console.WriteLine("Enter the dishnumber you want to view: ");
            Console.WriteLine("Or press 0 to go back");
            int dishNumber = Convert.ToInt32(Console.ReadLine());

            if (dishNumber == 0)
            {

            }



        }
        static void Info()
        {
            Console.WriteLine("_________________________\n\nOpening Hours\n\nMonday:     CLOSED\nTuesday:    16:00-20:00\n" +
            "Wednesday:  16:00-20:00\nThursday:   16:00-20:00\nFriday:     16:00-21:00\nSatuday:    16:00-21:00\nSunday:     " +
            "16:00-21:00\n_________________________\n\nContact Us\n\nPhone Number: 071-5119113\n_________________________\n");
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
    }
    public class UserInfo
    {
        public string firstname;
        public string lastname;
        public string email;
        public string phone;

        public UserInfo(string name, string surname, string mail, string number)
        {
            this.firstname = name;
            this.lastname = surname;
            this.email = mail;
            this.phone = number;
        }
    }

    class json
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    class MainJson
    {
        public dynamic parse;

        public void SerializeJson(json thething) // Gebruik deze method om een class die je maakt voor jouw eigen code om te zetten naar een json file
        {
            string text = JsonConvert.SerializeObject(thething);
            Console.Write("Enter File name: ");
            string filename = Console.ReadLine();
            File.WriteAllText($"{filename}.json", text);
        }

        public void DeserializeJson(string text)
        {
            dynamic json = JsonConvert.DeserializeObject(text); // Gebruik deze method om de values van de json bestand te printen, dit kan je gaan veranderen door een child class te maken en deze method te overwriten.
                                                                // Zo kan je bijvoorbeeld inplaats van Console.WriteLine(), iets returnen met de values erin.
            foreach (var key in json)
            {
                Console.WriteLine(key);
                //Console.WriteLine(key) werkt ook, maar dan krijg je "key" : "value" ("firstname" : "test") uitgeprint. Dat kan handig zijn.
                foreach (var variable in key)
                {
                    Console.WriteLine(variable);
                }

            }
        }

        public void Parse(string text) 
        {
            dynamic parse = JObject.Parse(text);
            this.parse = parse;
        }
    }
    class Code
    {
        public List<int> reservationCode { get; set; }
    }
}