using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class Responsavel
    {
        public long Inserir(Responsavel responsavel)
        {
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Responsavel.AddObject(responsavel);
                    fip.SaveChanges();
                    return responsavel.Id;
                }
            }
            catch
            {
                throw;
            }
        }


        public Responsavel Pesquisar(string cpf)
        {
            Responsavel ret = new Responsavel();
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    var rr = (from r in fip.Responsavel
                                       where (r.Cpf == cpf)
                                       select r).FirstOrDefault();
                    if (rr == null)
                        ret.Id = 0;
                    else
                        ret = rr;
                    return ret;

                }

            }
            catch
            {
                throw;
            }


        }


    }
}
