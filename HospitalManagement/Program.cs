using HospitalManagement.Users;
using MyEnums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    public class Program 
    {
        

        public static void Main(string[] args)
        {
            try
            {
                //call the login functionality on launching the application

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
