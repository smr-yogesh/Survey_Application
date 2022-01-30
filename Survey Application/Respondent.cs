using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Application
{
    class Respondent
    {
        private string ID;

        public void setId(string id)
        {
            ID = id;
        }

        public Boolean IDConform()
        {
            if (ID != "")
            {
                return true;
            }

            return false;
        }
        public override string ToString()
        {
            return ID;
        }

    }
}
