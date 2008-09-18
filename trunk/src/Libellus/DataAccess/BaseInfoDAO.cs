using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Libellus.Utilities;
using Libellus.Domain;
using System.Data;

namespace Libellus.DataAccess
{
    /// <summary>
    /// This class is to maintain the owner_info table in the database
    /// </summary>
    class BaseInfoDAO : BaseDAO
    {
        public BaseInfoDAO(string database) : base(database)
        {
            
        }

        public bool InitializeDatabase(string user, string dbname, string password)
        {
            string today = DateTime.Now.ToShortDateString();
            object[] parameters = new string[]{user,today,today,dbname,password};
            return this.ExecuteNonQuery(SQL.BaseInfo.INSERT,parameters);
        }

        public BaseInfo GetBaseInfo()
        {
            DataSet ds = this.ExecuteQuery(SQL.BaseInfo.SELECT,null);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];
            BaseInfo info = new BaseInfo();
            info.DateCreated = row[DBConstants.BaseInfo.DATE_CREATED].ToString();
            info.DBName = row[DBConstants.BaseInfo.DB_NAME].ToString();
            info.LastAccessed = row[DBConstants.BaseInfo.LAST_ACCESSED].ToString();
            info.Owner = row[DBConstants.BaseInfo.OWNER].ToString();
            info.Password = row[DBConstants.BaseInfo.PASSWORD].ToString();
            this.UpdateLastAccessed();
            return info;
        }

        public bool UpdateLastAccessed()
        {
            object[] data = { DateTime.Now.ToString()};
            return this.ExecuteNonQuery(SQL.BaseInfo.UPDATE_DTE,data);
        }

        public string getVersionNumber()
        {
            DataSet ds = this.ExecuteQuery("SELECT version FROM db_info WHERE id = '1'",null);
            return ds.Tables[0].Rows[0]["version"].ToString();
        }
        
        public bool ExecuteNonQuery(string query)
        {
        	return this.ExecuteNonQuery(query, null);
        }
    }
}
