using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ferramentas;


namespace DataAcess
{
    /// <summary>Classe responsavel por persistir os dados da matricula de todos os usuarios</summary>
    partial class Usuario
    {
        #region Propriedades

        #region Privada
        private static DataTable dTable;

        private List<Deficiencia> lstDeficiencia = new List<Deficiencia>();

        private List<Escola> lstEscola = new List<Escola>();

        private string serie;
        #endregion

        #region Pubilcas
        protected static DataTable UsuTabela
        {
            get
            {
                dTable = new DataTable();
                dTable.TableName = "Usuario";
                #region Colunas da Tabela

                #region Usuario
                dTable.Columns.Add("id",System.Type.GetType("System.Int64"));
                dTable.Columns.Add("nome",System.Type.GetType("System.String"));
                dTable.Columns.Add("cpf",System.Type.GetType("System.String"));
                dTable.Columns.Add("dataNascimento",System.Type.GetType("System.String"));
                dTable.Columns.Add("sexo",System.Type.GetType("System.String"));
                dTable.Columns.Add("identidade",System.Type.GetType("System.String"));
                dTable.Columns.Add("orgaoEmissor",System.Type.GetType("System.String"));
                dTable.Columns.Add("nomeMae",System.Type.GetType("System.String"));
                dTable.Columns.Add("nomePai",System.Type.GetType("System.String"));
                dTable.Columns.Add("dataCadastro",System.Type.GetType("System.String"));
                dTable.Columns.Add("endereco",System.Type.GetType("System.String"));
                dTable.Columns.Add("cep",System.Type.GetType("System.String"));
                dTable.Columns.Add("estado",System.Type.GetType("System.String"));
                dTable.Columns.Add("cidade",System.Type.GetType("System.String"));
                dTable.Columns.Add("bairro",System.Type.GetType("System.String"));
                dTable.Columns.Add("motivo",System.Type.GetType("System.String"));
                dTable.Columns.Add("status",System.Type.GetType("System.String"));

                #endregion

                #region Certidao
                dTable.Columns.Add("CN_Numero",System.Type.GetType("System.String"));
                dTable.Columns.Add("CN_Livro",System.Type.GetType("System.String"));
                dTable.Columns.Add("CN_Folha",System.Type.GetType("System.String"));
                dTable.Columns.Add("CN_Cartorio",System.Type.GetType("System.String"));
                dTable.Columns.Add("CN_Data",System.Type.GetType("System.String"));
                dTable.Columns.Add("CN_Estado",System.Type.GetType("System.String"));
                dTable.Columns.Add("CN_Cidade",System.Type.GetType("System.String"));

                #endregion

                #region Pesquisa
                dTable.Columns.Add("PSQ_JaEstudou",System.Type.GetType("System.String"));
                dTable.Columns.Add("PSQ_RecebePensao",System.Type.GetType("System.String"));
                dTable.Columns.Add("PSQ_Inss",System.Type.GetType("System.String"));
                dTable.Columns.Add("PSQ_BolsaFamilia",System.Type.GetType("System.String"));
                dTable.Columns.Add("PSQ_EstaGravida",System.Type.GetType("System.String"));
                dTable.Columns.Add("PSQ_MesGravidez",System.Type.GetType("System.String"));
                #endregion

                #region Responsavel
                dTable.Columns.Add("RSP_Nome",System.Type.GetType("System.String"));
                dTable.Columns.Add("RSP_Cpf",System.Type.GetType("System.String"));
                dTable.Columns.Add("RSP_Identidade",System.Type.GetType("System.String"));
                dTable.Columns.Add("RSP_Celular",System.Type.GetType("System.String"));
                dTable.Columns.Add("RSP_Telefone",System.Type.GetType("System.String"));
                dTable.Columns.Add("RSP_Email",System.Type.GetType("System.String"));
                #endregion

                #region Deficiencia & Escola
                dTable.Columns.Add("Deficiencias");
                dTable.Columns.Add("EscolaDestino");
                dTable.Columns.Add("EscolaOrigem");
                dTable.Columns.Add("Serie");
                #endregion


                #endregion

                return dTable;
            }

        }

        public List<Deficiencia> LstDeficiencia
        {
            get
            {

                return lstDeficiencia;
            }

        }

        public List<Escola> LstEscola
        {
            get
            {
                return lstEscola;
            }

        }

        public string Serie
        {
            get
            {
                return serie;
            }
        }

        #endregion


        #endregion



        //Mudar Status Usuario
        public int MudarStatus(long idUsuario,int idStatus)
        {
            try
            {
                using(Context ado = new Context())
                {
                    Usuario u = Pesquisar(idUsuario);
                    u.IdStatus = IdStatus;
                    ado.SaveChanges();

                }
                return 1;

            }
            catch(EntityCommandExecutionException r)
            {
                throw new AlertaException(r.Message);
            }
            catch
            {
                throw;
            }
        }

      

        //Edita as informaçoes de um usuario
        public void Editar()
        {
            try
            {
                using(Context fip = new Context())
                {
                    Usuario u = (from us in fip.Usuario
                                 where us.Id == this.Id
                                 select us).First();

                    u.IdStatus = this.IdStatus;
                    u.IdBairro = this.IdBairro;
                    u.IdEstado = this.IdEstado;
                    u.IdCidade = this.IdCidade;
                    u.Identidade = this.Identidade;
                    u.IdMotivoMatricula = this.IdMotivoMatricula;
                    u.Endereco = this.Endereco;
                    u.Nome = this.Nome;
                    u.Cpf = this.Cpf;
                    u.DataNascimento = this.DataNascimento;
                    u.Sexo = this.Sexo;
                    u.OrgaoEmissor = this.OrgaoEmissor;
                    u.Cep = this.Cep;

                    fip.SaveChanges();
                }



            }
            catch
           (ArgumentNullException e)
            {
                ArgumentException argExc =
                    new ArgumentException("Não foi encontrada nenhuma matricula com esse número",e.Source,e.InnerException);
                throw argExc;
            }
            catch(Exception)
            {
                throw;
            }

        }


        #region Metodos

        #region Consultas

        public Usuario Pesquisar(long idUsuario)
        {
            Usuario usu;
            try
            {
                using(Context fip = new Context())
                {
                    usu = (from u in fip.Usuario.Include("FK_DeficienciasDoUsuario")
                           where u.Id == idUsuario
                           select u).FirstOrDefault();

                    if(usu == null)
                        throw new AlertaException
                        ("Não foi encontrado nenhum Usuario com essa Matricula");
                    else
                        PreencherValorInstancia(usu);
                }
                return this;
            }
            catch
            {
                throw;
            }


        }

        public Usuario Pesquisar(string cpf,DateTime nascimento)
        {

            try
            {
                using(Context fip = new Context())
                {
                    IQueryable<Usuario> user = from u in fip.Usuario
                                               where u.Cpf == cpf && u.DataNascimento == nascimento
                                               select u;
                    if(user == null || user.Count() <= 0)
                        throw new AlertaException
                            ("Não foi encontrado nenhum Usuario com essa Matricula");
                    else
                    {
                        PreencherValorInstancia(user.First());
                        return this;
                    }
                }
            }
            catch
            {
                throw;
            }


        }


        #endregion

        #region Imprimir

        public DataTable Imprimir()
        {

            try
            {
                dTable = new DataTable();
                DataRow dRow = UsuTabela.NewRow();

                #region Usuario
                dRow["id"] = this.Id;
                dRow["nome"] = this.Nome;
                dRow["Cpf"] = this.Cpf;
                dRow["dataNascimento"] = this.DataNascimento.ToShortDateString();
                dRow["sexo"] = this.Sexo;
                dRow["identidade"] = this.Identidade;
                dRow["orgaoEmissor"] = this.OrgaoEmissor;
                dRow["nomeMae"] = this.NomeMae;
                dRow["nomePai"] = this.NomePai;
                dRow["dataCadastro"] = this.DataCadastro.ToShortDateString();
                dRow["endereco"] = this.Endereco;
                dRow["cep"] = this.Cep;

                #region FK

                dRow["estado"] = this.FK_EstadoDoUsuario.Nome;
                dRow["cidade"] = this.FK_CidadeDoUsuarioReference.Value.Nome;
                dRow["bairro"] = this.FK_BairroDoUsuario.Nome;
                dRow["motivo"] = this.FK_MotivoMatricula.Motivo;
                dRow["status"] = this.FK_Status.Nome;
                #endregion

                #endregion

                #region Certidao

                dRow["CN_Numero"] = this.FK_Certidao.Numero;
                dRow["CN_Livro"] = this.FK_Certidao.Livro;
                dRow["CN_Folha"] = this.FK_Certidao.Folha;
                dRow["CN_Cartorio"] = this.FK_Certidao.Cartorio;
                dRow["CN_Data"] = this.FK_Certidao.DataCertidao.Value.ToShortDateString();
                dRow["CN_Estado"] = this.FK_Certidao.FK_CidadeDaCertidao.FK_Estado.Nome;
                dRow["CN_Cidade"] = this.FK_Certidao.FK_CidadeDaCertidao.Nome;


                #endregion

                #region Pesquisa

                string PSQ_JaEstudou = string.Empty,
                    PSQ_RecebePensao = string.Empty,
                    PSQ_Inss = string.Empty,
                    PSQ_BolsaFamilia = string.Empty,
                    PSQ_EstaGravida = string.Empty,
                    PSQ_MesGravidez = string.Empty;

                if((this.IdPesquisa != 0))
                {
                    if(!string.IsNullOrEmpty(this.FK_Pesquisa.JaEstudou))
                        PSQ_JaEstudou = this.FK_Pesquisa.JaEstudou;
                    if(!string.IsNullOrEmpty(this.FK_Pesquisa.RecebePensaoAlimenticia))
                        PSQ_RecebePensao = this.FK_Pesquisa.RecebePensaoAlimenticia;
                    if(!string.IsNullOrEmpty(this.FK_Pesquisa.Inss))
                        PSQ_Inss = this.FK_Pesquisa.Inss;
                    if(!string.IsNullOrEmpty(this.FK_Pesquisa.BolsaFamilia))
                        PSQ_BolsaFamilia = this.FK_Pesquisa.BolsaFamilia;
                    if(!string.IsNullOrEmpty(this.FK_Pesquisa.EstaGravida))
                        PSQ_EstaGravida = this.FK_Pesquisa.EstaGravida;
                    if(!string.IsNullOrEmpty(this.FK_Pesquisa.MesGravidez))
                        PSQ_MesGravidez = this.FK_Pesquisa.MesGravidez;

                }
                dRow["PSQ_JaEstudou"] = PSQ_JaEstudou;
                dRow["PSQ_RecebePensao"] = PSQ_RecebePensao;
                dRow["PSQ_Inss"] = PSQ_Inss;
                dRow["PSQ_BolsaFamilia"] = PSQ_BolsaFamilia;
                dRow["PSQ_EstaGravida"] = PSQ_EstaGravida;
                dRow["PSQ_MesGravidez"] = PSQ_MesGravidez + " meses";



                #endregion

                #region Responsavel
                string
                   RSP_Nome = string.Empty,
                   RSP_Cpf = string.Empty,
                   RSP_Identidade = string.Empty,
                   RSP_Celular = string.Empty,
                   RSP_Telefone = string.Empty,
                   RSP_Email = string.Empty;

                if(this.IdResponsavel != 0)
                {
                    if(!string.IsNullOrEmpty(this.FK_Responsavel.Nome))
                        RSP_Nome = this.FK_Responsavel.Nome;

                    if(!string.IsNullOrEmpty(this.FK_Responsavel.Cpf))
                        RSP_Cpf = this.FK_Responsavel.Cpf;

                    if(!string.IsNullOrEmpty(this.FK_Responsavel.Identidade))
                        RSP_Identidade = this.FK_Responsavel.Identidade;

                    if(!string.IsNullOrEmpty(this.FK_Responsavel.Celular))
                        RSP_Celular = this.FK_Responsavel.Celular;

                    if(!string.IsNullOrEmpty(this.FK_Responsavel.Telefone))
                        RSP_Telefone = this.FK_Responsavel.Telefone;

                    if(!string.IsNullOrEmpty(this.FK_Responsavel.Email))
                        RSP_Email = this.FK_Responsavel.Email;

                }
                string deficiencia = string.Empty;

                dRow["RSP_Nome"] = RSP_Nome;
                dRow["RSP_Cpf"] = RSP_Cpf;
                dRow["RSP_Identidade"] = RSP_Identidade;
                dRow["RSP_Celular"] = RSP_Celular;
                dRow["RSP_Telefone"] = RSP_Telefone;
                dRow["RSP_Email"] = RSP_Email;


                #endregion

                #region Deficiencia e Escola
                foreach(var item in this.lstDeficiencia)
                    dRow["Deficiencias"] += "<li>" + item.Nome + "</li>";

                foreach(var item in this.lstEscola)
                {
                    if(item.FK_EscolaDestino != null)
                        dRow["EscolaDestino"] += item.Nome;
                    if(item.FK_EscolaOrigem != null)
                        dRow["EscolaOrigem"] += item.Nome;

                }
                dRow["Serie"] += this.serie;
                #endregion

                dTable.Rows.Add(dRow);


            }
            catch
            {
                throw;
            }
            return dTable;
        }

        #endregion

        ///Validação de Unicidade do Usuario
        ///<summary>
        /// Verifica se o Usuario já existe no banco
        /// Se o Usuario ja existir retorna true caso contrario retorna false
        /// </summary>
        public bool ValidarExistencia(Usuario usu)
        {
            try
            {
                using(Context fip = new Context())
                {
                    long usua;
                    usua =
                   (from u in fip.Usuario
                    where (((u.Cpf == usu.Cpf) & (u.Nome == usu.Nome)) ||
                    ((u.Nome == usu.Nome) & (u.NomeMae == usu.NomeMae) ||
                    ((u.NomePai == usu.NomePai) & (u.Nome == usu.Nome)) &&
                    (u.IdResponsavel == usu.IdResponsavel)))
                    select u.Id).FirstOrDefault()
                    ;

                    if(usua > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                throw;
            }



        }


        public void Apagar(long idUsuario)
        {
            try
            {
                using(Context fip = new Context())
                {
                    var u = (from usu in fip.Usuario
                             where usu.Id == idUsuario
                             select usu).First();

                    fip.Usuario.DeleteObject(u);
                    fip.SaveChanges();
                }
            }
            catch(Exception)
            {

                throw;
            }

        }



        private void PreencherValorInstancia(Usuario usu)
        {
            try
            {
                #region Propriedades
                if(usu.FK_EscolaEscolhida.Count > 0)
                    usu.FK_EscolaEscolhida.Load();

                if(usu.FK_DeficienciasDoUsuario.Count > 0)
                    usu.FK_DeficienciasDoUsuario.Load();

                #region Nativa
                this._Id = usu.Id;
                this._Nome = usu.Nome;
                this._Cpf = usu.Cpf;
                this._DataNascimento = usu.DataNascimento;
                this._Sexo = Sexo;
                this._Identidade = usu.Identidade;
                this._OrgaoEmissor = usu.OrgaoEmissor;
                this._Endereco = usu.Endereco;
                this._Cep = usu.Cep;
                this._IdCertidao = usu.IdCertidao;
                this._IdPesquisa = usu.IdPesquisa;
                this._IdMotivoMatricula = usu.IdMotivoMatricula;
                this._IdCidade = usu.IdCidade;
                this._IdStatus = usu.IdStatus;
                this._IdBairro = usu.IdBairro;
                this._IdEstado = usu.IdEstado;
                this._IdResponsavel = usu.IdResponsavel;
                this._NomeMae = usu.NomeMae;
                this._NomePai = usu.NomePai;
                this._DataCadastro = usu.DataCadastro;
                #endregion

                #region FK
                //----------Chave Estrangeira Unica------------------//

                if(usu.FK_Status != null)
                    this.FK_Status = usu.FK_Status;
                if(usu.FK_Pesquisa != null)
                    this.FK_Pesquisa = usu.FK_Pesquisa;
                if(usu.FK_EstadoDoUsuario != null)
                    this.FK_EstadoDoUsuario = usu.FK_EstadoDoUsuario;
                if(usu.FK_CidadeDoUsuario != null)
                    this.FK_CidadeDoUsuario = usu.FK_CidadeDoUsuario;
                if(usu.FK_CidadeDoUsuario != null)
                    this.FK_BairroDoUsuario = usu.FK_BairroDoUsuario;
                if(usu.FK_MotivoMatricula != null)
                    this.FK_MotivoMatricula = usu.FK_MotivoMatricula;
                if(usu.FK_Responsavel != null)
                    this.FK_Responsavel = usu.FK_Responsavel;
                //---------------------------------------------------//

                //----------Chave Estrangeira Multiplas------------------//
                //Certidao Nascimento
                if(usu.FK_Certidao != null)
                    this.FK_Certidao = usu.FK_Certidao;
                if(FK_Certidao.FK_CidadeDaCertidao != null)
                    this.FK_Certidao.FK_CidadeDaCertidao = usu.FK_Certidao.FK_CidadeDaCertidao;
                if(FK_Certidao.FK_CidadeDaCertidao.FK_Estado != null)
                    this.FK_Certidao.FK_CidadeDaCertidao.FK_Estado = usu.FK_Certidao.FK_CidadeDaCertidao.FK_Estado;

                //---------------------------------------------------//


                //----------Tabelas de Relacionamentos------------------//


                foreach(var item in FK_EscolaEscolhida)
                {
                    usu.lstEscola.Add(item.FK_EscolaOrigem);
                    usu.lstEscola.Add(item.FK_EscolaDestino);
                    usu.serie = item.FK_SerieDoAluno.Nome;
                }

                this.serie = usu.serie;
                this.lstEscola = usu.lstEscola;

                foreach(var item in usu.FK_DeficienciasDoUsuario)
                    usu.lstDeficiencia.Add(item.FK_DeficienciaDoUsuario);

                this.lstDeficiencia = usu.lstDeficiencia;

                //---------------------------------------------------//
                #endregion

                #endregion

            }
            catch
            {
                throw;
            }

        }

        //Cadastrar Usuario
        public long Inserir()
        {
            try
            {
                using(Context ado = new Context())
                {
                    ado.Usuario.AddObject(this);
                    ado.SaveChanges();
                    return  Id;
                }

            }
            catch
            {
                throw;
            }

        }

        #endregion
    }

 
    
    

}
