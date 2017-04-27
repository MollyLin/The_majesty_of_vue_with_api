using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Vue.lib
{
    internal class Sql
    {
        public SqlConnection Conn { get; set; }

        public Sql()
        {
            Conn = new SqlConnection(Config.ConectionString);
        }

        /// <summary>
        /// 依照Cmd回傳DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        /// <Programmer>Ryan Liang</Programmer>
        public DataTable GetDataTable(SqlCommand Cmd)
        {
            DataSet ds = GetDataSet(Cmd);
            
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return new DataTable();
        }

        /// <summary>
        /// 依照Cmd回傳DataSet
        /// </summary>
        /// <returns>DataSet</returns>
        /// <Programmer>Ryan Liang</Programmer>
        public DataSet GetDataSet(SqlCommand Cmd)
        {
            Cmd.CommandTimeout = 0;
            DataSet ds = new DataSet();
            try
            {
                Conn.Open();
                Cmd.Connection = Conn;
                using (SqlDataAdapter adapter = new SqlDataAdapter(Cmd))
                    adapter.Fill(ds); 
            }
            catch (Exception e)
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                throw e;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// 依照Cmd回傳第一筆值
        /// </summary>
        /// <returns>object</returns>
        /// <Programmer>Ryan Liang</Programmer>
        public object GetSingleValue(SqlCommand Cmd)
        {
            Cmd.CommandTimeout = 0;
            object _result;
            try
            {
                Conn.Open();
                Cmd.Connection = Conn;
                _result = Cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                throw e;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return _result;
        }

        /// <summary>
        /// 執行單筆Cmd
        /// </summary>
        /// <returns>影響的資料筆數</returns>
        public int ExecSQL(SqlCommand Cmd)
        {
            Cmd.CommandTimeout = 0;
            int _Result;
            try
            {
                Conn.Open();
                Cmd.Connection = Conn;
                _Result = Cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                throw e;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return _Result;
        }

        /// <summary>
        /// 以Transaction執行多筆Cmds
        /// </summary>
        /// <returns>影響的資料筆數</returns>
        public int ExecSQLs(List<SqlCommand> Cmds)
        {
            int _Result = 0;
            Conn.Open();
            SqlTransaction Trans = Conn.BeginTransaction();
            try
            {
                foreach (SqlCommand Cmd in Cmds)
                {
                    Cmd.CommandTimeout = 0;
                    Cmd.Transaction = Trans;
                    Cmd.Connection = Conn;
                    _Result += Cmd.ExecuteNonQuery();
                }
                Trans.Commit();
            }
            catch (Exception e)
            {
                Trans.Rollback();
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                throw e;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return _Result;
        }
        public int ExecSP(Commander Cmd)
        {
            int _Result;
            try
            {
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                Cmd.SetStoredProcedure();
                _Result = Cmd.Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return _Result;
        }
    }

    public class Commander
    {
        public IDbCommand Command { get; set; }

        public Commander(IDbConnection Conn)
        {
            this.Command = Conn.CreateCommand();
        }

        public string CommandText
        {
            get { return Command.CommandText; }
            set { Command.CommandText = value; }
        }

        public void AddPara(string ParameterName, object Value, DbType DbType,
                            ParameterDirection Direction = ParameterDirection.Input)
        {
            IDataParameter Para = Command.CreateParameter();
            Para.ParameterName = ParameterName;
            Para.Value = Value;
            Para.DbType = DbType;
            Para.Direction = Direction;

            Command.Parameters.Add(Para);
        }
        public void ClearParams()
        {
            Command.Parameters.Clear();
        }
        public void SetStoredProcedure()
        {
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandTimeout = 600;
        }
    }

    internal static class DtRow
    {
        public static T? GetValue<T>(this DataRow row, string columnName) where T : struct
        {
            if (row.IsNull(columnName))
                return null;

            return row[columnName] as T?;
        }

        public static string GetText(this DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
                return string.Empty;

            return row[columnName] as string ?? string.Empty;
        }
    }

    internal static class DbObj
    {
        public static object GetVal(object obj)
        {
            if (obj == null)
                return DBNull.Value;

            return obj;
        }
    }
}