using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LandingPageDataManagement.Data
{
    public class ProviderInsuranceData
    {
        private uint providerID;
        private uint practiceID;
        public Dictionary<string, List<int>> CarrierVsPlanIDs { get; set; }

        public uint ProviderID
        {
            get => providerID;
            set
            {
                if (value > 0)
                    providerID = value;
                else
                {
                    throw new Exception("Provider ID can't be negative");
                }
            }
        }

        public uint PracticeID
        {
            get => practiceID;
            set
            {
                if (value > 0)
                    practiceID = value;
                else
                {
                    throw new Exception("Practice ID can't be negative");
                }
            }
        }
    }
}
