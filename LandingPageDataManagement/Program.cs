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

            //GetTSVFile(insData);

            //foreach (ProviderInsuranceData data in insData)
            //{
            //    string recommendationListJSON = DataUtils.GetJSON(data.CarrierVsPlanIDs);
            //    Insert.InsertToSQL(data.ProviderID, data.PracticeID, recommendationListJSON);
            //}

            CreateSQLScript(insData);
        }

        private static void CreateSQLScript(List<ProviderInsuranceData> insData)
        {
            string insertionScript = "INSERT INTO ProviderInsuranceRecommendationsCampaign (\n ProfId,PracticeId, RecommendationList)\n VALUES\n";
            int entries = 0;
            foreach (ProviderInsuranceData ins in insData)
            {
                using (System.IO.StreamWriter file = new StreamWriter("insertionScript.sql", true))
                {
                    if (entries % 500 == 0)
                    {
                        file.WriteLine("-- Inserting batch number =" + (entries / 500 + 1).ToString() + "\n");
                        file.WriteLine(insertionScript);
                    }
                   
                    file.WriteLine("(");
                    file.WriteLine(ins.ProviderID + ",");
                    file.WriteLine(ins.PracticeID + ",");
                    file.WriteLine("'" + DataUtils.GetJSON(ins.CarrierVsPlanIDs) + "'");

                    if ((entries + 1) % 500 != 0)
                        file.WriteLine("),");
                    else
                        file.WriteLine(")");

                    entries++;
    
                    
                }
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
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