using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web.UI.WebControls;
using Business;
using Ferramentas;
namespace WebSite
{
    public partial class Matricula:System.Web.UI.Page
    {
        private ListItem _motivo;

        protected static DataTable DTable;

        #region Propriedades da Classe
        #region Propriedades do Usuario (Somente Leitura)
        private int _idCidade;

        private int _idBairro;

        private int _idEstado;

        private int _idMotivoMatricula;

        private DateTime _dataNascimento;

        private string _cpfAluno;

        private string _cep;

        private string _endereco;

        private string _nomeAluno;

        private string _nomeMae;

        private string _nomePai;

        private string _orgaoEmissorRg;

        private string _rG;

        private string _sexo;

        private long _idResponsavel;
        private int _idPesquisa;
        private int _idCertidao;
        long _idUsuario;
        #endregion

        #region Propriedades da Certidão de Nascimento (Somente Leitura)

        private string _folhaCertidao;

        private string _livroCertidao;

        private string _nCertidao;

        private string _cartorio;

        private string _sdataCertidao;

        private int _idCidadeNatal;

        #endregion

        #endregion

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
            catch(Exception error)
            {
                divAlert.InnerHtml = "Page_Load" + AlertaException.EnviarEmailSuporte(error);
                divAlert.Visible = true;
            }
        }

        private void InicializarVariaveis()
        {
            // Propriedades do Usuario (Somente Leitura)
            int.TryParse(ddlEstado.SelectedValue,out _idEstado);
            int.TryParse(ddlBairro.SelectedValue,out _idBairro);
            int.TryParse(DDLCidade.SelectedValue,out _idCidade);
            int.TryParse(DDLMotivo.SelectedValue,out _idMotivoMatricula);

            DateTime.TryParse(TxtDataNascimento.Text,out _dataNascimento);

            _cpfAluno = TxtCPF.Text;
            _cep = txtCep.Text;
            _endereco = txtEndereco.Text;
            _nomeAluno = TxtNome.Text;
            _nomeMae = TxtNomeMae.Text;
            _nomePai = txtPai.Text;
            _orgaoEmissorRg = TxtOrgaoEmissor.Text;
            _rG = TxtRG.Text;
            _sexo = ddlSexo.SelectedItem.Text;


            // Propriedades da Certidão de Nascimento (Somente Leitura)
            _folhaCertidao = tbFolha.Text;
            _livroCertidao = TxtLivro.Text;
            _nCertidao = TxtNCertidao.Text;
            _cartorio = TxtCartorio.Text;
            _sdataCertidao = TxtDataCertidao.Text;

            int.TryParse(ddlCidadeNatal.SelectedValue,out _idCidadeNatal);

            DTable = new DataTable();
        }

        protected void Matricularse(object sender,EventArgs e)
        {
            InicializarVariaveis();
            if(!VerificarSeAceitouTermo())
                return;

            _idResponsavel = CadastrarResponsavel();
            _idPesquisa = CadastrarPesquisa();
            _idCertidao = CadastrarCertidao();

            try
            {

                _idUsuario = UsuarioBus.Inserir
                  (_nomeAluno,_cpfAluno,_dataNascimento,_sexo,_rG,_orgaoEmissorRg,_endereco,_cep,
                   _idCertidao,_idPesquisa,_idMotivoMatricula,
                   _idCidade,1,_idBairro,_idEstado,_idResponsavel,
                   _nomePai,_nomeMae);

                CadastrarDeficiencia(_idUsuario);
                CadastrarEscolaEscolhida(_idUsuario);
                if(_idUsuario > 0)
                    Response.Redirect("Matricula_Print.aspx?id=" + _idUsuario);
            }

            catch(AlertaException erro)
            {
                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
                CancelarMatricula();
            }
            catch(Exception ex)
            {
                divAlert.InnerHtml = "Matricularse Erro" + AlertaException.EnviarEmailSuporte(ex);
                divAlert.Visible = true;
                CancelarMatricula();

            }
          

        }


        #region Rever esses codigos

        protected void Gravida(object sender,EventArgs e)
        {
            switch(DDLGravida.SelectedValue)
            {
                case "Sim":
                    lblMesGravida.Visible = true;
                    DdlMesGravidez.Visible = true;
                    break;
                default:
                    DdlMesGravidez.Visible = false;
                    lblMesGravida.Visible = false;
                    break;
            }
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
                        _motivo = DDLMotivo.Items.FindByText("Nunca Estudou");
                        if(_motivo != null)
                            ddl.Items.Remove(_motivo);
                    }
                }
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception error)
            {
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(error);
                divAlert.Visible = true;
            }
            #endregion

        }

        /// <summary>Não esta sendo usando no momento</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            catch
            {
            }


        }

        /// <summary>Obtém o identificador do alune e relaciona com as 
        /// escolas escolhidas de Destino, Origem e com a serie escolar do Aluno</summary>
        /// <param name="idUsuario">Identificador do Aluno</param>
        protected void CadastrarEscolaEscolhida(long idUsuario)
        {
            int escolaOrigem;
            int escolaDestino;
            int idSerieEscola;
            int.TryParse(hfEscolaOrigem.Value,out escolaOrigem);
            int.TryParse(hfEscolaDestino.Value,out escolaDestino);
            int.TryParse(DdlSerieEscola.SelectedValue,out idSerieEscola);

            if(escolaDestino > 0 && escolaOrigem > 0)
                EscolaEscolhidaBus.Inserir(idUsuario,escolaOrigem,escolaDestino,idSerieEscola);
        }

        #endregion

        #region Cadastros


        /// <summary>Envia os dados do responsavel para ser cadastrado no banco
        /// <para>Se já estiver cadastrado retorna o id do cadastro anterior</para>
        /// </summary>
        /// <returns>Retorna o identificador do responsavel</returns>
        protected long CadastrarResponsavel()
        {
            string cpfResponsavel = txtCpfResponsavel.Text,
                   nomeResponsavel = txtNomeResponsavel.Text,
                   identidadeResponsavel = TxtRGResponsavel.Text,
                   telefone = TbTelefone.Text,
                   celular = TbCelular.Text;

            return ResponsavelBus.Inserir
                (nomeResponsavel,cpfResponsavel,identidadeResponsavel,celular,telefone);

        }

        protected int CadastrarCertidao()
        {
            try
            {
                DateTime dataCertidao;
                DateTime.TryParse(_sdataCertidao,out dataCertidao);

                return CertidaoNascimentoBus.Inserir
                    (_nCertidao,_livroCertidao,_folhaCertidao,_cartorio,dataCertidao,_idCidadeNatal);

            }
            catch(Exception)
            {
                return 0;

            }
        }

        protected int CadastrarPesquisa()
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

        protected void CadastrarDeficiencia(long idUsuario)
        {

            if(!CblDeficiencia.Visible || (CblDeficiencia.Items.Count <= 0))
                return;
            List<int> listaDefinciencia = new List<int>();

            listaDefinciencia.AddRange
                (from ListItem i in CblDeficiencia.Items
                 where i.Selected
                 select int.Parse(i.Value));

            UsuDeficienciaBus.Inserir(idUsuario,listaDefinciencia);
        }

        protected void CancelarMatricula()
        {

            try
            {

                if(_idUsuario > 0)
                {
                    UsuDeficienciaBus.ApagarAsDeficienciasDoUsuario(_idUsuario);
                    EscolaEscolhidaBus.ApagarAsEscolasEscolhidasPeloUsuario(_idUsuario);
                    UsuarioBus.Apagar(_idUsuario);

                }

                if(_idCertidao > 0)
                    CertidaoNascimentoBus.Apagar(_idCertidao);

                if(_idPesquisa > 0)
                    PesquisaBus.Apagar(_idPesquisa);



            }
            #region Exceçoes
            catch(AlertaException erro)
            {
                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception error)
            {
                divAlert.InnerHtml = "CancelarMatricula Erro:" + AlertaException.EnviarEmailSuporte(error);
                divAlert.Visible = true;
            }
            #endregion
        }

        #endregion

        #region Funçoes que Controla a visibilidade dos itens na tela

        /// <summary>Exibe ou Oculta o controle que exibe as deficiencias</summary>
        protected void ExibirEOcultarDeficiencia(object sender,EventArgs e)
        {
            try
            {
                CblDeficiencia.Visible = RBLDeficiente.SelectedValue == "SIM";
                if(!CblDeficiencia.Visible)
                    return;

                DTable = (DTable == null || DTable.Rows.Count == 0) ? DeficienciaBus.Pesquisar() : DTable;

                CblDeficiencia.DataSource = DTable;
                CblDeficiencia.DataTextField = "nome";
                CblDeficiencia.DataValueField = "id";
                CblDeficiencia.DataBind();


            }

            catch(AlertaException erro)
            {
                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception error)
            {
                divAlert.InnerHtml = "ExibirEOcultarDeficiencia" + AlertaException.EnviarEmailSuporte(error);
                divAlert.Visible = true;
            }

        }


        /// <summary>Habilita o preenchimento dos dados do Responsavel</summary>
        protected void HabilitarCampoResponsavel(object sender,EventArgs e)
        {
            switch(rblResponsavel.SelectedValue)
            {
                case "Mãe":
                    txtNomeResponsavel.Text = TxtNomeMae.Text;
                    txtNomeResponsavel.Enabled = false;
                    //        txtNomeResponsavel.CssClass += " input ";
                    TxtNomeMae.AutoPostBack = true;
                    txtPai.AutoPostBack = false;
                    break;
                case "Pai":
                    txtNomeResponsavel.Text = txtPai.Text;
                    txtNomeResponsavel.Enabled = false;
                    //    txtNomeResponsavel.CssClass += " input ";
                    txtPai.AutoPostBack = true;
                    TxtNomeMae.AutoPostBack = false;
                    break;
                default:
                    txtNomeResponsavel.Text = "";
                    txtNomeResponsavel.Enabled = true;
                    txtPai.AutoPostBack = false;
                    TxtNomeMae.AutoPostBack = false;
                    break;
            }

        }

        /// <summary>Habilita o botão que envia o formulario pro servidor</summary>
        /// <param name="sender">CheckBox que informa se aceitou os termos</param>
        /// <param name="e"></param>
        protected void AceitarTermo(object sender,EventArgs e)
        {
            btnMatricularse.Enabled = (sender is CheckBox) && (sender as CheckBox).Checked;
        }


        #endregion

        #region  Area destinadas a funçoes que preenche controles de dados na pagina

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
            catch(Exception error)
            {
                divAlert.InnerHtml = "PreencheDDLEstado" + AlertaException.EnviarEmailSuporte(error);
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
            catch(Exception error)
            {
                divAlert.InnerHtml = "CarregarDDLCidadeNatal" + AlertaException.EnviarEmailSuporte(error);
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
            catch(Exception error)
            {
                divAlert.InnerHtml = "CarregarDDLCidade" + AlertaException.EnviarEmailSuporte(error);
                divAlert.Visible = true;
            }
            #endregion
        }

        /// <summary>Preenche a DDL Bairro do Endereco</summary>
        protected void CarregarDDLBairroEndereco(object sender,EventArgs e)
        {
            try
            {
                int.TryParse(DDLCidade.SelectedValue,out _idCidade);
                BairroBus.Pesquisar(ddlBairro,_idCidade);
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;
            }
            catch(Exception error)
            {
                divAlert.InnerHtml = "CarregarDDLBairroEndereco" + AlertaException.EnviarEmailSuporte(error);
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
            catch(Exception error)
            {
                divAlert.InnerHtml = "CarregarDDLCidadeEscola" + AlertaException.EnviarEmailSuporte(error);
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
            catch(Exception error)
            {
                divAlert.InnerHtml = "PreencheDDLBairroEscola" + AlertaException.EnviarEmailSuporte(error);
                divAlert.Visible = true;
            }
            #endregion
        }


        #endregion

        #region Verifica se foi digitado tudo corretamente e dentro da logica do sistema

        /// <summary>Verifica se o usuario aceitou os termos de cadastro do sistema</summary>
        /// <returns>Retorna True caso tenha aceito</returns>
        protected bool VerificarSeAceitouTermo()
        {
            return (ckTermo.Checked);

        }



        #endregion



    }
}