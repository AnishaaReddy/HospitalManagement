using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Objects
{
    //this namespace stores all of the patient attributes of json as objects
    class PatientData_Pat
    {
        public string ID { get; set; }
        public PatientDetails Details { get; set; }
        public Doctor Doctor { get; set; }
        public Appointments Appointments { get; set; }
        public string Doctor_Reg { get; set; }

        public PatientData_Pat()
        {
            Details = new PatientDetails();
            Doctor = new Doctor();
            Appointments = new Appointments();
        }
    }

    class PatientDetails
    {
        public string Full_Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    class Doctor
    {
        public string Doctor_ID { get; set; }
        public string Doctor_Full_Name { get; set; }
        public string Doctor_Address { get; set; }
        public string Doctor_Email { get; set; }
        public string Doctor_Phone { get; set; }
    }

    class Appointments
    {
        public List<AppointmentDataforPatient> appts { get; set; }
    }

    class AppointmentDataforPatient
    {
        public string App_ID { get; set; }
        public string Doctor_Name { get; set; }
        public string Description { get; set; }
        public string Patient_Name { get; set; }
    }
}
