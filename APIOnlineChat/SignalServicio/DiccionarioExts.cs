using System.Collections.Generic;

namespace APIOnlineChat.SignalServicio
{
    public static class DiccionarioExts
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source.ContainsKey(key))
                source[key] = value;
            else
                source.Add(key, value);
        }
    }
}
