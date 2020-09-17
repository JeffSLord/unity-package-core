using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class ItemBT {
        // public Item item;
        // public List<Item> nearbyItems;
        // public float searchRadius;
        // public ItemBT(Character character, Item item, float searchRadius = 5.0f):
        //     base(character, item.transform.position, 1.5f) {
        //         this.item = item;
        //         this.searchRadius = searchRadius;
        //     }
        private static NodeStates Pickup(Dictionary<string, object> context) {
            // Debug.Log(item);
            Item _item;
            if (context.TryGetValue<Item>("item", out _item)) {
                GameObject.Destroy(_item.gameObject);
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.FAILURE;
            }
            // if (context.item != null) {
            //     GameObject.Destroy(context.item.gameObject);
            //     return NodeStates.SUCCESS;
            // } else {
            //     return NodeStates.FAILURE;
            // }
        }
        public static Node PickupNode(Dictionary<string, object> context) {
            return new TaskContextNode(Pickup, context, "Pickup Item");
        }

        public static Node PickupSequence(Dictionary<string, object> context) {
            return new Sequence(new List<Node> {
                MoveBT.MoveSequence(context),
                PickupNode(context)
            });
        }

        // private static NodeStates FindNearbyItems(BehaviorContext context) {
        //     Collider[] _colliders = Physics.OverlapSphere(character.transform.position, this.searchRadius);
        //     List<Item> _items = new List<Item>();
        //     foreach (Collider _col in _colliders) {
        //         if (_col.gameObject.GetComponent<Item>() != null) {
        //             _items.Add(_col.gameObject.GetComponent<Item>());
        //         }
        //     }
        //     nearbyItems = _items;
        //     if (nearbyItems.Count > 0)
        //         return NodeStates.SUCCESS;
        //     else
        //         return NodeStates.FAILURE;

        //     // Physics.SphereCast(character.transform.position, 5.0f, );
        // }
        private static NodeStates AutoPickup() {
            return NodeStates.FAILURE;
        }
    }

}