using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;

namespace WebAPI.DAL
{
    /// <summary>
    /// Classe Abstrata, no qual serve de base para as classes de Acesso ao dados.
    /// Na construção desta, que a conexão é iniciada e controlada.
    /// </summary>
    /// <typeparam name="T">Tipo do Objeto que será utilizado na Classe que Herda desta.</typeparam>
    abstract public class BaseDAL<T>
    {
        #region Propriedades
        string DefaultConnection = "Server = DESKTOP-HQJPE87\\SQLEXPRESS; Database = db_CRUD; Integrated Security = True; ";
        /// <summary>
        /// Propriedade de Conexão
        /// </summary>
        private SqlConnection connection;
        /// <summary>
        /// Propriedade de Transação do Banco de Dados.
        /// </summary>
        private SqlTransaction transaction;
        /// <summary>
        /// Parametros utilizados nos comandos SQL das classes DAL.
        /// </summary>
        private SqlParameter[] parameters;
        /// <summary>
        /// Comando SQL que será utilizado pelas classes DAL.
        /// </summary>
        private SqlCommand command;
        /// <summary>
        /// SqlDataReader utilizado nas classes DAL.
        /// </summary>
        private SqlDataReader reader;

        /// <summary>
        /// 
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(DefaultConnection);
                    connection.Open();
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Dispose();
                        connection = new SqlConnection(DefaultConnection);
                        connection.Open();
                    }
                }
                return connection;
            }
            set { connection = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlParameter[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlCommand Command
        {
            get
            {
                command.CommandTimeout = 30000;
                return command;
            }
            set { command = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlDataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        /// <summary>
        /// Propriedades de TryParse
        /// </summary>
        public DateTime data = new DateTime();
        public int inteiro = new int();
        public float numero = new float();
        #endregion

 
        /// <summary>
        /// Comando padrão para execução NonQuery com controle de conexão.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo do Comando</param>
        /// <param name="pParameters">Parametros do Comando</param>
        /// <returns>Retorna Identity em caso de Insert, ou a quantidade de linhas alteradas.</returns>
        public int ExecuteNonQuery(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (connection = new SqlConnection(DefaultConnection))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    Command = new SqlCommand(pSqlCommand, Connection);
                    Command.CommandType = pCommandType;

                    Command.Parameters.AddRange(pParameters);

                    return Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataSet ExecuteReader(string pSqlCommand, System.Data.CommandType pCommandType)
        {
            parameters = new SqlParameter[0];
            return ExecuteReader(pSqlCommand, pCommandType, parameters);
        }

        public DataSet ExecuteReader(string pSqlCommand, System.Data.CommandType pCommandType, List<SqlParameter> pParameters)
        {
            return ExecuteReader(pSqlCommand, pCommandType, pParameters.ToArray());
        }

        /// <summary>
        /// Comando padrão para execução de comandos de Select com controle de conexão.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo de Comando</param>
        /// <param name="pParameters">Parametros do Comando</param>
        /// <returns>SqlDataReader</returns>
        public DataSet ExecuteReader(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (connection = new SqlConnection(DefaultConnection))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    Command = new SqlCommand(pSqlCommand, connection);
                    Command.CommandType = pCommandType;
                    Command.CommandTimeout = 60;

                    if (pParameters != null)
                        Command.Parameters.AddRange(pParameters);

                    SqlDataAdapter sda = new SqlDataAdapter(Command);

                    DataSet ds = new DataSet();

                    sda.Fill(ds);

                    return ds;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //if (Connection.State == ConnectionState.Open)
                //    Connection.Close();
            }
        }


        public object ExecuteScalar(string pSqlCommand, System.Data.CommandType pCommandType, List<SqlParameter> pParameters)
        {
            return ExecuteScalar(pSqlCommand, pCommandType, pParameters.ToArray());
        }
        /// <summary>
        /// Comando padrão para execução de um comando SQL retornando Objeto.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo do Comando</param>
        /// <param name="pParameters">Parametros</param>
        /// <returns>Objeto</returns>
        public object ExecuteScalar(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (connection = new SqlConnection(DefaultConnection)) 
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    Command = new SqlCommand(pSqlCommand, Connection);
                    Command.CommandType = pCommandType;

                    Command.Parameters.AddRange(pParameters);

                    return Command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;                    
            }
        }

        public DataTable ExecuteDataTable(string pSqlCommand, System.Data.CommandType pCommandType)
        {
            Command = new SqlCommand(pSqlCommand, connection);
            parameters = new SqlParameter[0];
            return ExecuteDataTable(pSqlCommand, pCommandType, parameters);
        }

        public DataTable ExecuteDataTable(string pSqlCommand, System.Data.CommandType pCommandType, List<SqlParameter> pParameters)
        {
            return ExecuteDataTable(pSqlCommand, pCommandType, pParameters.ToArray());
        }

        /// <summary>
        /// Comando padrão para execução de comandos de Select com controle de conexão.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo de Comando</param>
        /// <param name="pParameters">Parametros do Comando</param>
        /// <returns>SqlDataReader</returns>
        public DataTable ExecuteDataTable(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (Connection)
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    Command = new SqlCommand(pSqlCommand, connection);
                    Command.CommandType = pCommandType;
                    Command.CommandTimeout = 60;

                    if (pParameters != null)
                        Command.Parameters.AddRange(pParameters);

                    SqlDataAdapter sda = new SqlDataAdapter(Command);

                    DataTable dt = new DataTable();

                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //if (Connection.State == ConnectionState.Open)
                //    Connection.Close();
            }
        }

        public bool VerificaColuna(string NomeColuna, SqlDataReader Reader)
        {
            for (int i = 0; i < Reader.FieldCount; i++)
            {
                if (Reader.GetName(i) == NomeColuna)
                    return true;
            }
            return false;
        }

        public bool VerificaColuna(string NomeColuna, DataRow dr)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                if (dr.Table.Columns[i].ColumnName == NomeColuna)
                    return true;
            }
            return false;
        }

        public string GetParameters(SqlParameter[] parameters)
        {
            StringBuilder lstParameter = new StringBuilder();
            foreach (var parameter in parameters)
            {
                lstParameter.Append(string.Concat("   ", parameter.ParameterName, ": ", parameter.Value, "\r\n"));
            }
            return "[INNER]" + lstParameter.ToString();
        }

        public Exception ExceptionWithParameters(Exception ex)
        {
            return new Exception(GetParameters(Parameters), ex);
        }

        /// <summary>
        /// Metodo que popula um objeto Typ com dados de um registro do banco de dados.
        /// </summary>
        /// <param name="registro">Registro do banco de dados.</param>
        /// <returns>Lista com objetos do tipo Typ.</returns>
        protected List<Typ> ConverterParaLista<Typ>(DataTable dt)
        {
            List<Typ> lst = new List<Typ>();
            foreach (DataRow row in dt.Rows)
            {
                Typ item = PegarItem<Typ>(row);
                lst.Add(item);
            }
            return lst;
        }

        /// <summary>
        /// Metodo que popula um objeto Typ (tipo primitivo e com apenas um valor) 
        /// a partir um registro escalar do banco de dados.
        /// </summary>
        /// <param name="registro">Registro do banco de dados.</param>
        /// <returns>Lista com objetos do tipo Typ.</returns>
        protected List<Typ> ConverterParaListaUmTipo<Typ>(DataTable dt)
        {
            List<Typ> lst = new List<Typ>();
            foreach (DataRow row in dt.Rows)
            {
                Typ item = (Typ)row[0];
                lst.Add(item);
            }
            return lst;
        }


        /// <summary>
        /// Gera um objeto do tipo Typ, carregando os dados de um registro
        /// em seus atributos.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto que sera gerado.</typeparam>
        /// <param name="registro">Registro do banco de dados.</param>
        /// <returns>Objeto do tipo Typ carregado com os dados do registro.</returns>
        protected Typ PegarItem<Typ>(DataRow registro)
        {
            Type temp = typeof(Typ);
            Typ obj = Activator.CreateInstance<Typ>();

            foreach (DataColumn coluna in registro.Table.Columns)
            {
                foreach (PropertyInfo propriedade in temp.GetProperties())
                {
                    if (propriedade.Name == coluna.ColumnName)
                    {
                        if (registro[coluna.ColumnName] != DBNull.Value)
                        {
                            propriedade.SetValue(obj, registro[coluna.ColumnName], null);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return obj;
        }


    }
}
