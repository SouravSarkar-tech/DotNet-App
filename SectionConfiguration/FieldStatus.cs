using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SectionConfiguration
{
    /// <summary>
    /// Summary description for Field
    /// </summary>
    public class FieldStatus
    {
        public FieldStatus()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum enumFieldStatus
        {
            Mandatory = 1,
            Optional = 2,
            Hide = 3

        }

        public enum enumSection
        {
            Accounting1 = 1,
            Accounting2 = 2,
            Basic1 = 3,
            Basic2 = 4,
            Costing1 = 5,
            Costing2 = 6,
            ForeignTrade = 7,
            MRP1 = 8,
            MRP2 = 9,
            MRP3 = 10,
            MRP4 = 11,
            Purchasing = 12,
            PlantStorageLocation = 13,
            Quality = 14,
            Sales1 = 15,
            Sales2 = 16,
            SalesPlant = 17,
            Tax = 18,
            WM1 = 19,
            WM2 = 20,
            WorkScheduling = 21

        }

        public static void SetFieldStatus<Panel, Section>(Panel pnlctrl, Section section ,string ext = "")
        {
            try
            {
                Control pnlControl;

                if (pnlctrl is System.Web.UI.WebControls.Panel || pnlctrl is System.Web.UI.WebControls.GridViewRow)
                {
                    if (pnlctrl is System.Web.UI.WebControls.Panel)
                    {
                        System.Web.UI.WebControls.Panel pnlControlp = pnlctrl as System.Web.UI.WebControls.Panel;
                        pnlControl = pnlControlp;
                    } 
                    else
                    {
                        System.Web.UI.WebControls.GridViewRow grdControl = pnlctrl as System.Web.UI.WebControls.GridViewRow;
                        pnlControl = grdControl;
                    }


                    foreach (var sectionProperty in section.GetType().GetProperties())
                    {
                        int FStatus;
                        object oSec = sectionProperty.GetValue(section, null);
                        if (oSec != null)
                            if (int.TryParse(oSec.ToString(), out FStatus))
                            {
                                string reqControlName = ("req" + sectionProperty.Name + ext).Trim();
                                string lblControlName = ("lable" + sectionProperty.Name + ext).Trim();

                                Control ctrl = pnlControl.FindControl(reqControlName) as Control;
                                Control lblCtrl = pnlControl.FindControl(lblControlName) as Control;

                                if (ctrl != null)
                                {
                                    if (FStatus == (int)FieldStatus.enumFieldStatus.Optional)
                                    {
                                        SetFieldStatus(ctrl, false);
                                        SetFieldStatus(lblCtrl, false);
                                    }
                                    else if (FStatus == (int)FieldStatus.enumFieldStatus.Mandatory)
                                    {
                                        SetFieldStatus(ctrl, true);
                                    }
                                    else if (FStatus == (int)FieldStatus.enumFieldStatus.Hide)
                                    {
                                        SetFieldStatus(ctrl, false);
                                        SetFieldStatus(lblCtrl, false);
                                        ctrl = pnlControl.FindControl(sectionProperty.Name + ext) as Control;
                                        SetFieldStatus(ctrl, false);

                                    }
                                }
                                else
                                {
                                    if (FStatus == (int)FieldStatus.enumFieldStatus.Hide)
                                    {
                                        SetFieldStatus(lblCtrl, false);
                                        ctrl = pnlControl.FindControl(sectionProperty.Name + ext) as Control;
                                        SetFieldStatus(ctrl, false);

                                    }
                                    else if (FStatus == (int)FieldStatus.enumFieldStatus.Optional)
                                    {
                                        SetFieldStatus(lblCtrl, false);
                                    }
                                }
                            }
                    }
                }
            }
            catch (Exception ex)

            { throw; }
        }

        public static void SetFieldStatus(Panel pnlControl, List<SectionFieldMaster> sectionFields)
        {
            try
            {
                if (pnlControl != null)
                {
                    foreach (SectionFieldMaster item in sectionFields)
                    {
                        string reqControlName = ("req" + item.FieldName).Trim();
                        string lblControlName = ("lable" + item.FieldName).Trim();

                        Control ctrl = pnlControl.FindControl(reqControlName) as Control;
                        Control lblCtrl = pnlControl.FindControl(lblControlName) as Control;

                        if (ctrl != null)
                        {
                            if (item.FieldStatus == (int)FieldStatus.enumFieldStatus.Optional)
                            {
                                SetFieldStatus(ctrl, false);
                                SetFieldStatus(lblCtrl, false);
                            }
                            else if (item.FieldStatus == (int)FieldStatus.enumFieldStatus.Mandatory)
                            {
                                SetFieldStatus(ctrl, true);
                            }
                            else if (item.FieldStatus == (int)FieldStatus.enumFieldStatus.Hide)
                            {
                                SetFieldStatus(ctrl, false);
                                SetFieldStatus(lblCtrl, false);
                                ctrl = pnlControl.FindControl(item.FieldName) as Control;
                                SetFieldStatus(ctrl, false);

                            }
                        }
                        else
                        {
                            if (item.FieldStatus == (int)FieldStatus.enumFieldStatus.Hide)
                            {
                                SetFieldStatus(lblCtrl, false);
                                ctrl = pnlControl.FindControl(item.FieldName) as Control;
                                SetFieldStatus(ctrl, false);

                            }
                        }

                    }

                }
            }
            catch { throw; }
        }

        public static void SetFieldStatus(Control ctrl, bool status)
        {
            try
            {
                if (ctrl != null)
                {
                    if (ctrl is TextBox)
                    {
                        TextBox txtControl = ctrl as TextBox;
                        txtControl.Enabled = status;
                        txtControl.CssClass = status ? "textbox" : "textboxDisable";
                    }
                    else if (ctrl is Label)
                    {
                        Label lblControl = ctrl as Label;
                        lblControl.Visible = status;
                    }
                    else if (ctrl is Panel)
                    {
                        Panel pnlControl = ctrl as Panel;
                        pnlControl.Visible = status;
                    }
                    else if (ctrl is DropDownList)
                    {
                        DropDownList ddlControl = ctrl as DropDownList;
                        ddlControl.Enabled = status;
                    }
                    else if (ctrl is CheckBox)
                    {
                        CheckBox chkControl = ctrl as CheckBox;
                        chkControl.Enabled = status;
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        CheckBoxList chkControl = ctrl as CheckBoxList;
                        chkControl.Enabled = status;
                    }
                    else if (ctrl is AjaxControlToolkit.ComboBox)
                    {
                        AjaxControlToolkit.ComboBox chkControl = ctrl as AjaxControlToolkit.ComboBox;
                        chkControl.Enabled = status;
                    }
                    else if (ctrl is RadioButton)
                    {
                        RadioButton rdbControl = ctrl as RadioButton;
                        rdbControl.Enabled = status;
                    }
                    else if (ctrl is RadioButtonList)
                    {
                        RadioButtonList rdbControl = ctrl as RadioButtonList;
                        rdbControl.Enabled = status;
                    }
                    else if (ctrl is RequiredFieldValidator)
                    {
                        RequiredFieldValidator reqControl = ctrl as RequiredFieldValidator;
                        reqControl.Enabled = status;
                    }
                    else if (ctrl is CompareValidator)
                    {
                        CompareValidator cmpControl = ctrl as CompareValidator;
                        cmpControl.Enabled = status;
                    }
                    else if (ctrl is CustomValidator)
                    {
                        CustomValidator cstumControl = ctrl as CustomValidator;
                        cstumControl.Enabled = status;
                    }
                    else if (ctrl is FileUpload)
                    {
                        FileUpload fileControl = ctrl as FileUpload;
                        fileControl.Enabled = status;
                    }
                    else 
                    {
                        ctrl.Visible = false;
                    }
                }
            }
            catch { throw; }
        }

        public static void ClearPanel(Panel pnlCtrl)
        {
            try
            {
                if (pnlCtrl != null)
                {
                    foreach (var ctrl in pnlCtrl.Controls)
                    {

                        if (ctrl != null)
                        {
                            if (ctrl is TextBox)
                            {
                                TextBox txtControl = ctrl as TextBox;
                                txtControl.Text = null;
                            }
                            else if (ctrl is DropDownList)
                            {
                                DropDownList ddlControl = ctrl as DropDownList;
                                ddlControl.ClearSelection();
                            }
                            else if (ctrl is CheckBox)
                            {
                                CheckBox chkControl = ctrl as CheckBox;
                                chkControl.Checked = false;
                            }
                            else if (ctrl is CheckBoxList)
                            {
                                CheckBoxList chkControl = ctrl as CheckBoxList;
                                chkControl.ClearSelection();
                            }
                            else if (ctrl is RadioButton)
                            {
                                RadioButton rdbControl = ctrl as RadioButton;
                                rdbControl.Checked = false;
                            }
                            else if (ctrl is RadioButtonList)
                            {
                                RadioButtonList rdbControl = ctrl as RadioButtonList;
                                rdbControl.ClearSelection();
                            }
                            else if (ctrl is RequiredFieldValidator)
                            {
                                RequiredFieldValidator reqControl = ctrl as RequiredFieldValidator;

                            }
                            else if (ctrl is CompareValidator)
                            {
                                CompareValidator cmpControl = ctrl as CompareValidator;

                            }
                            else if (ctrl is CustomValidator)
                            {
                                CustomValidator cstumControl = ctrl as CustomValidator;

                            }
                            else if (ctrl is FileUpload)
                            {
                                FileUpload fileControl = ctrl as FileUpload;

                            }
                        }
                    }
                }
            }
            catch
            {

                throw;
            }
        }

    }
}