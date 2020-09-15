using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Just.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Just.Setting
{
    public class SettingManager
    {
        public string DefaultSettingPath = AppDomain.CurrentDomain.BaseDirectory + "Settings.Default.json";
        public string UserSettingPath = AppDomain.CurrentDomain.BaseDirectory + "Settings.User.json";
        public JObject DefaultSettings { get; private set; }
        public JObject UserSettings { get; private set; }
        public Encoding SettingEncoding { get; set; } = Encoding.UTF8;

        private static SettingManager _Instance = null;
        public static SettingManager Instance
        {
            get
            {
                _Instance = _Instance ?? new SettingManager();
                return _Instance;
            }
        }

        public SettingManager()
        {
            Load();
        }

        public void Load()
        {
            try
            {
                DefaultSettings = JObject.Parse(File.ReadAllText(DefaultSettingPath, SettingEncoding));
                //没有用户配置文件时自动创建空对象
                if (!File.Exists(UserSettingPath))
                {
                    using (var file = File.Create(UserSettingPath))
                    {
                        var data = SettingEncoding.GetBytes("{}");
                        file.Write(data, 0, data.Length);
                    }
                }
                UserSettings = JObject.Parse(File.ReadAllText(UserSettingPath, SettingEncoding));
            }
            catch (Exception ex)
            {
                Logger.Error("加载配置错误", ex);
            }
        }
        public void Save()
        {
            try
            {
                //仅保存用户配置
                File.WriteAllText(UserSettingPath, UserSettings.ToString(), SettingEncoding);
            }
            catch (Exception ex)
            {
                Logger.Error("保存配置错误", ex);
            }
        }

        public T Read<T>(string key, T defaultValue = default)
        {
            var defToken = DefaultSettings[key];
            //优先取用户配置，再取默认配置
            var token = UserSettings[key] ?? defToken;
            //未配置时取默认值
            var value = token == null ? defaultValue : token.ToObject<T>();
            return value;
        }
        public void Write<T>(string key, T value, bool save = true)
        {
            //不存在或与默认值不相同才写入
            if (UserSettings.ContainsKey(key) || JsonConvert.SerializeObject(DefaultSettings[key]) != JsonConvert.SerializeObject(value))
                UserSettings[key] = value == null ? null : JToken.FromObject(value);
            //立刻保存
            if (save)
                Save();
        }

        /// <summary>
        /// 读取指定属性配置
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProp">属性类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="prop">属性</param>
        /// <returns></returns>
        public SettingManager ReadProperty<T, TProp>(T obj, Expression<Func<T, TProp>> prop)
        {
            var propInfo = GetPropertyInfo(prop);
            var key = $"{typeof(T).Name}.{propInfo.Name}";
            var value = Read(key, (TProp)propInfo.GetValue(obj));
            //将属性值设置为读取配置
            propInfo.SetValue(obj, value);
            return this;
        }
        /// <summary>
        /// 设置指定属性配置
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProp">属性类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="prop">属性</param>
        /// <param name="save">立刻保存</param>
        public SettingManager WriteProperty<T, TProp>(T obj, Expression<Func<T, TProp>> prop, bool save = true)
        {
            var propInfo = GetPropertyInfo(prop);
            var key = $"{typeof(T).Name}.{propInfo.Name}";
            Write(key, (TProp)propInfo.GetValue(obj));
            return this;
        }
        private PropertyInfo GetPropertyInfo<T, TProp>(Expression<Func<T, TProp>> prop)
        {
            var body = prop.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                var o = (body as UnaryExpression).Operand;
                return (o as MemberExpression).Member as PropertyInfo;
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
            {
                return (body as MemberExpression).Member as PropertyInfo;
            }
            return null;
        }
    }
}
