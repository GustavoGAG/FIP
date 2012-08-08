using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class EscolaEscolhida
    {

        private static int intRet = new int();

        public int Inserir(EscolaEscolhida escolaEscolhida)
        {
            try
            {
                using (Context fip = new Context())
                {
                    fip.EscolaEscolhida.AddObject(escolaEscolhida);
                    intRet = fip.SaveChanges();

                }
            }
            catch { intRet = -1; }
            return intRet;

        }

        public int Editar(EscolaEscolhida escolaEscolhida)
        {
            try
            {
                using (Context fip = new Context())
                {
                    foreach (EscolaEscolhida e in Pesquisar(Convert.ToInt32(escolaEscolhida.Id)))
                    {
                        e.IdEscolaDestino = escolaEscolhida.IdEscolaDestino;
                        e.IdEscolaOrigem = escolaEscolhida.IdEscolaOrigem;
                        fip.SaveChanges();

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

        public int Apagar(int idEscolaEscolhida)
        {
            try
            {
                using (Context fip = new Context())
                {
                    foreach (EscolaEscolhida e in Pesquisar(idEscolaEscolhida))
                    {

                        fip.EscolaEscolhida.DeleteObject(e);
                        intRet = fip.SaveChanges();
                    }
                }
            }
            catch { intRet = -1; }
            return intRet;

        }

        public void Apagar(long idUsuario)
        {
            try
            {
                using (Context fip = new Context())
                {
                    var lista = from l in fip.EscolaEscolhida
                                where l.IdUsuario == idUsuario
                                select l;

                    foreach (var e in lista)
                    {
                        fip.EscolaEscolhida.DeleteObject(e);    
                    }
                    
                    fip.SaveChanges();

                }
            }
            catch { throw; }

        }

        public bool ValidarExistencia(int idEscolha)
        {
            bool existe = false;
            try
            {
                foreach (EscolaEscolhida e in Pesquisar(idEscolha))
                {
                    existe = true;
                }
            }
            catch { }
            return existe;
        }


        #region Consultas
        public IQueryable<EscolaEscolhida> PesquisarPorUsuario(int idUsuario)
        {
            IQueryable<EscolaEscolhida> escolha;
            try
            {
                using (Context fip = new Context())
                {
                    escolha = from e in fip.EscolaEscolhida
                              where e.IdUsuario == idUsuario
                              select e;
                }
            }
            catch { return null; }
            return escolha;
        }

        public IQueryable<EscolaEscolhida> Pesquisar(int idEscolha)
        {
            IQueryable<EscolaEscolhida> escolha;
            try
            {
                using (Context fip = new Context())
                {
                    escolha = from e in fip.EscolaEscolhida
                              where e.Id == idEscolha
                              select e;
                }
            }
            catch { return null; }
            return escolha;
        }

        #endregion
    }
}
