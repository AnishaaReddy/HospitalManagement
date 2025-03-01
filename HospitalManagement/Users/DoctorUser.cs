using HospitalManagement.Objects;
using MyEnums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Users
{
    internal class DoctorUser: DisplayMenu
    {
        public string docID { get; protected set; }
        public string docName { get; protected set; }
        public DoctorUser(string id, string name)
        {

            docID = id;
            docName = name;
        }
        public void TopMenu()
        {
            Console.Clear();
            // call the display menu and pass the heading texts accordingly
            TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Doctor_Menu);
            short value = (short)Heading.DOTNET_Hospital_Management_System;
            var heading = EnumsCl.GetEnumDisplay((Heading)value);
            Console.WriteLine("Welcome to " + heading + " " + docName);
            MenuList();

        }

        //display the menu options
        public override void MenuList()
        {
            try
            {
                Console.WriteLine("Please choose an option\n 1. List Doctor Details\n 2. Check Patient Details\n 3. List all Appointments\n 4. Check particular patient\n 5. Check appointment with patient\n 6. Exit to Login\n 7. Exit System");
                Console.Write("Enter your choice: ");


                int choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        DoctorDetails();
                        break;
                    case 2:
                        ViewPatients();
                        break;
                    case 3:
                        ViewAppointments();
                        break;
                    case 4:
                        ViewPatientID();
                        break;
                    case 5:
                        ViewApptPatientID();
                        break;
                    case 6:
                        ExitLogin();
                        break;
                    case 7:
                        ExitSystem();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        TopMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Following error has occurred:" + ex.Message);
            }

        }


        //display doctor details
        public void DoctorDetails()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.My_Details);

                //read data from txt file
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                DoctorData doctor = doctorDataList.Find(d => d.ID == docID.ToString());

                if (doctor != null)
                {
                    Console.WriteLine(new string('-', 100));
                    Console.WriteLine($"{"Full Name",-20} | {"Address",-15} | {"Email",-25} | {"Phone",-15} |");
                    Console.WriteLine(new string('-', 100));
                    Console.WriteLine($" {doctor.Details.Full_Name,-20} | {doctor.Details.Address,-15} | {doctor.Details.Email,-25} | {doctor.Details.Phone,-15} |");
                    Console.WriteLine(new string('-', 100));

                }
                else
                {
                    Console.WriteLine("Doctor not found with the provided ID.");
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

        //view all assigned patients
        public void ViewPatients()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.My_Patients);

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                DoctorData doctor = doctorDataList.Find(d => d.ID == docID.ToString());

                if (doctor != null)
                {
                    Console.WriteLine("All of the patients assigned to " + doctor.Details.Full_Name);
                    Console.WriteLine(new string('-', 85));
                    Console.WriteLine($"{"Full Name",-20} | {"Address",-20} | {"Email",-20} | {"Phone",-15} | {"Doctor",-20} |");
                    Console.WriteLine(new string('-', 85));
                    if (doctor.Patients.patient != null)
                    {
                       
                        foreach (var patientData in doctor.Patients.patient)
                        {
                            var patient = patientData;
                            Console.WriteLine($" {patient.Patient_Full_Name,-20} | {patient.Patient_Address,-20} | {patient.Patient_Email,-20} | {patient.Patient_Phone,-15} | {patient.Doctor_Name,-20} |");
                            Console.WriteLine(new string('-', 85));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No patients found.");
                    }

                }
                else
                {
                    Console.WriteLine("Doctor not found with the provided ID.");
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

        //view all appointments
        public void ViewAppointments()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.All_appts);

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                DoctorData doctor = doctorDataList.Find(d => d.ID == docID.ToString());

                if (doctor != null)
                {
                    Console.WriteLine(new string('-', 85));
                    Console.WriteLine($" {"Doctor",-20} | {"Patient",-20} | {"Description",-30} |");
                    Console.WriteLine(new string('-', 85));
                    if (doctor.Appointments.appts != null)
                    {
                        foreach (var appointment in doctor.Appointments.appts)
                        {
                            Console.WriteLine($" {appointment.Doctor_Name,-20} | {appointment.Patient_Name,-20} | {appointment.Description,-30} |");
                            Console.WriteLine(new string('-', 85));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No appointments found.");
                    }

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

        //view patient details by patient id
        public void ViewPatientID()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Chk_pat_details);

                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;

                Console.WriteLine("Enter the ID of the patient you want to check details");
                string patientID = Console.ReadLine();
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                DoctorData doctor = doctorDataList.Find(d => d.ID == docID.ToString());

                if (doctor != null)
                {
                    Console.WriteLine(new string('-', 85));
                    Console.WriteLine($"{"Full Name",-20} | {"Address",-25} | {"Email",-20} | {"Phone",-15} | {"Doctor",-20} |");
                    Console.WriteLine(new string('-', 85));


                    foreach (var patientData in doctor.Patients.patient)
                    {
                        if (patientData.Patient_ID == patientID)
                        {
                            var patient = patientData;
                            Console.WriteLine($" {patient.Patient_Full_Name,-20} | {patient.Patient_Address,-25} | {patient.Patient_Email,-20} | {patient.Patient_Phone,-15} | {patient.Doctor_Name,-20} |");
                            Console.WriteLine(new string('-', 85));
                        }
                        else
                        {
                            Console.WriteLine("No patient found.");
                        }

                    }
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

        //view an appointment by patient id
        public void ViewApptPatientID()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Pat_appts);

                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;

                Console.WriteLine("Enter the ID of the patient you want to check appointment details");
                string patientID = Console.ReadLine();
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData);

                DoctorData doctor = doctorDataList.Find(d => d.ID == docID.ToString());

                if (doctor != null)
                {
                    Console.WriteLine(new string('-', 85));
                    Console.WriteLine($" {"Doctor",-20} | {"Patient",-20} | {"Description",-30} |");
                    Console.WriteLine(new string('-', 85));
                    foreach (var appointment in doctor.Appointments.appts)
                    {
                        if (appointment.Patient_ID == patientID.ToString())
                        {
                            Console.WriteLine($" {appointment.Doctor_Name,-20} | {appointment.Patient_Name,-20} | {appointment.Description,-30} |");
                            Console.WriteLine(new string('-', 85));
                        }
                        else
                        {
                            Console.WriteLine("No patient found.");
                        }

                    }
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

        //exit console application
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


        //return to login
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
