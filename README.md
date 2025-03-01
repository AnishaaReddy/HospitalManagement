# HospitalManagement
This is a Hospital Management system created using C# console application in Microsoft visual studio. The data is stored in json format in txt files.

DOT NET HOSPITAL MANAGEMENT SYSTEM

There are 3 core roles: Patient, Doctor and Admin. Each of these roles have their own version of the menu with it’s own subset of menu options. 

Specification and basic functionality to check out:

1) Console design  
2) Login Menu
3) Patient Menu : List Patient Details , List My Doctor Details , List All Appointments , Book Appointment, functionality including creating
Appointment object and writing to txt file
4) Doctor Menu: List Doctor Details , List Patients , List Appointments , Check Particular Patient , List Appointments With Patient
5) Admin Menu: List All Doctors , Check Doctor Details, List All Patients, Check Patient Details , Add Doctor, Add Patient
6) Logout
7) Abstraction and Inheritance and other strong OOP concepts are followed.

Users:
Following are the users with login credentials and specific assigned roles:
1.	Patient: 
UserID: 111
Password: 123P

2.	Doctor:
UserID: 112
Password: 123D

3.	Admin:
UserID: 113
Password: 123A


The above passwords are also the default passwords when you create users with respective roles (“123D” for Doctor and “123P” for Patient).
The data for this project is saved under the DB folder. The format used is JSON which is easy to read or update.

Enums:
There are numerical enums defined for roles as following which is saved in Login.txt
Patient =1;
Doctor =2;
Admin = 3;

Database:
Following are the txt files that save data:
1)	Login txt: It has login details of every user and their specified roles.
2)	PatientDB.txt: It stores all the patient details with their assigned doctors and appointment details.
3)	DoctorDB.txt: It stores all the doctor details and their patient’s info like appointments.


