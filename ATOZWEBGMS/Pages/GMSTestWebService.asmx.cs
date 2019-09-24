using System;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace ATOZWEBGMS.Pages
{
    /// <summary>
    /// Summary description for GMSTestWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GMSTestWebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public string[] GetPartyName(string prefix)
        {
            List<string> party = new List<string>();

            string conStrig = "Data Source =  (local) ; Initial Catalog = A2ZACGMS; User Id =; Password =; Integrated Security = SSPI";

            using (SqlConnection con = new SqlConnection(conStrig))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetPartyName", con);

                cmd.Parameters.AddWithValue("@strValue", prefix);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    party.Add(string.Format("{0}-{1}", rdr["LPartyName"].ToString(), rdr["LPartyCode"].ToString()));
                }
            }

            return party.ToArray();
        }

    }
}
