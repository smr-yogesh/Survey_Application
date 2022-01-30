using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Application
{
    class Survey
    {
        string survey;

        public Boolean SetSurvey(string sr)
        {
            if(survey == null)
            {
                survey = sr;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return survey;
        }
    }
}
