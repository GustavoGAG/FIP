using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Pesquisa
    /// </summary>
    public static class PesquisaBus
    {
        private static Pesquisa pesquisa;

        /// <summary>
        /// Inserir dados referentes à Pesquisa
        /// </summary>
        /// <param name="jaEstudou">Dados do campo "Já estudou" da Pesquisa</param>
        /// <param name="recebePensaoALimenticia">Dados do campo "Recebe pensão alimentícia" da Pesquisa</param>
        /// <param name="inss">Dados do campo "INSS" da Pesquisa</param>
        /// <param name="bolsaFamilia">Dados do campo "Bolsa família" da Pesquisa</param>
        /// <param name="estaGravida">Dados do campo "Está grávida" da Pesquisa</param>
        /// <param name="mesGravidez">Dados do campo "Mês gravidez" da Pesquisa</param>
        /// <returns></returns>
        public static int Inserir
            (string jaEstudou, string recebePensaoALimenticia, string inss, string bolsaFamilia,
           string estaGravida, string mesGravidez)
        {
            try
            {
                pesquisa = new Pesquisa()
                {
                    JaEstudou = jaEstudou,
                    RecebePensaoAlimenticia = recebePensaoALimenticia,
                    Inss = inss,
                    BolsaFamilia = bolsaFamilia,
                    EstaGravida = estaGravida,
                    MesGravidez = mesGravidez,

                };

                return pesquisa.Inserir(pesquisa);
            }
            catch
            {
                throw;
            }

        }

        public static void Apagar(int idPesquisa)
        {
            try
            {
                pesquisa = new Pesquisa();
                pesquisa.Apagar(idPesquisa);
            }
            catch { throw; }
        }

    }
}
