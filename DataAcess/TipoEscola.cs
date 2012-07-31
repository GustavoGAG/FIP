using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAcess
{
    partial class TipoEscola
    {

        private static int intRet;
        private static List<TipoEscola> lstTipoEscola = new List<TipoEscola>();

        public int Inserir(TipoEscola tipoEscola)
        {

            try
            {
                using (Context fip = new Context())
                {

                    fip.TipoEscola.AddObject(tipoEscola);
                    fip.SaveChanges();

                    intRet = tipoEscola.Id;
                }
                return intRet;
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source,e.InnerException);
                throw argExc;
            }




        }

        /// <summary>Altera o tipo de escola</summary>
        /// <param name="tipoEscola">Novos valores a serem recebidos</param>
        /// <returns>Retorna o id</returns>
        public int Editar(TipoEscola tipoEscola)
        {
            try
            {
                using (Context fip = new Context())
                {

                    foreach (TipoEscola te in Pesquisar(tipoEscola.Id ))
                    {
                        te.Nome = tipoEscola.Nome;

                        fip.SaveChanges();

                        intRet = tipoEscola.Id ;
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

        public int Apagar(int id)
        {
            try
            {
                using (Context fip = new Context())
                {

                    foreach (TipoEscola i in Pesquisar(id))
                    {
                        fip.TipoEscola.DeleteObject(i);
                        intRet = fip.SaveChanges();

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

        public bool ValidarExistencia(string tipoEscola1)
        {
            bool existe = false;
            try
            {
                foreach (TipoEscola t in Pesquisar(tipoEscola1))
                {
                    existe = true;

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

        /// <summary>Retorna todos os tipos de Escola</summary>
        /// <returns>Todos os tipos de escola</returns>
        public List<TipoEscola> Pesquisar()
        {

            IQueryable<TipoEscola> tipoEscola;
            try
            {
                using (Context fip = new Context())
                {
                    tipoEscola = from t in fip.TipoEscola
                                 select t;
                    if (tipoEscola != null && tipoEscola.Count() > 0)
                        lstTipoEscola = tipoEscola.ToList();
                }
            }
            catch { return null; }
            return lstTipoEscola;
        }

        public List<TipoEscola> Pesquisar(int idTipoEscola)
        {
            IQueryable<TipoEscola> tipoEscola;
            try
            {
                using (Context fip = new Context())
                {
                    tipoEscola = from t in fip.TipoEscola
                                 where t.Id  == idTipoEscola
                                 select t;
                    lstTipoEscola = tipoEscola.ToList();
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return lstTipoEscola;

        }

        public List<TipoEscola> Pesquisar(string nome)
        {
            IQueryable<TipoEscola> tipoEscola;
            try
            {
                using (Context fip = new Context())
                {
                    tipoEscola = from t in fip.TipoEscola
                                 where t.Nome == nome
                                 select t;

                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return tipoEscola.ToList();

        }
        #endregion
    }
}
