<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DevizaValto.Default" Theme="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Devizaváltó program</title>
    <link href="App_Themes/Default/Default.css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableViewState ="true">
        </asp:ScriptManager>
        <h1>Devizaváltó program</h1>
        <p>Az 
        <a href='<%= DevizaValto.Properties.Resources.Webhely %>' target="_blank" >
            itt található</a> <asp:Label ID="lblDatum" runat="server"/> napi árfolyam táblázat alapján
        </p>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txbValtando" runat="server" MaxLength="10" SkinID="NTextBox80" Text="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvValtando" runat="server" ControlToValidate="txbValtando"
                        SkinID="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender
                        ID="vceValtandoR" runat="Server" HighlightCssClass="vcehighlight" PopupPosition="BottomLeft"
                        TargetControlID="rfvValtando">
                    </ajaxToolkit:ValidatorCalloutExtender>

                    <asp:CompareValidator ID="cpvValtando" runat="server"
                        Type="Double" Operator="DataTypeCheck"
                        SkinID="CompareValidatorDouble" ControlToValidate="txbValtando">
                    </asp:CompareValidator>
                    <ajaxToolkit:ValidatorCalloutExtender
                        ID="vceValatandoT" runat="Server" HighlightCssClass="vcehighlight" PopupPosition="BottomLeft"
                        TargetControlID="cpvValtando">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:DropDownList ID="ddlValutak" runat="server" SkinID="DropDownList"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnValt" runat="server" Text="Egyenlő" OnClick="btnValt_Click" />
                </td>
                <td>
                    <asp:TextBox ID="txbForint" runat="server" SkinID="NTextBox120" ReadOnly="true"></asp:TextBox>
                    &nbsp;Forint (HUF)
                </td>
            </tr>
        </table>
        <asp:Label ID="lblHiba" runat="server" CssClass="Hiba"></asp:Label>
        <asp:HiddenField ID="hifHUF" runat="server" />
    </form>
    <div id="Footer">Magyar Államkincstár, Kompetencia felmérés 2022, Vanyó Tamás</div>
</body>
</html>
