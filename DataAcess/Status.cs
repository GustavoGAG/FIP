using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class Status
    {
        /// <summary>Variavel de Retorno de valores do tipo Int</summary>
        private static int intRet = new int();


        public int Inserir(Status status)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Status.AddObject(status);
                    intRet += fip.SaveChanges();

                }

            }
            catch
            (Exception e)
            {                
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
            
            }
            return intRet;

        }


        public int Editar(Status status)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    foreach (Status s in Pesquisar(status.Id))
                    {
                        s.Nome = status.Nome;
                        intRet += fip.SaveChanges();
                    }
                }
            }
            catch(Exception e) {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                }
            return intRet;
        }


        public int Apagar(int idStatus)
        {

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    foreach (Status b in Pesquisar(idStatus))
                    {
                        fip.Status.DeleteObject(b);
                        intRet += fip.SaveChanges();
                    }

                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                
            }
            return intRet;


        }

        #region Consultas


        public List<Status> Pesquisar(int idStatus)
        {
            IQueryable<Status> status;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    status = from s in fip.Status
                             where s.Id  == idStatus
                             select s; 
                    return status.ToList();
                }
            }
            catch
            (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                 
            }
           

        }


        public List<Status> Pesquisar(string nomeStatus)
        {
            IQueryable<Status> status;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    status = from s in fip.Status
                             where s.Nome == nomeStatus
                             select s;return status.ToList();
                }
            }
            catch
            (Exception e)
            {
                throw new ArgumentException(e.Message, e.Source, e.InnerException);
                 
            }
            

        }



        #endregion



    }
}
