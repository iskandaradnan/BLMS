using BLMS.v2.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.v2.Context
{
    public class AuditLogDbContext
    {
        readonly string connectionstring = "Data Source = 10.249.1.125; Database=BLMSDev;User ID = Appsa; Password=Opuswebsql2017;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    
        public IEnumerable<AuditLog> GetAuditLog()
        {
            var AuditLogList = new List<AuditLog>();


            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuditLogGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var auditList = new AuditLog();

                    auditList.Command = dr["Command"].ToString();
                    auditList.SPName = dr["SPName"].ToString();
                    auditList.ScreenPath = dr["ScreenPath"].ToString();
                    auditList.OldValue = dr["OldValue"].ToString();
                    auditList.NewValue = dr["NewValue"].ToString();
                    auditList.CreatedBy = dr["CreatedBy"].ToString();
                    auditList.CreatedDt = dr["CreatedDt"].ToString();
                    AuditLogList.Add(auditList);

                }

                conn.Close();
            }

            return AuditLogList;
        }

        //Create Audit Log
        public void AddAuditLog(AuditLog auditlist)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuditLogAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Command", auditlist.Command);
                cmd.Parameters.AddWithValue("SPName", auditlist.SPName);
                cmd.Parameters.AddWithValue("ScreenPath", auditlist.ScreenPath);
                cmd.Parameters.AddWithValue("OldValue", auditlist.OldValue);
                cmd.Parameters.AddWithValue("NewValue", auditlist.NewValue);
                cmd.Parameters.AddWithValue("CreatedBy", auditlist.CreatedBy);


                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}
