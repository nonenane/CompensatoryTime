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

        /// <summary>
        /// Получение справочника отделов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getDeps(DateTime dateInvent,int id_PersonnelType, bool withAllDeps = false)
        {
            ap.Clear();
            ap.Add(dateInvent);
            ap.Add(id_PersonnelType);

            DataTable dtResult = executeProcedure("[inventory].[getDepsLinkKadrs]",
                 new string[2] { "@dateInvent","@id_PersonnelType" },
                 new DbType[2] { DbType.Date,DbType.Int32 }, ap);

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
        /// Получение справочника отделов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getListKadr(DateTime dateInvent, int id_PersonnelType)
        {
            ap.Clear();
            ap.Add(dateInvent);
            ap.Add(id_PersonnelType);

            DataTable dtResult = executeProcedure("[inventory].[getListKadr]",
                 new string[2] { "@dateInvent", "@id_PersonnelType" },
                 new DbType[2] { DbType.Date, DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Запись справочника типов отзывов
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="npp">Аббревиатура</param>
        /// <param name="ViewArchive"></param>
        /// <param name="ViewAdd"></param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>

        public async Task<DataTable> setException(int id, int id_kadr, int id_shop, int id_ttost, int id_ExceptionType, bool isDop, decimal? Summa, decimal? CountDays, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_kadr); 
            ap.Add(id_shop); 
            ap.Add(id_ttost);
            ap.Add(id_ExceptionType); 
            ap.Add(isDop); 
            ap.Add(Summa); 
            ap.Add(CountDays);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);

            DataTable dtResult = executeProcedure("[inventory].[setException]",
                 new string[11] { "@id", "@id_kadr", "@id_shop", "@id_ttost", "@id_ExceptionType", "@isDop", "@Summa", "@CountDays", "@id_user", "@result", "@isDel" },
                 new DbType[11] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение Дат Инв
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getException(int id_ttost)
        {
            ap.Clear();
            ap.Add(id_ttost);

            DataTable dtResult = executeProcedure("[inventory].[getException]",
                 new string[1] { "@id_ttost" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение Дат Инв
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getViewUserWork(int id_ttost,int id_kadr)
        {
            ap.Clear();
            ap.Add(id_ttost);
            ap.Add(id_kadr);

            DataTable dtResult = executeProcedure("[inventory].[getViewUserWork]",
                 new string[2] { "@id_ttost","@id_kadr" },
                 new DbType[2] { DbType.Int32, DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Перенос сотрудников на другую дату инвенты
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> transferKadrToNextInvent(int id, int id_kadr, int id_ttost, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_kadr);
            ap.Add(id_ttost);            
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);


            DataTable dtResult = executeProcedure("[inventory].[transferKadrToNextInvent]",
                 new string[5] {"@id","@id_kadr", "@id_ttost", "@id_user", "@result" },
                 new DbType[5] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Запись, обновление и удаление настроек.
        /// </summary>
        /// <param name="id_value">Код значния</param>
        /// <param name="type_value">Тип значения</param>
        /// <param name="value_name">Название типа</param>
        /// <param name="value">Значение</param>
        /// <param name="comment">Комментарий</param>
        public async Task<DataTable> setSettings(string id_value, string type_value, string value_name, string value, string comment)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add(type_value);
            ap.Add(value_name);
            ap.Add(value);
            ap.Add(comment);
            ap.Add(false);

            return executeProcedure("[inventory].[setSettings]",
                new string[7] { "@id_prog", "@id_value", "@type_value", "@value_name", "@value", "@comment", "@isDel" },
                new DbType[7] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение настроек.
        /// </summary>
        /// <param name="id_value">Код значния</param>
        public async Task<DataTable> getSettings(string id_value)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_value);

            return executeProcedure("[inventory].[getSettings]",
                new string[2] { "@id_prog", "@id_value" },
                new DbType[2] { DbType.Int32, DbType.String }, ap);
        }

        /// <summary>
        /// Получение списка сотрудников которые отработали в морозилке за деньги!
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getReportBonusPayment(int id_ttost)
        {
            ap.Clear();
            ap.Add(id_ttost);

            DataTable dtResult = executeProcedure("[inventory].[getReportBonusPayment]",
                 new string[1] { "@id_ttost" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }


        /// <summary>
        /// Получение списка сотрудников которые отработали сутки со сканером!
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getReportDayWorkingUser(int id_ttost)
        {
            ap.Clear();
            ap.Add(id_ttost);

            DataTable dtResult = executeProcedure("[inventory].[getReportDayWorkingUser]",
                 new string[1] { "@id_ttost" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

    }
}
