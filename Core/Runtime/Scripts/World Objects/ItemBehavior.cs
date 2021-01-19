using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Selectable))]
    // [RequireComponent(typeof(Pickupable))]
    public class ItemBehavior : WorldObject {
        protected override void Start() {
            base.Start();
        }
        protected override void Select1(GameObject selector, int option = 0) {
            PlayerController playerController = selector.GetComponent<PlayerController>();
            if (playerController != null) {
                Debug.Log("Starting pickup...");
                // ItemBT itemBT = new ItemBT(playerController.character, this);
                // BehaviorTreeBehavior _bt = playerController.characterBehavior.btBehavior;
                BehaviorTreeBehavior _bt = playerController.characterBehavior.GetComponent<BehaviorTreeBehavior>();
                _bt.BT.Context.SetContext("item", this);
                _bt.BT.SetManualNode(ItemBT.PickupSequence(_bt.BT.Context));
            }
        }
    }
}