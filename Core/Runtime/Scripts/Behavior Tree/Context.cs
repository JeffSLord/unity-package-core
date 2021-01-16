using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    [System.Serializable]
    public class Context {
        public Dictionary<string, object> data;
        public int CharacterId{get;set;}
        public List<int> DetectedCharacterIDs{get;set;}
        public List<int> DetectedEnemeyCharacterIDs{get;set;}
        public Context() {
            data = new Dictionary<string, object>();
        }
        public Context(int characterId){
            this.CharacterId = characterId;
            this.DetectedCharacterIDs = new List<int>();
            this.DetectedEnemeyCharacterIDs = new List<int>();
        } 

        public T SetContext<T>(string name, T obj) {
            data[name] = obj;
            return (T) data[name];
        }
        public List<T> SetContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (data.TryGetValue<List<T>>(name, out _val)) {
                data[name] = ((List<T>) data[name]).Union(obj).ToList();
            } else {
                SetContext<List<T>>(name, obj);
            }
            return (List<T>) data[name];
        }
        public void RemoveContext<T>(string name) {
            T _val;
            if (data.TryGetValue<T>(name, out _val)) {
                data.Remove(name);
            }
        }
        // Return true if fully removed, false if list still has elements
        public bool RemoveContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (data.TryGetValue<List<T>>(name, out _val)) {
                List<T> _l1 = ((List<T>) data[name]).Except(obj).ToList();
                if (_l1.Count > 0) {
                    data[name] = _l1;
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