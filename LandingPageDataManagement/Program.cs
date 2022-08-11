using LandingPageDataManagement.Data;
using LandingPageDataManagement.SQL;
using System.Text;

namespace LandingPageDataManagement
{
    internal class Program
    {
        private static string filePath = "recommentationData.tsv";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            DataReader reader = new DataReader();
            reader.ReadData();
            reader.PopulateData();

            //Collect output
            List<ProviderInsuranceData> insData = reader.ProviderInsData;

            GetTSVFile(insData);
            foreach (ProviderInsuranceData data in insData)
            {
                string recommendationListJSON = DataUtils.GetJSON(data.CarrierVsPlanIDs);
                Insert.InsertToSQL(data.ProviderID, data.PracticeID, recommendationListJSON);
            }
        }

        static void GetTSVFile(List<ProviderInsuranceData> data)
        {
            
            int row = 0;
            
            foreach (ProviderInsuranceData insData in data)
            {
                string recommendationListJSON = DataUtils.GetJSON(insData.CarrierVsPlanIDs);
                
                using(System.IO.StreamWriter file = new System.IO.StreamWriter("recommendationList_TSV.tsv", true))
                {
                    if(row++ == 0)
                        file.WriteLine("ProfessionalId" + "\t" + "PracticeId" + "\t" + "RecommendationList");

                    file.WriteLine(insData.ProviderID.ToString() + "\t" + insData.PracticeID.ToString() + "\t" + recommendationListJSON);

                }
                //tsvContent.AppendLine(insData.ProviderID.ToString() + '\t' + insData.PracticeID.ToString() + '\t' + recommendationListJSON);
               
            }
            
        }
    }
}