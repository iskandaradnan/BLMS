using BLMS.Models.License;
using BLMS.v2.Context;
using BLMS.v2.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Context
{
    public class LicenseDBContext
    {
        //readonly string connectionstring = "Data Source=EGBS11N10043471;Database=BLMS;User ID = sa; Password=P@ss1234; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly string connectionstring = "Data Source = 10.249.1.125; Database=BLMSDev;User ID = Appsa; Password=Opuswebsql2017;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly AuditLogDbContext auditDbContext = new AuditLogDbContext();


        #region LICENSE SITE
        #region GRIDVIEW
        public IEnumerable<LicenseSite> LicenseSiteGetAll()
        {
            var licenseSiteList = new List<LicenseSite>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseSiteGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var licenseSite = new LicenseSite();

                    licenseSite.LicenseID = Convert.ToInt32(dr["LicenseID"].ToString());
                    licenseSite.LicenseName = dr["LicenseName"].ToString();
                    licenseSite.CategoryName = dr["CategoryName"].ToString();
                    licenseSite.UnitName = dr["UnitName"].ToString();
                    licenseSite.IssuedDT = dr["IssuedDT"].ToString();
                    licenseSite.ExpiredDT = dr["ExpiredDT"].ToString();
                    licenseSite.PIC1Name = dr["PIC1Name"].ToString();
                    licenseSite.PIC2Name = dr["PIC2Name"].ToString();
                    licenseSite.PIC3Name = dr["PIC3Name"].ToString();
                    licenseSite.isRegistered = Convert.ToBoolean(dr["isRegistered"].ToString());
                    licenseSite.isRenewed = Convert.ToBoolean(dr["isRenewed"].ToString());

                    licenseSite.RenewReminderDT = Convert.ToDateTime(dr["RenewReminderDT"].ToString());

                    licenseSite.hasFile = Convert.ToBoolean(dr["hasFile"].ToString());

                    licenseSiteList.Add(licenseSite);
                }

                conn.Close();
            }

            return licenseSiteList;
        }
        #endregion

        #region REGISTER
        public void RegisterLicenseSite(LicenseSite licenseSite, string Issued, string Expired, string UserName)
        {
                            AuditLog auditLog = new AuditLog();
                auditLog.Command = "CREATE";
                auditLog.ScreenPath = "REGISTER LICENSE SITE";
                auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseSiteRegister", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryID", licenseSite.CategoryID);
                cmd.Parameters.AddWithValue("DivID", licenseSite.DivID);
                cmd.Parameters.AddWithValue("UnitID", licenseSite.UnitID);
                cmd.Parameters.AddWithValue("LicenseName", licenseSite.LicenseName);
                cmd.Parameters.AddWithValue("RegistrationNo", licenseSite.RegistrationNo);
                cmd.Parameters.AddWithValue("SerialNo", licenseSite.SerialNo);
                cmd.Parameters.AddWithValue("IssuedDT", Issued);
                cmd.Parameters.AddWithValue("ExpiredDT", Expired);
                cmd.Parameters.AddWithValue("PIC1Name", licenseSite.PIC1Name);
                cmd.Parameters.AddWithValue("PIC2StaffNo", licenseSite.PIC2StaffNo);
                cmd.Parameters.AddWithValue("PIC3StaffNo", licenseSite.PIC3StaffNo);
                cmd.Parameters.AddWithValue("Remarks", licenseSite.Remarks);

                //License File
                cmd.Parameters.AddWithValue("LicenseFileName", licenseSite.LicenseFileName);
                cmd.Parameters.AddWithValue("FileType", licenseSite.FileType);
                cmd.Parameters.AddWithValue("Extension", licenseSite.Extension);
                cmd.Parameters.AddWithValue("Data", licenseSite.Data);

                cmd.Parameters.AddWithValue("UserName", UserName);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region RENEWAL
        public void RenewalLicenseSite(LicenseSite licenseSite, string UserName)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseSiteRenewal", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryID", licenseSite.CategoryID);
                cmd.Parameters.AddWithValue("DivID", licenseSite.DivID);
                cmd.Parameters.AddWithValue("UnitID", licenseSite.UnitID);
                cmd.Parameters.AddWithValue("LicenseName", licenseSite.LicenseName);
                cmd.Parameters.AddWithValue("RegistrationNo", licenseSite.RegistrationNo);
                cmd.Parameters.AddWithValue("SerialNo", licenseSite.SerialNo);
                cmd.Parameters.AddWithValue("IssuedDT", licenseSite.IssuedDT);
                cmd.Parameters.AddWithValue("ExpiredDT", licenseSite.ExpiredDT);
                cmd.Parameters.AddWithValue("PIC1Name", licenseSite.PIC1Name);
                cmd.Parameters.AddWithValue("PIC2StaffNo", licenseSite.PIC2StaffNo);
                cmd.Parameters.AddWithValue("PIC3StaffNo", licenseSite.PIC3StaffNo);
                cmd.Parameters.AddWithValue("Remarks", licenseSite.Remarks);

                //License File
                cmd.Parameters.AddWithValue("LicenseFileName", licenseSite.LicenseFileName);
                cmd.Parameters.AddWithValue("FileType", licenseSite.FileType);
                cmd.Parameters.AddWithValue("Extension", licenseSite.Extension);
                cmd.Parameters.AddWithValue("Data", licenseSite.Data);

                cmd.Parameters.AddWithValue("UserName", UserName);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET LICENSE BY ID
        //License By ID
        public LicenseSite GetLicenseSiteByID(int? id)
        {
            var licenseSite = new LicenseSite();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseSiteGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("LicenseID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    licenseSite.LicenseName = dr["LicenseName"].ToString();
                    licenseSite.CategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                    licenseSite.CategoryName = dr["CategoryName"].ToString();
                    licenseSite.RegistrationNo = dr["RegistrationNo"].ToString();
                    licenseSite.SerialNo = dr["SerialNo"].ToString();
                    licenseSite.DivID = Convert.ToInt32(dr["DivID"].ToString());
                    licenseSite.DivName = dr["DivName"].ToString();
                    licenseSite.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                    licenseSite.UnitName = dr["UnitName"].ToString();
                    licenseSite.IssuedDT = dr["IssuedDT"].ToString();
                    licenseSite.ExpiredDT = dr["ExpiredDT"].ToString();
                    licenseSite.PIC1StaffNo = dr["PIC1StaffNo"].ToString();
                    licenseSite.PIC1Name = dr["PIC1Name"].ToString();
                    licenseSite.PIC1Email = dr["PIC1Email"].ToString();
                    licenseSite.PIC2StaffNo = dr["PIC2StaffNo"].ToString();
                    licenseSite.PIC2Name = dr["PIC2Name"].ToString();
                    licenseSite.PIC2Email = dr["PIC2Email"].ToString();
                    licenseSite.PIC3StaffNo = dr["PIC3StaffNo"].ToString();
                    licenseSite.PIC3Name = dr["PIC3Name"].ToString();
                    licenseSite.PIC3Email = dr["PIC3Email"].ToString();
                    licenseSite.Remarks = dr["Remarks"].ToString();
                    licenseSite.isRegistered = Convert.ToBoolean(dr["isRegistered"].ToString());
                    licenseSite.isRenewed = Convert.ToBoolean(dr["isRenewed"].ToString());

                    licenseSite.LicenseFileName = dr["LicenseFileName"].ToString();
                    licenseSite.FileType = dr["FileType"].ToString();
                    licenseSite.Extension = dr["Extension"].ToString();
                    licenseSite.Data = (byte[])dr["Data"];

                    licenseSite.RenewReminderDT = Convert.ToDateTime(dr["RenewReminderDT"].ToString());

                    licenseSite.UserNameStaffNo = dr["CreatedBY"].ToString();
                }

                conn.Close();
            }

            return licenseSite;
        }
        #endregion

        #region CHECK DUPLICATE LICENSE SITE NAME
        public LicenseSite CheckLicenseSiteByName(string LicenseName)
        {
            var licenseSite = new LicenseSite();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseSiteCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("LicenseName", LicenseName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    licenseSite.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return licenseSite;
        }
        #endregion
        #endregion

        #region LICENSE HQ
        #region GRIDVIEW
        public IEnumerable<LicenseHQ> LicenseHQGetAll()
        {
            var licenseHQList = new List<LicenseHQ>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseHQGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var licenseHQ = new LicenseHQ();

                    licenseHQ.LicenseID = Convert.ToInt32(dr["LicenseID"].ToString());
                    licenseHQ.LicenseName = dr["LicenseName"].ToString();
                    licenseHQ.CategoryName = dr["CategoryName"].ToString();
                    licenseHQ.UnitName = dr["UnitName"].ToString();
                    licenseHQ.IssuedDT = dr["IssuedDT"].ToString();
                    licenseHQ.ExpiredDT = dr["ExpiredDT"].ToString();
                    licenseHQ.PIC1Name = dr["PIC1Name"].ToString();
                    licenseHQ.PIC2Name = dr["PIC2Name"].ToString();
                    licenseHQ.PIC3Name = dr["PIC3Name"].ToString();
                    licenseHQ.isRequested = Convert.ToBoolean(dr["isRequested"].ToString());
                    licenseHQ.isApproved = Convert.ToBoolean(dr["isApproved"].ToString());
                    licenseHQ.isRegistered = Convert.ToBoolean(dr["isRegistered"].ToString());
                    licenseHQ.isRenewed = Convert.ToBoolean(dr["isRenewed"].ToString());

                    licenseHQ.RenewReminderDT = Convert.ToDateTime(dr["RenewReminderDT"].ToString());

                    licenseHQ.hasFile = Convert.ToBoolean(dr["hasFile"].ToString());

                    licenseHQList.Add(licenseHQ);
                }

                conn.Close();
            }

            return licenseHQList;
        }
        #endregion

        #region REQUEST
        //License Request
        public void RequestLicenseHQ(LicenseHQ licenseHQ, string UserName)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseHQRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryID", licenseHQ.CategoryID);
                cmd.Parameters.AddWithValue("DivID", licenseHQ.DivID);
                cmd.Parameters.AddWithValue("UnitID", licenseHQ.UnitID);
                cmd.Parameters.AddWithValue("LicenseName", licenseHQ.LicenseName);
                cmd.Parameters.AddWithValue("PIC1Name", licenseHQ.PIC1Name);
                cmd.Parameters.AddWithValue("PIC2StaffNo", licenseHQ.PIC2StaffNo);
                cmd.Parameters.AddWithValue("PIC3StaffNo", licenseHQ.PIC3StaffNo);
                cmd.Parameters.AddWithValue("Remarks", licenseHQ.Remarks);

                cmd.Parameters.AddWithValue("UserName", UserName);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET LICENSE BY ID
        //Get License HQ By ID
        public LicenseHQ GetLicenseHQByID(int? id)
        {
            var licenseHQ = new LicenseHQ();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseHQGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("LicenseID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    licenseHQ.LicenseName = dr["LicenseName"].ToString();
                    licenseHQ.CategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                    licenseHQ.CategoryName = dr["CategoryName"].ToString();
                    licenseHQ.DivID = Convert.ToInt32(dr["DivID"].ToString());
                    licenseHQ.DivName = dr["DivName"].ToString();
                    licenseHQ.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                    licenseHQ.UnitName = dr["UnitName"].ToString();

                    licenseHQ.RegistrationNo = dr["RegistrationNo"].ToString();
                    licenseHQ.SerialNo = dr["SerialNo"].ToString();

                    licenseHQ.PIC1StaffNo = dr["PIC1StaffNo"].ToString();
                    licenseHQ.PIC1Name = dr["PIC1Name"].ToString();
                    licenseHQ.PIC1Email = dr["PIC1Email"].ToString();

                    licenseHQ.PIC2StaffNo = dr["PIC2StaffNo"].ToString();
                    licenseHQ.PIC2Name = dr["PIC2Name"].ToString();
                    licenseHQ.PIC2Email = dr["PIC2Email"].ToString();
                    licenseHQ.PIC3StaffNo = dr["PIC3StaffNo"].ToString();
                    licenseHQ.PIC3Name = dr["PIC3Name"].ToString();
                    licenseHQ.PIC3Email = dr["PIC3Email"].ToString();
                    licenseHQ.Remarks = dr["Remarks"].ToString();

                    licenseHQ.isRequested = Convert.ToBoolean(dr["isRequested"].ToString());
                    licenseHQ.isApproved = Convert.ToBoolean(dr["isApproved"].ToString());
                    licenseHQ.isRegistered = Convert.ToBoolean(dr["isRegistered"].ToString());
                    licenseHQ.isRenewed = Convert.ToBoolean(dr["isRenewed"].ToString());

                    licenseHQ.LicenseFileName = dr["LicenseFileName"].ToString();
                    licenseHQ.FileType = dr["FileType"].ToString();
                    licenseHQ.Extension = dr["Extension"].ToString();
                    licenseHQ.Data = (byte[])dr["Data"];

                    licenseHQ.RenewReminderDT = Convert.ToDateTime(dr["RenewReminderDT"].ToString());
                }

                conn.Close();
            }

            return licenseHQ;
        }
        #endregion

        #region CHECK DUPLICATE LICENSE HQ
        //Check Duplication in Request new license
        public LicenseHQ CheckLicenseByName(string LicenseName)
        {
            var licenseHQ = new LicenseHQ();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseHQCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("LicenseName", LicenseName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    licenseHQ.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return licenseHQ;
        }
        #endregion
        #endregion

        #region LICENSE ADMIN
        #region GRIDVIEW
        public IEnumerable<LicenseAdmin> LicenseAdminGetAll()
        {
            var licenseAdminList = new List<LicenseAdmin>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spLicenseAdminGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var licenseAdmin = new LicenseAdmin();

                    licenseAdmin.LicenseID = Convert.ToInt32(dr["LicenseID"].ToString());
                    licenseAdmin.LicenseName = dr["LicenseName"].ToString();
                    licenseAdmin.CategoryName = dr["CategoryName"].ToString();
                    licenseAdmin.UnitName = dr["UnitName"].ToString();
                    licenseAdmin.IssuedDT = dr["IssuedDT"].ToString();
                    licenseAdmin.ExpiredDT = dr["ExpiredDT"].ToString();
                    licenseAdmin.PIC1Name = dr["PIC1Name"].ToString();
                    licenseAdmin.PIC2Name = dr["PIC2Name"].ToString();
                    licenseAdmin.PIC3Name = dr["PIC3Name"].ToString();
                    licenseAdmin.isRequested = Convert.ToBoolean(dr["isRequested"].ToString());
                    licenseAdmin.isApproved = Convert.ToBoolean(dr["isApproved"].ToString());
                    licenseAdmin.isRegistered = Convert.ToBoolean(dr["isRegistered"].ToString());
                    licenseAdmin.isRenewed = Convert.ToBoolean(dr["isRenewed"].ToString());

                    licenseAdmin.RenewReminderDT = Convert.ToDateTime(dr["RenewReminderDT"].ToString());

                    licenseAdmin.hasFile = Convert.ToBoolean(dr["hasFile"].ToString());

                    licenseAdmin.UserType = dr["UserType"].ToString();

                    licenseAdminList.Add(licenseAdmin);
                }

                conn.Close();
            }

            return licenseAdminList;
        }
        #endregion
        #endregion
    }
}
