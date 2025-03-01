using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEnums;
using HospitalManagement.Objects;
using System.Reflection;
using System.Xml.Linq;

namespace HospitalManagement.Users
{
     public class HospitalUsers
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public string Name { get; set; }
    

        // ask user for their credentials
        public void LogInUI()
        {
            try
            {
                Console.Clear();
                DisplayMenu.TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Login);
                Console.WriteLine("Login System");
                Console.Write("Enter username:");
                Id = Console.ReadLine();
                Console.Write("Enter Password: ");
                Password = ReadPassword();
                ValidateLogin();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }

        }

        //mask the password with * 
        public string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

        //validate the credentials provided by the user
        public void ValidateLogin()
        {
            try {
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\Login.txt";               
                var jsonText = File.ReadAllText(filepath);

                List<LoginUser> allUsers = JsonConvert.DeserializeObject<List<LoginUser>>(jsonText);

                if (allUsers != null)
                {
                    LoginUser loggedInUser = allUsers.Find(d => d.Id == Id);
                    if (loggedInUser != null && loggedInUser.Password== Password) {
                        Console.WriteLine("Valid credentials");
                        if (loggedInUser.UserRole == Role.Patient)
                        {
                            //show patient menu
                            PatientUser patient = new PatientUser((loggedInUser.Id), loggedInUser.Name);
                            patient.TopMenu();

                        }
                        else if (loggedInUser.UserRole == Role.Doctor)
                        {
                            //show doctor menu
                            DoctorUser doctor = new DoctorUser((loggedInUser.Id), loggedInUser.Name);
                            doctor.TopMenu();
                        }
                        else if (loggedInUser.UserRole == Role.Admin)
                        {
                            //show admin menu
                            AdminUser admin = new AdminUser((loggedInUser.Id), loggedInUser.Name);
                            admin.TopMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found. Invalid credentials.");
                    }
                }
                else
                {
                    Console.WriteLine("Error loading database");
                }

            }
            catch (Exception ex) { 
                Console.WriteLine("Following error has occurred:"+ex.Message);
            }
           
        }

    }





}
