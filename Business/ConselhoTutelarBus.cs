using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;
using System.Web.UI.WebControls;
using System.Data;
using Ferramentas;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Conselho Tutelar
    /// </summary>
    public static class ConselhoTutelarBus
    {


        private static ConselhoTutelar conselhoTutelar = new ConselhoTutelar();

        /// <summary>Cadastra um Conselho Tutelar no Sistema</summary>
        /// <param name="nome">Nome do Conselho Tutelar</param>
        /// <param name="endereco">Endereço do Conselho Tutelar</param>
        /// <param name="telefone">Telefone Principal</param>
        /// <param name="cep">CEP do Endereco</param>
        /// <param name="fax">Telefone Secundario (FAX)</param>
        /// <param name="idEstado">ID do Estado</param>
        /// <param name="idBairro"> ID do Bairro</param>
        /// <param name="idCidade">ID da Cidade</param>
        /// <returns>Retorna a resposta do Banco</returns>
        public static string Inserir(string nome, string endereco, string telefone,
            string cep, string fax, int idEstado, int idBairro, int idCidade)
        {
            string retValue = string.Empty;
            try
            {

                if (conselhoTutelar.ValidarExistencia(nome, telefone, endereco))
                    return "Já Existe um Conselho Tutelar Cadastrado com essas Informações";

                conselhoTutelar = new ConselhoTutelar()
                {
                    Nome = nome,
                    Endereco = endereco,
                    Telefone = telefone,
                    Cep = cep,
                    Fax = fax,
                    IdBairro = idBairro,
                     
                };
                if (conselhoTutelar.Inserir(conselhoTutelar) > 0)
                    retValue = "Cadastro Efetuado com Sucesso";


            }
            catch (Exception er)
            {
                AlertaException.EnviarEmailSuporte(er);
            }

            return retValue;
        }

        //
        public static DropDownList ListarConselhos(DropDownList ddlConselho)
        {
            try
            {
                ddlConselho.Items.Clear();

                ListItem li = new ListItem();
                li.Text = "Selecione";
                li.Value = "0";

                ddlConselho.Items.Add(li);
                foreach (ConselhoTutelar c in conselhoTutelar.PesquisarConselho())
                {
                    li = new ListItem();
                    li.Text = c.Nome;
                    li.Value = c.Id.ToString();
                    ddlConselho.Items.Add(li);
                }
            }

            catch
            {
            }
            return ddlConselho;
        }
        //
        public static DropDownList ListarCres(DropDownList ddlCre)
        {
            try
            {
                ddlCre.Items.Clear();

                ListItem li = new ListItem();
                li.Text = "Selecione";
                li.Value = "0";

                ddlCre.Items.Add(li);
                foreach (ConselhoTutelar c in conselhoTutelar.PesquisarCre())
                {
                    li = new ListItem();
                    li.Text = c.Nome;
                    li.Value = c.Id.ToString();
                    ddlCre.Items.Add(li);

                }
            }
            catch
            {
            }

            return ddlCre;
        }

    }
}
