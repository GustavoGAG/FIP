using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class MotivoMatricula
    {

        public void Inserir(MotivoMatricula mm)
        {
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.MotivoMatricula.AddObject(mm);
                }
            }
            catch { throw; }
        }

        public bool ValidarExistencia(MotivoMatricula mm)
        {

            try
            {
                string Conexao = Context.ObterStringConexaoWebConfig();
                using (Context fip = new Context(Conexao))
                {
                    List<MotivoMatricula> lstMotivo = (from m in fip.MotivoMatricula
                                                       where (m.Motivo == mm.Motivo) || m.Id == mm.Id
                                                       select m).ToList();

                    foreach (MotivoMatricula item in lstMotivo)
                    {
                        return true;

                    }

                }
                return false;
            }
            catch { throw; }

        }



        #region Consultas
        public List<MotivoMatricula> Pesquisar()
        {
            try
            {
                
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    return (from m in fip.MotivoMatricula select m).ToList();

                }
            }
            catch { throw; }

        }

        #endregion


    }
}
