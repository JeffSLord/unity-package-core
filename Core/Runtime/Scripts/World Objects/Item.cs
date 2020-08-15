using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Pickupable))]
    public class Item : WorldObjectPickup {
        protected override void Start() {
            base.Start();
        }
    }
}