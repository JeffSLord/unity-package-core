using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Pickupable))]
    public class WorldObjectPickup : WorldObjectSelect {
        protected override void Start() {
            base.Start();
        }
        // protected override void Select1(GameObject selector, int option = 0) {
        //     PlayerController playerController = selector.GetComponent<PlayerController>();
        //     if (playerController != null) {
        //         ItemBT itemBT = new ItemBT(playerController.character, this);
        //         playerController.character.behaviorTree.SetNode(itemBT.PickupSequence());
        //     }
        //     if (selector.GetComponent<PlayerController>()) {

        //     }
        //     Destroy(this.gameObject);
        // }
    }
}