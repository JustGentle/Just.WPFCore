using System;
using System.Collections;
using System.Collections.Generic;

namespace Just
{
    public static class DictionaryExtension
    {
        public static IDictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (dic.ContainsKey(key))
                dic[key] = value;
            else
                dic.Add(key, value);

            return dic;
        }

        public static IDictionary<TKey, TValue> RemoveWhenContains<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
        {
            if (dic.ContainsKey(key))
                dic.Remove(key);
            
            return dic;
        }
    }
}
