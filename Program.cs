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
            MainMenu(); 
        }
        static void MainMenu()
        {
            Console.WriteLine("1. Log in\n2. Current Menu\n3. Future Menu\n4. Information about the Restaurant\n5. Exit");
            Console.WriteLine("fnfknkanodsnfkanfanfahnfafaiofanofniofgnio");
            string x = Console.ReadLine();
            if(x == "1")
            {
                LogIn();
            }
            if(x == "2")
            {
                CurrentMenu();
            }
            if(x == "3")
            {
                FutureMenu();
            }
            if(x == "4")
            {
                Info();
            }
            if(x == "5")
            {
                Exit();
            }
        }
        static void LogIn()
        {
            bool adminLogin = false;
            bool userLogin = false;
            Console.WriteLine("Press a numberkey to choose option");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register new account");
            Console.WriteLine("0: Back");
            string input = Console.ReadLine();
            if (input == "2") // Register
            {
                var user = inputUsernamePassword();

                Register(user);
            }

            else if (input == "1") // Login
            {
                var user = inputUsernamePassword();

                if (user.Item1 == "Admin" && user.Item2 == "Admin")
                {
                    adminLogin = true;
                }

                else
                {
                    var login = Login(user);
                    while (login == false && userLogin == false)
                    {
                        Console.WriteLine("Enter 0 to cancel, or press enter to retry");
                        var quit = Console.ReadLine();
                        if (quit == "0")
                        {
                            MainMenu();
                        }
                        user = inputUsernamePassword();
                        login = Login(user);
                        if (login == true)
                        {
                            Console.WriteLine("Login succesful");
                            userLogin = true;
                        }
                    }
                }
            }

            else if (input == "0") // Back
            {
                MainMenu();
            }
            

        }
        static void CurrentMenu()
        {

            var json = File.ReadAllText("details.json");
            dynamic stuff = JsonConvert.DeserializeObject(json);

            foreach (var s in stuff)
            {
                Console.WriteLine(s.Number + s.Name);
            }
            Console.WriteLine("\nPlease press the number of the dish you want to know more information about.");
            Console.WriteLine("Or press 0 to go back");
            string Dish2 = Console.ReadLine();
            int dish = Int32.Parse(Dish2);



            if (dish == 0)
            {
                MainMenu();
            }

            if (dish == 1)
            {
                Console.WriteLine("sambai vinaigrette, sojaboontjes, sesam mayonaise, gepofte wilde rijst \n" +
                    "€ 12,50");
            }
            else if (dish == 2)
            {
                Console.WriteLine("Hollands rund-truffel vinaigrette, oude kaas, rucola\n" +
                    "12,50)");
            }
            else if (dish == 3)
            {
                Console.WriteLine("U get whatever u want!!!!!3");
            }
            else if (dish == 4)
            {
                Console.WriteLine("U get whatever u want!!!!!4");
            }
            else if (dish == 5)
            {
                Console.WriteLine("U get whatever u want!!!!!5");
            }
            else if (dish == 6)
            {
                Console.WriteLine("U get whatever u want!!!!!6");
            }
            else if (dish == 7)
            {
                Console.WriteLine("U get whatever u want!!!!!7");
            }
            else if (dish == 8)
            {
                Console.WriteLine("U get whatever u want!!!!!8");
            }
            else if (dish == 9)
            {
                Console.WriteLine("U get whatever u want!!!!!9");
            }
            else if (dish == 10)
            {
                Console.WriteLine("U get whatever u want!!!!!10");
            }
            else if (dish == 11)
            {
                Console.WriteLine("U get whatever u want!!!!!11");
            }
            else if (dish == 12)
            {
                Console.WriteLine("U get whatever u want!!!!!12");
            }
            else if (dish == 13)
            {
                Console.WriteLine("U get whatever u want!!!!!13");
            }
            else if (dish == 14)
            {
                Console.WriteLine("U get whatever u want!!!!!14");
            }
            else if (dish == 15)
            {
                Console.WriteLine("U get whatever u want!!!!!15");
            }
            else if (dish == 16)
            {
                Console.WriteLine("U get whatever u want!!!!!16");
            }
            else if (dish == 17)
            {
                Console.WriteLine("U get whatever u want!!!!!17");
            }
            else
            {
                Console.WriteLine("The is no such dish, please choose another dish!!!");
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
            if(x == "0")
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
                }

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
    }   
    
}
