using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Pickupable))]
    public class WorldObjectPickup : WorldObjectSelect {
        protected override void Start() {
            base.Start();
        }
        protected override void Select1(GameObject selector, int option = 0) {
            if (selector.GetComponent<PlayerController>()) {

            }
            Destroy(this.gameObject);
        }
    }
}