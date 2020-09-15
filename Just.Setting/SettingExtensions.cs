using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Just.Setting
{
    public static class SettingExtensions
    {
        public static T ReadProperty<T, TProp>(this T obj, Expression<Func<T, TProp>> prop) where T : ISetting
        {
            SettingManager.Instance.ReadProperty(obj, prop);
            return obj;
        }
        public static T WriteProperty<T, TProp>(this T obj, Expression<Func<T, TProp>> prop, bool save = true) where T : ISetting
        {
            SettingManager.Instance.WriteProperty(obj, prop, save);
            return obj;
        }
    }
}
