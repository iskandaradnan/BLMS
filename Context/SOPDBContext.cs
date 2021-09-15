using BLMS.Models.Admin;
using BLMS.Models.SOP;
using BLMS.v2.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Context
{
    public class SOPDBContext
    {
        //readonly string connectionstring = "Data Source=EGBS11N10043471;Database=BLMS;User ID = sa; Password=P@ss1234; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly string connectionstring = "Data Source = 10.249.1.125; Database=BLMSDev;User ID = Appsa; Password=Opuswebsql2017;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly AuditLogDbContext auditDbContext = new AuditLogDbContext();

        #region AUTHORITY LINK
        #region GRIDVIEW
        public IEnumerable<Authority> AuthorityGetAll()
        {
            var authorityList = new List<Authority>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuthorityLinkGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var authority = new Authority();

                    authority.AuthorityID = Convert.ToInt32(dr["AuthorityID"].ToString());
                    authority.AuthorityName = dr["AuthorityName"].ToString();
                    authority.AuthorityLink = dr["AuthorityLink"].ToString();

                    authorityList.Add(authority);
                }

                conn.Close();
            }

            return authorityList;
        }
        #endregion

        #region CREATE
        public void AddAuthority(Authority authority, string UserName)
        {

            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "AUTHORITY";
            auditLog.CreatedBy = UserName;


            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuthorityLinkAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("AuthorityName", authority.AuthorityName);
                cmd.Parameters.AddWithValue("AuthorityLink", authority.AuthorityLink);
                cmd.Parameters.AddWithValue("UserName", UserName);


                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "\nAuthority Name:" + authority.AuthorityName + "\nAuthority Link:" + authority.AuthorityLink + "\nUsername:" + UserName;

                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        public void EditAuthority(Authority authority, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "AUTHORITY";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuthorityLinkEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("AuthorityID", authority.AuthorityID);
                cmd.Parameters.AddWithValue("AuthorityName", authority.AuthorityName);
                cmd.Parameters.AddWithValue("AuthorityLink", authority.AuthorityLink);
                cmd.Parameters.AddWithValue("UserName", UserName);


                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = authority.OldAuthorityName + authority.OldAuthorityLink;
                auditLog.NewValue = "Authority Name:" + authority.AuthorityName + ",\nAuthority Link:" + authority.AuthorityLink;

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DeleteAuthority(int? id, string AuthorityName, string UserName)
        {

            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "AUTHORITY";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuthorityLinkDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("AuthorityID", id);


                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = AuthorityName;
                auditLog.NewValue = "-";


                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET AUTHORITY BY ID
        public Authority GetAuthorityByID(int? id)
        {
            var authority = new Authority();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuthorityLinkGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("AuthorityID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    authority.AuthorityID = Convert.ToInt32(dr["AuthorityID"].ToString());
                    authority.AuthorityName = dr["AuthorityName"].ToString();
                    authority.AuthorityLink = dr["AuthorityLink"].ToString();
                    authority.OldAuthorityName = dr["AuthorityName"].ToString();
                    authority.OldAuthorityLink = dr["AuthorityLink"].ToString();
                }

                conn.Close();
            }

            return authority;
        }
        #endregion

        #region CHECK EXISTING AUTHORITY
        public Authority CheckAuthorityByName(string AuthorityName)
        {
            var authority = new Authority();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAuthorityLinkCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("AuthorityName", AuthorityName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    authority.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return authority;
        }
        #endregion
        #endregion
    }
}
