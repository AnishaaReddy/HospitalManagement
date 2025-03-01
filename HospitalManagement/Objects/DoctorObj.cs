using MyEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Objects
{

    //this namespace stores all of the doctor attributes of json as objects
    class DoctorData
    {
        public string ID { get; set; }
        public DoctorDetails Details { get; set; }
        public PatientsData Patients { get; set; }
        public AppointmentsData Appointments { get; set; }

        public DoctorData()
        {
            Details = new DoctorDetails();
            Patients = new PatientsData();
            Appointments = new AppointmentsData();
        }
    }

    class DoctorDetails
    {
        public string Full_Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    class PatientsData
    {
        public List<PatientData> patient { get; set; }


    }

    class PatientData
    {
        public string Patient_ID { get; set; }
        public string Patient_Full_Name { get; set; }
        public string Patient_Address { get; set; }
        public string Patient_Email { get; set; }
        public string Patient_Phone { get; set; }
        public string Doctor_Name { get; set; }
    }

    class AppointmentsData
    {
        public List<AppointmentData> appts { get; set; }
    }

    class AppointmentData
    {
        public string App_ID { get; set; }
        public string Patient_ID { get; set; }
        public string Doctor_Name { get; set; }
        public string Patient_Name { get; set; }
        public string Description { get; set; }
    }


    class LoginUser
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public string Name { get; set; }

        public LoginUser()
        {
            UserRole = new Role();
        }
    }

}