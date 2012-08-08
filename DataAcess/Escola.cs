using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAcess
{
    partial class Escola
    {
        //Retorna o id da escola (idEscola), do tipo inteiro
        private int intRet;


        public int Inserir(Escola escola)
        {

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Escola.AddObject(escola);
                    fip.SaveChanges();

                    intRet = escola.Id;

                }
                return intRet;
            }
            catch 
            {
             
                throw  ;

            }



        }

        /// <summary>
        /// Os dados serão apagados a partir de um parâmetro passado (provavelmente id)
        /// </summary>
        public void Apagar(int id)
        {

        }

        /// <summary>Altera dados da escola a partir de um parâmetro passado para consultar o registro e os parâmetros que serão alterados</summary>
        public void Editar(Escola escola)
        {

        }

        /*
        public List<Escola> Pesquisar()
        {
            List<Escola> lstEscola = null;

            try
            {
                using (ADOContext fip = new ADOContext())
                {
                    IQueryable<Escola> escola = from e in fip.Escola
                                                select e;

                    lstEscola = escola.ToList();
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

            return lstEscola;

        }
        */
        public DataTable Pesquisar(int idCidade)
        {
            DataTable dTable = new DataTable();
            try
            {
                #region Colunas
                dTable.Columns.Add("idEscola", typeof(string));
                dTable.Columns.Add("nome", typeof(string));
                dTable.Columns.Add("fax", typeof(string));
                dTable.Columns.Add("bairro", typeof(string));
                dTable.Columns.Add("cidade", typeof(string));
                dTable.Columns.Add("estado", typeof(string));
                dTable.Columns.Add("TipoEscola", typeof(string));
                dTable.Columns.Add("telefone", typeof(string));
                dTable.Columns.Add("endereco", typeof(string));
                #endregion

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Escola> escola = (from e in fip.Escola
                                                 where e.FK_BairroDaEscola.FK_Cidade.Id == idCidade
                                                 select e);

                    foreach (Escola e in escola)
                    {
                        DataRow dRow = dTable.NewRow();
                        dRow["idEscola"] = e.Id;
                        dRow["nome"] = e.Nome;
                        dRow["fax"] = e.Fax;
                        if (e.FK_BairroDaEscola != null)
                            dRow["bairro"] = e.FK_BairroDaEscola.Nome;
                        if (e.FK_BairroDaEscola.FK_Cidade != null)
                            dRow["cidade"] = e.FK_BairroDaEscola.FK_Cidade.Nome;
                        if (e.FK_BairroDaEscola.FK_Cidade.FK_Estado != null)
                            dRow["estado"] = e.FK_BairroDaEscola.FK_Cidade.FK_Estado.Nome;
                        if (e.FK_TipoEscola != null)
                            dRow["tipoEscola"] = e.FK_TipoEscola.Nome;
                        dRow["endereco"] = e.Endereco;
                         
                        dRow["telefone"] = e.Telefone;
                        dTable.Rows.Add(dRow);
                    }
                }
            }
            catch (EntityException ex)
            {
                DataRow dRow = dTable.NewRow();
                dRow["idEscola"] = ex.GetHashCode();
                dRow["nome"] = ex.Message;


                dTable.Rows.Add(dRow);
            }
            catch
            {
                
                throw  ;
            }
            return dTable;
        }

        public DataTable Pesquisar()
        {
            DataTable dTable = new DataTable();
            try
            {
                #region Colunas
                dTable.Columns.Add("idEscola", typeof(string));
                dTable.Columns.Add("nome", typeof(string));
                dTable.Columns.Add("fax", typeof(string));
                dTable.Columns.Add("idBairro", typeof(string));
                dTable.Columns.Add("idCidade", typeof(string));
                dTable.Columns.Add("idEstado", typeof(string));
                dTable.Columns.Add("idTipoEscola", typeof(string));
                dTable.Columns.Add("telefone", typeof(string));
                #endregion

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Escola> escola = (from e in fip.Escola
                                                 select e);

                    foreach (Escola e in escola)
                    {
                        DataRow dRow = dTable.NewRow();
                        dRow["idEscola"] = e.Id;
                        dRow["nome"] = e.Nome;
                        dRow["fax"] = e.Fax;
                        if (e.FK_BairroDaEscola != null)
                            dRow["idBairro"] = e.FK_BairroDaEscola.Nome;
                        if (e.FK_BairroDaEscola.FK_Cidade != null)
                            dRow["idCidade"] = e.FK_BairroDaEscola.FK_Cidade.Nome;
                        if (e.FK_BairroDaEscola.FK_Cidade.FK_Estado != null)
                            dRow["idEstado"] = e.FK_BairroDaEscola.FK_Cidade.FK_Estado.Nome;
                        if (e.FK_TipoEscola != null)
                            dRow["idTipoEscola"] = e.FK_TipoEscola.Nome;

                        dRow["telefone"] = e.Telefone;
                        dTable.Rows.Add(dRow);
                    }
                }
            }
            catch (EntityException ex)
            {
                DataRow dRow = dTable.NewRow();
                dRow["idEscola"] = ex.GetHashCode();
                dRow["nome"] = ex.Message;

                dTable.Rows.Add(dRow);
            }
            catch 
            {
            
                throw  ;
            }
            return dTable;
        }


        /// <summary>Verifica se ja Existe alguma escola Cadastrada com o tal Nome</summary>
        /// <param name="nome">Nome da Escola</param>
        /// <returns>Retorna True caso ja exista uma escola com o mesmo nome</returns>
        public bool ValidarExistencia(string nome)
        {
            bool existe = false;

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Escola> escola = from e in fip.Escola
                                                where e.Nome == nome
                                                select e;
                    if (escola.Count() > 0)
                        existe = true;

                }
            }
            catch 
            {
                throw;
            }

            return existe;

        }

        /// <summary>Verifica se ja Existe alguma escola Cadastrada com o tal Nome</summary>
        ///<param name="fax">Numero do Fax/Telefone Secundario</param>
        ///<param name="telefone">Numero do Telefone Principal</param>
        /// <returns>Retorna True caso ja exista uma escola com o mesmo nome</returns>
        public bool ValidarExistencia(string telefone, string fax)
        {
            bool existe = false;

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Escola> escola = from e in fip.Escola
                                                where (e.Telefone == telefone || e.Fax == fax)
                                                select e;
                    if (escola.Count() > 0)
                        existe = true;

                }
            }
            catch
            {
                throw;
            }

            return existe;

        }

    }
}
