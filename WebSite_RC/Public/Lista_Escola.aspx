<%@ Page Title="FIP - Lista de Escolas" Language="C#" MasterPageFile="~/Default.Master"
    AutoEventWireup="true" CodeBehind="Lista_Escola.aspx.cs" Inherits="WebSite.Public.Lista_Escola" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyMain" runat="server">

<div>
<asp:Label ID="lblAlert" runat="server" Visible="false"/>
</div>
   <div>
   <asp:Label ID="lblEstadoFiltro" runat="server" Text="Estado"   AssociatedControlID="ddlEstado" />
   <asp:DropDownList ID="ddlEstado" runat="server" CommandArgument="Estado" AutoPostBack="true"
       ValidationGroup="Estado"   ToolTip="Escolha um Estdo para Filtrar" OnSelectedIndexChanged="PreencherListView" />
    
       <asp:Label ID="lblCidadeFiltro" runat="server" Text="Filtro" 
           AssociatedControlID="ddlEstado" />    
        
       <asp:DropDownList ID="ddlCidade" ToolTip="Escolha uma Cidade para Filtrar" ValidationGroup="Cidade"
               runat="server" AutoPostBack="true" CommandArgument="Cidade" OnSelectedIndexChanged="PreencherListView" />
   </div>
    <asp:ListView ID="lvEscola" EditIndex="-1" ConvertEmptyStringToNull="true"  
        OnItemDataBound="IDataBound" InsertItemPosition="None" ItemPlaceholderID="linhaItem"
        runat="server">
        <LayoutTemplate>
            <table>
                <caption>
                    Escolas</caption>
                <thead>
                    <tr>
                         
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server"  id="linhaItem">
                        
                    </tr>
                </tbody>
                <tfoot>
                <tr></tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <td runat="server">
                <tr>
                    <td>
                        <asp:HiddenField ID="hfId" runat="server" Visible="false" />
                        <asp:Label ID="lblTipo" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblNome" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTel" runat="server" />
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblFax" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEstado" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblCidade" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblBairro" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblEndereco"  runat="server" />
                    </td>
                </tr>
            </td>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
