using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace InspectIT.srvAPI
{
    /// <summary>
    /// Summary description for _1srv_getfrmData
    /// </summary>
    public class _1srv_getfrmData : IHttpHandler
    {

        readonly JsonSerializer serializer = new JsonSerializer();

        public sealed class SerializerWrapper
        {
            readonly JsonSerializer serializer = new JsonSerializer();

            public void Serialize(Stream ms, object obj)
            {
                var jsonTextWriter = new JsonTextWriter(new StreamWriter(ms));
                serializer.Serialize(jsonTextWriter, obj);
                jsonTextWriter.Flush();
                ms.Position = 0;
            }

            public TType Deserialize<TType>(Stream ms)
            {
                var jsonTextReader = new JsonTextReader(new StreamReader(ms));
                return serializer.Deserialize<TType>(jsonTextReader);
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            
            if (String.IsNullOrEmpty(context.Request["did"]))
            {
                context.Response.End();
            }

            Global DLdb = new Global();
            DLdb.DB_Connect();

            var json_data = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormUserData where DataID = '" + context.Request["did"] + "'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            DataTable schemaTable = theSqlDataReader.GetSchemaTable();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                json_data = theSqlDataReader["json"].ToString().Replace("[", "").Replace("]", "");

            }
            else
            {
                json_data = "{}";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();

            //json_data = serializer.Serialize(json_data);

            context.Response.ContentType = "application/json";
            context.Response.Write(json_data);

        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}