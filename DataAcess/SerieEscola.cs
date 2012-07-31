using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAcess
{
    partial class SerieEscola
    {
        private int intRet;
        private DataTable dTable = new DataTable();

        public int Inserir(SerieEscola serie)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.SerieEscola.AddObject(serie);
                    intRet = fip.SaveChanges();
                }
                return intRet;
            }
            catch(Exception er)
            {
                System.ArgumentException arg = new System.ArgumentException(er.Message, er.InnerException);

                throw arg;
            }
            
        }

        /// <summary>Apaga uma Serie Escolar do Banco de Dados</summary>
        /// <param name="id">ID da Serie</param>
        /// <returns>Retorna 1 Caso tenha sido apagado, e 0(Zero) se não</returns>
        public int Apagar(int id)
        {
            try
            {
                if (VerificarExistencia(id))
                {
                    using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                    {

                        IQueryable<SerieEscola> serie = from s in fip.SerieEscola
                                                        where s.Id  == id
                                                        select s;

                        foreach (SerieEscola s in serie)
                        {
                            fip.SerieEscola.DeleteObject(s);
                            intRet = 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

            return intRet;
        }

        /// <summary>Verifica se ha alguma Serie Cadastrada com o ID Determinado</summary>
        /// <param name="idSerie">ID da Serie que deseja validar</param>
        /// <returns>Retorna True Caso Exita, e False</returns>
        public bool VerificarExistencia(int idSerie)
        {
            //O item inicia como inexistente
            bool existe = false;
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    IQueryable<SerieEscola> serie = from s in fip.SerieEscola
                                                    where s.Id  == idSerie
                                                    select s;

                    foreach (var i in serie)
                    {
                        //Caso encontre no banco sua existencia passa pra true
                        existe = true;
                    }
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return existe;
        }

        /// <summary>Verifica se ha alguma Serie Cadastrada com o Nome Determinado</summary>
        /// <param name="Serie">Nome da Serie que deseja validar</param>
        /// <returns>Retorna True Caso Exita, Default False</returns>
        public bool VerificarExistencia(string Serie)
        {

            //O item inicia como inexistente
            bool existe = false;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<SerieEscola> serie = from s in fip.SerieEscola
                                                    where s.Nome == Serie
                                                    select s;

                    foreach (var i in serie)
                    {
                        //Caso encontre no banco sua existencia passa pra true
                        existe = true;
                    }
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

            return existe;

        }

        #region Consultas


        /// <summary>Retorna uma Lista com as Serie Cadastradas</summary>
        /// <returns>Retorna em FOrma de Lista as Serie Cadastradas ou null Caso haja erro de acesso</returns>
        public List<SerieEscola> Pesqusiar()
        {
            List<SerieEscola> serie;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    serie = (from s in fip.SerieEscola
                            select s).ToList();
                }
            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return serie;
        }

        #endregion

    }
}
