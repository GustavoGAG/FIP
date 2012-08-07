<%@ Page Title="Matricular-se" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Matricula.aspx.cs" Inherits="WebSite.Matricula" %>

<asp:Content ID="HeadMatricula" ContentPlaceHolderID="HeadMain" runat="server">
</asp:Content>
<asp:Content ID="BodyMatricula" ContentPlaceHolderID="BodyMain" runat="server">
    <asp:UpdatePanel    runat="server">
        
        <ContentTemplate>
            <script type="text/javascript">
                var mascaras = $( function ()
                {
                    $( ".data" ).wijinputdate( 
                {
                    smartInputMode: false,
                    dateFormat: "dd/MM/yyyy",
                    culture: "pt-BR",
                    showNullText: true,
                    startYear: 1950,
                    disableUserInput: false,
                    keyDelay: 2000

                } );

                

                    $( ".cep" ).wijinputmask( { mask: "99999-999" } );
                    $( ".telefone" ).wijinputmask( { mask: "(99)9999-9999" } );
                    $( ".cpf" ).wijinputmask( { mask: "999.999.999-99" } );

                } );
    
function formatar(mascara, documento){
  var i = documento.value.length;
  var saida = mascara.substring(0,1);
    var texto = mascara.substring(i);
  
  if (texto.substring(0,1) != saida){
            documento.value += texto.substring(0,1);
  }
  
}
         
        
            </script>
            
             <div runat="server" id="divAlerta" visible="false" class="alerta">
    </div>
    <section class="caixa1">
        <h3>
            Cadastro do Aluno</h3>
        <p>
            Cadastro para processamento e regularização de sua inscrição para matriculas em
            creche, escola pública, ensino fundamental, médio e faculdade da prefeitura e estado.
            Gratuitamente!.
        </p>
    </section>
    <h3>
        Dados para inscrição</h3>
    <section class="caixa1">
        <!-- Nome -->
        <div class="field">
            <asp:Label runat="server" class="lb1" AssociatedControlID="TxtNome" Text="Nome:"
                ID="lblNome" />
            <asp:TextBox ID="TxtNome" class="input" runat="server" ToolTip="Digite seu nome completo"
                MaxLength="100" AutoCompleteType="DisplayName" Min="30" required="Digite o seu Nome Completo (Max. 100 letras)."
                ValidationGroup="MtriculaVG" />
        </div>
        <!-- Identidade -->
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Identidade:" ID="lblIdentidade" />
            <asp:TextBox ID="TxtRG" class="input2" runat="server" MaxLength="20" ToolTip="Número da Identidade do Aluno"
                AutoCompleteType="None" />
        </div>
        <!-- Orgão Emissor -->
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Orgão Emissor:" ID="lblorgaoemisor" />
            <asp:TextBox ID="TxtOrgaoEmissor" ValidationGroup="MtriculaVG" class="input2" runat="server"
                MaxLength="15" ToolTip="Orgão Emissor da Identidade" AutoCompleteType="None" />
        </div>
        <!-- CPF -->
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="CPF:" ID="lblCPF" AssociatedControlID="TxtCPF" />
            <asp:TextBox ValidationGroup="MtriculaVG" ID="TxtCPF" class="input2  cpf" onKeyUp="JavaScript:mascaraTexto(even,999.999.999-99);"
                runat="server" MaxLength="14" ToolTip="Número de CPF do Aluno" AutoCompleteType="None" />
        </div>
        <!-- Sexo -->
                      <div class="field">
                    <asp:Label ID="lblSexo" runat="server" class="lb1" Text="Sexo" />
                    <asp:DropDownList ID="ddlSexo" class="input2" runat="server" ToolTip="Sexo do Aluno"
                        AutoPostBack="true" OnSelectedIndexChanged="HabilitarDesabilitarAreaGravidez"
                        ValidationGroup="vgSexo">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                        <asp:ListItem Text="FEMININO" Value="Feminino"></asp:ListItem>
                        <asp:ListItem Text="MASCULINO" Value="Masculino"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="SexoRFV" runat="server" ErrorMessage="*Escolha o Sexo"
                        SetFocusOnError="true" ControlToValidate="ddlSexo" InitialValue="0" Display="Dynamic"
                        ToolTip="Qual é seu sexo?" ValidationGroup="MtriculaVG" />
                </div>
       
        <!-- Data Nascimento -->
        <div class="field">
            <asp:Label runat="server" class="lb1" AssociatedControlID="TxtDataNascimento" Text="Data de Nascimento:"
                ID="lblDataNascimento" />
            <asp:TextBox ID="TxtDataNascimento" class="input2" ValidationGroup="MtriculaVG" type="text"
                required="Digite a data em que nasceu" runat="server" MaxLength="10" OnKeyPress="mascaraTexto(##/##/####, this);"
                ToolTip="Data de Nascimento" AutoCompleteType="Disabled" />
        </div>
    </section>
    <h3>
        Certidão de Nascimento</h3>
    <section class="caixa1">
        <%--Cartorio--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Cartorio:" ID="lbl" />
            <asp:TextBox ID="TxtCartorio" ValidationGroup="MtriculaVG" class="input2" runat="server"
                MaxLength="30" ToolTip="Cartorio em que foi escrito"/>
        </div>
        <%-- Certidão Numero--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Número da Certidão:" ID="lblCertidaoNascimento" />
            <asp:TextBox ID="TxtNCertidao" ValidationGroup="MtriculaVG"  class="input2" runat="server"
                MaxLength="15" ToolTip="Número da Certidão de Nascimento" AutoCompleteType="None" />
        </div>
        <%--Livro--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Livro:" ID="lblLivro" />
            <asp:TextBox ID="TxtLivro" ValidationGroup="MtriculaVG" class="input2" runat="server"
                MaxLength="10" ToolTip="Livro em que foi Lavrado" AutoCompleteType="None" />
        </div>
        <%--Folha--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Folha:" ID="lblFolha" />
            <asp:TextBox ID="tbFolha" ValidationGroup="MtriculaVG" AutoCompleteType="None" class="input2"
                runat="server" MaxLength="10" ToolTip="Livro em que foi Lavrado"></asp:TextBox>
        </div>
     
                <%--Estado Natal--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Estado Natal:" ID="lblEstadoNatal" />
                    <asp:DropDownList ID="ddlEstadoNatal" class="input2" ValidationGroup="vgCertidao"
                        runat="server" ToolTip="Estado em que Nasceu." AutoPostBack="true" OnSelectedIndexChanged="CarregarDDLCidadeNatal">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="EstadoNatalRFG" runat="server" ErrorMessage="*Campo Obrigatório"
                        SetFocusOnError="true" ControlToValidate="ddlEstadoNatal" InitialValue="0" Display="Dynamic"
                        ValidationGroup="MtriculaVG" />
                </div>
                <%-- Cidade Natal--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Cidade Natal:" ID="lblNaturalidade" />
                    <asp:DropDownList ID="ddlCidadeNatal" class="input2" runat="server" ValidationGroup="vgCertidao"
                        ToolTip="Cidade em que Nasceu">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="CidadeNatalRFG" runat="server" ErrorMessage="*Campo Obrigatório"
                        SetFocusOnError="true" ControlToValidate="ddlCidadeNatal" InitialValue="0" Display="Dynamic"
                        ValidationGroup="MtriculaVG" />
                </div>
     
        <%--DataCertidao--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Data da Certidão:" ID="lblDataCertidao" />
            <asp:TextBox ID="TxtDataCertidao" class="input2 data" ValidationGroup="MtriculaVG"
                runat="server" MaxLength="10" ToolTip="Data em que a certidão foi lavrada" AutoCompleteType="None"></asp:TextBox>
        </div>
    </section>
    <br />
    <h3>
        Endereço Residêncial</h3>
    <section class="caixa1">
        <%--Endereco--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="Endereco:" ID="lblEndereco" />
            <asp:TextBox ID="txtEndereco" class="input" runat="server" MaxLength="150" required="Digite seu endereço atual"
                ValidationGroup="MtriculaVG" ToolTip="Endereco em que reside. Ex.Rua Dois Irmãos, n 01"
                AutoCompleteType="HomeStreetAddress" />
        </div>
 
                <%--Estado--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Estado:" ID="lblEstado" />
                    <asp:DropDownList ID="ddlEstado" class="input2" runat="server" ToolTip="Estado em que você mora."
                        ValidationGroup="vgEndereco" AutoPostBack="true" OnSelectedIndexChanged="CarregarDDLCidade">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="EstadoRFV" runat="server" ErrorMessage="*Campo Obrigatório"
                        SetFocusOnError="true" ControlToValidate="ddlEstado" InitialValue="0" Display="Dynamic"
                        ToolTip="Em que Estado você mora" ValidationGroup="MtriculaVG" />
                </div>
                <%--Cidade--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Cidade:" ID="lblCidade" />
                    <asp:DropDownList ID="DDLCidade" class="input2" runat="server" ToolTip="Cidade em que você mora."
                        ValidationGroup="vgEndereco" AutoPostBack="true" OnSelectedIndexChanged="CarregarDDLBairroEndereco">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <%--Bairro--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Bairro:" ID="lblBairro" />
                    <asp:DropDownList ID="ddlBairro" class="input2" runat="server" ValidationGroup="vgEndereco"
                        ToolTip="Bairro em que você mora.">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Selecione um Bairro"
                        SetFocusOnError="true" ControlToValidate="ddlBairro" InitialValue="0" Display="Dynamic"
                        ValidationGroup="MtriculaVG" />
                </div>
         
        <%--CEP--%>
        <div class="field">
            <asp:Label runat="server" class="lb1" Text="CEP:" ID="lblCep" />
            <asp:TextBox ID="txtCep" class="input2 cep" runat="server" MaxLength="9" requerid="CEP"
                ToolTip="CEP em que reside. (Somente Numeros)" AutoCompleteType="HomeZipCode" />
        </div>
    </section>
    <%--Pais e Responsavel--%>
   
            <div>
                <h3>
                    Dados dos Pais</h3>
                <section class="caixa1">
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="Nome da Mãe:" ID="lblNomeCartorio" />
                        <asp:TextBox ID="TxtNomeMae" AutoCompleteType="None" class="input" runat="server"
                            ValidationGroup="vgResponsavel" OnTextChanged="HabilitarCampoResponsavel" AutoPostBack="True"
                            MaxLength="150" ToolTip="Nome da Mãe do Aluno"></asp:TextBox>
                    </div>
                    <p />
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="Nome do Pai:" ID="lblPai" />
                        <asp:TextBox ID="txtPai" AutoPostBack="True" AutoCompleteType="None" class="input"
                            ValidationGroup="vgResponsavel" runat="server" MaxLength="150" ToolTip="Nome do Pai do Aluno"
                            OnTextChanged="HabilitarCampoResponsavel"></asp:TextBox>
                    </div>
                </section>
                <p />
                <h3>
                    Dados do Responsável</h3>
                <section class="caixa1">
                    <div class="field">
                        <asp:RadioButtonList ID="rblResponsavel" RepeatDirection="Horizontal" runat="server"
                            ValidationGroup="vgResponsavel" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="HabilitarCampoResponsavel">
                            <asp:ListItem Text="Mãe" Value="Mãe" Selected="False" />
                            <asp:ListItem Text="Pai" Value="Pai" Selected="False" />
                            <asp:ListItem Text="Outros" Value="Outros" Selected="False" />
                        </asp:RadioButtonList>
                    </div>
                    <p />
                    <hr />
                    <p />
                    <%-- Nome Responsavel--%>
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="Nome:" ID="lblResponsavel" />
                        <asp:TextBox ID="txtNomeResponsavel" AutoCompleteType="None" required="Digite o nome do Responsavel"
                            ValidationGroup="MtriculaVG" Enabled="false" class="input" runat="server" MaxLength="150"
                            ToolTip="Nome do Responsavel" />
                    </div>
                    <p />
                    <%-- Cpf Responsavel--%>
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="CPF:" ID="lblCpfResponsavel" />
                        <asp:TextBox ID="txtCpfResponsavel" AutoCompleteType="None" ValidationGroup="MtriculaVG"
                            class="input2 cpf" MaxLength="14" runat="server" ToolTip="CPF do Responsavel pelo Aluno"></asp:TextBox>
                    </div>
                    <p />
                    <%--RG Responsavel--%>
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="RG:" ID="lblRGResponsavel" />
                        <asp:TextBox ID="TxtRGResponsavel" AutoCompleteType="None" Enabled="true" class="input2"
                            ValidationGroup="MtriculaVG" runat="server" MaxLength="15" ToolTip="Número da Identidade"/>
                    </div>
                    <p />
                    <%--Telefone--%>
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="Telefone:" ID="lblTelefone" />
                        <asp:TextBox ID="TbTelefone" AutoCompleteType="HomePhone" Enabled="true" class="input2 telefone"
                            ValidationGroup="MtriculaVG" runat="server" MaxLength="15" ToolTip="Número de Telefone"></asp:TextBox>
                    </div>
                    <p />
                    <%--Celular--%>
                    <div class="field">
                        <asp:Label runat="server" class="lb1" Text="Celular:" ID="lblCelular" />
                        <asp:TextBox ID="TbCelular" AutoCompleteType="Cellular" Enabled="true" class="input2 telefone"
                            ValidationGroup="MtriculaVG" runat="server" MaxLength="15" ToolTip="Número de Celular"/>
                    </div>
                </section>
            </div>
 
    <p />
    <h3>
        Responda a Pesquisa abaixo:</h3>
    <section class="caixa1">
        
                <%--  Ja Estudou--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Já Estudou?" ID="lblJaestudou" />
                    <asp:DropDownList ID="DDLJaEstudou" class="input2" runat="server" ToolTip="Ja estudou alguma vez?"
                        AutoPostBack="true" OnSelectedIndexChanged="PesquisarSeJaEstudou">
                        <asp:ListItem Value="NÃO RESPONDEU" Text="SELECIONE"></asp:ListItem>
                        <asp:ListItem Value="SIM" Text="SIM"></asp:ListItem>
                        <asp:ListItem Value="NÃO" Text="NÃO"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <%-- Motivo--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Motivo Matricula:" ID="lblmotivo" />
                    <asp:DropDownList ID="DDLMotivo" class="input2" runat="server" ToolTip="Por Que está se matriculando?">
                        <asp:ListItem Value="0" Text="SELECIONE"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <%--   Gravida--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Esta gravida?" ID="lblGravida" Visible="false" />
                    <asp:DropDownList ID="DDLGravida" Visible="false" class="input2" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="Gravida">
                        <asp:ListItem Selected="True">NÃO</asp:ListItem>
                        <asp:ListItem>SIM</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <%-- Mes Gravidez--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Quantos meses?" Visible="false" ID="lblMesGravida" />
                    <asp:DropDownList ID="DdlMesGravidez" Visible="false" class="input2" runat="server">
                        <asp:ListItem Selected="True">--</asp:ListItem>
                        <asp:ListItem Selected="False">01</asp:ListItem>
                        <asp:ListItem Selected="False">02</asp:ListItem>
                        <asp:ListItem Selected="False">03</asp:ListItem>
                        <asp:ListItem Selected="False">05</asp:ListItem>
                        <asp:ListItem Selected="False">06</asp:ListItem>
                        <asp:ListItem Selected="False">07</asp:ListItem>
                        <asp:ListItem Selected="False">08</asp:ListItem>
                        <asp:ListItem Selected="False">09</asp:ListItem>
                    </asp:DropDownList>
                </div>
       
    </section>
    <p />
  
            <h3>
                .</h3>
            <p />
            <section class="caixa1">
                <%--Pensao Alimenticia--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Recebe Pensão?" ID="lblpensao" />
                    <asp:RadioButtonList ID="RBLPensãoAlimenticia" required="Selecione uma opção." runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem>SIM</asp:ListItem>
                        <asp:ListItem>NÃO</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p />
                <%--INSS--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Recebe INSS?" ID="lblInss" />
                    <asp:RadioButtonList ID="RBLINSS" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>SIM</asp:ListItem>
                        <asp:ListItem>NÃO</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p />
                <%--Rio Card--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Tem Rio Card?" ID="Label1" />
                    <asp:RadioButtonList ID="RBLRioCard" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>SIM</asp:ListItem>
                        <asp:ListItem>NÃO</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p />
                <%--Bolsa Familia--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Recebe Bolsa Familia?" ID="lblBolsaFammilia" />
                    <asp:RadioButtonList ID="RBLBolsaFamilia" runat="server" Height="26px" RepeatDirection="Horizontal">
                        <asp:ListItem>SIM</asp:ListItem>
                        <asp:ListItem>NÃO</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <p />
                <br />
                <%--Deficiencia--%>
                <div class="field">
                    <asp:Label runat="server" class="lb1" Text="Possui alguma Deficiência?" ID="LblDeficiencia" />
                    <asp:RadioButtonList ID="RBLDeficiente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ExibirEOcultarDeficiencia"
                        RepeatDirection="Horizontal">
                        <asp:ListItem>SIM</asp:ListItem>
                        <asp:ListItem>NÃO</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="field">
                    <asp:CheckBoxList ID="CblDeficiencia" Visible="false" runat="server" RepeatColumns="3"
                        RepeatDirection="Horizontal" RepeatLayout="Table">
                    </asp:CheckBoxList>
                </div>
            </section>
            <p />
            <%--Finalizar Matricula--%>
            <section class="caixa1">
                <%--  Termos--%>
                <div class="field">
                    <asp:TextBox runat="server" ID="txtTermo" Rows="5" class="input" Enabled="false"
                        Visible="true" TextMode="MultiLine" Width="600" ReadOnly="true">
Eu responsável pela criança citada acima, autorizo ao presidente da fundação cultural social esportivo e comunitário de educação Iolanda da Páscoa Ferreira, Sr. Moisés Ferreira RG: 07681664-4/IFP, CPF: 816.490.457-00, a pleitear uma vaga na creche, escola pública ou faculdade para o meu filho.
                    </asp:TextBox>
                </div>
                <%--Aceitar--%>
                <div class="field">
                    <asp:CheckBox ID="ckTermo" runat="server" AutoPostBack="true" OnCheckedChanged="AceitarTermo"
                        Text="Aceitos os Termos acima" Checked="false" />
                </div>
                <p />
                <%-- Botao--%>
                <div class="field">
                    <asp:Button ID="btnMatricularse" Enabled="false" CssClass="button" runat="server"
                        ValidationGroup="MtriculaVG" Text="Go" OnClick="Matricularse" />
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Area Escola--%>
    <asp:Panel ID="pnEscola" Visible="false" Enabled="false" runat="server">
        <h3>
            Escola Pretendida</h3>
        <section class="caixa1">
            <div id="modal" class="modal">
                <div class="field">
                    <asp:Label ID="lblEstadoEscola" AssociatedControlID="DDLEstadoEscola" Text="Estado:"
                        class="lb1" runat="server" />
                    <asp:DropDownList ID="DDLEstadoEscola" class="input2" runat="server" TabIndex="35"
                        AutoPostBack="true" OnSelectedIndexChanged="CarregarDDLCidadeEscola">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="field">
                    <asp:Label ID="lblCidadeEscola" AssociatedControlID="ddlCidadeEscola" Text="Cidade:"
                        class="lb1" runat="server" />
                    <asp:DropDownList ID="ddlCidadeEscola" class="input2" runat="server" TabIndex="35"
                        AutoPostBack="true" OnSelectedIndexChanged="PreencheDDLBairroEscola">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="field">
                    <asp:Label ID="lblBairroEscola" AssociatedControlID="ddlBairroEscola" Text="Bairro:"
                        class="lb1" runat="server" />
                    <asp:DropDownList ID="ddlBairroEscola" class="input2" runat="server" TabIndex="35">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="field">
                    <asp:Label runat="server" Text="Tipo Escola:" class="lb1" ID="lblTipoEscola" />
                    <asp:DropDownList ID="DDLTipoEsola" class="input2" runat="server" ToolTip="Nivel Escolar">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="field">
                    <asp:Label CssClass="label" class="lb1" runat="server" Text="Serie:" ID="LblSerie" />
                    <asp:DropDownList ID="DdlSerieEscola" class="input2" runat="server" ToolTip="Serie que irá estudar">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <br />
                <div class="field">
                    <asp:Label CssClass="label" class="lb1" runat="server" Text="Nome da Escola:" ID="lblNomeEscola" />
                    <asp:DropDownList ID="DdlNomeEscola" class="input" runat="server" required="Escola uma Escola"
                        ToolTip="Nome da Escolar">
                        <asp:ListItem Text="SELECIONE" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <p />
            <div class="field">
                <asp:HiddenField ID="hfEscolaOrigem" runat="server" />
                <asp:Label ID="lblEscolaOrigemTxt" class="lb1" runat="server" Text="Escola de Origem:"></asp:Label>
            </div>
            <p />
            <br />
            <div class="field">
                <asp:HiddenField ID="hfEscolaDestino" runat="server" />
                <asp:Label ID="lblEscolaDestinoTxt" class="lb1" runat="server" Text="Escola de Destino:"></asp:Label>
            </div>
        </section>
    </asp:Panel>
</asp:Content>