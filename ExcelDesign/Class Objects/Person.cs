using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Person
    {
        private string name;
        private string surname;

        public Person(string nameP, string surnameP)
        {
            this.Name = nameP;
            this.surname = surnameP;
        }

        public Person()
        {

        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}