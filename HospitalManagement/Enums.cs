using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyEnums
{

    public enum Role
    {
        Patient = 1,
        Doctor = 2,
        Admin = 3
    }

    public enum Heading
    {
        [Display(Name = "DOTNET Hospital Management System")]
        DOTNET_Hospital_Management_System=1,
        [Display(Name = "Login")]
        Login=2,
        [Display(Name = "Patient Menu")]
        Patient_Menu=3,
        [Display(Name = "My Details")]
        My_Details=4,
        [Display(Name = "My Doctor")]
        My_Doctor=5,
        [Display(Name = "Administrator Menu")]
        Admin_Menu=6,
        [Display(Name = "Doctor Menu")]
        Doctor_Menu=7,
        [Display(Name = "Book Appointment")]
        Booking = 8,
        [Display(Name = "All Doctors")]
        Doctors = 9,
        [Display(Name = "Doctor Details")]
        Doctor_details = 10,
        [Display(Name = "All Patients")]
        Patients = 11,
        [Display(Name = "Patient Details")]
        Patient_details = 12,
        [Display(Name = "My Patients")]
        My_Patients = 13,
        [Display(Name = "All appointments")]
        All_appts = 14,
        [Display(Name = "Check Patient Details")]
        Chk_pat_details = 15,
        [Display(Name = "Appointments with")]
        Pat_appts = 16,
        [Display(Name = "Add Doctor")]
        Add_doc = 17,
        [Display(Name = "Add Patient")]
        Add_pat= 18
    }
   
   


}


