using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Application
{
    public class Manager
    {
        private string managerkey = "manager123";
        private string managerID;
        private string managerpasssword;
       
        public Manager() { }
        public void SetID(string id)
        {
            managerID = id;
        }
        public void SetPassword(string pass)
        {
            managerpasssword = pass;
        }
        
        public bool CheckManagerKey(string mk)
        {
            if (mk == managerkey)
            {
                return true;
            }

            return false;
        }

        public Boolean IDPassConform()
        {
            if (managerID != "" && managerpasssword != "")
            {
                return true;
            }

            return false;
        }

        public Boolean CheckIDPass(string id, string pass)
        {
            if(managerID == id && managerpasssword == pass)
            {
                return true;
            }

            return false;
        }

        public bool checkManagerAccount(string id, string pass)
        {
            if (managerID == id && managerpasssword == pass)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return managerID;
        }
    }
}
