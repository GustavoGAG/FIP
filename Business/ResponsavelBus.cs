using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Responsável
    /// </summary>
    public class ResponsavelBus
    {

        private static Responsavel responsavel;

        public ResponsavelBus()
        {
            responsavel = new Responsavel();
        }

       

        /// <summary>Inserir dados do Responsável</summary>
        /// <param name="nome">Nome do Responsável</param>
        /// <param name="cpf">CPF do Responsável</param>
        /// <param name="identidade">Identidade do Responsável</param>
        /// <param name="celular">Celular do Responsável</param>
        /// <param name="telefone">Telefone do Responsável</param>
        /// <returns>Retorna o ID do Responsavel</returns>
        public static long Inserir(string nome,string cpf,string identidade,string celular,string telefone)
        {
            try
            {
                responsavel = new Responsavel()
            {
                Nome = nome,
                Cpf = cpf,
                Identidade = identidade,
                Celular = celular,
                Telefone = telefone
            };

                if(VerificaSeExiste())
                {
                    return responsavel.Id;
                }

                return responsavel.Inserir(responsavel);
            }
            catch
            {
                throw;
            }
        }


        private static bool VerificaSeExiste()
        {
            try
            {
                long id = responsavel.Pesquisar(responsavel.Cpf).Id;
                if(id != 0)
                {
                    responsavel.Id = id;
                    return true;
                }
                return false;
            }
            catch(Exception)
            {

                throw;
            }

        }
    }
}
