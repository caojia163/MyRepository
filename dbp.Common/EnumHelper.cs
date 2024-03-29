﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GAManage.Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumStr"></param>
        /// <returns></returns>
        public static string GetDescription<T>(string enumStr)
        {
            Type type = typeof(T);
            return GetDescription(type, enumStr);
        }

        public static string GetDescription(Type type, string enumStr)
        {
            MemberInfo[] memInfos = type.GetMembers();

            MemberInfo memInfo = null;

            string temp = string.Empty;

            for (int i = 0; i < memInfos.Length; i++)
            {
                memInfo = memInfos[i];

                if (string.Compare(enumStr, memInfo.Name, true) == 0)
                {
                    object[] attrs = memInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        temp = ((DescriptionAttribute)attrs[0]).Description;
                    }
                    return temp;
                }
            }

            throw new Exception("未知的枚举描述");
        }

        public static T GetValue<T>(string description)
        {
            Type type = typeof(T);

            MemberInfo[] memInfos = type.GetMembers();

            MemberInfo memInfo = null;

            string temp = "";

            for (int i = 0; i < memInfos.Length; i++)
            {
                memInfo = memInfos[i];

                object[] attrs = memInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    temp = ((DescriptionAttribute)attrs[0]).Description;

                    if (temp == description)
                    {
                        return (T)Enum.Parse(typeof(T), memInfo.Name, true);
                    }
                }
            }

            throw new Exception("未知的枚举描述");
        }

        public static int GetEnumValue<T>(string description)
        {
            Dictionary<int, string> dict = GetEnumValueAndDescriptions<T>();
            if (dict != null && dict.Count > 0)
            {
                List<int> keys = dict.Keys.ToList();
                List<string> values = dict.Values.ToList();
                for (int i = 0; i < dict.Count; i++)
                {
                    if (String.Compare(description, values[i], true) == 0)
                    {
                        return keys[i];
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 根据Name取得描述值
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetEnumValue(FieldInfo[] fields, string name)
        {
            foreach (var item in fields)
            {
                if (item.Name.Equals(name))
                {
                    object[] custAttrs = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (custAttrs != null && custAttrs.Length > 0)
                    {
                        return ((DescriptionAttribute)custAttrs[0]).Description;
                    }
                }
            }
            return name;
        }
        /// <summary>
        /// 获取枚举字典信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumValueAndDescriptions<T>()
        {
            var rValue = new Dictionary<int, string>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            string[] names = Enum.GetNames(typeof(T));
            Array values = Enum.GetValues(typeof(T));
            if (fields.Length == names.Length && fields.Length == values.Length)
            {
                int intCnt = 0;
                foreach (object value in values)
                {
                    object[] attrs = fields[intCnt].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description = string.Empty;
                    if (attrs != null && attrs.Length > 0)
                    {
                        description = ((DescriptionAttribute)attrs[0]).Description;
                    }
                    else
                    {
                        description = names[intCnt];
                    }
                    if (!rValue.ContainsKey((int)value))
                    {
                        rValue.Add((int)value, description);
                    }
                    intCnt += 1;
                }
            }
            return rValue;
        }
        /// <summary>  
        /// 得到对枚举的描述文本  
        /// </summary>  
        /// <param name="enumType">枚举类型</param>  
        /// <returns></returns>  
        public static string GetEnumText(Type enumType)
        {
            EnumDescription[] eds = (EnumDescription[])enumType.GetCustomAttributes(typeof(EnumDescription), false);
            if (eds.Length != 1)
                return string.Empty;
            return eds[0].EnumDisplayText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns>键：枚举对应的数字，值：枚举的描述</returns>
        public static IList<KeyValuePair<string, string>> GetDictionary(Type type)
        {
            FieldInfo[] fields = type.GetFields();
            IList<KeyValuePair<string, string>> dict = new List<KeyValuePair<string, string>>();
            for (int i = 1; i < fields.Length; i++)
            {
                FieldInfo item = fields[i];
                string desription;
                object[] objs = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs != null && objs.Length != 0)
                {
                    desription = ((DescriptionAttribute)objs[0]).Description;
                }
                else
                {
                    desription = item.Name;
                }
                dict.Add(new KeyValuePair<string, string>(((int)Enum.Parse(type, item.Name)).ToString(), desription));
            }
            return dict;
        }

        /// <summary>
        /// 取得枚举项的描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="enumValue">枚举数值</param>
        /// <returns></returns>
        public static string GetDescription(Type type, int enumValue)
        {
            string descriptions = string.Empty;
            var custAttr = type.GetCustomAttributes(typeof(FlagsAttribute), false);
            if (custAttr != null && custAttr.Length > 0)
            {
                Array enumValues = Enum.GetValues(type);
                String[] enumNames = Enum.GetNames(type);
                for (int i = 0; i < enumValues.Length; i++)
                {
                    if ((Convert.ToInt32(enumValues.GetValue(i)) & enumValue) == Convert.ToInt32(enumValues.GetValue(i)))
                    {
                        if (descriptions != string.Empty)
                            descriptions += ";";
                        descriptions += GetDescription(type, enumNames[i]);
                    }
                }
                return descriptions;
            }
            return GetDescription(type, Enum.GetName(type, enumValue));
        }
        /// <summary>
        /// 获取枚举字典信息
        /// 处理乱序或者包含负数的Enum顺序问题
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumValueAndDescriptionsEx<T>()
        {
            var rValue = new Dictionary<int, string>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            string[] names = Enum.GetNames(typeof(T));
            int[] values = Enum.GetValues(typeof(T)) as int[];
            if (fields.Length == names.Length && fields.Length == values.Length)
            {
                string desc;
                for (int i = 0; i < names.Length; i++)
                {
                    desc = GetEnumValue(fields, names[i]);
                    if (!rValue.ContainsKey(values[i]))
                    {
                        rValue.Add(values[i], desc);
                    }
                }
            }
            return rValue;
        }

        public static IEnumerable<EnumInfo> GetEnumInfoList<T>() where T : struct, IConvertible
        {
            var type = typeof(T);
            return GetEnumInfoList(type);
        }

        public static IEnumerable<EnumInfo> GetEnumInfoList(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("T 必须是枚举类型");
            }

            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                if (!field.FieldType.IsEnum) continue;

                string name = field.Name;
                //获取枚举对应的值
                int value = Convert.ToInt32(type.InvokeMember(field.Name, BindingFlags.GetField, null, null, null));

                object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string description = string.Empty;
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
                else
                {
                    description = name;
                }
                EnumInfo enumInfo = new EnumInfo();

                enumInfo.Name = name;
                enumInfo.Value = value;
                enumInfo.Description = description;

                yield return enumInfo;
            }

        }


        public static EnumInfo GetEnumInfo<T>(T obj) where T : struct, IConvertible
        {
            var list = GetEnumInfoList<T>();
            return list.FirstOrDefault(x => x.Value == Convert.ToInt32(obj));
        }
        public static EnumInfo GetEnumInfo(Type type, object obj)
        {
            var list = GetEnumInfoList(type);
            return list.FirstOrDefault(x => x.Value == Convert.ToInt32(obj));
        }
    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    public class EnumInfo
    {
        public string Name { get; internal set; }
        public int Value { get; internal set; }
        public string Description { get; internal set; }

        public override string ToString()
        {
            return string.Format("Name:{0} Value:{1} Description:{2}", Name, Value, Description);
        }
    }
}
