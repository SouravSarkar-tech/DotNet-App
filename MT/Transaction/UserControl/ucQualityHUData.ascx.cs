using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accenture.MWT.DomainObject;
using Accenture.MWT.DataAccess;

public partial class Transaction_UserControl_ucQualityHUData : System.Web.UI.UserControl
{
    QualityAccess inspDataAccess = new QualityAccess();

    //New Addition for HU tick Start
    //public string InspectionType
    //{
    //    get { return lblInspectionType.Text; }
    //    set { lblInspectionType.Text = value; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
    //    if (Session[StaticKeys.LoggedIn_User_Id] != null)
    //    {
    //        if (!IsPostBack)
    //        {
    //            lblUserId.Text = Session[StaticKeys.LoggedIn_User_Id].ToString();
    //            lblMasterHeaderId.Text = Session[StaticKeys.MasterHeaderId].ToString();
    //            lblModuleId.Text = Session[StaticKeys.SelectedModuleId].ToString();                
                
    //        }
    //        //FillData();
    //    }
    }
    
    //public void Save()
    //{
    //    InspData objInspData = new InspData();

    //    try
    //    {
    //        objInspData = GetQualityInspControlData();
    //        if (inspDataAccess.SaveQualityInspData(objInspData) > 0)
    //        {
    //            //materialDesc = objMatDesc.Material_Desc;
    //            FillData();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //public void FillData()
    //{
    //    InspData objInspData = inspDataAccess.GetInspDataByMasterHeaderId(lblMasterHeaderId.Text, lblInspectionType.Text);
    //    if (objInspData.Mat_InspData_Id > 0)
    //    {
    //        lblInspDataId.Text = Convert.ToString(objInspData.Mat_InspData_Id);
    //        lblInspectionType.Text = objInspData.InspectionType;
    //        chkPostInspStock.Checked = objInspData.PostInspStock.ToLower() == "true" ? true : false;
    //        chkInspHU.Checked = objInspData.InspHU.ToLower() == "true" ? true : false;
    //    }
    //    else
    //    {
    //        lblInspDataId.Text = "0";
    //        chkPostInspStock.Checked = false;
    //        chkInspHU.Checked = false;
    //    }
    //}

    //private InspData GetQualityInspControlData()
    //{
    //    InspData objInspData = new InspData();
    //    Utility util = new Utility();

    //    try
    //    {
    //        objInspData.Mat_InspData_Id = Convert.ToInt32(lblInspDataId.Text);
    //        objInspData.Master_Header_Id = Convert.ToInt32(lblMasterHeaderId.Text);
    //        objInspData.InspectionType = lblInspectionType.Text;
    //        objInspData.PostInspStock = chkPostInspStock.Checked == true ? "1" : "0";
    //        objInspData.InspHU = chkInspHU.Checked == true ? "1" : "0";


    //        objInspData.UserId = lblUserId.Text;
    //        objInspData.TodayDate = util.GetDate();
    //        objInspData.IPAddress = util.GetIpAddress();
    //        objInspData.IsActive = 1;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    return objInspData;
    //}
    //New Addition for HU tick End
}