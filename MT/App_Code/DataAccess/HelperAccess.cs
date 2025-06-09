using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web;
using Saplin.Controls;
using AjaxControlToolkit;

namespace Accenture.MWT.DataAccess
{
    public class HelperAccess
    {
        /// <summary>
        ///  //MSC_8300001775 start
        /// </summary>
        public static string ReqType { get; set; }

        public void PopuplateDropDownList(DropDownList ddl)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Proc_Read_LookUp_Header";
            string controlId = ddl.ID.ToString();
            hashPara.Add("@ControlName", controlId);
            try
            {
                ddl.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                ddl.DataTextField = "LookUp_Desc";
                ddl.DataValueField = "LookUp_Code";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateDropDownListCV(DropDownList ddl, string Query, string DataTextField, string DataValueField, string val = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string controlId = ddl.ID.ToString();

            try
            {
                ddl.Items.Clear();
                DataSet ds = objDal.FillDataSet(Query);
                ddl.DataSource = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.SelectedValue = null;
                    ddl.DataBind();

                    ddl.DataSource = ds;
                    ddl.DataTextField = DataTextField;
                    ddl.DataValueField = DataValueField;
                    ddl.DataBind();
                }

                //ddl.Items.FindByValue("12").Enabled = false;

                ddl.Items.Insert(0, new ListItem("---Select---", val));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void PopuplateDropDownList(DropDownList ddl, string Query, string DataTextField, string DataValueField, string val = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string controlId = ddl.ID.ToString();

            try
            {
                ddl.Items.Clear();
                DataSet ds = objDal.FillDataSet(Query);
                ddl.DataSource = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds;
                    ddl.DataTextField = DataTextField;
                    ddl.DataValueField = DataValueField;
                    ddl.DataBind();
                }

                //ddl.Items.FindByValue("12").Enabled = false;

                ddl.Items.Insert(0, new ListItem("---Select---", val));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUnblockOption(DropDownList ddl)
        {
            ddl.Items.Insert(1, new ListItem("Unblock", "#"));
        }

        public void AddUnblockOption(ComboBox ddl)
        {
            ddl.Items.Insert(1, new ListItem("Unblock", "#"));
        }

        public void AddUnblockOption(CheckBoxList ddl)
        {
            ddl.Items.Insert(1, new ListItem("Unblock", "#"));
        }

        public void AddBlankOption(DropDownList ddl)
        {
            ddl.Items.Insert(1, new ListItem("#-Blank", "#"));
        }

        public void AddBlankOption(ComboBox ddl)
        {
            ddl.Items.Insert(1, new ListItem("#-Blank", "#"));
        }

        public void AddBlankOption(CheckBoxList ddl)
        {
            ddl.Items.Insert(1, new ListItem("#-Blank", "#"));
        }

        public void PopuplateComboBox(ComboBox ddl, string Query, string DataTextField, string DataValueField, string val = "0")
        {
            DataAccessLayer objDal = new DataAccessLayer();
            string controlId = ddl.ID.ToString();

            try
            {
                ddl.Items.Clear();
                DataSet ds = objDal.FillDataSet(Query);
                ddl.DataSource = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = DataTextField;
                    ddl.DataValueField = DataValueField;

                }
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Select---", val));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateCheckBoxList(CheckBoxList chkList)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "Proc_Read_LookUp_Header";
            string controlId = chkList.ID.ToString();
            hashPara.Add("@ControlName", controlId);
            try
            {
                chkList.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                chkList.DataTextField = "LookUp_Desc";
                chkList.DataValueField = "LookUp_Code";
                chkList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// MSC_8300001775
        /// ReqType add new parameter
        /// </summary>
        /// <param name="currentSectionId"></param>
        /// <param name="MasterHeaderId"></param>
        /// <param name="deptID"></param>
        /// <param name="btnPrevious"></param>
        /// <param name="btnNext"></param>
        public static void SetNextPreviousSectionURL(string currentSectionId, string MasterHeaderId, string deptID, Button btnPrevious, Button btnNext)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            string procName = "pr_Get_Next_Previous_Section_URL";
            hashPara.Add("@CurrenctSectionId", currentSectionId);
            hashPara.Add("@MasterHeaderId", MasterHeaderId);
            hashPara.Add("@DepartmentId", deptID);
            try
            {
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    btnPrevious.CommandArgument = dstData.Tables[0].Rows[0]["PreviousPageURL"].ToString();
                    btnNext.CommandArgument = dstData.Tables[0].Rows[0]["NextPageURL"].ToString();
                    btnPrevious.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["PreviousBtnStatus"].ToString());
                    btnNext.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["NextBtnStatus"].ToString());
                    //MSC_8300001775
                    ReqType = dstData.Tables[0].Rows[0]["ReqType"].ToString();
                    //MSC_8300001775

                }
                else
                {
                    btnPrevious.Visible = false;
                    btnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetNextPreviousSectionURLOld(string currentSectionSeq, string moduleId, string deptID, Button btnPrevious, Button btnNext)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            DataSet dstData = new DataSet();
            string procName = "Proc_Get_Next_Previous_Section_URL";
            hashPara.Add("@CurrenctSectionSeq", currentSectionSeq);
            hashPara.Add("@ModuleId", moduleId);
            hashPara.Add("@DepartmentId", deptID);
            try
            {
                dstData = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (dstData.Tables[0].Rows.Count > 0)
                {
                    btnPrevious.CommandArgument = dstData.Tables[0].Rows[0]["PreviousPageURL"].ToString();
                    btnNext.CommandArgument = dstData.Tables[0].Rows[0]["NextPageURL"].ToString();
                    btnPrevious.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["PreviousBtnStatus"].ToString());
                    btnNext.Visible = SafeTypeHandling.ConvertStringToBoolean(dstData.Tables[0].Rows[0]["NextBtnStatus"].ToString());
                }
                else
                {
                    btnPrevious.Visible = false;
                    btnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplatePlantList(DropDownCheckBoxes ddl, string Master_Header_Id, string Section_Code, string Reference_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetPlantList";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Code", Section_Code);
            hashPara.Add("@Reference_Id", Reference_Id);
            try
            {
                ddl.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                ddl.DataTextField = "Plant_Name";
                ddl.DataValueField = "Plant_Id";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateCompanyList(DropDownCheckBoxes ddl, string Master_Header_Id, string Section_Code, string Reference_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetCompanyList";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Code", Section_Code);
            hashPara.Add("@Reference_Id", Reference_Id);
            try
            {
                ddl.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                ddl.DataTextField = "Company_Name";
                ddl.DataValueField = "Company_Id";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateSalesOrganisationList(DropDownCheckBoxes ddl, string Master_Header_Id, string Section_Code, string Reference_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetSalesOrganisationList";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Code", Section_Code);
            hashPara.Add("@Reference_Id", Reference_Id);
            try
            {
                ddl.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                ddl.DataTextField = "Sales_Organization_Name";
                ddl.DataValueField = "Sales_Organization_Id";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateDistributionChannelList(DropDownCheckBoxes ddl, string Master_Header_Id, string Section_Code, string Reference_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDistributionChannelList";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Code", Section_Code);
            hashPara.Add("@Reference_Id", Reference_Id);
            try
            {
                ddl.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                ddl.DataTextField = "Distribution_Channel_Name";
                ddl.DataValueField = "Distribution_Channel_ID";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateDropDownCheckBox(DropDownCheckBoxes ddl, string Query, string DataTextField, string DataValueField)
        {
            DataAccessLayer objDal = new DataAccessLayer();

            try
            {
                DataSet ds = objDal.FillDataSet(Query);
                ddl.DataSource = null;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds;
                    ddl.DataTextField = DataTextField;
                    ddl.DataValueField = DataValueField;
                    ddl.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopuplateDivisionList(DropDownCheckBoxes ddl, string Master_Header_Id, string Section_Code, string Reference_Id)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_GetDivisionList";
            hashPara.Add("@Master_Header_Id", Master_Header_Id);
            hashPara.Add("@Section_Code", Section_Code);
            hashPara.Add("@Reference_Id", Reference_Id);
            try
            {
                ddl.DataSource = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                ddl.DataTextField = "Division_Name";
                ddl.DataValueField = "Division_Id";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDistributionChannel(string DistChannelID = null)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            DataTable dt = new DataTable();
            Hashtable hashPara = new Hashtable();
            string procName = "USP_Get_M_Distribution_Channel";
            hashPara.Add("@DistributionChannelID", DistChannelID);

            try
            {
                DataSet ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds != null)
                    dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSalesOrganization(string SalesOrganization = null)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            DataTable dt = new DataTable();
            Hashtable hashPara = new Hashtable();
            string procName = "USP_Get_M_Sales_Organization";
            hashPara.Add("@SalesOrganizationId", SalesOrganization);

            try
            {
                DataSet ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds != null)
                    dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetWarehouse(string WarehouseID = null)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            DataTable dt = new DataTable();
            Hashtable hashPara = new Hashtable();
            string procName = "USP_Get_M_Warehouse";
            hashPara.Add("@WarehouseID", WarehouseID);

            try
            {
                DataSet ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds != null)
                    dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStorageType(string StorageTypeID = null)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            DataTable dt = new DataTable();
            Hashtable hashPara = new Hashtable();
            string procName = "USP_Get_M_Storage_Type";
            hashPara.Add("@StorageTypeID", StorageTypeID);

            try
            {
                DataSet ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds != null)
                    dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStorageLocation(string StorageLocationID = null)
        {

            DataAccessLayer objDal = new DataAccessLayer();
            DataTable dt = new DataTable();
            Hashtable hashPara = new Hashtable();
            string procName = "USP_Get_M_Storage_Location";
            hashPara.Add("@StorageLocationID", StorageLocationID);

            try
            {
                DataSet ds = objDal.FillDataSet(CommandType.StoredProcedure, procName, hashPara);
                if (ds != null)
                    dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  //MSC_8300001775 
        /// </summary>
        /// <param name="sectionid"></param>
        /// <param name="masterHeaderId"></param>
        /// <param name="userId"></param> 
        public int MaterialChange(string sectionid, string masterHeaderId, string userId)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            SqlCommand objCommand = new SqlCommand();
            string procName = "pr_SChange_Request_Material";
            int retVal = 0;
            int ChangeSMatID1 = 0;
            objCommand.Parameters.AddWithValue("@MasterHeaderId", masterHeaderId);
            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@sectionid", sectionid);
            SqlParameter ret = objCommand.Parameters.Add("@return", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            SqlParameter OutChangeSMatID = objCommand.Parameters.Add("@OutChangeSMatID", SqlDbType.Int);
            OutChangeSMatID.Direction = ParameterDirection.Output;
            try
            {
                objDal.OpenConnection();
                objCommand.Connection = objDal.cnnConnection;
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = procName;
                //Srinidhi
                objCommand.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlCommandTimeOut"]);

                objCommand.ExecuteNonQuery();
                retVal = (int)ret.Value;
                if (retVal > 0)
                {
                    ChangeSMatID1 = SafeTypeHandling.ConvertStringToInt32(OutChangeSMatID.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
            return ChangeSMatID1;
        }

        /// <summary>
        /// MSC_8300001775
        /// </summary>
        /// <param name="ChangeSMatID"></param>
        /// <param name="FieldID"></param>
        /// <param name="sOldVal"></param>
        /// <param name="sNewVal"></param>
        /// <returns></returns>
        public int MaterialChangeDetails(int ChangeSMatID, int FieldID, string sOldVal, string sNewVal)
        {
            DataAccessLayer objDal = new DataAccessLayer();
            Utility objUtil = new Utility();
            Hashtable hashPara = new Hashtable();
            string procName = "pr_SChange_Details";
            hashPara.Add("@ChangeSMatID", ChangeSMatID);
            hashPara.Add("@FieldID", FieldID);
            hashPara.Add("@sOldVal", sOldVal);
            hashPara.Add("@sNewVal", sNewVal);

            try
            {
                objDal.OpenConnection();
                return objDal.ReturnExecuteNonQuery(CommandType.StoredProcedure, procName, hashPara, ref objDal.cnnConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDal.CloseConnection(objDal.cnnConnection);
                objDal = null;
            }
        }
    }
}