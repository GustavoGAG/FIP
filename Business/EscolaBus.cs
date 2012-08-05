using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;
using System.Data;
using System.Web.UI.WebControls;
using Ferramentas;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Escola
    /// </summary>
    public static class EscolaBus
    {


        private static Escola escola = new Escola();




        /// <summary>
        /// Inserir dados relativos a Escola
        /// </summary>
        /// <param name="nome">Nome da Escola</param>
        /// <param name="telefone">Telefone da Escola</param>
        /// <param name="fax">Fax da Escola</param>
        /// <param name="idTipoEscola">id referente ao Tipo da Escola</param>
        /// <param name="idEstado">id do Estado em que a Escola está localizada</param>
        /// <param name="idCidade">id da Cidade em que a Escola está localizada</param>
        /// <param name="idBairro">id do Bairro em que a Escola está localizada</param>
        /// <returns>Retorna um texto informando se a Escola foi ou não cadastrada com sucesso</returns>
        public static string Inserir(string nome,string telefone,string fax,int idTipoEscola,int idEstado,int idCidade,int idBairro)
        {
            try
            {
                if(escola.ValidarExistencia(nome))
                    return "Já existe uma escola Cadastrada com esse Nome";
                if(escola.ValidarExistencia(telefone,fax))
                    return "Ja existe outra escola usando esse numero de Telefone";

                escola.Nome = nome;
                escola.Telefone = telefone;
                escola.Fax = fax;
                escola.IdTipoEscola = idTipoEscola;
                escola.IdBairro = idBairro;


                escola.Inserir(escola);
                return "Escola Cadastrada com Sucesso";
            }
            catch(Exception er)
            {
                return AlertaException.EnviarEmailSuporte(er);
            }


        }


        /// <summary>
        /// Pesquisar Cidade
        /// </summary>
        /// <param name="idCidade">id da Cidade a ser pesquisada</param>
        /// <returns></returns>
        public static DataTable Pesquisar(int idCidade)
        {
            try
            {
                return escola.Pesquisar(idCidade);
            }
            catch(Exception)
            {

                throw;
            }



        }



    }
}
