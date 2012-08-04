using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ultilitarios
{
    public class AlertaException:Exception
    {
        private string alerta;
        public string Alerta
        {
            get
            {
                if(!string.IsNullOrEmpty(alerta))
                    return alerta;
                else
                    return string.Empty;
            }
        }
 
        #region Construtores
        public AlertaException(string alerta)
        {
            this.alerta = alerta;
        }
        public AlertaException()
        {
            this.alerta = "";

        }
        #endregion


        public static string EnviarEmailSuporte(Exception erro)
        {
            AlertaException ae = new AlertaException();
             string retValue = ae.MontarStringDoErro(erro);
            try
            {

                EnviarEmail mail = new EnviarEmail
                                       {
                                           Assunto = "Falha FIP 24HR " + erro.Message,
                                           Destinatario = "gustavo.americo@hotmail.com.br",
                                           Mensagem = retValue
                                       };
                mail.Enviar();
                
            }
            catch
            {
               
            }
          //  return retValue;
            return "Houve um erro no sistema, solicito que tente mais tarde <br />" + erro.Message;
        }

        protected string MontarStringDoErro(Exception erro)
        {
            try
            {
                string retValue = "";
                string espaco = "<br /> =-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=";
             
                retValue += "<br />MENSAGEM: " + erro.Message + "<br /> ";
                if(erro is AlertaException)
                    retValue += "<br />Alerta: " + (erro as AlertaException).Alerta;
                retValue += "PROJETO: " + erro.Source;
                retValue += "<br />MÉTODO ALVO: " + erro.Source+ " " + erro.TargetSite.ToString();
                retValue += "<br />DATA: " + erro.Data.ToString();
                retValue += espaco;
                retValue += "<br />ROTA DO ERRO: <br />" + erro.StackTrace;
                
                retValue += espaco;
          
                
                if(erro.InnerException != null)
                {
                    retValue += "<br />MENSAGEM: " + erro.InnerException.Message + "<br /> ";
                    if(erro.InnerException is AlertaException)
                        retValue += "<br />Alerta: " + (erro.InnerException as AlertaException).Alerta;
                    retValue += "PROJETO: " + erro.InnerException.Source;
                    retValue += "<br />MÉTODO ALVO: " + erro.InnerException.Source + " " + erro.InnerException.TargetSite.ToString();
                    retValue += "<br />DATA: " + erro.InnerException.Data.ToString();
                    retValue += espaco;
                    retValue += "<br />ROTA DO ERRO: <br />" + erro.InnerException.StackTrace;

                    if(erro.InnerException.InnerException != null)
                    {
                        retValue += "InnerException: " + erro.InnerException.InnerException.ToString();
                        retValue += "<br />SOURCE: " + erro.InnerException.InnerException.Source;
                        retValue += "<br />STACKTRACE: " + erro.InnerException.InnerException.StackTrace;
                        retValue += "<br />TARGETSITE: " + erro.InnerException.InnerException.TargetSite.ToString();
                        retValue += "<br />DATA: " + erro.InnerException.InnerException.Data.ToString();
                        retValue += espaco;
                        retValue += "<br />MENSAGEM: " + erro.InnerException.InnerException.Message;
                    }

                }
                return retValue;
            }
            catch(Exception e)
            {
                return "Erro ou montar string de Falha " + e.Message;
            }

        }
    }
}
