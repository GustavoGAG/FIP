using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using Ultilitarios;


namespace DataAcess
{
    partial class Context
    {
        /// <summary>Procura no XML de configuração do site por uma ConectionString.</summary>
        /// <param name="nomeConectiString">Nome da ConectionString</param>
        /// <returns>Retorna a conectionString</returns>
        public static string ObterStringConexaoWebConfig(string nomeConectiString)
        {
            try
            {
                string conexaoString = ConfigurationManager.ConnectionStrings[nomeConectiString].ConnectionString;
                if (string.IsNullOrEmpty(conexaoString))
                    throw new AlertaException("Não foi encontrada uma String de Conexão com esse nome");

                return conexaoString;

            }
            catch (Exception e)
            {
                AlertaException.EnviarEmailSuporte(e);
                throw e;
            }

        }


        /// <summary>Procura no XML de configuração do site por uma ConectionString.</summary>
        /// <param name="indexConectionString">index da ConectionString</param>
        /// <returns>Retorna a conectionString</returns>
        public static string ObterStringConexaoWebConfig(int indexConectionString)
        {
            try
            {
                string conexaoString = ConfigurationManager.ConnectionStrings[indexConectionString].ConnectionString;
                if (string.IsNullOrEmpty(conexaoString))
                    throw new AlertaException("Não foi encontrada uma String de Conexão nessa posição de index");

                return conexaoString;

            }
            catch (Exception e)
            {
                AlertaException.EnviarEmailSuporte(e);
                throw e;
            }

        }


        /// <summary>Procura no XML de configuração do site por uma ConectionString.</summary>
        /// <returns>Retorna a primeira conectionString no XML</returns>
        public static string ObterStringConexaoWebConfig()
        {
            try
            {
                string conexaoString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;

                if (string.IsNullOrEmpty(conexaoString))
                    throw new AlertaException("Não foi encontrada uma String de Conexão com esse nome");

                return conexaoString;

            }
            catch (NullReferenceException )
            {
                throw new AlertaException("Não foi encontrada uma String de Conexão");
            }
            catch (Exception e)
            {
                AlertaException.EnviarEmailSuporte(e);
                throw e;
            }

        }

    }
}
