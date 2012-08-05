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
    /// Classe de Negócio Deficiência
    /// </summary>
    public static class DeficienciaBus
    { 
        private static Deficiencia deficiencia = new Deficiencia();

        /// <summary>Cadastra uma Deficiencia</summary>
        /// <param name="nomeDeficiencia">Nome da Deficiencia</param>
        /// <returns>Retorna uma mensagem informando o status do Cadastro</returns>
        public static string Inserir(string nomeDeficiencia)
        {
            try
            {
                deficiencia = new Deficiencia();

                if ((deficiencia.Pesqusiar(nomeDeficiencia) == null) ||
                    (deficiencia.Pesqusiar(nomeDeficiencia).Count <= 0))
                {
                    deficiencia.Nome = nomeDeficiencia;
                    int retValue = deficiencia.Inserir(deficiencia);

                    if (retValue != 0){
                        return "Deficiencia Cadastrada com Sucesso <br />"+ 
                            "ID: " + retValue +"<br />" +
                            "Nome: " + nomeDeficiencia;
                    }
                    else
                        return
                            "Erro ao Cadastrar, tente mais tarde.";

                }
                else
                {
                    return "Essa deficiencia já esta cadastrada.";
                }
            }
            catch (Exception er)
            {
                return AlertaException.EnviarEmailSuporte(er);
            }
        }

        /// <summary>
        /// Editar uma Deficiência cadastrada no Banco de Dados
        /// </summary>
        /// <param name="id">id da Deficiência a ser editada</param>
        /// <param name="nomeDeficiencia">Nome da Deficiência a ser alterada no Banco de Dados</param>
        /// <returns>Retorna um texto informando se a Deficiência foi ou não alterada
        /// </returns>
        public static string Editar(int id, string nomeDeficiencia)
        {
            string retValue = string.Empty;
            try
            {
                if ((id != 0) && (!string.IsNullOrEmpty(nomeDeficiencia)))
                {
                    deficiencia = new Deficiencia();
                    deficiencia.Id = id;
                    deficiencia.Nome = nomeDeficiencia;

                    if (deficiencia.Editar(deficiencia) == 1)
                        retValue = "Deficiencia Atualizada com sucesso";
                    else
                        retValue = "Erro ao Atualizar, Tente mais Tarde";

                }
            }
            catch (Exception er)
            {
                retValue = AlertaException.EnviarEmailSuporte(er);
            }
            return retValue;
        }

        /// <summary>
        /// Apaga os dados de uma Deficiência do Banco de Dados
        /// </summary>
        /// <param name="idDeficiencia">id da Deficiência a ser apagada do Banco de Dados</param>
        /// <returns>Retorna um texto informando se a Deficiência foi ou não apagada com sucesso</returns>
        public static string Apagar(int idDeficiencia)
        {
            string retValue = string.Empty;
            try
            {
                if (idDeficiencia != 0)
                {
                    deficiencia = new Deficiencia();
                    if (deficiencia.Apagar(idDeficiencia) == 1)
                        retValue = "Deficiencia Atualizada com sucesso";
                    else
                        retValue = "Erro ao Atualizar, Tente mais Tarde";
                }
            }
            catch (Exception er)
            {
                retValue = AlertaException.EnviarEmailSuporte(er);
            }
            return retValue;
        }


        #region Consultas

        /// <summary>Lista todas as Deficiencia</summary>
        /// <returns>Retorna uma Tabela com nome e IDs correspondente</returns>
        public static DataTable Pesquisar()
        {
            deficiencia = new Deficiencia();
            DataTable dTable = new DataTable();
            dTable.Columns.Add("id", typeof(string));
            dTable.Columns.Add("nome", typeof(string));
            try
            {
                List<Deficiencia> lst = deficiencia.Pesqusiar();
                if (lst != null && lst.Count >= 0)
                {
                    foreach (Deficiencia d in lst)
                    {
                        DataRow dRow = dTable.NewRow();
                        dRow["id"] = d.Id.ToString();
                        dRow["nome"] = d.Nome;
                        dTable.Rows.Add(dRow);

                    }
                }
                else
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["id"] = 0;
                    dRow["nome"] = "Não há deficiencia cadastradas";
                    dTable.Rows.Add(dRow);
                }

            }
            catch (Exception ex)
            {
                DataRow dRow = dTable.NewRow();
                dRow["id"] = ex.GetHashCode().ToString();
                dRow["nome"] = ex.Message;
                dTable.Rows.Add(dRow);

            }
            return dTable;

        }

        #endregion
    }
}
