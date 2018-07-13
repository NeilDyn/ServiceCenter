using ExcelDesign.ServiceFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string SessionID { get; set; }
        public bool CreateRMA { get; set; }
        public bool CreateReturnLabel { get; set; }
        public bool Admin { get; set; }
        public bool Developer { get; set; }
        public string LastPasswordUpdate { get; set; }

        public User(UserSetup us)
        {
            UserID = us.User[0].UserID;
            //Password = us.User[0].Password;
            SessionID = Convert.ToString(Guid.NewGuid());

            CreateRMA = us.User[0].CreateRMA.ToUpper() == "YES" ? true : false;
            CreateReturnLabel = us.User[0].CreateRetLabel.ToUpper() == "YES" ? true : false;
            Admin = us.User[0].Admin.ToUpper() == "YES" ? true : false;
            Developer = us.User[0].Developer.ToUpper() == "YES" ? true : false;

            //LastPasswordUpdate = us.User[0].LastPasswordUpdate;
        }

        public User()
        {

        }
    }
}