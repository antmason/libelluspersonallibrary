using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Libellus.Utilities;
using System.Windows.Forms;
using Libellus.UI.Forms;

namespace Libellus.DataAccess
{
    /// <summary>
    /// This class is here just to eliminate redundancy of code and settings through
    /// inheritance of subclasses and forms
    /// </summary>
    class BaseDAO
    {
        protected string _database;

        public BaseDAO(string database)
        {
            _database = database;
        }

        protected string ConnectionString
        {
            get { return "Data Source=" + _database + ";New=False;Compress=True;Synchronous=Off"; }
        }

        protected SQLiteConnection getConnection()
        {
            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = this.ConnectionString;
            try
            {
                conn.Open();
            }
            catch (SQLiteException e)
            {
                ExceptionHandler.HandleException(e);
                return null;
            }
            
            return conn;
        }

        protected bool ExecuteNonQuery(string query, object[] parameters)
        {
            int affected = 0;
            this.EscapeSpecialCharacters(parameters);
            if (parameters != null && parameters.Length > 0)
                query = Utils.ReplaceSQLParameters(query, parameters);

            SQLiteConnection connection = this.getConnection();
            if (connection == null)
                return false;

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = query;

            try
            {
                affected = command.ExecuteNonQuery();
#if DEBUG
                Console.WriteLine("Non-Query:\t" + query);
#endif
            }
            catch (Exception e1)
            {
                ExceptionHandler.HandleException(e1);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        protected DataSet ExecuteQuery(string query, object[] parameters)
        {
            this.EscapeSpecialCharacters(parameters);
            if (parameters != null && parameters.Length > 0)
                query = Utils.ReplaceSQLParameters(query, parameters);

#if DEBUG
            Console.Out.WriteLine("Query: " + query);
#endif
            SQLiteConnection connection = this.getConnection();
            if (connection == null)
                return null;

            SQLiteCommand command = connection.CreateCommand();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception e1)
            {
                ExceptionHandler.HandleException(e1);
                MessageBox.Show(query);
                return null;
            }
            finally
            {
                connection.Close();
            }
#if DEBUG
            Console.Out.WriteLine(this.getDataSetInfo(ds));
#endif
            return ds;
        }
        
        protected void EscapeSpecialCharacters(object[] ary)
        {
            if(ary != null)
                for (int i = 0; i < ary.Length; i++)
                {
                    if (ary[i] is string)
                    {
                        ary[i] = ary[i].ToString().Replace("'", "''");
                    }
                }
        }

        protected string getDataSetInfo(DataSet ds)
        {
            string result = "";
            if (ds != null)
            {
                foreach (DataTable t in ds.Tables)
                {
                    result += "Table: " + t.TableName + "\n";
                    result += "Columns: ";
                    for (int k = 0; k < t.Columns.Count; k++)
                    {
                        result += t.Columns[k].ColumnName + "\t";
                    }
                    result += "\n";
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        result += "\tRow " + i + ": " + t.Rows[i].ToString() + "\n";
                        object[] ary = t.Rows[i].ItemArray;
                        for (int j = 0; j < ary.Length; j++)
                        {
                            result += "\t\tCell " + j + ": " + ary[j].ToString();
                        }
                        result += "\n";
                    }
                }
            }
            return result;
        }
       
    }
}
