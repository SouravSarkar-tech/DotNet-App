<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="error.aspx.cs" Inherits="WebPages_error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td align="center" bgcolor="#0270BF">
                <span style="color: White; font-weight: bold;">APPLICATION ERROR</span>
            </td>
        </tr>
        <tr>
            <td align="center" style="font-family:Verdana; font-size:12px;"><br />
                We are sorry , an unhandled error occured on the server.<br />
                The Server Administrator has been notified and the error logged.<br />
                <br />
                Please continue on clicking the Home Link.<br />
                <br />
                Thanks<br />
            </td>
        </tr>
    </table>
</asp:Content>
