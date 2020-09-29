using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Selectable))]
    public class WorldObject : MonoBehaviour {
        public WorldObjectData worldObjectData;
        private Selectable selectable;
        protected virtual void Awake() {

        }
        protected virtual void Start() {
            selectable = GetComponent<Selectable>();
            selectable.select0Delegate = Select0;
            selectable.select1Delegate = Select1;
            selectable.deselectDelegate = Deselect;
        }
        protected virtual void Select0(GameObject selector, int option = 0) {
            Debug.Log("Selector0: " + selector.name + " Target: " + this.name);
        }
        protected virtual void Select1(GameObject selector, int option = 0) {
            Debug.Log("Selector1: " + selector.name + " Target: " + this.name);
        }
        protected virtual void Deselect(GameObject selector, int option = 0) {
            Debug.Log("Deselect: " + this.name);
        }
    }
}