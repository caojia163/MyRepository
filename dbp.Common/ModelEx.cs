using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GAManage.Common.Attributes;

namespace GAManage.Common
{
    public static class ModelEx
    {
        /// <summary>
        /// 排除相应属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="filter">筛选属性名称,此集合的属性对象不会出现在返回结果中</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> Except(this IEnumerable<PropertyInfo> property, IEnumerable<String> filter)
        {
            if (filter != null && filter.Count() > 0)
            {
                List<PropertyInfo> listExcept = new List<PropertyInfo>();
                foreach (var item in property)
                {
                    if (!filter.Contains(item.Name))
                    {
                        listExcept.Add(item);
                    }
                }
                return listExcept;
            }
            return property;
        }

        /// <summary>
        /// 把datatable转换为mode
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T">model的类型</typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static T SetValueFromDB<T>(this T model, DataTable dt) where T : new()
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return ConvertDataRowToModel<T>(dt.Rows[0], dt.Columns);
            }
            return default(T);
        }

        /// <summary>
        /// 把datatable转换为modeList
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T">model的类型</typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static List<T> SetListValueFromDB<T>(this IList<T> listModel, DataTable dt) where T : new()
        {
            List<T> lstReturn = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataColumnCollection dcc = dt.Columns;
                foreach (DataRow dr in dt.Rows)
                {
                    lstReturn.Add(ConvertDataRowToModel<T>(dr, dcc));
                }
                return lstReturn;
            }
            return lstReturn;
        }

        /// <summary>
        /// 把datarow转换为model
        /// 调用此函数请保证一下几点：
        /// 1.model的内不包含子model。
        /// 2.model有无参构造函数
        /// 3.model的属性名和datatable的列名一致（不区分大小写）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="dcc"></param>
        /// <returns></returns>
        public static T ConvertDataRowToModel<T>(DataRow dr, DataColumnCollection dcc) where T : new()
        {
            if (dr == null || dcc == null)
            {
                return default(T);
            }
            T t = new T();
            PropertyInfo[] pis = t.GetType().GetProperties();
            PropertyInfo pi = null;
            foreach (DataColumn dc in dcc)
            {
                pi = pis.FirstOrDefault(p => p.Name.ToLower().Equals(dc.ColumnName.ToLower()));
                if (pi != null
                    && dr[dc] != null
                    && dr[dc] != DBNull.Value
                    && pi.CanWrite)
                {
                    Type type = pi.PropertyType;
                    if (type.Name.ToLower().Contains("nullable"))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }
                    if (type.IsEnum)
                    {
                        if (dr[dc] != null && dr[dc] != DBNull.Value)
                        {
                            pi.SetValue(t, System.Enum.Parse(type, Convert.ToInt32(dr[dc]).ToString()), null);
                        }
                    }
                    else
                    {
                        pi.SetValue(t, Convert.ChangeType(dr[dc], type), null);
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 转换列表到二维数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="listModel">原始列表</param>
        /// <param name="listFilter">需要筛选的列表</param>
        /// <returns></returns>
        public static String[,] To2Array<T>(this IList<T> listModel, params String[] filter) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] arrProperty = type.GetProperties();
            if (filter != null && filter.Length > 0)
            {
                arrProperty = arrProperty.Except(filter).ToArray();
            }
            PropertyInfo pi = null;
            String[,] arrData = new String[listModel.Count + 1, arrProperty.Length];
            //append title
            for (int i = 0; i < arrProperty.Length; i++)
            {
                pi = arrProperty[i];
                var descAttr = pi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descAttr != null && descAttr.Length > 0)
                {
                    arrData[0, i] = (descAttr[0] as DescriptionAttribute).Description;
                    continue;
                }
                arrData[0, i] = pi.Name;
            }
            //append data
            for (int i = 0; i < listModel.Count; i++)
            {
                for (int j = 0; j < arrProperty.Length; j++)
                {
                    pi = arrProperty[j];
                    object piValue = pi.GetValue(listModel[i], null);
                    if (piValue == null)
                    {
                        arrData[i + 1, j] = null;
                        continue;
                    }
                    Type pitype = pi.PropertyType;
                    if (pitype.Name.ToLower().Contains("nullable"))
                    {
                        pitype = Nullable.GetUnderlyingType(pitype);
                    }
                    var rowNumberAttr = pi.GetCustomAttributes(typeof(ShowRowNumberAttribute), false);
                    if (rowNumberAttr != null && rowNumberAttr.Length > 0)
                    {
                        arrData[i + 1, j] = (i + 1).ToString();
                        continue;
                    }
                    if (pitype == typeof(bool))
                    {
                        arrData[i + 1, j] = Convert.ToBoolean(piValue) ? "是" : "否";
                        continue;
                    }
                    if (pitype.IsEnum)
                    {
                        arrData[i + 1, j] = EnumHelper.GetDescription(pitype, (int)piValue);
                        continue;
                    }
                    if (pitype == typeof(DateTime)
                        || pitype == typeof(DateTime?))
                    {
                        var showDateTimeAttr = pi.GetCustomAttributes(typeof(DateTimeFormatAttribute), false);
                        if (showDateTimeAttr != null && showDateTimeAttr.Length > 0)
                        {
                            DateTime nowtime = DateTime.Parse(piValue.ToString());
                            arrData[i + 1, j] = nowtime.ToString((showDateTimeAttr[0] as DateTimeFormatAttribute).DataFormatString);
                            continue;
                        }
                        //default datetime showformater
                        arrData[i + 1, j] = DateTime.Parse(piValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        continue;
                    }
                    arrData[i + 1, j] = piValue.ToString();
                }
            }
            return arrData;
        }
    }
}
