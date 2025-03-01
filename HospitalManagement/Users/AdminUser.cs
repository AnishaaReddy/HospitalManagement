using HospitalManagement.Objects;
using MyEnums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Users
{
    internal class AdminUser : DisplayMenu
    {
        public string adminID { get; protected set; }
        public string adminName { get; protected set; }
        public AdminUser(string id, string name)
        {

            adminID = id;
            adminName = name;
        }
        public void TopMenu()
        {
            Console.Clear();
            // call the display menu and pass the heading texts accordingly
            TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Admin_Menu);
            short value = (short)Heading.DOTNET_Hospital_Management_System;
            var heading = EnumsCl.GetEnumDisplay((Heading)value);
            Console.WriteLine("Welcome to " + heading + " " + adminName);
            MenuList();

        }

        //display the menu options
        public override void MenuList()
        {
            try
            {

                Console.WriteLine("Please choose an option\n 1. List all Doctors\n 2. Check Doctor Details\n 3. List all patients\n 4. Check patient details\n 5. Add doctor\n 6. Add patient\n 7. Exit to Login\n 8. Exit System");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        DisplayDoctors();
                        break;
                    case 2:
                        CheckDoctorDetails();
                        break;
                    case 3:
                        DisplayPatients();
                        break;
                    case 4:
                        CheckPatientDetails();
                        break;
                    case 5:
                        AddDoctor();
                        break;
                    case 6:
                        AddPatient();
                        break;
                    case 7:
                        ExitLogin();
                        break;
                    case 8:
                        ExitSystem();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        TopMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }

        
      }

        //display all registered doctors
        public void DisplayDoctors()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Doctors);
                Console.WriteLine("All doctors registered with DOT NET Hospital Management System");

                //read doctors data from txt file
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);
                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"{"Full Name",-20} | {"Address",-30} | {"Email",-30} | {"Phone",-15} |");
                if (doctorDataList != null)
                {
                    foreach (var doctorData in doctorDataList)
                    {
                        Console.WriteLine(new string('-', 100));
                        Console.WriteLine($" {doctorData.Details.Full_Name,-20} | {doctorData.Details.Address,-30} | {doctorData.Details.Email,-30} | {doctorData.Details.Phone,-15} |");
                        Console.WriteLine(new string('-', 100));

                    }
                }
                else
                {
                    Console.WriteLine("No doctors found");
                }

                //can return to main menu by esc
                Console.WriteLine("Press ESC to return");


                ConsoleKeyInfo cki;
                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                        TopMenu();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }

        }

        //display a doctor by doctor id
        public void CheckDoctorDetails()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Doctor_details);
                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;

                Console.WriteLine("Enter the ID of the doctor you want to check details");
                string doctorID = Console.ReadLine();
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                //find doctor by id
                DoctorData doctor = doctorDataList.Find(d => d.ID == doctorID);

                if (doctor != null)
                {
                    Console.WriteLine("Details of "+ doctor.Details.Full_Name);
                    Console.WriteLine(new string('-', 100));
                    Console.WriteLine($"{"Full Name",-20} | {"Address",-30} | {"Email",-30} | {"Phone",-15} |");
                    Console.WriteLine(new string('-', 100));
                    Console.WriteLine($" {doctor.Details.Full_Name,-20} | {doctor.Details.Address,-30} | {doctor.Details.Email,-30} | {doctor.Details.Phone,-15} |");
                    Console.WriteLine(new string('-', 100));
                }
                else
                {
                    Console.WriteLine("Doctor not found with the provided ID.");
                }


                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                        TopMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }

        }

        //display all registered patients
        public void DisplayPatients()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Patients);
                Console.WriteLine("All patients registered with DOT NET Hospital Management System");

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);
                //   List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);
                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);

                Console.WriteLine(new string('-', 85));
                Console.WriteLine($" {"Full Name",-15} | {"Address",-30} | {"Email",-15} | {"Phone",-15} | {"Doctor",-15} |");
                Console.WriteLine(new string('-', 85));

                if (patientsDataList != null)
                {


                    foreach (var patientData in patientsDataList)
                    {
                        var patient = patientData;
                        Console.WriteLine($" {patient.Details.Full_Name,-15} | {patient.Details.Address,-30} | {patient.Details.Email,-15} | {patient.Details.Phone,-15} | {patient.Doctor.Doctor_Full_Name,-15} |");
                        Console.WriteLine(new string('-', 100));
                        Console.WriteLine(new string('-', 100));
                    }



                }
                else
                {
                    Console.WriteLine("No patients found");
                }

                Console.WriteLine("Press ESC to return");


                ConsoleKeyInfo cki;
                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                        TopMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }


        }

        //display a patient by patient id
        public void CheckPatientDetails()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Patient_details);
                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;


                Console.WriteLine("Enter the ID of the patient you want to check details");
                string patientID = Console.ReadLine();
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);
                PatientData_Pat patient = patientsDataList.Find(p => p.ID == patientID);

                if (patient != null)
                {
                    Console.WriteLine("Details of " + patient.Details.Full_Name);
                    Console.WriteLine(new string('-', 85));
                    Console.WriteLine($" {"Full Name",-20} | {"Address",-30} | {"Email",-30} | {"Phone",-15} | {"Doctor",-20} |");
                    Console.WriteLine(new string('-', 85));
                    Console.WriteLine($"{patient.Details.Full_Name,-20} | {patient.Details.Address,-30} | {patient.Details.Email,-30} | {patient.Details.Phone,-15} | {patient.Doctor.Doctor_Full_Name,-20} |");
                    Console.WriteLine(new string('-', 85));
                }
                else
                {
                    Console.WriteLine("No patient found");
                }

                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                        TopMenu();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }



        }

        //add a new doctor
        public void AddDoctor()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Add_doc);

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorsDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                string filepath2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\Login.txt";
                var jsonUserData = File.ReadAllText(filepath2);
                List<LoginUser> usersDataList = JsonConvert.DeserializeObject<List<LoginUser>>(jsonUserData);

                // Adding a new doctor
                DoctorData newDoctor = new DoctorData();
                LoginUser newUser = new LoginUser();
                Console.WriteLine("Registering a new doctor with the DOT NET Hospital Management System:");
                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;

                //capture details
                Console.WriteLine("Enter Doctor's First Name:");
                string fname = Console.ReadLine();
                Console.WriteLine("Enter Doctor's Last Name:");
                string lname = Console.ReadLine();
                string name = fname +" "+ lname;
                newDoctor.Details.Full_Name = name;
                newUser.Name = name;
                Console.WriteLine("Enter Street:");
                string street = Console.ReadLine();
                Console.WriteLine("Enter  Suburb:");
                string suburb = Console.ReadLine();
                Console.WriteLine("Enter Postcode:");
                string pcode = Console.ReadLine();
                Console.WriteLine("Enter State:");
                string state = Console.ReadLine();
                string address =street+" "+suburb+" "+pcode+" "+state;
                newDoctor.Details.Address = address;
                Console.WriteLine("Enter Doctor's Email:");
                newDoctor.Details.Email = Console.ReadLine();
                Console.WriteLine("Enter Doctor's Phone:");
                newDoctor.Details.Phone = Console.ReadLine();

                // Generating a new ID (For simplicity, incrementing the last ID by 1)
                if (usersDataList.Count > 0)
                {
                    int lastId = int.Parse(usersDataList[usersDataList.Count - 1].Id);
                    newDoctor.ID = (lastId + 1).ToString();
                    newUser.Id = (lastId + 1).ToString();
                    newUser.UserRole = Role.Doctor;
                    newUser.Password = "123D"; //default password

                }

                //add new user to doctor db and users db
                doctorsDataList.Add(newDoctor);
                usersDataList.Add(newUser);

                // Serialize and display updated JSON data
                string updatedJsonData = JsonConvert.SerializeObject(doctorsDataList, Formatting.Indented);
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                File.WriteAllText(path, updatedJsonData);

                string updatedJsonData2 = JsonConvert.SerializeObject(usersDataList, Formatting.Indented);
                string path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\Login.txt";
                File.WriteAllText(path2, updatedJsonData2);

                Console.WriteLine(name+" Successfully added to the system");

                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                        TopMenu();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }

        }

        //add a new patient
        public void AddPatient()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Add_pat);

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);

                string filepath2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\Login.txt";
                var jsonUserData = File.ReadAllText(filepath2);
                List<LoginUser> usersDataList = JsonConvert.DeserializeObject<List<LoginUser>>(jsonUserData);

                PatientData_Pat newPatient = new PatientData_Pat();
                LoginUser newUser = new LoginUser();
                Console.WriteLine("Registering a new patient with the DOT NET Hospital Management System:");
                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;

                //capture details
                Console.WriteLine("Enter Patient's First Name:");
                string fname = Console.ReadLine();
                Console.WriteLine("Enter Patient's Last Name:");
                string lname = Console.ReadLine();
                string name = fname + " " + lname;
                newPatient.Details.Full_Name = name;
                newUser.Name = name;
                Console.WriteLine("Enter Street:");
                string street = Console.ReadLine();
                Console.WriteLine("Enter  Suburb:");
                string suburb = Console.ReadLine();
                Console.WriteLine("Enter Postcode:");
                string pcode = Console.ReadLine();
                Console.WriteLine("Enter State:");
                string state = Console.ReadLine();
                string address = street + " " + suburb + " " + pcode + " " + state;
                newPatient.Details.Address = address;
                Console.WriteLine("Enter Patients's Email:");
                newPatient.Details.Email = Console.ReadLine();
                Console.WriteLine("Enter Patients's Phone:");
                newPatient.Details.Phone = Console.ReadLine();
                newPatient.Doctor_Reg = null;


                // Generating a new ID (For simplicity, incrementing the last ID by 1)
                if (usersDataList.Count > 0)
                {
                    int lastId = int.Parse(usersDataList[usersDataList.Count - 1].Id);
                    newPatient.ID = (lastId + 1).ToString();
                    newUser.Id = (lastId + 1).ToString();
                    newUser.UserRole = Role.Patient;
                    newUser.Password = "123P";

                }

                //add new user to patient db and user db
                patientsDataList.Add(newPatient);
                usersDataList.Add(newUser);

                // Serialize and display updated JSON data
                string updatedJsonData = JsonConvert.SerializeObject(patientsDataList, Formatting.Indented);
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                File.WriteAllText(path, updatedJsonData);

                string updatedJsonData2 = JsonConvert.SerializeObject(usersDataList, Formatting.Indented);
                string path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\Login.txt";
                File.WriteAllText(path2, updatedJsonData2);

                Console.WriteLine(name + " Successfully added to the system");

                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Escape)
                        TopMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }



        }

        private void ExitSystem()
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }
        }

        private void ExitLogin()
        {
            try
            {
                HospitalUsers loginUser = new HospitalUsers();
                loginUser.LogInUI();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }
        }
    }

}
