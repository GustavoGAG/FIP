using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ultilitarios;

namespace DataAcess
{
    partial class Pesquisa
    {
    



        public int Inserir(Pesquisa pesquisa)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Pesquisa.AddObject(pesquisa);
                    fip.SaveChanges();
                    return pesquisa.Id;
                }
            }
            catch (UpdateException upException)
            {
                throw new AlertaException(upException.Message);
            }
            catch
            { throw; }
        }

        public void Apagar(int idPesquisa)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Pesquisa.DeleteObject(
                        (from p in fip.Pesquisa where p.Id == idPesquisa select p).FirstOrDefault()
                        );
                    fip.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
