using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using Ferramentas;

namespace WebSite
{
    public partial class Matricula_Print:System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            try
            {
                long matricula;
                long.TryParse(Request.QueryString["id"],out matricula);
                bool fazer = false;

                if(matricula > 0)
                {
                    fazer = true;
                    pnMatricula.Visible = true;
                    pnPesquisa.Visible = false;
                }
                else
                {
                    fazer = false;
                    pnMatricula.Visible = false;
                    pnPesquisa.Visible = true;
                }


                if(!IsPostBack)
                {
                    if(fazer)
                        CarregarRepeater(matricula);

                }
            }

            #region Exception
            catch(AlertaException alerta)
            {
                divAlert.InnerHtml = alerta.Alerta;
                divAlert.Visible = true;
                rpMatricula.Visible = false;
                pnPesquisa.Visible = true;
            }
            catch(Exception er)
            {
                divAlert.InnerHtml = Ferramentas.AlertaException.EnviarEmailSuporte(er);
                divAlert.Visible = true;
            }
            #endregion
        }

        private void CarregarRepeater(long id)
        {
            try
            {

                rpMatricula.DataSource = UsuarioBus.Pesquisar(id);
                rpMatricula.DataBind();
                pnMatricula.Visible = true;
                pnPesquisa.Visible = false;
            }

            #region Exceptions
            catch(AlertaException alerta)
            {
                divAlert.InnerHtml = alerta.Alerta;
                divAlert.Visible = true;
                rpMatricula.Visible = false;
                pnPesquisa.Visible = true;
            }
            catch(Exception er)
            {
                divAlert.InnerHtml = Ferramentas.AlertaException.EnviarEmailSuporte(er);
                divAlert.Visible = true;
            }
            #endregion
        }

        protected void PsqPorMatriculas(object sender,EventArgs e)
        {
            try
            {
                string commandArgument = ((Button)sender).CommandArgument;
                switch(commandArgument)
                {

                    case "Matricula":
                        #region
                        string matricula = txtPsqMatricula.Text;
                        long id = 0;

                        if(long.TryParse(matricula,out id))
                        {
                            CarregarRepeater(id);
                            pnMatricula.Visible = true;
                            rpMatricula.Visible = true;
                            pnPesquisa.Visible = false;
                        }
                        else
                        {
                            throw
                                new FormatException("Número da Matricula inválido, certifique-se que não a letras no meio");
                        }
                        break;
                        #endregion

                    case "CPF":
                        #region CPF

                        string cpf = txtPsqCpf.Text;
                        string dta = txtPsqNascimento.Text;

                        DateTime dataNascimento = Convert.ToDateTime(dta);

                        rpMatricula.DataSource = UsuarioBus.Pesquisar(cpf,dataNascimento);
                        rpMatricula.DataBind();
                        pnMatricula.Visible = true;
                        pnPesquisa.Visible = false;
                        break;
                        #endregion
                    case "Responsavel":

                        break;
                }
            }
            #region Exception
            catch(FormatException Error)
            {

                divAlert.InnerHtml = Error.Message;
                divAlert.Visible = true;
                rpMatricula.Visible = false;
                pnPesquisa.Visible = true;
            }
            catch(AlertaException alerta)
            {
                divAlert.InnerHtml = alerta.Alerta;
                divAlert.Visible = true;
                rpMatricula.Visible = false;
                pnPesquisa.Visible = true;
            }
            catch(Exception er)
            {
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(er);
                divAlert.Visible = true;
                pnMatricula.Visible = false;
                pnPesquisa.Visible = true;
            }
            #endregion
        }

        #region Repeater

        protected void ItemDataBound(object sender,RepeaterItemEventArgs e)
        {
            try
            {
                RepeaterItem item = e.Item;
                if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    #region Usuario
                    #region Obtendo os Controles
                    Label
                        lblId = (Label)item.FindControl("lblId"),
                        lblNome = (Label)item.FindControl("lblNome"),
                       lblCpf = (Label)item.FindControl("lblCpf"),
                     lblDataNascimento = (Label)item.FindControl("lblDataNascimento"),
                       lblSexo = (Label)item.FindControl("lblSexo"),
                    lblIdentidade = (Label)item.FindControl("lblIdentidade"),
                     lblOrgaoEmissor = (Label)item.FindControl("lblOrgaoEmissor"),
                                          lblNomeMae = (Label)item.FindControl("lblNomeMae"),
                        lblNomePai = (Label)item.FindControl("lblNomePai"),
                        lblDataCadastro = (Label)item.FindControl("lblDataCadastro"),
      lblEndereco = (Label)item.FindControl("lblEndereco"),
                 lblCEP = (Label)item.FindControl("lblCEP"),
        lblEstado = (Label)item.FindControl("lblEstado"),
        lblCidade = (Label)item.FindControl("lblCidade"),
          lblBairro = (Label)item.FindControl("lblBairro"),
            lblMotivo = (Label)item.FindControl("lblMotivo"),
      lblStatus = (Label)item.FindControl("lblStatus");

                    #endregion

                    #region Definindo Valores Valores

                    lblId.Text = DataBinder.Eval(item.DataItem,"id").ToString();
                    lblNome.Text = DataBinder.Eval(item.DataItem,"nome").ToString();
                    lblCpf.Text = DataBinder.Eval(item.DataItem,"cpf").ToString();
                    lblDataNascimento.Text = DataBinder.Eval(item.DataItem,"dataNascimento").ToString();
                    lblSexo.Text = DataBinder.Eval(item.DataItem,"sexo").ToString();
                    lblIdentidade.Text = DataBinder.Eval(item.DataItem,"identidade").ToString();
                    lblOrgaoEmissor.Text = DataBinder.Eval(item.DataItem,"orgaoEmissor").ToString();
                    lblNomeMae.Text = DataBinder.Eval(item.DataItem,"nomeMae").ToString();
                    lblNomePai.Text = DataBinder.Eval(item.DataItem,"nomePai").ToString();
                    lblDataCadastro.Text = DataBinder.Eval(item.DataItem,"dataCadastro").ToString();
                    lblEndereco.Text = DataBinder.Eval(item.DataItem,"endereco").ToString();
                    lblCEP.Text = DataBinder.Eval(item.DataItem,"cep").ToString();
                    lblEstado.Text = DataBinder.Eval(item.DataItem,"estado").ToString();
                    lblCidade.Text = DataBinder.Eval(item.DataItem,"cidade").ToString();
                    lblBairro.Text = DataBinder.Eval(item.DataItem,"bairro").ToString();
                    lblMotivo.Text = DataBinder.Eval(item.DataItem,"motivo").ToString();
                    lblStatus.Text = DataBinder.Eval(item.DataItem,"status").ToString();


                    #endregion

                    #endregion

                    #region Certidao
                    #region Obtendo os Controles
                    Label
                        lblCN_Cartorio = (Label)item.FindControl("lblCN_Cartorio"),
                        lblCN_Estado = (Label)item.FindControl("lblCN_Estado"),
                        lblCN_Cidade = (Label)item.FindControl("lblCN_Cidade"),
                        lblCN_Numero = (Label)item.FindControl("lblCN_Numero"),
                        lblCN_Livro = (Label)item.FindControl("lblCN_Livro"),
                        lblCN_Folha = (Label)item.FindControl("lblCN_Folha"),
                        lblCN_Data = (Label)item.FindControl("lblCN_Data");
                    #endregion

                    #region Definindo Valores Valores
                    lblCN_Numero.Text = DataBinder.Eval(item.DataItem,"CN_Numero").ToString();
                    lblCN_Livro.Text = DataBinder.Eval(item.DataItem,"CN_Livro").ToString();
                    lblCN_Folha.Text = DataBinder.Eval(item.DataItem,"CN_Folha").ToString();
                    lblCN_Cartorio.Text = DataBinder.Eval(item.DataItem,"CN_Cartorio").ToString();
                    lblCN_Data.Text = DataBinder.Eval(item.DataItem,"CN_Data").ToString();
                    lblCN_Estado.Text = DataBinder.Eval(item.DataItem,"CN_Estado").ToString();
                    lblCN_Cidade.Text = DataBinder.Eval(item.DataItem,"CN_Cidade").ToString();
                    #endregion
                    #endregion

                    #region Pesquisa
                    #region Obtendo os Controles
                    Label
                        lblJaEstudou = (Label)item.FindControl("lblJaEstudou"),
                        lblPensão = (Label)item.FindControl("lblPensão"),
                        lblINSS = (Label)item.FindControl("lblINSS"),
                        lblBolsaFamilia = (Label)item.FindControl("lblBolsaFamilia"),
                        lblGravida = (Label)item.FindControl("lblGravida"),
                        lblMesGravidez = (Label)item.FindControl("lblMesGravidez");

                    #endregion

                    #region Definindo Valores Valores
                    lblJaEstudou.Text = DataBinder.Eval(item.DataItem,"PSQ_JaEstudou").ToString();
                    lblPensão.Text = DataBinder.Eval(item.DataItem,"PSQ_RecebePensao").ToString();
                    lblINSS.Text = DataBinder.Eval(item.DataItem,"PSQ_Inss").ToString();
                    lblBolsaFamilia.Text = DataBinder.Eval(item.DataItem,"PSQ_BolsaFamilia").ToString();
                    lblGravida.Text = DataBinder.Eval(item.DataItem,"PSQ_EstaGravida").ToString();
                    lblMesGravidez.Text = DataBinder.Eval(item.DataItem,"PSQ_MesGravidez").ToString();
                    #endregion


                    #endregion

                    #region Responsavel

                    #region Obtendo os Controles
                    Label
                        lblRSP_Nome = (Label)item.FindControl("lblRSP_Nome"),
                        lblRSP_Cpf = (Label)item.FindControl("lblRSP_Cpf"),
                        lblRSP_Identidade = (Label)item.FindControl("lblRSP_Identidade"),
                        lblRSP_Celular = (Label)item.FindControl("lblRSP_Celular"),
                        lblRSP_Telefone = (Label)item.FindControl("lblRSP_Telefone"),
                        lblRSP_Email = (Label)item.FindControl("lblRSP_Email");

                    #endregion

                    #region Definindo Valores Valores
                    lblRSP_Nome.Text = DataBinder.Eval(item.DataItem,"RSP_Nome").ToString();
                    lblRSP_Cpf.Text = DataBinder.Eval(item.DataItem,"RSP_Cpf").ToString();
                    lblRSP_Identidade.Text = DataBinder.Eval(item.DataItem,"RSP_Identidade").ToString();
                    lblRSP_Celular.Text = DataBinder.Eval(item.DataItem,"RSP_Celular").ToString();
                    lblRSP_Telefone.Text = DataBinder.Eval(item.DataItem,"RSP_Telefone").ToString();
                    lblRSP_Email.Text = DataBinder.Eval(item.DataItem,"RSP_Email").ToString();

                    #endregion


                    #endregion

                    #region Deficienia e Escola

                    #region Obtendo os Controles
                    Label
                        lblEscolaOrigem = (Label)item.FindControl("lblEscolaOrigem"),
                        lblEscolaDestino = (Label)item.FindControl("lblEscolaDestino"),
                        lblSerie = (Label)item.FindControl("lblSerie");

                    Literal ltDeficiencia = (Literal)item.FindControl("ltDeficiencia");


                    #endregion

                    #region Definindo Valores Valores
                    lblEscolaOrigem.Text = DataBinder.Eval(item.DataItem,"EscolaOrigem").ToString();
                    lblEscolaDestino.Text = DataBinder.Eval(item.DataItem,"EscolaDestino").ToString();
                    lblSerie.Text = DataBinder.Eval(item.DataItem,"Serie").ToString();
                    ltDeficiencia.Text = DataBinder.Eval(item.DataItem,"Deficiencias").ToString();

                    #endregion


                    #endregion

                }
            }

            #region Exceptions
            catch(ArgumentNullException Error)
            {
                divAlert.InnerHtml = Error.Message;
                divAlert.Visible = true;
                rpMatricula.Visible = false;
            }
            catch(AlertaException alerta)
            {
                divAlert.InnerHtml = alerta.Alerta;
                divAlert.Visible = true;
                rpMatricula.Visible = false;
                pnPesquisa.Visible = true;
            }
            catch(Exception er)
            {
                divAlert.InnerHtml = Ferramentas.AlertaException.EnviarEmailSuporte(er);
                divAlert.Visible = true;
                pnMatricula.Visible = false;
                pnPesquisa.Visible = false;
            }
            #endregion
        }

        protected void ItemCommand(object source,RepeaterCommandEventArgs e)
        {

        }

        #endregion

    }
}