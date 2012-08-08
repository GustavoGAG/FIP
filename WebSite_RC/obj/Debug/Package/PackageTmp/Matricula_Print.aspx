<%@ Page Title="FIP - Detalhes da Matricula" Language="C#" MasterPageFile="~/Default.Master"
    AutoEventWireup="true" CodeBehind="Matricula_Print.aspx.cs" Inherits="WebSite.Matricula_Print" %>

<asp:Content ID="MatriculaPrintHead" ContentPlaceHolderID="HeadMain" runat="server">
</asp:Content>
<asp:Content ID="MatriculaPrintBody" ContentPlaceHolderID="BodyMain" runat="server">
    
    <div runat="server" id="divAlerta" visible="false" class="alert">   </div>

    <section class="caixa1"> 
      <p>Se voce fez o seu cadastro e precisa do comprovante procure ele aqui.</p>  
    </section>


<asp:Panel ID="pnPesquisa" runat="server">


<h3>Pesquisa pela Código Matricula</h3>

 <section class="caixa1">
<div class="field"><!--Aqui esta o Formulario de Pesquisa -->
    <asp:Label ID="lblPsqMatricula" class="lb1" runat="server" Text="Matricula: "></asp:Label>
    
    <asp:TextBox ID="txtPsqMatricula" class="input2" runat="server" Text="" />

    <asp:Button ID="btPesquisaMatricula" CssClass="button" runat="server" OnClick="PsqPorMatriculas"
                        CommandArgument="Matricula" Text="Pesquisar" />
    
	
</div>
</section>

<h3>Pesquisa pelos dados do Aluno </h3>

<section class="caixa1">
<div class="field">
     <asp:Label ID="lblPsqCpf" class="lb1" runat="server" Text="CPF: "></asp:Label>
    <asp:TextBox ID="txtPsqCpf" class="input2" runat="server" Text="" />   
	
</div>

<div class="field">
    <asp:Label ID="lblPsqNascimento" class="lb1" runat="server" Text="Data Nascimento: "></asp:Label>
    <asp:TextBox ID="txtPsqNascimento" class="input2" runat="server" Text="" />   
	
</div>

<div class="field">
    <asp:Button ID="btPesquisaCPF" CssClass="button" class="input2" runat="server" OnClick="PsqPorMatriculas"
                        CommandArgument="CPF" Text="Pesquisar" />  
	
</div>
</section>  

<div runat="server" visible="false">
<h3>Pesquisa pelos dados do Responsável pelo Aluno </h3>
</div>
<section class="caixa1">
<div class="field">
    <asp:Label ID="lblCpfResponsavel" class="lb1" runat="server" Text="CPF: "></asp:Label>
    <asp:TextBox ID="txtCpfResponsavel" class="input2" runat="server" Text="" ToolTip="Digite o número do CPF do Responsavel" />   
	
</div>

<div class="field">
     <asp:Label ID="lblDataNascimento" class="lb1" runat="server" Text="Data Nascimento: "></asp:Label>
     <asp:TextBox ID="txtDataNascimento" class="input2" runat="server" Text="" ToolTip="Digite a Data de Nascimento do aluno" />  
	
</div>

<div class="field">
    <asp:Button ID="Button1" CssClass="button" class="input2" runat="server" OnClick="PsqPorMatriculas"
                        CommandArgument="Responsavel" Text="Pesquisar" /> 
	
</div>
</section>         

    </asp:Panel>
   
    <!-- Aqui esta  os dados resultante da pesquisa -->

<section class="caixa1">
    <asp:Panel ID="pnMatricula" runat="server">
        <asp:Repeater ID="rpMatricula" runat="server" OnItemCommand="ItemCommand" OnItemDataBound="ItemDataBound">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <!-- Dados Matricula -->
                <tr>
                    <td colspan="2">
                        <h3>
                            Matricula:
                            <asp:Label ID="lblId" runat="server" />
                        </h3>
                        <h1>
                            <asp:Label ID="lblStatus" runat="server" />
                        </h1>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nome:
                        <asp:Label ID="lblNome" runat="server" />
                    </td>
                    <td>
                        <td>
                            Sexo:<asp:Label ID="lblSexo" runat="server" />
                        </td>
                        <td>
                            Data Nascimento:<asp:Label ID="lblDataNascimento" runat="server" />
                        </td>
                    </td>
                </tr>
                <tr>
                    <td>
                        CPF:<asp:Label ID="lblCpf" runat="server" />
                    </td>
                    <td>
                        <td>
                            Identidade:
                            <asp:Label ID="lblIdentidade" runat="server" />
                        </td>
                        <td>
                            Orgão Emissor
                            <asp:Label ID="lblOrgaoEmissor" runat="server" />
                        </td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Endereço:
                        <asp:Label ID="lblEndereco" runat="server" />
                    </td>
                    <td>
                        Cidade:<asp:Label ID="lblCidade" runat="server" />
                        Bairo:
                        <asp:Label ID="lblBairro" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Estado:<asp:Label ID="lblEstado" runat="server" />
                        CEP:<asp:Label ID="lblCEP" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Data Cadastro:<asp:Label ID="lblDataCadastro" runat="server" />
                    </td>
                    <td>
                        Motivo:<asp:Label ID="lblMotivo" runat="server" />
                    </td>
                </tr>
                <!-- Certidao de Nascimento -->
                <tr>
                    <td>
                        Cartorio:
                        <asp:Label ID="lblCN_Cartorio" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCN_Estado" runat="server" />
                        <asp:Label ID="lblCN_Cidade" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Número da Certidão:<asp:Label ID="lblCN_Numero" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Livro:<asp:Label ID="lblCN_Livro" runat="server" />
                    </td>
                    <td>
                        Folha:<asp:Label ID="lblCN_Folha" runat="server" />
                    </td>
                    <td>
                        Data:<asp:Label ID="lblCN_Data" runat="server" />
                    </td>
                </tr>
                <!--Responsaveis -->
                <tr>
                    <td>
                        Filho de:
                        <asp:Label ID="lblNomeMae" runat="server" />
                        e
                    </td>
                    <td>
                        <asp:Label ID="lblNomePai" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Responsavel:<asp:Label ID="lblRSP_Nome" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        CPF:<asp:Label ID="lblRSP_Cpf" runat="server" />
                    </td>
                    <td>
                        Identidade:<asp:Label ID="lblRSP_Identidade" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Celular:
                        <asp:Label ID="lblRSP_Celular" runat="server" />
                    </td>
                    <td>
                        Telefone:
                        <asp:Label ID="lblRSP_Telefone" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Email:
                        <asp:Label ID="lblRSP_Email" runat="server" />
                    </td>
                </tr>
                <!-- Pesquisa -->
                <tr>
                    <td>
                        Já Estudou ?
                        <br />
                        <asp:Label ID="lblJaEstudou" runat="server" />
                    </td>
                    <td>
                        Recebe Pensão Alimenticia ?
                        <br />
                        <asp:Label ID="lblPensão" runat="server" />
                    </td>
                    <td>
                        Recebe algum beneficio do INSS ?
                        <br />
                        <asp:Label ID="lblINSS" runat="server" />
                    </td>
                    <td>
                        Recebe Bolsa Familia ?
                        <br />
                        <asp:Label ID="lblBolsaFamilia" runat="server" />
                    </td>
                    <td runat="server" id="tdgravida">
                        Esta Gravida ? Qtds Meses?
                        <br />
                        <asp:Label ID="lblGravida" runat="server" />
                        <asp:Label ID="lblMesGravidez" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Portador das Seguintes Necessidades Especiais
                        <ul>
                            <asp:Literal ID="ltDeficiencia" runat="server"></asp:Literal>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEscolaOrigem" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEscolaDestino" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSerie" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
         <input onclick="windows.print" value="Imprimir" type="submit" class="buton" />
    </asp:Panel>

    </section> 
</asp:Content>
