﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GAManage.Common
{
    public class DataConvert
    {
        public static int? ToInt(object objValue, int? falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            int result;

            bool success = Int32.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }
        public static int ToInt(object objValue, int falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            int result;

            bool success = Int32.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }

        public static int ToInt(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(int);

            int result;

            bool success = Int32.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return default(int);
        }

        public static double? ToDouble(object objValue, double? falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            double result;

            bool success = Double.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }

        public static double? ToDouble(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(double);

            double result;

            bool success = Double.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return default(double);
        }

        public static decimal? ToDecimal(object objValue, decimal? falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            decimal result;

            bool success = Decimal.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }

        public static decimal ToDecimal(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(decimal);

            decimal result;

            bool success = Decimal.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return default(decimal);
        }

        public static long ToLong(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(long);

            long result;

            bool success = long.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return default(long);
        }

        public static long? ToLong(object objValue, long falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            long result;

            bool success = long.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }

        public static long? ToLong(object objValue, long? falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            long result;

            bool success = long.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }

        public static bool ToBoolean(object objValue, bool falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            bool result;

            bool success = Boolean.TryParse(objValue.ToString(), out result);

            if (success == true) return result;

            return falseReturnValue;
        }

        public static bool ToBoolean(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(bool);

            int intValue;

            if (Int32.TryParse(objValue.ToString(), out intValue) == true)
            {
                if (intValue == 1) return true;

                return false;
            }

            if (objValue.ToString().Trim().ToLower() == "true")
            {
                return true;
            }

            if (objValue.ToString().Trim().ToLower() == "false")
            {
                return false;
            }

            throw new Exception("未知的布尔值:" + objValue.ToString());
        }

        public static int BooleanToInt(string value)
        {
            if (value.Trim().ToLower() == "true") return 1;

            if (value.Trim().ToLower() == "false") return 0;

            throw new Exception("错误的输入值:" + value);
        }

        public static string ToString(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return "";

            return objValue.ToString();
        }

        public static object ToObject<T>(object objValue, T defaultValue)
        {
            if (objValue == null || objValue == DBNull.Value) return defaultValue;

            return objValue;
        }

        public static T ToObject<T>(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(T);

            if (objValue.GetType() != typeof(T)) return default(T);

            return (T)objValue;
        }

        public static bool IsDouble(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return false;

            double result;

            bool success = Double.TryParse(objValue.ToString(), out result);

            return success;
        }

        public static bool IsDecimal(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return false;

            decimal result;

            bool success = Decimal.TryParse(objValue.ToString(), out result);

            return success;
        }

        public static bool IsInt(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return false;

            int result;

            bool success = Int32.TryParse(objValue.ToString(), out result);

            return success;
        }

        public static T ToValue<T>(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return default(T);

            object value = Convert.ChangeType(objValue, typeof(T));

            return (T)value;
        }

        public static T ToValue<T>(object objValue, T defaultValue)
        {
            if (objValue == null || objValue == DBNull.Value) return defaultValue;

            object value = Convert.ChangeType(objValue, typeof(T));

            return (T)value;
        }

        public static DateTime? ToDateTime(object objValue, DateTime? falseReturnValue)
        {
            if (objValue == null || objValue == DBNull.Value) return falseReturnValue;

            DateTime outDateTime;

            if (DateTime.TryParse(objValue.ToString(), out outDateTime) == false)
            {
                return falseReturnValue;
            }

            return outDateTime;
        }

        public static DateTime ToDateTime(object objValue)
        {
            if (objValue == null || objValue == DBNull.Value) return DateTime.MinValue;

            DateTime outDateTime;

            if (DateTime.TryParse(objValue.ToString(), out outDateTime) == false)
            {
                return DateTime.MinValue;
            }

            return outDateTime;
        }

        public static decimal ToCeiling(decimal floatValue, int precision)
        {
            if (precision <= 0)
            {
                return floatValue;
            }

            StringBuilder strPrecision = new StringBuilder();

            strPrecision.Append("1");

            for (int i = 0; i < precision; i++)
            {
                strPrecision.Append("0");
            }

            precision = DataConvert.ToInt(strPrecision);

            return Math.Ceiling((floatValue) * precision) / precision;
        }

        public static string ToDbIds(IList<int> listId)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < listId.Count; i++)
            {
                builder.Append(listId[i]);

                if (i != listId.Count - 1)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }

        public static string ToDbIds(IList<string> listId)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < listId.Count; i++)
            {
                builder.Append("'");
                builder.Append(listId[i]);
                builder.Append("'");

                if (i != listId.Count - 1)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }

        public static string ToDbIds(int[] arrayId)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < arrayId.Length; i++)
            {
                builder.Append(arrayId[i]);

                if (i != arrayId.Length - 1)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }

        public static string SqlFormat(string strSql)
        {
            if (string.IsNullOrEmpty(strSql))
                return strSql;
            strSql = strSql.Replace("\r", "");
            strSql = strSql.Replace("\n", "");
            strSql = strSql.Replace("\t", "");
            return Regex.Replace(strSql, @"\s+", " ");
        }

        public static string ToCeilingString(decimal floatValue, int precision)
        {
            if (precision <= 0)
            {
                return floatValue.ToString();
            }

            StringBuilder strPrecision = new StringBuilder();

            strPrecision.Append("1");

            for (int i = 0; i < precision; i++)
            {
                strPrecision.Append("0");
            }

            int ceilingPrecision = DataConvert.ToInt(strPrecision);

            floatValue = Math.Ceiling((floatValue) * ceilingPrecision) / ceilingPrecision;

            string strFloatValue = floatValue.ToString();

            if (strFloatValue.IndexOf('.') == -1)
            {
                strFloatValue += ".";

                for (int i = 0; i < precision; i++)
                {
                    strFloatValue += "0";
                }
            }
            else
            {
                int intPrecision = strFloatValue.Substring(strFloatValue.IndexOf('.') + 1).Length;

                for (int i = 0; i < precision - intPrecision; i++)
                {
                    strFloatValue += "0";
                }
            }

            return strFloatValue;
        }

        public static string ToJson(DataTable table)
        {
            IList<string> columns = new List<string>();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                columns.Add(table.Columns[i].ColumnName);
            }

            DataRow row = null;

            var datalist = new List<dynamic>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dynamic expando = new ExpandoObject();

                row = table.Rows[i];

                var x = expando as IDictionary<string, Object>;

                foreach (var column in columns)
                {
                    x.Add(column, row[column].ToString());
                }

                datalist.Add(expando);
            }

            return JsonConvert.SerializeObject(datalist);
        }
    }
}
