using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [System.Serializable]
    public class Context {
        public Dictionary<string, object> data;
        public int CharacterId;
        [System.NonSerialized]
        public Character Character;
        public Context() {
            data = new Dictionary<string, object>();
        }
        public Context(int characterID){
            data = new Dictionary<string, object>();
            SetCharacter(characterID);
        }
        public void SetCharacter(int characterID){
            this.CharacterId = characterID;
            this.Character = Character.GetCharacter(this.CharacterId);
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

        public bool ValidateRequest(string name){
            object _obj;
            if(this.data.TryGetValue(name, out _obj)){
                return true;
            } else{
                return false;
            }
        }
        public bool ValidateRequest(List<string> names){
            foreach (string name in names){
                if(!ValidateRequest(name)){
                    return false;
                }
            }
            return true;
        }
    }
}