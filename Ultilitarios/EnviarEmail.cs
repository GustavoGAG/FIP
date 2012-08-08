using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mail = System.Net.Mail;
using Net = System.Net;

namespace Ultilitarios
{
    public class EnviarEmail
    {
        #region Construtores

        /// <summary>Inicializa uma estancia de SMTP com as configurações do web.config</summary>
        public EnviarEmail()
        {
            try
            {
                smtp = new Mail.SmtpClient();
                smtp.TargetName = "SAC";
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;


            }
            catch
            {
                throw;
            }

        }

        /// <summary>Inicializa uma estancia SMTP personalizada</summary>
        /// <param name="host">Endereço IP ou dominio</param>
        /// <param name="port">Porta de acesso de Email (25 Padrão)</param>
        /// <param name="enableSsl">O Servidor aceita SSL?</param>
        /// <param name="credenciaisAcesso">Crediciais usada para enviar emails</param>
        public EnviarEmail
            (string host,int port,bool enableSsl,System.Net.NetworkCredential credenciaisAcesso)
        {
            try
            {

                smtp = new Mail.SmtpClient();
                smtp.Host = host;
                smtp.Port = port;
                smtp.EnableSsl = enableSsl;
                smtp.Credentials = credenciaisAcesso;


            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region Propriedades
        private string remetente = "sac@fip24hr.com.br";


        private string assunto;
        public string Assunto
        {
            get
            {
                return assunto;
            }
            set
            {
                assunto = value;
            }
        }

        private string mensagem;
        public string Mensagem
        {
            get
            {
                return mensagem;
            }
            set
            {
                mensagem = value;
            }
        }

        private string destinatario;
        public string Destinatario
        {
            get
            {
                return destinatario;
            }
            set
            {
                destinatario = value;
            }
        }

        private Mail.SmtpClient smtp;
        #endregion

        #region Metodos

        /// <summary>Envia o objeto email preenchido para o destinatario</summary>
        public void Enviar()
        {
            //Se não houver um destinatario devinido retorna um erro
            if(string.IsNullOrEmpty(destinatario))
                throw new ArgumentException("Destinatario Inválido, verifique se foi digitado corretamente");

            //Se não houver um remetente devinido retorna um erro
            if(string.IsNullOrEmpty(remetente))
                throw new ArgumentException("Remetente Inválido, verifique se foi digitado corretamente");


            //Cria um objeto de email               
            Mail.MailMessage mail = new Mail.MailMessage();

            //Define quem recebe o email
            Mail.MailAddress maDestinatario = new Mail.MailAddress(destinatario);

            mail.To.Add(maDestinatario);

            //Define quem envia
            Mail.MailAddress maRemetente = new Mail.MailAddress(remetente);
            mail.From = maRemetente;

            //Declara que o corpo do email esta no formato html
            mail.IsBodyHtml = true;

            //Define as informaçoes ao Corpo do email
            mail.Body = mensagem;

            //Define o nivel de importancia do email               
            mail.Priority = Mail.MailPriority.Normal;

            //Define o Assunto
            mail.Subject = assunto;

            //Envia o email
            smtp.Send(mail);
        }


        /// <summary>Envia o email para o destinatario e copias para uma coleção de endereco de email</summary>
        public void Enviar(Mail.MailAddressCollection colecaoDestinatario)
        {

            try
            {
                //Se não houver um destinatario devinido retorna um erro
                if(string.IsNullOrEmpty(destinatario))
                    throw new ArgumentException("Destinatario Inválido, verifique se foi digitado corretamente");

                //Se não houver um remetente devinido retorna um erro
                if(string.IsNullOrEmpty(remetente))
                    throw new ArgumentException("Remetente Inválido, verifique se foi digitado corretamente");


                //Cria um objeto de email               
                Mail.MailMessage mail = new Mail.MailMessage();

                //Envia uma mensagem de erro caso ocorra
                mail.DeliveryNotificationOptions = Mail.DeliveryNotificationOptions.OnFailure;


                //Define quem envia
                Mail.MailAddress maRemetente = new Mail.MailAddress(remetente);
                mail.From = maRemetente;

                //Define quem recebe o email
                Mail.MailAddress maDestinatario = new Mail.MailAddress(destinatario);
                mail.To.Add(maDestinatario);

                //Adiciona quem vai recebe as copias
                foreach(var item in colecaoDestinatario)
                    mail.CC.Add(item);


                //Declara que o corpo do email esta no formato html
                mail.IsBodyHtml = true;

                //Define as informaçoes ao Corpo do email
                mail.Body = mensagem;

                //Define o nivel de importancia do email               
                mail.Priority = Mail.MailPriority.Normal;

                //Define o Assunto
                mail.Subject = assunto;

                //Envia o email
                smtp.Send(mail);
            }
            catch
            {
                throw;
            }

        }


        #endregion
    }
}
