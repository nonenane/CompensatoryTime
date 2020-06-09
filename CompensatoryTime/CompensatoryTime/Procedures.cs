using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace CompensatoryTime
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();


        /// <summary>
        /// Получение справочника отделов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getShop(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[inventory].[getShop]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (withAllDeps)
            {
                if (dtResult != null)
                {
                    if (!dtResult.Columns.Contains("isMain"))
                    {
                        DataColumn col = new DataColumn("isMain", typeof(int));
                        col.DefaultValue = 1;
                        dtResult.Columns.Add(col);
                        dtResult.AcceptChanges();
                    }

                    DataRow row = dtResult.NewRow();

                    row["cName"] = "Все магазины";
                    row["id"] = 0;
                    row["isMain"] = 0;
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.Sort = "isMain asc, cName asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }

            return dtResult;
        }


        /// <summary>
        /// Получение Дат Инв
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getDateInv(int promeg = 0)
        {
            ap.Clear();
            ap.Add(promeg);

            DataTable dtResult = executeProcedure("[inventory].[getDateInv]",
                 new string[1] { "@isGlobal" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение списка типов исключений
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getExceptionType()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[inventory].[getExceptionType]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

    }
}
