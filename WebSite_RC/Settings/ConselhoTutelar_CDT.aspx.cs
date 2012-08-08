using System;
using Business;
namespace WebSite.Settings
{
    public partial class ConselhoTutelar_CDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblRetValue.Visible = false;
                EstadoBus.TodosEstados(ddlEstado);
            }

        }

        protected void CarregarCidades(object sender, EventArgs e)
        {
            CidadeBus.Pesquisar(int.Parse(ddlEstado.SelectedValue), ddlCidade);
        }

        protected void CarregarBairros(object sender, EventArgs e)
        {
            BairroBus.Pesquisar(ddlBairro, int.Parse(ddlCidade.SelectedValue));
        }

        protected void Cadastrar(object sender, EventArgs e)
        {
            string nome = txtNome.Text,
                telefone = txtTelefone.Text,
                fax = txtFax.Text,
                endereco = txtEndereco.Text,
                cep = txtCep.Text;
            int idBairro = int.Parse(ddlBairro.SelectedValue),
                idCidade = int.Parse(ddlCidade.SelectedValue),
                idEstado = int.Parse(ddlEstado.SelectedValue);
            lblRetValue.Text = ConselhoTutelarBus.Inserir
                   (nome, endereco, telefone, cep, fax, idEstado, idBairro, idCidade);
            lblRetValue.Visible = true;
        }
    }
}