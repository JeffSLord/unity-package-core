using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public static class Extensions {
        public static T Get<T>(this Dictionary<string, object> instance, string name) {
            return (T) instance[name];
        }
        public static bool TryGetValue<T>(this Dictionary<string, object> instance, string name, out T value) {
            object _val;
            bool _try = instance.TryGetValue(name, out _val);
            value = (T) _val;
            return _try;
        }
    }
}

// bool Dictionary<string, object>.TryGetValue(string key, out object value)