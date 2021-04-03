using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                Console.WriteLine("1. Log in\n2. Current Menu\n3. Future Menu\n4. Information about the Restaurant\n5. Account\n6. Exit\n");
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
                                Console.WriteLine("empty for now");
                            }
                        }
                        Console.WriteLine("Login succesful\n");
                        theUser = userTuple.Item1;
                        continue;
                    }
                    if (choice == "2")
                    {
                        var userTuple = inputUsernamePassword();
                        Register(userTuple);
                        continue;
                    }
                    if (choice == "3")
                    {
                        Console.WriteLine("go back\n");
                        continue;
                    }
                }
                if (x == "2") // Current Menu
                {
                    CurrentMenuPage();
                }
                if (x == "3") // FutureMenu
                {
                    FutureMenu();
                }
                if (x == "4") // Info
                {
                    Info();
                }
                if (x == "5") // Account 
                {
                    if (theUser == "")
                    {
                        Console.WriteLine("You are not logged in\n");
                    }
                    else
                    {
                        Console.WriteLine("1. Add personal information\n2. Edit personal information\n");
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

                            UserInfo info = new UserInfo(name, surname, mail, number);

                            json createFile = new json()
                            {
                                firstname = name,
                                lastname = surname,
                                email = mail,
                                phone = number
                            };
                            string text = JsonConvert.SerializeObject(createFile);
                            File.WriteAllText($"{theUser}.json", text);
                            Console.WriteLine("Information saved succesfully\n");
                        }

                        if (y == "2")
                        {
                            Console.WriteLine("empty for now");
                        }
                    }

                }
                if (x == "6")
                {
                    Exit();
                }
            }


        }
        static void MainMenu()
        {
            Console.WriteLine("1. Log in\n2. Current Menu\n3. Future Menu\n4. Information about the Restaurant\n5. Account\n6. Exit");
            string x = Console.ReadLine();
            if (x == "1")
            {
                LogIn();
            }
            if (x == "2")
            {
                CurrentMenuPage();
            }
            if (x == "3")
            {
                FutureMenu();
            }
            if (x == "4")
            {
                Info();
            }
            if (x == "5")
            {

            }
            if (x == "6")
            {
                Exit();
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
                Console.WriteLine(s.Number + s.Dot + s.Name + s.Price);
            }
            SortingMenu();

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
                MainMenu();
            }
            Console.WriteLine("Please press Enter to go back.");
            Console.ReadLine();
            CurrentMenuPage();
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
                MainMenu();
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
                MainMenu();
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
        static void Account()
        {
            Console.WriteLine("1. Fill in personal information\n2. Edit personal information");
            string x = Console.ReadLine();
            if (x == "1")
            {

            }
        }
        public static void Register(Tuple<string, string> username) // Function to register an account using a tuple that consists of a username and password.
        {
            using (StreamWriter text = new StreamWriter(File.Create($"C:\\{username.Item1}.json")))
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
                using (StreamReader acc = new StreamReader(File.Open($"C:\\{username.Item1}.json", FileMode.Open)))
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
    class UserInfo
    {
        public string firstname;
        public string lastname;
        public string email;
        public string phone;

        public UserInfo(string name, string surname, string mail, string number)
        {
            firstname = name;
            lastname = surname;
            email = mail;
            phone = number;
        }
    }

    class json
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

}
