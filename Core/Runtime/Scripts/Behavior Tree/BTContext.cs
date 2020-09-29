using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    public class BTContext {
        public Dictionary<string, object> context;

        public BTContext() {
            context = new Dictionary<string, object>();
        }

        public T SetContext<T>(string name, T obj) {
            context[name] = obj;
            return (T) context[name];
        }
        public List<T> SetContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (context.TryGetValue<List<T>>(name, out _val)) {
                context[name] = ((List<T>) context[name]).Union(obj).ToList();
            } else {
                SetContext<List<T>>(name, obj);
            }
            return (List<T>) context[name];
        }
        public void RemoveContext<T>(string name) {
            T _val;
            if (context.TryGetValue<T>(name, out _val)) {
                context.Remove(name);
            }
        }
        // Return true if fully removed, false if list still has elements
        public bool RemoveContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (context.TryGetValue<List<T>>(name, out _val)) {
                List<T> _l1 = ((List<T>) context[name]).Except(obj).ToList();
                if (_l1.Count > 0) {
                    context[name] = _l1;
                    return false;
                } else {
                    RemoveContext<List<T>>(name);
                    return true;
                }
            } else {
                return true;
            }
        }
    }
}