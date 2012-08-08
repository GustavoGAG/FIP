using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class Deficiencia
    {
        /// <summary>Variavel de Retorno de valores do Tipo Inteiro</summary>
        private static int intRet = new int();

        public int Inserir(Deficiencia deficiencia)
        {
            intRet = new int();
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    fip.Deficiencia.AddObject(deficiencia);
                    intRet = fip.SaveChanges();
                    if (intRet == 1)
                        intRet = deficiencia.Id;
                }

            }
            catch
            (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                
            }
            return intRet;
        }

        public int Apagar(int idDeficiencia)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    foreach (Deficiencia d in Pesqusiar(idDeficiencia))
                    {
                        fip.Deficiencia.DeleteObject(d);
                        intRet += fip.SaveChanges();
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

       

        public int Editar(Deficiencia deficiencia)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    foreach (Deficiencia d in Pesqusiar(deficiencia.Id))
                    {
                        d.Nome = deficiencia.Nome;
                        intRet += fip.SaveChanges();
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


        #region Consultas

        public List<Deficiencia> Pesqusiar()
        {
            IQueryable<Deficiencia> Deficiencia;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    Deficiencia = from d in fip.Deficiencia
                                  select d;
                    if (Deficiencia.Count() > 0)
                        return Deficiencia.ToList();
                    else
                        return null;
                }
            }
            catch
            { return null; }

        }

        public List<Deficiencia> Pesqusiar(int idDeficiencia)
        {
            IQueryable<Deficiencia> Deficiencia;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    Deficiencia = from d in fip.Deficiencia
                                  where d.Id == idDeficiencia

                                  select d;

                    return Deficiencia.ToList();
                }
            }
            catch
            (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                 
            }

        }

        public List<Deficiencia> Pesqusiar(string nomeDeficiencia)
        {

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Deficiencia> deficiencia = from d in fip.Deficiencia
                                                          where d.Nome == nomeDeficiencia
                                                          select d;
                    if (deficiencia.Count() > 0)
                        return deficiencia.ToList();
                    else
                        return null;


                }
            }
            catch
            (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                 
            }

        }


        #endregion

        public bool ValidarExistencia(int idDeficiencia)
        {
            try
            {
                bool existe = false;

                List<Deficiencia> lst = Pesqusiar(idDeficiencia);

                if ((lst != null) && lst.Count > 0)
                    existe = true;

                return existe;
            }
            catch 
            {
                throw;
                 
            }

        }
    }
}
