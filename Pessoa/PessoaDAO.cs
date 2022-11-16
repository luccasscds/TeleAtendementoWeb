using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace TeleAtendimentoWeb
{
    /* 
     * Essa classe em questão é a responsável por buscar, inserir, deletar e alterar os dados
     * do banco da aplicação
    */
    class PessoaDAO
    {
        private static SQLiteConnection connection;

        protected static SQLiteConnection ConnectDB()
        {
            connection = new SQLiteConnection(@"Data Source=C:\\Users\\Lucas\\source\\repos\\TeleAtendimentoWeb\\db\\db_teleAtendimento.db");
            connection.Open();
            return connection;
        }

        // Inserir
        public static void Inserir(Pessoa newPerson)
        {
            long newIdPerson = 0;
            long newIdEndereco = 0;
            long newIdTelefone = 0;
            long newIdTipoTelefone = 0;

            if (newPerson.TELEFONES?[0]?.TIPO != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "INSERT INTO TIPO_TELEFONE " +
                        "(TIPO) VALUES (@TIPO) RETURNING ID";
                    command.Parameters.AddWithValue("@TIPO", newPerson.TELEFONES?[0]?.TIPO?.TIPO);
                    newIdTipoTelefone = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            if (newPerson.TELEFONES != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "INSERT INTO TELEFONE "+
                        "(NUMERO, DDD, TIPO_ID)"+
                        " VALUES (@NUMERO, @DDD, @TIPO_ID) RETURNING ID";
                    command.Parameters.AddWithValue("@NUMERO", newPerson.TELEFONES[0]?.NUMERO);
                    command.Parameters.AddWithValue("@DDD", newPerson.TELEFONES[0]?.DDD);
                    command.Parameters.AddWithValue("@TIPO_ID", newIdTipoTelefone);
                    newIdTelefone = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            if (newPerson.ENDERECO != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "INSERT INTO ENDERECO "+
                        "(LOGRADOURO, NUMERO, CEP, BAIRRO, CIDADE, ESTADO)"+
                        "VALUES (@LOGRADOURO, @NUMERO, @CEP, @BAIRRO, @CIDADE, @ESTADO) RETURNING ID";
                    command.Parameters.AddWithValue("@LOGRADOURO", newPerson.ENDERECO?.LOGRADOURO);
                    command.Parameters.AddWithValue("@NUMERO", newPerson.ENDERECO?.NUMERO);
                    command.Parameters.AddWithValue("@CEP", newPerson.ENDERECO?.CEP);
                    command.Parameters.AddWithValue("@BAIRRO", newPerson.ENDERECO?.BAIRRO);
                    command.Parameters.AddWithValue("@CIDADE", newPerson.ENDERECO?.CIDADE);
                    command.Parameters.AddWithValue("@ESTADO", newPerson.ENDERECO?.ESTADO);
                    newIdEndereco = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            if (newPerson.NOME != null && !newPerson.CPF.Equals(null))
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "INSERT INTO PESSOA "+
                        "(NOME, CPF, ENDERECO_ID, TELEFONES_ID) "+
                        "VALUES (@NOME, @CPF, @ENDERECO_ID, @TELEFONES_ID) RETURNING ID";
                    command.Parameters.AddWithValue("@NOME", newPerson.NOME);
                    command.Parameters.AddWithValue("@CPF", newPerson.CPF);
                    command.Parameters.AddWithValue("@ENDERECO_ID", newIdEndereco);
                    command.Parameters.AddWithValue("@TELEFONES_ID", newIdTelefone);
                    newIdPerson = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            if (newPerson.NOME != null && newPerson.TELEFONES != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "INSERT INTO PESSOA_TELEFONE " +
                        "(ID_PESSOA, ID_TELEFONE) " +
                        "VALUES (@ID_PESSOA, @ID_TELEFONE)";
                    command.Parameters.AddWithValue("@ID_PESSOA", newIdPerson);
                    command.Parameters.AddWithValue("@ID_TELEFONE", newIdTelefone);
                    command.ExecuteNonQuery();
                }
            }

            ConnectDB().Close();
        }

        // Consultar
        public static Pessoa[] Consultar(string cpf = "all")
        {
            if (cpf == "all")
            {
                return GetAllPeople();
            }
            else
            {
                return GetPeople(cpf);
            };

        }

        // Excluir
        public static void Excluir(Pessoa person)
        {
            if (person.NOME != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "DELETE FROM PESSOA WHERE ID = @ID";
                    command.Parameters.AddWithValue("@ID", person.ID);
                    command.ExecuteNonQuery();
                }
                ConnectDB().Close();
            }
        }

        // Alterar
        public static void Alterar(Pessoa person)
        {
            if (person.TELEFONES != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "UPDATE TIPO_TELEFONE SET TIPO=@TIPO WHERE ID = @ID";
                    command.Parameters.AddWithValue("@TIPO", person.TELEFONES?[0]?.TIPO?.TIPO);
                    command.Parameters.AddWithValue("@ID", person.TELEFONES?[0]?.TIPO?.ID);
                    command.ExecuteNonQuery();
                }
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "UPDATE TELEFONE SET " +
                        "NUMERO=@NUMERO, DDD=@DDD WHERE ID = @ID";
                    command.Parameters.AddWithValue("@NUMERO", person.TELEFONES?[0]?.NUMERO);
                    command.Parameters.AddWithValue("@DDD", person.TELEFONES?[0]?.DDD);
                    command.Parameters.AddWithValue("@ID", person.TELEFONES?[0]?.ID);
                    command.ExecuteNonQuery();
                }
            }
            if (person.ENDERECO != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "UPDATE ENDERECO SET " +
                        "LOGRADOURO=@LOGRADOURO, NUMERO=@NUMERO, CEP=@CEP, BAIRRO=@BAIRRO, CIDADE=@CIDADE, ESTADO=@ESTADO WHERE ID = @ID";
                    command.Parameters.AddWithValue("@LOGRADOURO", person.ENDERECO?.LOGRADOURO);
                    command.Parameters.AddWithValue("@NUMERO", person.ENDERECO?.NUMERO);
                    command.Parameters.AddWithValue("@CEP", person.ENDERECO?.CEP);
                    command.Parameters.AddWithValue("@BAIRRO", person.ENDERECO?.BAIRRO);
                    command.Parameters.AddWithValue("@CIDADE", person.ENDERECO?.CIDADE);
                    command.Parameters.AddWithValue("@ESTADO", person.ENDERECO?.ESTADO);
                    command.Parameters.AddWithValue("@ID", person.ENDERECO?.ID);
                    command.ExecuteNonQuery();
                }
            }
            if (person.NOME != null)
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "UPDATE PESSOA SET NOME=@NOME, CPF=@CPF WHERE ID = @ID";
                    command.Parameters.AddWithValue("@NOME", person.NOME);
                    command.Parameters.AddWithValue("@CPF", person.CPF);
                    command.Parameters.AddWithValue("@ID", person.ID);
                    command.ExecuteNonQuery();
                }
            }
            ConnectDB().Close();
        }

        protected static Pessoa[] GetAllPeople()
        {
            SQLiteDataAdapter commandLinne = null;
            DataTable result = new DataTable();

            try
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM PESSOA";
                    commandLinne = new SQLiteDataAdapter(command.CommandText, ConnectDB());
                    commandLinne.Fill(result);
                }
                Pessoa[] pessoas = new Pessoa[result.Rows.Count];

                for (int i = 0; i < result.Rows.Count; i++)
                {

                    pessoas[i] = new Pessoa(
                        result.Rows[i].Field<long>("ID"),
                        result.Rows[i].Field<string>("NOME"),
                        result.Rows[i].Field<long>("CPF"),
                        null,
                        null
                    );
                }
                return pessoas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ConnectDB().Close();
        }
        protected static Pessoa[] GetPeople(string CPF)
        {
            SQLiteDataAdapter commandLinne = null;
            DataTable result = new DataTable();

            try
            {
                using (var command = ConnectDB().CreateCommand())
                {
                    command.CommandText = "SELECT "+
                            "p.ID, p.NOME, p.CPF,"+
                            " e.ID AS ID_ENDERECO, e.LOGRADOURO, e.NUMERO, e.CEP, e.BAIRRO, e.CIDADE, e.ESTADO," +
                            " t.ID AS 'ID_TELEFONE', t.DDD, t.NUMERO AS 'NUMERO_TELEFONE',"+
                            " tt.ID AS ID_TIPO_TELEFONE, tt.TIPO " +
                        "FROM PESSOA p LEFT JOIN ENDERECO e ON p.ENDERECO_ID = e.ID "+
                        "LEFT JOIN PESSOA_TELEFONE pt ON pt.ID_PESSOA = P.ID "+
                        "LEFT JOIN TELEFONE t ON pt.ID_TELEFONE = t.ID "+
                        "LEFT JOIN TIPO_TELEFONE tt ON tt.ID = t.TIPO_ID "+
                        "WHERE p.CPF = "+CPF;
                    commandLinne = new SQLiteDataAdapter(command.CommandText, ConnectDB());
                    commandLinne.Fill(result);
                }

                Pessoa[] pessoas = new Pessoa[result.Rows.Count];
                Endereco endereco = new Endereco();
                List<Telefone> telefones = new List<Telefone>();

                long id = result.Rows[0].Field<long>("ID");

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (!result.Rows[0].ItemArray[11].Equals(DBNull.Value))
                    {
                        TipoTelefone tipoTelefone = new TipoTelefone(
                            result.Rows[i].Field<long>("ID_TIPO_TELEFONE"),
                            result.Rows[i].Field<string>("TIPO")
                        );
                        telefones.Add( new Telefone(
                            result.Rows[i].Field<long>("ID_TELEFONE"),
                            result.Rows[i].Field<long>("NUMERO_TELEFONE"),
                            result.Rows[i].Field<long>("DDD"),
                            tipoTelefone
                        ));
                    }
                }
                if (result.Rows[0].Field<string>("LOGRADOURO") != null)
                {
                    endereco = new Endereco(
                        result.Rows[0].Field<long>("ID_ENDERECO"),
                        result.Rows[0].Field<string>("LOGRADOURO"),
                        result.Rows[0].Field<long>("NUMERO"),
                        result.Rows[0].Field<long>("CEP"),
                        result.Rows[0].Field<string>("BAIRRO"),
                        result.Rows[0].Field<string>("CIDADE"),
                        result.Rows[0].Field<string>("ESTADO")
                    );
                }

                pessoas[0] = new Pessoa(
                    id,
                    result.Rows[0].Field<string>("NOME"),
                    result.Rows[0].Field<long>("CPF"),
                    endereco,
                    telefones
                );

                return pessoas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ConnectDB().Close();
        }
    }
}