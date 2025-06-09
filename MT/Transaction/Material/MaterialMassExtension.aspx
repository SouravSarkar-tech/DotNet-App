<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master" AutoEventWireup="true" CodeFile="MaterialMassExtension.aspx.cs" Inherits="Transaction_Material_MaterialMassExtension" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucExcelDownloadl.ascx" TagPrefix="uc" TagName="ExcelDownload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                        <asp:Label ID="lblMsg" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="trHeading" align="center" colspan="2">Material Mass Extension Data
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
            <tr >
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Type of Mass Updation
                            <asp:Label ID="lblddlTypeOfMassUpdm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" align="left">
                                <asp:DropDownList ID="ddlTypeOfMassUpdm" runat="server" AppendDataBoundItems="false"
                                    AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlTypeOfMassUpdm_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlTypeOfMassUpdm" runat="server" ControlToValidate="ddlTypeOfMassUpdm"
                                    ValidationGroup="massUpload" ErrorMessage="Type of Mass Updation cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of Mass Updation cannot be blank.' />" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                <asp:Label ID="lblselectcap" runat="server" Text="Select File"></asp:Label>
                                <asp:Label ID="lblSelectFile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" align="left">
                                <div>
                                    <asp:FileUpload ID="fileUploadMS" runat="server"  TabIndex="36" />
                                    <asp:Button ID="btnMSProcess" runat="server" OnClick="btnMSProcess_Click" Text="Upload" ValidationGroup="massUpload" />
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="Document_Upload_Id"
                                    Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attached Documents">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblUploadedFileName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:HyperLink ID="aDocPath" runat="server" Text='<%# Eval("Document_Name") %>' NavigateUrl='<%# Eval("Document_Path") %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="X" ForeColor="Red" Font-Size="15px"
                                                    CommandName="DEL" Font-Bold="true" OnClientClick="return confirm('Are you certain you want to delete this document?');" />&nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                            <td class="tdSpace">
                                <asp:HyperLink ID="hlMSImportFormat" runat="server" Text="Excel Format" Target="_blank"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <%--MSC_8300001775--%>
            <tr id="trMassBtn" runat="server" visible="false">
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnMassSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                        TabIndex="39" OnClick="btnMassSave_Click" Visible="false" />
                </td>
            </tr>
            <%--MSC_8300001775--%>
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnNext" runat="server" Text="Save & Proceed to Submit" UseSubmitBehavior="true"
                        TabIndex="40" CssClass="button" OnClick="btnNext_Click" Width="160px" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4"></td>
            </tr>
        </table>
    </asp:Panel>

    <div id="divValidationModulePopUp" style="display: none;" title="Warning Message">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">

                    <tr>
                        <td class="leftTD" colspan="2">
                            <span style="color: brown; font-size: 11px">

                                <i>Dear User,</i>
                                <br />
                                <br />
                                <i>Your request will be submitt after validation completed.</i>
                                <br />
                                <br />

                                <i>If validation is failed then will send mail to your register email.</i>
                                <br />
                                <br />
                                <i>We appreciate your actions in trying to embrace the new, Thanks!</i>

                            </span>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnbackMsg" runat="server" Text="Ok" CssClass="button"
                        OnClick="btnbackMsg_Click" />
                </td>
            </tr>
        </table>
    </div>
     
    <asp:ValidationSummary ID="vsSM" runat="server" ValidationGroup="massUpload" ShowMessageBox="true"
        ShowSummary="false" /> 
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="58" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMaterialChangeId" runat="server" Visible="false" />
    <asp:Label ID="lblMatPlantGrpId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" /> 
    <asp:Label ID="lblRefModuleId" runat="server" Visible="false" /> 
    
    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownloadEXTDATA" runat="server" ActionType="MEF" Visible="false" />
    </div>

    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownloadError" runat="server" ActionType="MEE" Visible="false" />
    </div>


    <script type="text/javascript" language="javascript">
        function ShowValidationNewDialog() {
            $("#divValidationModulePopUp").dialog({
                height: 350,
                width: 600,
                modal: true,
                closeOnEscape: true,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });


        }

    </script>

</asp:Content>

