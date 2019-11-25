using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace HTTP5101_School_System
{
    public class Student
    {
        /*
         These fields are private cannot be accessed normally
         will NOT be able to 
            set like Student.Fname = "Christine"
            get like Student.Fname ==> returns "Christine"
         */
        private string Fname;
        private string Lname;
        private string Number;
        private DateTime EnrolDate;

        /*
         we can build special methods which get and set values for our class
         the methods are public, but the fields themselves are private.
         this technique is known as "encapsulation"
        */

        //methods which return the private content requested
        //if we want the firstname we use
        //student.GetFname(); ==> returns "Christine"
        public string GetFname()
        {
            return Fname;
        }
        public string GetLname()
        {
            return Lname;
        }
        public string GetNumber()
        {
            return Number;
        }
        public DateTime GetEnrolDate()
        {
            return EnrolDate;
        }

        //These methods are used to set values in an object
        //i.e. if I want to change the last name to Bittle
        //student.SetLname("Bittle")
        public void SetFname(string value)
        {
            Fname = value;
        }
        public void SetLname(string value)
        {
            Lname = value;
        }
        public void SetNumber(string value)
        {
            Number = value;
        }
        public void SetEnrolDate(DateTime value)
        {
            
            EnrolDate = value;
        }

    }
}