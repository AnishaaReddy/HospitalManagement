using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using MyEnums;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using HospitalManagement.Objects;
using System.Security.Cryptography;
using System.Numerics;
using System.Reflection;

namespace HospitalManagement.Users
{

    public class PatientUser : DisplayMenu
    {
        public string patId { get; protected set; }
        public string patName { get; protected set; }


        public PatientUser(string patientId, string patientName)
        {

            patId = patientId;
            patName = patientName;
        }



        public void TopMenu()
        {
            Console.Clear();
            // call the display menu and pass the heading texts accordingly which are saved as enums to avoid text redundancy
            TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Patient_Menu);
            short value = (short)Heading.DOTNET_Hospital_Management_System;
            var heading = EnumsCl.GetEnumDisplay((Heading)value);
            Console.WriteLine("Welcome to " + heading + " " + patName);
            MenuList();

        }

        //display the menu options
        public override void MenuList()
        {

            Console.WriteLine("Please choose an option\n 1. List Patient Details\n 2. List My Doctor Details\n 3. List all appointments\n 4. Book an appointment\n 5. Exit to Login\n 6. Exit System");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());


            switch (choice)
            {
                case 1:
                    DisplayPatientDetails();
                    break;
                case 2:
                    DisplayDoctor_PatientDetails();
                    break;
                case 3:
                    DisplayAppointments();
                    break;
                case 4:
                    BookAppointment();
                    break;
                case 5:
                    ExitLogin();
                    break;
                case 6:
                    ExitSystem();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    TopMenu();
                    break;
            }
        }


        //display the patient details
        public void DisplayPatientDetails()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.My_Details);

                //read text from .txt file and convert to json objects
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);
                if (patientsDataList != null)
                {
                    PatientData_Pat patient = patientsDataList.Find(p => p.ID == patId);
                    if (patient != null)
                    {
                        Console.WriteLine(patName + " Details\n\n");
                        Console.WriteLine("Patient ID: " + patient.ID);
                        Console.WriteLine("Patient Name: " + patient.Details.Full_Name);
                        Console.WriteLine("Patient Address: " + patient.Details.Address);
                        Console.WriteLine("Patient Email: " + patient.Details.Email);
                        Console.WriteLine("Patient Phone: " + patient.Details.Phone);
                    }
                    else
                    {
                        Console.WriteLine("Error loading patient details");
                    }
                }
                else
                {
                    Console.WriteLine("Error loading database");
                }


                // esc key can be used to return to main menu
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

        //display the doctor details
        public void DisplayDoctor_PatientDetails()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.My_Doctor);
                Console.WriteLine("Your Doctor\n\n");

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);
                if (patientsDataList != null)
                {
                    //find patient by patient id
                    PatientData_Pat patient = patientsDataList.Find(p => p.ID == patId);
                    if (patient != null)
                    {
                        Console.WriteLine(new string('-', 100));
                        Console.WriteLine($"{"Full Name",-20} | {"Address",-15} | {"Email",-25} | {"Phone",-15} |");
                        Console.WriteLine(new string('-', 100));
                        Console.WriteLine($" {patient.Doctor.Doctor_Full_Name,-20} | {patient.Doctor.Doctor_Address,-15} | {patient.Doctor.Doctor_Email,-25} | {patient.Doctor.Doctor_Phone,-15} |");
                        Console.WriteLine(new string('-', 100));
                    }
                    else
                    {
                        Console.WriteLine("No doctor details found");
                    }
                }
                else
                {
                    Console.WriteLine("Error loading database");
                }


                //Console.ReadKey();
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

        private void BookAppointment()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Booking);

                Console.WriteLine("Press ESC to return");
                ConsoleKeyInfo cki;
                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);
                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);
                if (patientsDataList != null)
                {
                    PatientData_Pat patient = patientsDataList.Find(p => p.ID == patId);
                    if (patient.Doctor.Doctor_ID != null && patient.Doctor_Reg != null )
                    {
                        //check if doctor is already assigned
                        Console.WriteLine("You are booking an appointment with " + patient.Doctor.Doctor_Full_Name);
                        Console.WriteLine("Description of the appointment:");
                        string des = Console.ReadLine();
                        AppointmentDataforPatient newAppointment = new AppointmentDataforPatient();
                        var apptId = GenerateAppointmentId(patientsDataList);
                        newAppointment.App_ID = apptId;
                        newAppointment.Doctor_Name = patient.Doctor.Doctor_Full_Name;
                        newAppointment.Patient_Name = patient.Details.Full_Name;
                        newAppointment.Description = des;

                        // Adding the new appointment to the patient's data
                        if (patient.Appointments.appts == null)
                        {
                            patient.Appointments.appts = new List<AppointmentDataforPatient>();
                        }
                        patient.Appointments.appts.Add(newAppointment);
                        string updatedJsonData = JsonConvert.SerializeObject(patientsDataList, Formatting.Indented);

                        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                        File.WriteAllText(path, updatedJsonData);

                        string filepath2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                        var jsonData2 = File.ReadAllText(filepath2);
                        List<DoctorData> doctorDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData2);
                        DoctorData doctor = doctorDataList.Find(d => d.ID == patient.Doctor.Doctor_ID.ToString());

                        AppointmentData newAppointment2 = new AppointmentData();
                        newAppointment2.Patient_ID = patient.ID;
                        newAppointment2.Patient_Name= patient.Details.Full_Name;
                        newAppointment2.Description = des;
                        newAppointment2.App_ID = apptId;
                        newAppointment2.Doctor_Name= patient.Doctor.Doctor_Full_Name;

                        // Adding the new appointment to the doctor's data
                        if (doctor.Appointments.appts == null)
                        {
                            doctor.Appointments.appts = new List<AppointmentData>();
                        }

                        //save updated data
                        doctor.Appointments.appts.Add(newAppointment2);
                        string updatedJsonData2 = JsonConvert.SerializeObject(doctorDataList, Formatting.Indented);
                        string path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                        File.WriteAllText(path2, updatedJsonData2);

                        Console.WriteLine("Appointment booked successfully");

                    }
                    else if (patient.Doctor.Doctor_ID == null)
                    {
                        //check if no doctor is assigned to the patient

                        Console.WriteLine("You are not booked with any doctor. Please choose which doctor you'd like to register with");

                        string filepath3 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                        var jsonData3 = File.ReadAllText(filepath3);
                        List<DoctorData> doctorsDataList = JsonConvert.DeserializeObject<List<DoctorData>>(jsonData3);
                        Console.WriteLine(new string('-', 100));
                        Console.WriteLine($"{"Option No.",-20} | {"Full Name",-20} | {"Address",-15} | {"Email",-30} | {"Phone",-15} |");

                        for (int i = 0; i < doctorsDataList.Count; i++)
                        {
                            Console.WriteLine($"{i+1,-20} | {doctorsDataList[i].Details.Full_Name,-20} | {doctorsDataList[i].Details.Address,-15} | {doctorsDataList[i].Details.Email,-30} | {doctorsDataList[i].Details.Phone,-15} |");
                        }
                        Console.WriteLine("Please choose a doctor");

                        if (int.TryParse(Console.ReadLine(), out int selectedDoctorNumber) && selectedDoctorNumber >= 1 && selectedDoctorNumber <= doctorsDataList.Count)
                        {
                            //check for user input for selected doctor
                            Console.Clear();
                            TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.Booking);

                            Console.WriteLine("Press ESC to return");


                            // Get the selected doctor
                            DoctorData selectedDoctor = doctorsDataList[selectedDoctorNumber - 1];

                            // Capture appointment details
                            AppointmentData newAppointment = new AppointmentData();
                            var apptIdforDoc = GenerateAppointmentIdforDoc(selectedDoctor.Appointments.appts);
                            newAppointment.App_ID = apptIdforDoc;
                            newAppointment.Doctor_Name = selectedDoctor.Details.Full_Name;
                            newAppointment.Patient_Name = patient.Details.Full_Name;
                            newAppointment.Patient_ID = patient.ID;


                            Console.WriteLine("You are booking an appointment with " + selectedDoctor.Details.Full_Name);
                            Console.WriteLine("Description of the appointment:");
                            string des = Console.ReadLine();
                            newAppointment.Description = des;

                            // Add the new appointment to the selected doctor's data
                            if (selectedDoctor.Appointments.appts == null)
                            {
                                selectedDoctor.Appointments.appts = new List<AppointmentData>();
                            }
                            selectedDoctor.Appointments.appts.Add(newAppointment);

                            //add patient to doctor's data
                            PatientData patientData = new PatientData();
                            patientData.Patient_ID=patient.ID;
                            patientData.Patient_Full_Name = patient.Details.Full_Name;
                            patientData.Patient_Address=patient.Details.Address;
                            patientData.Patient_Email= patient.Details.Email;
                            patientData.Patient_Phone= patient.Details.Phone;
                            patientData.Doctor_Name = selectedDoctor.Details.Full_Name;

                            if (selectedDoctor.Patients.patient == null)
                            {
                                selectedDoctor.Patients.patient = new List<PatientData>();
                            }

                            selectedDoctor.Patients.patient.Add(patientData);

                            // save updated appointment data
                            string updatedJsonData = JsonConvert.SerializeObject(doctorsDataList, Formatting.Indented);
                            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\DoctorDB.txt";
                            File.WriteAllText(path, updatedJsonData);

                            //update patients

                            AppointmentDataforPatient newAppointment2 = new AppointmentDataforPatient();
                            newAppointment2.App_ID = apptIdforDoc;
                            newAppointment2.Doctor_Name = selectedDoctor.Details.Full_Name;
                            newAppointment2.Patient_Name = patient.Details.Full_Name;
                            newAppointment2.Description = des;

                            // Adding the new appointment to the patient's data
                            if (patient.Appointments.appts == null)
                            {
                                patient.Appointments.appts = new List<AppointmentDataforPatient>();
                            }
                            patient.Appointments.appts.Add(newAppointment2);

                            patient.Doctor.Doctor_ID = selectedDoctor.ID;
                            patient.Doctor.Doctor_Full_Name= selectedDoctor.Details.Full_Name;
                            patient.Doctor.Doctor_Address = selectedDoctor.Details.Address;
                            patient.Doctor.Doctor_Email = selectedDoctor.Details.Email;
                            patient.Doctor.Doctor_Phone = selectedDoctor.Details.Phone;
                            patient.Doctor_Reg= selectedDoctor.Details.Full_Name;


                            string updatedJsonData2 = JsonConvert.SerializeObject(patientsDataList, Formatting.Indented);
                            string path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                            File.WriteAllText(path2, updatedJsonData2);

                            Console.WriteLine("Appointment booked successfully");

                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please select a valid doctor number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error loading patient details");
                    }
                }
                else
                {
                    Console.WriteLine("Error loading database");
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

        //generate appointment id for patient
        private string GenerateAppointmentId(List<PatientData_Pat> patientsDataList)
        {
            // Generating a new ID based on existing appointments
            int maxId = 0;
            foreach (var patient in patientsDataList)
            {
                if (patient.Appointments.appts != null)
                {
                    foreach (var appointment in patient.Appointments.appts)
                    {
                        int appointmentId;
                        if (int.TryParse(appointment.App_ID, out appointmentId))
                        {
                            if (appointmentId > maxId)
                            {
                                maxId = appointmentId;
                            }
                        }
                    }
                }
            }
            return (maxId + 1).ToString("D3"); // Format as 3-digit ID (e.g., 001, 002, ...)

        }
      
        //generate appointment id for doctor
        static string GenerateAppointmentIdforDoc(List<AppointmentData> appointments)
        {
            int maxId = 0;
            if (appointments != null)
            {
                foreach (var appointment in appointments)
                {
                    if (int.TryParse(appointment.App_ID, out int id) && id > maxId)
                    {
                        maxId = id;
                    }
                }
            }
            return (maxId + 1).ToString("D3");
        }

        //display all appointments
        private void DisplayAppointments()
        {
            try
            {
                Console.Clear();
                TopMenu(Heading.DOTNET_Hospital_Management_System, Heading.All_appts);
                Console.WriteLine("Appointments for" + patName);

                string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split("bin")[0] + "DB\\PatientDB.txt";
                var jsonData = File.ReadAllText(filepath);

                List<PatientData_Pat> patientsDataList = JsonConvert.DeserializeObject<List<PatientData_Pat>>(jsonData);
                if (patientsDataList != null)
                {
                    PatientData_Pat patient = patientsDataList.Find(p => p.ID == patId);
                    if (patient != null)
                    {
                        if (patient.Appointments.appts != null)
                        {
                            Console.WriteLine(new string('-', 85));
                            Console.WriteLine($" {"Doctor",-20} | {"Patient",-20} | {"Description",-30} |");
                            Console.WriteLine(new string('-', 85));
                            foreach (var appointment in patient.Appointments.appts)
                            {

                                Console.WriteLine($" {appointment.Doctor_Name,-20} | {appointment.Patient_Name,-20} | {appointment.Description,-30} |");
                                Console.WriteLine(new string('-', 85));


                            }
                        }
                        else
                        {
                            Console.WriteLine("You do not have any appointments");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Patient Not Found. Error loading appointment details");
                    }
                }
                else
                {
                    Console.WriteLine("Error loading database");
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

        //Exit the application
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

        //Return to login menu
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
