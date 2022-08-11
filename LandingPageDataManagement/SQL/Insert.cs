using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;

namespace LandingPageDataManagement.SQL
{
    public class Insert
    {
        private static string sqlConnectionString = @"Data Source=localhost;Initial Catalog=zocdoc;Integrated Security=True";
        
         
        public static void InsertToSQL(uint providerID, uint practiceID, string recommendationList)
        {
            SqlConnection con = new SqlConnection(sqlConnectionString);
            con.Open();
            SqlCommand cmd = new 
                SqlCommand("INSERT INTO ProviderInsuranceRecommendationsCampaign (ProfId, PracticeId, RecommendationList) VALUES (@profID,@practiceID,@recList)", con);

            cmd.Parameters.Add("@profID", System.Data.SqlDbType.BigInt);
            cmd.Parameters["@profID"].Value = providerID;

            cmd.Parameters.Add("@practiceID", System.Data.SqlDbType.BigInt);
            cmd.Parameters["@practiceID"].Value = practiceID;

            cmd.Parameters.Add("@recList", System.Data.SqlDbType.NVarChar);
            cmd.Parameters["@recList"].Value = recommendationList;
            cmd.ExecuteNonQuery();


        }

        private static void InsertData(uint providerId, uint practiceId, string recommendationList)
        {
            throw new NotImplementedException();
        }

        private static SqlConnection ConnectToSQL()
        {
            return null;
        }

        /*
         * Queries
         * CREATE TABLE ProviderInsuranceRecommendationsCampaign (
        ProfId bigint,
        PracticeId bigint,
        RecommendationList nvarchar(max),
    );
         */
    }
}
