using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using Business;
using Ultilitarios;
namespace WebSite
{
    public partial class Matricula:System.Web.UI.Page
    {
        private ListItem motivo;


        protected static DataTable dTable = new DataTable();


        protected void Page_Load(object sender,EventArgs e)
        {
            try
            {

                if(!IsPostBack)
                {
                    PreencheDDLEstado();
                    divAlert.Visible = false;
                }
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "Page_Load" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
        }

        protected void Matricularse(object sender,EventArgs e)
        {
            long idResponsavel = 0,idUsuario = 0;
            int idCertidao = 0,idPesquisa = 0;

            try
            {
                if(VerificarSeAceitouTermo())
                {
                    #region Seta e converte os dados da Matricula

                    int idCidade,
                        idBairro,
                        idEstado,
                        idMotivoMatricula = new int();

                    DateTime dataNascimento = new DateTime();

                    string
                        cpfAluno = TxtCPF.Text,
                        cep = txtCep.Text,
                        endereco = txtEndereco.Text,
                        nomeAluno = TxtNome.Text,
                        nomeMae = TxtNomeMae.Text,
                        nomePai = txtPai.Text,
                        orgaoEmissorRG = TxtOrgaoEmissor.Text,
                        rG = TxtRG.Text,
                        sexo = ddlSexo.SelectedItem.Text;

                    int.TryParse(ddlEstado.SelectedValue,out idEstado);
                    int.TryParse(ddlBairro.SelectedValue,out idBairro);
                    int.TryParse(DDLCidade.SelectedValue,out idCidade);
                    int.TryParse(DDLMotivo.SelectedValue,out idMotivoMatricula);

                    DateTime.TryParse(TxtDataNascimento.Text,out dataNascimento);




                    #endregion

                    idResponsavel = CadastrarResponsavel();
                    idCertidao = CadastrarCertidao();
                    idPesquisa = CadastrarPesquisa();

                    idUsuario = UsuarioBus.Inserir
                                 (nomeAluno,cpfAluno,dataNascimento,sexo,rG,orgaoEmissorRG,endereco,cep,
                                 idCertidao,idPesquisa,idMotivoMatricula,
                                 idCidade,1,idBairro,idEstado,idResponsavel,
                                 nomePai,nomeMae);

                    CadastrarDeficiencia(idUsuario);
                    CadastrarEscolaEscolhida(idUsuario);
                }
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = "Alerta Erro Matricularse <br />" + erro.Alerta;

                divAlert.InnerHtml += "<br /> <br />" + AlertaException.EnviarEmailSuporte(erro);

                divAlert.Visible = true;
                CancelarMatricula(idUsuario,idCertidao,idPesquisa);
            }
            catch(Exception ex)
            {
                divAlert.InnerHtml = "Matricularse Erro" + AlertaException.EnviarEmailSuporte(ex);
                divAlert.Visible = true;
                CancelarMatricula(idUsuario,idCertidao,idPesquisa);

            }
            if(idUsuario > 0)
                Response.Redirect("Matricula_Print.aspx?id=" + idUsuario);
            #endregion
        }

        #region Cadastros

        /// <summary>Obtém o identificador do alune e relaciona com as 
        /// escolas escolhidas de Destino, Origem e com a serie escolar do Aluno</summary>
        /// <param name="idUsuario">Identificador do Aluno</param>
        protected void CadastrarEscolaEscolhida(long idUsuario)
        {
            try
            {
                int escolaOrigem = 0;
                int escolaDestino = 0;
                int idSerieEscola = 0;
                int.TryParse(hfEscolaOrigem.Value,out escolaOrigem);
                int.TryParse(hfEscolaDestino.Value,out escolaDestino);
                int.TryParse(DdlSerieEscola.SelectedValue,out idSerieEscola);

                if(escolaDestino > 0 && escolaOrigem > 0)
                    EscolaEscolhidaBus.Inserir(idUsuario,escolaOrigem,escolaDestino,idSerieEscola);


            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>Envia os dados do responsavel para ser cadastrado no banco
        /// <para>Se já estiver cadastrado retorna o id do cadastro anterior</para>
        /// </summary>
        /// <returns>Retorna o identificador do responsavel</returns>
        protected long CadastrarResponsavel()
        {

            try
            {
                string cpfResponsavel = txtCpfResponsavel.Text,
                       nomeResponsavel = txtNomeResponsavel.Text,
                identidadeResponsavel = TxtRGResponsavel.Text,
                telefone = TbTelefone.Text,
                celular = TbCelular.Text;

                return ResponsavelBus.Inserir
                       (nomeResponsavel,cpfResponsavel,identidadeResponsavel,celular,telefone);
            }
            #region Exceçoes

            catch
            {
                throw;
            }
            #endregion

        }

        protected int CadastrarCertidao()
        {
            try
            {
                string folhaCertidao = tbFolha.Text,
                 livroCertidao = TxtLivro.Text,
                 nCertidao = TxtNCertidao.Text,
                 cartorio = TxtCartorio.Text,
                 sdataCertidao = TxtDataCertidao.Text;
                int idCidadeNatal = 0;
                int.TryParse(ddlCidadeNatal.SelectedValue,out idCidadeNatal);

                DateTime dataCertidao = DateTime.MinValue;
                DateTime.TryParse(sdataCertidao,out dataCertidao);

                return CertidaoNascimentoBus.Inserir
                    (nCertidao,livroCertidao,folhaCertidao,cartorio,dataCertidao,idCidadeNatal);

            }
            catch(Exception)
            {
                return 0;

            }
        }

        protected int CadastrarPesquisa()
        {
            try
            {
                string
                     jaEstudou = DDLJaEstudou.SelectedValue,
                    recebePensaoALimenticia = RBLPensãoAlimenticia.SelectedValue,
                    inss = RBLINSS.SelectedValue,
                    bolsaFamilia = RBLBolsaFamilia.SelectedValue,
                estaGravida = DDLGravida.SelectedValue,
                    mesGravidez = DdlMesGravidez.SelectedValue;

                return PesquisaBus.Inserir(jaEstudou,recebePensaoALimenticia,inss,bolsaFamilia,
                   estaGravida,mesGravidez);

            }
            catch
            {
                throw;
            }

        }

        protected void CadastrarDeficiencia(long idUsuario)
        {
            try
            {
                List<int> listaDefinciencia = new List<int>();

                if(CblDeficiencia.Visible = true && CblDeficiencia.Items.Count > 0)
                {
                    foreach(ListItem i in CblDeficiencia.Items)
                        if(i.Selected == true)
                            listaDefinciencia.Add(int.Parse(i.Value));

                    UsuDeficienciaBus.Inserir(idUsuario,listaDefinciencia);
                }
            }
            catch
            {
                throw;
            }


        }

        protected void CancelarMatricula(long idUsuario,int idCertidao,int idPesquisa)
        {

            try
            {
                if(idUsuario > 0)
                {
                    UsuDeficienciaBus.ApagarAsDeficienciasDoUsuario(idUsuario);
                    EscolaEscolhidaBus.ApagarAsEscolasEscolhidasPeloUsuario(idUsuario);
                    UsuarioBus.Apagar(idUsuario);

                }

                if(idCertidao > 0)
                    CertidaoNascimentoBus.Apagar(idCertidao);

                if(idPesquisa > 0)
                    PesquisaBus.Apagar(idPesquisa);



            }
            #region Exceçoes
            catch(AlertaException erro)
            {
                divAlert.InnerHtml = "CancelarMatricula Alerta:" + erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "CancelarMatricula Erro:" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        #endregion

        #region Validações de campos do formulario

        protected void Gravida(object sender,EventArgs e)
        {
            try
            {
                if(DDLGravida.SelectedValue == "Sim")
                {
                    lblMesGravida.Visible = true;
                    DdlMesGravidez.Visible = true;

                }
                else
                {

                    DdlMesGravidez.Visible = false;

                    lblMesGravida.Visible = false;
                }
            }
            catch(Exception)
            {

                throw;
            }

        }

        protected void HabilitarDesabilitarAreaGravidez(object sender,EventArgs e)
        {
            try
            {
                if(ddlSexo.SelectedValue != "Feminino")
                {
                    DDLGravida.Visible = false;
                    DdlMesGravidez.Visible = false;
                    lblGravida.Visible = false;
                    lblMesGravida.Visible = false;
                }
                else
                {
                    DDLGravida.Visible = true;

                    lblGravida.Visible = true;

                }
                // upPesquisa1.Update();
            }
            catch(Exception)
            {

                throw;
            }


        }


        protected bool VerificarSeAceitouTermo()
        {
            try
            {

                if(!ckTermo.Checked)
                {
                    divAlert.InnerHtml = "Aceite nossos termos";
                    divAlert.Visible = true;
                    return false;
                }
                divAlert.Visible = false;
                return true;
            }
            #region Exceçoes
            catch(Exception Error)
            {
                divAlert.InnerHtml = "VerificarSeAceitouTermo" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
                throw;
            }
            #endregion
        }

        protected void AceitarTermo(object sender,EventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)sender;

                if(cb.Checked)
                    btnMatricularse.Enabled = true;
                else
                    btnMatricularse.Enabled = false;

            }

            #region Exceçoes
            catch(AlertaException erro)
            {
                divAlert.InnerHtml = "AceitarTermo" + erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "AceitarTermo" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        /// <summary>Habilita o preenchimento dos dados do Responsavel</summary>
        protected void HabilitarCampoResponsavel(object sender,EventArgs e)
        {
            try
            {
                if(rblResponsavel.SelectedValue == "Mãe")
                {
                    txtNomeResponsavel.Text = TxtNomeMae.Text;
                    txtNomeResponsavel.Enabled = false;
                    txtNomeResponsavel.CssClass += "input";
                }
                else if(rblResponsavel.SelectedValue == "Pai")
                {
                    txtNomeResponsavel.Text = txtPai.Text;
                    txtNomeResponsavel.Enabled = false;
                    txtNomeResponsavel.CssClass += "input";

                }
                else
                {
                    txtNomeResponsavel.Text = "";
                    txtNomeResponsavel.Enabled = true;
                }

                if(TbCelular.Enabled == false)
                    TbCelular.Enabled = true;
                if(TbTelefone.Enabled == false)
                    TbTelefone.Enabled = true;
                if(txtCpfResponsavel.Enabled == false)
                    txtCpfResponsavel.Enabled = true;

                if(TxtRGResponsavel.Enabled == false)
                    TxtRGResponsavel.Enabled = true;

                if(txtPai.AutoPostBack == false)
                    txtPai.AutoPostBack = true;
                if(TxtNomeMae.AutoPostBack == false)
                    TxtNomeMae.AutoPostBack = true;
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        protected void PesquisarSeJaEstudou(object sender,EventArgs e)
        {
            try
            {
                if(sender.Equals(DDLJaEstudou))
                {
                    DropDownList ddl = (DropDownList)sender;
                    if(ddl.SelectedValue == "SIM")
                    {

                        motivo = DDLMotivo.Items.FindByText("Nunca Estudou");
                        if(motivo != null)
                            ddl.Items.Remove(motivo);
                    }
                }

            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion

        }

        #endregion

        #region Preencher Controles

        /// <summary>Preenche todas DropDownList Estado</summary>
        protected void PreencheDDLEstado()
        {
            try
            {

                MotivoMatriculaBus.Pesquisar(DDLMotivo);
                EstadoBus.TodosEstados(DDLEstadoEscola,
ddlEstado,ddlEstadoNatal);

            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "PreencheDDLEstado" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        /// <summary>Exibe ou Oculta o controle que exibe as deficiencias</summary>
        protected void ExibirEOcultarDeficiencia(object sender,EventArgs e)
        {
            try
            {
                if(dTable.Rows.Count == 0)
                {
                    dTable = DeficienciaBus.Pesquisar();
                    CblDeficiencia.DataSource = dTable;
                    CblDeficiencia.DataTextField = "nome";
                    CblDeficiencia.DataValueField = "id";
                    CblDeficiencia.DataBind();
                }

                if(RBLDeficiente.SelectedValue == "SIM")
                    CblDeficiencia.Visible = true;
                else
                    CblDeficiencia.Visible = false;

            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "ExibirEOcultarDeficiencia" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        /// <summary>Preenche a DDL Cidade Natal</summary>
        protected void CarregarDDLCidadeNatal(object sender,EventArgs e)
        {
            try
            {
                int idEstado;

                if(int.TryParse(ddlEstadoNatal.SelectedValue,out  idEstado))
                    CidadeBus.Pesquisar(idEstado,ddlCidadeNatal);
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "CarregarDDLCidadeNatal" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion

        }

        /// <summary>Carrega a DDL Cidade do Endereco</summary>
        protected void CarregarDDLCidade(object sender,EventArgs e)
        {
            try
            {
                int idEstado;

                if(int.TryParse(ddlEstado.SelectedValue,out  idEstado))
                    CidadeBus.Pesquisar(idEstado,DDLCidade);
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "CarregarDDLCidade" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        /// <summary>Preenche a DDL Bairro do Endereco</summary>
        protected void CarregarDDLBairroEndereco(object sender,EventArgs e)
        {

            try
            {
                int idCidade;

                if(int.TryParse(DDLCidade.SelectedValue,out  idCidade))
                    BairroBus.Pesquisar(ddlBairro,idCidade);
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "CarregarDDLBairroEndereco" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }

        /// <summary>Preenche a DDL Cidade da Escola</summary>
        protected void CarregarDDLCidadeEscola(object sender,EventArgs e)
        {
            try
            {

                int idEstado;

                if(int.TryParse(DDLEstadoEscola.SelectedValue,out  idEstado))
                    CidadeBus.Pesquisar(idEstado,ddlCidadeEscola);
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "CarregarDDLCidadeEscola" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion

        }

        /// <summary>Preenche a DDL Bairro da Escola</summary>
        protected void PreencheDDLBairroEscola(object sender,EventArgs e)
        {
            try
            {
                int idCidade;

                if(int.TryParse(ddlCidadeEscola.SelectedValue,out  idCidade))
                    BairroBus.Pesquisar(ddlBairroEscola,idCidade);
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = "PreencheDDLBairroEscola" + AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;
            }
            #endregion
        }


        #endregion

    }
}