using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAcess
{

    partial class Bairro
    {
        /// <summary>Variavel de Retorno de valores inteiro</summary>
        private static int intRet = new int();



        public int Inserir(Bairro bairro)
        {
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Bairro.AddObject(bairro);
                    intRet = fip.SaveChanges();

                }

            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;

        }


        public int Editar(int idBairro, string nome, int idCidade)
        {
            try
            {
                intRet = new int();
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Bairro> bairros = from b in fip.Bairro
                                                 where b.Id  == idBairro
                                                 select b;
                    foreach (Bairro b in bairros)
                    {
                        b.Nome = nome;
                        b.IdCidade = idCidade;
                        fip.Bairro.ApplyCurrentValues(b);
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


        public int Apagar(int idBairro)
        {

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    Bairro bairro  = (from b in fip.Bairro
                                      where b.Id  == idBairro
                                      select b).First();

                    fip.Bairro.DeleteObject(bairro);
                        intRet = fip.SaveChanges();
                    

                }


            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;


        }




        public bool ValidarExistencia(string nome)
        {
            bool existe = false;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    if ((Pesquisar(nome) != null) || (Pesquisar(nome).Count > 0))
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


        public bool ValidarExistencia(int idCidade, string nomeBairro)
        {
            bool existe = false;
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    var ve = from b in fip.Bairro
                             where b.IdCidade == idCidade && b.Nome == nomeBairro
                             select b;

                    if (ve != null || ve.Count() > 0)
                        existe = true;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                
            }
            return existe;


        }


        public bool ValidarExistencia(int idBairro)
        {
            bool existe = false;
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    var ve = from b in fip.Bairro
                             where b.Id  == idBairro
                             select b;

                    if (ve != null || ve.Count() > 0)
                        existe = true;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
               
            }
            return existe;


        }


        #region Consultas

        /// <summary>Pesquisa os Bairros de uma determinada Cidade</summary>
        /// <param name="idCidade">ID da Cidade que o bairro esta</param>
        /// <returns>Lista IQueryble do tipo Bairro</returns>
        public List<Bairro> Pesquisar(int idCidade)
        {
            List<Bairro> lstBairro = new List<Bairro>();
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Bairro> bairro = from b in fip.Bairro
                                                where b.IdCidade == idCidade
                                                orderby b.Nome
                                                select b;

                    lstBairro = bairro.ToList();
                }
            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return lstBairro;
        }


        /// <summary>Pesquisa os Bairros de uma determinada Cidade</summary>
        /// <param name="idCidade">ID do Bairro</param>
        /// <returns>Lista IQueryble do tipo Bairro</returns>
        public List<Bairro> Pesquisar(string nome)
        {
            try
            {
                List<Bairro> lstBairro = new List<Bairro>();
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Bairro> bairro = from b in fip.Bairro
                                                where b.Nome == nome
                                                select b;
                    return lstBairro = bairro.ToList();
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
        }


        #endregion
    }

}
