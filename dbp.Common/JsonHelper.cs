using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GAManage.Common
{
    public static class JsonHelper
    {
        /// <summary>
        /// 使用JSON.NET 转换对象到JSON字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ConvertToJosnString(object item)
        {
            if (item != null)
            {
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                return JsonConvert.SerializeObject(item, timeFormat);
            }
            return "";
        }

        /// <summary>
        /// 转换为动态类型
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        //public static dynamic ConvertJsonToDynamic(string json)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
        //    return serializer.Deserialize(json, typeof(object));
        //}

        /// <summary>
        /// 使用JSON.NET 转换JSON字符串到.NET对象
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(string strJson)
        {
            if (!string.IsNullOrEmpty(strJson))
            {
                return JsonConvert.DeserializeObject<T>(strJson, new JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            return default(T);
        }

        /// <summary>
        /// 使用JSON.NET 转换对象到JSON字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ConvertToJosnNS(object item)
        {
            if (item != null)
            {
                return JsonConvert.SerializeObject(item);
            }
            return "";
        }

        /// <summary>
        ///  序列化DataTable为Json,用于发送到客户端
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dataTable)
        {
            StringBuilder objSb = new StringBuilder();
            JavaScriptSerializer objSer = new JavaScriptSerializer();
            Dictionary<string, object> resultMain = new Dictionary<string, object>();
            int index = 0;
            foreach (DataRow dr in dataTable.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                resultMain.Add(index.ToString(), result);
                index++;
            }
            objSer.Serialize(resultMain, objSb);
            return objSb.ToString();
        }

        /// <summary>
        /// 序列化DataRow为Json,用于发送到客户端
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static string DataRowToJson(DataRow dataRow)
        {
            StringBuilder objSb = new StringBuilder();
            JavaScriptSerializer objSer = new JavaScriptSerializer();
            var dataTable = dataRow.Table;
            var result = new Dictionary<string, object>();
            foreach (DataColumn dc in dataTable.Columns)
            {
                result.Add(dc.ColumnName, dataRow[dc].ToString());
            }
            objSer.Serialize(result, objSb);
            return objSb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static Dictionary<string, object> JsonToDictionary(string jsonStr)
        {
            try
            {
                var javaScriptSerializer = new JavaScriptSerializer();
                var value =
                    javaScriptSerializer.DeserializeObject(jsonStr) as Dictionary<string, object>;
                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ObjectToJson<T>(T o)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var value = javaScriptSerializer.Serialize(o);
            return value;
        }

        public static string TryObjectToJson<T>(T o)
        {
            try
            {
                var javaScriptSerializer = new JavaScriptSerializer();
                var value = javaScriptSerializer.Serialize(o);
                return value;
            }
            catch
            {
                return string.Empty;
            }

        }

        public static T JsonToObject<T>(string str)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            var value = javaScriptSerializer.Deserialize<T>(str);
            return value;
        }

        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="entitys">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        #region 新加转换
        /// <summary>  
        /// 通过DataRow 填充实体  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="dr"></param>  
        /// <returns></returns>  
        public static T GetModelByDataRow<T>(System.Data.DataRow dr) where T : new()
        {
            T model = new T();
            foreach (PropertyInfo pInfo in model.GetType().GetProperties())
            {
                object val = getValueByColumnName(dr, pInfo.Name);
                pInfo.SetValue(model, val, null);
            }
            return model;
        }

        //返回DataRow 中对应的列的值。  
        public static object getValueByColumnName(System.Data.DataRow dr, string columnName)
        {
            if (dr.Table.Columns.IndexOf(columnName) >= 0)
            {
                if (dr[columnName] == DBNull.Value)
                    return null;
                return dr[columnName];
            }
            return null;
        }
        public static object ToEntity(DataRow adaptedRow, Type entityType)
        {
            if (entityType == null || adaptedRow == null)
            {
                return null;
            }

            object entity = Activator.CreateInstance(entityType);
            CopyToEntity(entity, adaptedRow);

            return entity;
        }

        public static T ToEntity<T>(DataRow adaptedRow, T value) where T : new()
        {
            T item = new T();
            if (value == null || adaptedRow == null)
            {
                return item;
            }

            item = Activator.CreateInstance<T>();
            CopyToEntity(item, adaptedRow);

            return item;
        }

        public static void CopyToEntity(object entity, DataRow adaptedRow)
        {
            if (entity == null || adaptedRow == null)
            {
                return;
            }
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (!CanSetPropertyValue(propertyInfo, adaptedRow))
                {
                    continue;
                }

                try
                {
                    if (adaptedRow[propertyInfo.Name] is DBNull)
                    {
                        propertyInfo.SetValue(entity, null, null);
                        continue;
                    }
                    SetPropertyValue(entity, adaptedRow, propertyInfo);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static bool CanSetPropertyValue(PropertyInfo propertyInfo, DataRow adaptedRow)
        {
            if (!propertyInfo.CanWrite)
            {
                return false;
            }

            if (!adaptedRow.Table.Columns.Contains(propertyInfo.Name))
            {
                return false;
            }

            return true;
        }

        private static void SetPropertyValue(object entity, DataRow adaptedRow, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(DateTime?) ||
                propertyInfo.PropertyType == typeof(DateTime))
            {
                DateTime date = DateTime.MaxValue;
                DateTime.TryParse(adaptedRow[propertyInfo.Name].ToString(),
                    CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

                propertyInfo.SetValue(entity, date, null);
            }
            else
            {
                propertyInfo.SetValue(entity, adaptedRow[propertyInfo.Name], null);
            }
        }

        #endregion

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows);
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch(Exception ex)
                    {  //You can log something here     

                        //throw;    
                    }
                }
            }
            return obj;
        }
    }
}
