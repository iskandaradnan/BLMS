﻿using BLMS.Models.Admin;
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
    public class AdminDBContext
    {
        //readonly string connectionstring = "Data Source=EGBS11N10043471;Database=BLMS;User ID = sa; Password=P@ss1234; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly string connectionstring = "Data Source = 10.249.1.125; Database=BLMSDev;User ID = Appsa; Password=Opuswebsql2017;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly AuditLogDbContext auditDbContext = new AuditLogDbContext();


        #region BUSINESS DIVISION
        #region GRIDVIEW
        public IEnumerable<BusinessDiv> BusinessDivGetAll()
        {
            var businessDivList = new List<BusinessDiv>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var businessDiv = new BusinessDiv();

                    businessDiv.DivID = Convert.ToInt32(dr["DivID"].ToString());
                    businessDiv.DivName = dr["DivName"].ToString();

                    businessDivList.Add(businessDiv);
                }

                conn.Close();
            }

            return businessDivList;
        }
        #endregion

        #region CREATE
        public void AddBusinessDiv(BusinessDiv businessDiv, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "BUSINESS DIVISION";
            auditLog.CreatedBy = UserName;


            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivName", businessDiv.DivName);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "DivID: " + businessDiv.DivID + ",\nUserName:" + UserName;

                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        public void EditBusinessDiv(BusinessDiv businessDiv, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "BUSINESS DIVISION";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivID", businessDiv.DivID);
                cmd.Parameters.AddWithValue("DivName", businessDiv.DivName);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = businessDiv.OldDivName;
                auditLog.NewValue = "DivID: " + businessDiv.DivID + ",\nDiv Name:" + businessDiv.DivName + "\nUsername:" + UserName;

                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DeleteBusinessDiv(int? id, string DivName, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "BUSINESS UNIT";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivID", id);

                auditLog.SPName = cmd.CommandText.ToString(); 

                auditLog.OldValue = DivName;
                auditLog.NewValue = "-";



                auditDbContext.AddAuditLog(auditLog);


                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET BUSINESS DIVISION BY ID
        public BusinessDiv GetBusinessDivByID(int? id)
        {
            var businessDiv = new BusinessDiv();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    businessDiv.DivID = Convert.ToInt32(dr["DivID"].ToString());
                    businessDiv.DivName = dr["DivName"].ToString();
                    businessDiv.OldDivName = dr["DivName"].ToString();

                }

                conn.Close();
            }

            return businessDiv;
        }
        #endregion

        #region CHECK EXISTING BUSINESS DIVISION
        public BusinessDiv CheckBusinessDivByName(string DivName)
        {
            var businessDiv = new BusinessDiv();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivName", DivName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    businessDiv.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return businessDiv;
        }
        #endregion

        #region CHECK BUSINESS UNIT (LINKED) BEFORE DELETE
        public BusinessDiv CheckLinkedBusinessUnitByName(string DivName)
        {
            var businessDiv = new BusinessDiv();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessDivCheckLinkedBusinessUnit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivName", DivName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    businessDiv.LinkedData = Convert.ToInt32(dr["LinkedData"].ToString());
                }

                conn.Close();
            }

            return businessDiv;
        }
        #endregion
        #endregion

        #region BUSINESS UNIT
        #region GRIDVIEW
        public IEnumerable<BusinessUnit> BusinessUnitGetAll()
        {
            var businessUnitList = new List<BusinessUnit>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessUnitGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var businessUnit = new BusinessUnit();

                    businessUnit.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                    businessUnit.DivName = dr["DivName"].ToString();
                    businessUnit.UnitName = dr["UnitName"].ToString();
                    businessUnit.HoCName = dr["HoCName"].ToString();

                    businessUnitList.Add(businessUnit);
                }

                conn.Close();
            }

            return businessUnitList;
        }
        #endregion

        #region CREATE
        public void AddBusinessUnit(BusinessUnit businessUnit, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "BUSINESS UNIT";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessUnitAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("DivID", businessUnit.DivID);
                cmd.Parameters.AddWithValue("UnitName", businessUnit.UnitName);
                cmd.Parameters.AddWithValue("HoCName", businessUnit.HoCName);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "DivID: " + businessUnit.DivID + ",\nUnit Name:" + businessUnit.UnitName + ",\nHoC Name:" + businessUnit.HoCName;

                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        public void EditBusinessUnit(BusinessUnit businessUnit, string UserName)
        {

            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "BUSINESS UNIT";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessUnitEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UnitID", businessUnit.UnitID);
                cmd.Parameters.AddWithValue("DivID", businessUnit.DivID);
                cmd.Parameters.AddWithValue("UnitName", businessUnit.UnitName);
                cmd.Parameters.AddWithValue("HoCName", businessUnit.HoCName);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = businessUnit.OldUnitName;
                auditLog.NewValue = "DivID: " + businessUnit.DivID + ",\nUnit Name:" + businessUnit.UnitName + ",\nHoC Name:" + businessUnit.HoCName;

                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DeleteBusinessUnit(int? id, string UnitName, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "BUSINESS UNIT";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessUnitDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UnitID", id);

                auditLog.SPName = cmd.CommandText.ToString();

                auditLog.OldValue = UnitName;
                auditLog.NewValue = "-";

                
            
                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET BUSINESS UNIT BY ID
        public BusinessUnit GetBusinessUnitByID(int? id)
        {
            var businessUnit = new BusinessUnit();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessUnitGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UnitID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    businessUnit.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                    businessUnit.DivID = Convert.ToInt32(dr["DivID"].ToString());
                    businessUnit.DivName = dr["DivName"].ToString();
                    businessUnit.UnitName = dr["UnitName"].ToString();
                    businessUnit.HoCName = dr["HoCName"].ToString();

                    businessUnit.OldUnitName = dr["UnitName"].ToString();
                }

                conn.Close();
            }

            return businessUnit;
        }
        #endregion

        #region CHECK DUPLICATION
        public BusinessUnit CheckBusinessUnitByName(string UnitName)
        {
            var businessUnit = new BusinessUnit();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spBusinessUnitCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UnitName", UnitName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    businessUnit.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return businessUnit;
        }
        #endregion
        #endregion

        #region CERT BODY
        #region GRIDVIEW
        //List All into gridview
        public IEnumerable<CertBody> CertBodyGetAll()
        {
            var CertBodyList = new List<CertBody>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCertBodyGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var certBody = new CertBody();

                    certBody.CertBodyID = Convert.ToInt32(dr["CertBodyID"].ToString());
                    certBody.CertBodyName = dr["CertBodyName"].ToString();

                    CertBodyList.Add(certBody);
                }

                conn.Close();
            }

            return CertBodyList;
        }
        #endregion

        #region CREATE
        public void AddCertBody(CertBody certBody, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "CERT BODY";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCertBodyAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CertBodyName", certBody.CertBodyName);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "Unit Name:" + certBody.CertBodyName + ",\nUsername:" + UserName;

                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        public void EditCertBody(CertBody certBody, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "CERT BODY";
            auditLog.CreatedBy = UserName;


            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCertBodyEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CertBodyID", certBody.CertBodyID);
                cmd.Parameters.AddWithValue("CertBodyName", certBody.CertBodyName);
                cmd.Parameters.AddWithValue("UserName", UserName);


                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = certBody.OldCertBodyName;
                auditLog.NewValue =  "\nUnit Name:" + certBody.CertBodyName + ",\nUsername:" + UserName;

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DeleteCertBody(int? id, string CertBodyName, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "CERT BODY";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCertBodyDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CertBodyID", id);
                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = CertBodyName;
                auditLog.NewValue = "-";

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET CERT BODY BY ID
        public CertBody GetCertBodyByID(int? id)
        {
            var certBody = new CertBody();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCertBodyGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CertBodyID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    certBody.CertBodyID = Convert.ToInt32(dr["CertBodyID"].ToString());
                    certBody.CertBodyName = dr["CertBodyName"].ToString();
                    certBody.OldCertBodyName = dr["CertBodyName"].ToString();

                }

                conn.Close();
            }

            return certBody;
        }
        #endregion

        #region CHECK EXISTING CERT BODY
        public CertBody CheckCertBodyByName(string CertBodyName)
        {
            var certBody = new CertBody();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCertBodyCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CertBodyName", CertBodyName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    certBody.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return certBody;
        }
        #endregion
        #endregion

        #region LICENSE TYPE
        #region GRIDVIEW
        public IEnumerable<Category> CategoryGetAll()
        {
            var categoryList = new List<Category>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCategoryGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var category = new Category();

                    category.CategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                    category.CategoryName = dr["CategoryName"].ToString();
                    category.Description = dr["Description"].ToString();

                    categoryList.Add(category);
                }

                conn.Close();
            }

            return categoryList;
        }
        #endregion

        #region CREATE
        public void AddCategory(Category category, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "LICENSE CATEGORY";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCategoryAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("Description", category.Description);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "DivID: " + category.CategoryName + ",\nDescription:" + category.Description + ",\nUsername:" + UserName;
                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        public void EditCategory(Category category, string UserName)
        {

            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "LICENSE CATEGORY";
            auditLog.CreatedBy = UserName;


            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCategoryEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryID", category.CategoryID);
                cmd.Parameters.AddWithValue("CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("Description", category.Description);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = category.OldCategoryName + category.OldDesc;
                auditLog.NewValue = "DivID: " + category.CategoryName + ",\nDescription:" + category.Description + ",\nUsername:" + UserName;
                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DeleteCategory(int? id, String CategoryName, String UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "LICENSE CATEGORY";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCategoryDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryID", id);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = CategoryName;
                auditLog.NewValue = "-";

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET CATEGORY BY ID
        public Category GetCategoryByID(int? id)
        {
            var category = new Category();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCategoryGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    category.CategoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                    category.CategoryName = dr["CategoryName"].ToString();
                    category.Description = dr["Description"].ToString();
                    category.OldCategoryName = dr["CategoryName"].ToString();
                    category.OldDesc = dr["Description"].ToString();
                }

                conn.Close();
            }

            return category;
        }
        #endregion

        #region CHECK DUPLICATION CATEGORY
        public Category CheckCategoryByName(string CategoryName)
        {
            var category = new Category();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spCategoryCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CategoryName", CategoryName);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    category.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return category;
        }
        #endregion
        #endregion

        #region PIC
        #region GRIDVIEW
        public IEnumerable<PIC> PICGetAll()
        {
            var PICList = new List<PIC>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var pic = new PIC();

                    pic.PICID = Convert.ToInt32(dr["PICID"].ToString());
                    pic.PICStaffNo = dr["PICStaffNo"].ToString();
                    pic.UserTypeID = Convert.ToInt32(dr["UserTypeID"].ToString());
                    pic.PICName = dr["PICName"].ToString();
                    pic.PICEmail = dr["PICEmail"].ToString();
                    pic.UserType = dr["UserType"].ToString();

                    PICList.Add(pic);
                }

                conn.Close();
            }

            return PICList;
        }
        #endregion

        #region CREATE
        public void AddPIC(PIC pic, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "PIC";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserTypeID", pic.UserTypeID);
                cmd.Parameters.AddWithValue("PICStaffNo", pic.PICStaffNo);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "UserTypeID: " + pic.UserTypeID + ",\nPICStaffNo:" + pic.PICStaffNo + ",\nUsername:" + UserName;
                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        public void EditPIC(PIC pic, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "PIC";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("PICID", pic.PICID);
                cmd.Parameters.AddWithValue("UserTypeID", pic.UserTypeID);
                cmd.Parameters.AddWithValue("PICStaffNo", pic.PICStaffNo);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = pic.OldUserTypeID + pic.OldPICStaffNo;
                auditLog.NewValue = "PICID: " + pic.PICID + "UserTypeID: " + pic.UserTypeID + ",\nPICStaffNo:" + pic.PICStaffNo + ",\nUsername:" + UserName;
                auditDbContext.AddAuditLog(auditLog);


                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DeletePIC(int? id, String UserType,  String UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "PIC";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("PICID", id);


                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = UserType;
                auditLog.NewValue = "-";


                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET PIC BY ID
        public PIC GetPICByID(int? id)
        {
            var pic = new PIC();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("PICID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    pic.PICID = Convert.ToInt32(dr["PICID"].ToString());
                    pic.UserTypeID = Convert.ToInt32(dr["UserTypeID"].ToString());
                    pic.PICStaffNo = dr["PICStaffNo"].ToString();
                    pic.PICName = dr["PICName"].ToString();
                    pic.PICEmail = dr["PICEmail"].ToString();
                    pic.UserType = dr["UserType"].ToString();

                    pic.OldPICStaffNo = dr["PICStaffNo"].ToString();
                    pic.OldUserTypeID = Convert.ToInt32(dr["UserTypeID"].ToString());
                }

                conn.Close();
            }

            return pic;
        }
        #endregion

        #region GET PIC NAME BY STAFF NO
        public PIC GetPICByStaffNo(string StaffNo)
        {
            var pic = new PIC();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICGetByStaffNo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("StaffNo", StaffNo);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    pic.PICID = Convert.ToInt32(dr["PICID"].ToString());
                    pic.PICStaffNo = dr["PICStaffNo"].ToString();
                    pic.PICName = dr["PICName"].ToString();
                    pic.PICEmail = dr["PICEmail"].ToString();
                }

                conn.Close();
            }

            return pic;
        }
        #endregion

        #region CHECK DUPLICATION PIC
        public PIC CheckPICByName(string PICStaffNo)
        {
            var pic = new PIC();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spPICCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("PICStaffNo", PICStaffNo);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    pic.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return pic;
        }
        #endregion
        #endregion

        #region USER ROLE
        #region GRIDVIEW
        public IEnumerable<UserRole> UserRoleGetAll()
        {
            var UserRoleList = new List<UserRole>();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleGetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var userRole = new UserRole();

                    userRole.UserRoleID = Convert.ToInt32(dr["UserRoleID"].ToString());
                    userRole.UserRoleName = dr["UserRoleName"].ToString();
                    userRole.UserRoleStaffNo = dr["UserRoleStaffNo"].ToString();
                    userRole.UserRoleEmail = dr["UserRoleEmail"].ToString();
                    userRole.Role = dr["UserRole"].ToString();
                    userRole.UserTypeID = Convert.ToInt32(dr["UserTypeID"].ToString());
                    userRole.UserType = dr["UserType"].ToString();

                    UserRoleList.Add(userRole);
                }

                conn.Close();
            }

            return UserRoleList;
        }
        #endregion

        #region CREATE
        public void AddUserRole(UserRole userRole, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "CREATE";
            auditLog.ScreenPath = "USER ROLE";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserRoleStaffNo", userRole.UserRoleStaffNo);
                cmd.Parameters.AddWithValue("RoleID", userRole.RoleID);
                cmd.Parameters.AddWithValue("UserTypeID", userRole.UserTypeID);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = "-";
                auditLog.NewValue = "UserRoleStaffNo: " + userRole.UserRoleStaffNo + "RoleID: " + userRole.RoleID + ",\nUserTypeID:" + userRole.UserTypeID + ",\nUsername:" + UserName;
                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region EDIT
        //Edit User Role
        public void EditUserRole(UserRole userRole, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "UPDATE";
            auditLog.ScreenPath = "USER ROLE";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleEdit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserRoleID", userRole.UserRoleID);
                cmd.Parameters.AddWithValue("UserRoleStaffNo", userRole.OldUserRoleStaffNo);
                cmd.Parameters.AddWithValue("RoleID", userRole.RoleID);
                cmd.Parameters.AddWithValue("UserTypeID", userRole.UserTypeID);
                cmd.Parameters.AddWithValue("UserName", UserName);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = userRole.OldUserRoleStaffNo + userRole.OldUserRoleStaffNo + userRole.OldRoleID ;
                auditLog.NewValue = "UserRoleStaffNo: " + userRole.UserRoleStaffNo + "RoleID: " + userRole.RoleID + ",\nUserTypeID:" + userRole.UserTypeID + ",\nUsername:" + UserName;
                auditDbContext.AddAuditLog(auditLog);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        //Delete User Role
        public void DeleteUserRole(int? id, string UserRoleStaffNo, string UserName)
        {
            Log auditLog = new Log();
            auditLog.Command = "DELETE";
            auditLog.ScreenPath = "USER ROLE";
            auditLog.CreatedBy = UserName;

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserRoleID", id);

                auditLog.SPName = cmd.CommandText.ToString();
                auditLog.OldValue = UserRoleStaffNo;
                auditLog.NewValue = "-";

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        #endregion

        #region GET USER BY ID
        public UserRole GetUserRoleByID(int? id)
        {
            var UserRole = new UserRole();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleGetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserRoleID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    UserRole.UserRoleID = Convert.ToInt32(dr["UserRoleID"].ToString());
                    UserRole.RoleID = Convert.ToInt32(dr["RoleID"].ToString());
                    UserRole.UserTypeID = Convert.ToInt32(dr["UserTypeID"].ToString());
                    UserRole.UserRoleStaffNo = dr["UserRoleStaffNo"].ToString();
                    UserRole.UserRoleName = dr["UserRoleName"].ToString();
                    UserRole.UserRoleEmail = dr["UserRoleEmail"].ToString();
                    UserRole.Role = dr["UserRole"].ToString();
                    UserRole.UserType = dr["UserType"].ToString();

                    UserRole.OldUserRoleStaffNo = dr["UserRoleStaffNo"].ToString();
                    UserRole.OldRoleID = Convert.ToInt32(dr["RoleID"].ToString());
                    UserRole.OldUserTypeID = Convert.ToInt32(dr["UserTypeID"].ToString());
                }

                conn.Close();
            }

            return UserRole;
        }
        #endregion

        #region CHECK DUPLICATE USER ROLE
        //Check Duplication in Create New Entry
        public UserRole CheckUserRoleByName(string UserRoleStaffNo)
        {
            var userRole = new UserRole();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserRoleStaffNo", UserRoleStaffNo);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    userRole.ExistData = Convert.ToInt32(dr["ExistData"].ToString());
                }

                conn.Close();
            }

            return userRole;
        }
        #endregion

        #region GET USER BY STAFF NO
        public UserRole GetUserRoleName(string UserRoleStaffNo)
        {
            var userRole = new UserRole();

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spUserRoleGetName", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("UserRoleStaffNo", UserRoleStaffNo);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    userRole.UserRoleName = dr["UserRoleName"].ToString();
                }

                conn.Close();
            }

            return userRole;
        }
        #endregion
        #endregion
    }
}
