using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ferramentas;

namespace WebSite
{
    public partial class Contato:System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {

        }

        protected void EnviarMensagem(object obj,EventArgs e)
        {
            try
            {
                //Se a pessoa nao deixou em branco os campos de mensagem e nome prosiga
                if(!string.IsNullOrEmpty(txtNome.Text) && (!string.IsNullOrEmpty(txtMensagem.Text)))
                {
                 string    mensagemEmail = MontarMensagem();

                    ///Envia a mensagem do usuario para o email abaixo
                    Ferramentas.EnviarEmail enviarEmail = new Ferramentas.EnviarEmail();
                    enviarEmail.Destinatario = "gustavo.americo@hotmail.com.br";
                    enviarEmail.Assunto = txtAssunto.Text;
                    enviarEmail.Mensagem = mensagemEmail;
                    enviarEmail.Enviar();
                    divAlert.Visible = false;
                }
                else
                {
                    divAlert.InnerHtml = "O campo nome e o campo mensagem não pode ficar vazio ";
                    divAlert.Visible = true;

                }


            }
            catch(AlertaException er)
            {
                divAlert.InnerHtml = er.Message;
                divAlert.Visible = true;
            }
            catch(Exception er)
            {                 
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(er);
                divAlert.Visible = true;
            }

        }


        protected string MontarMensagem()
        {
            try
            {
                string nome = txtNome.Text,
                            email = txtEmail.Text,
                            telefone = txtTel.Text,
                            mensagem = txtMensagem.Text,
                            assunto = txtAssunto.Text;

                string espaco = "<br /> -=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=--=-=- <br />";

                string mensagemEmail = "<table><caption>Fale Conosco</caption>";
                mensagemEmail += "<tr><td>Mensagem:</td><td>" + mensagem + "</td></tr>";
                mensagemEmail += "<tr><td colspan='2'>" + espaco + "</td></tr>";
                mensagemEmail += "<tr><td>Nome:</td><td>" + nome + "</td></tr>";
                mensagemEmail += "<tr><td>Telefone:</td><td>" + telefone + "</td></tr>";
                mensagemEmail += "<tr><td>Email:</td><td>" + email + "</td></tr>";
                return mensagemEmail;
            }
            catch
            {
                throw;
            }
        }
    
    }
}