using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public class ItemBT : MoveBT {
        public Item item;
        public List<Item> nearbyItems;
        public float searchRadius;
        public ItemBT(Character character, Item item, float searchRadius = 5.0f):
            base(character, item.transform.position, 1.5f) {
                this.item = item;
                this.searchRadius = searchRadius;
            }
        private NodeStates Pickup() {
            Debug.Log(item);
            if (item != null) {
                GameObject.Destroy(item.gameObject);
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.FAILURE;
            }
        }
        public TaskNode PickupNode() {
            return new TaskNode(Pickup, "Pickup Item");
        }

        public Sequence PickupSequence() {
            return new Sequence(new List<Node> {
                MoveSequence(),
                PickupNode()
            });
        }

        private NodeStates FindNearbyItems() {
            RaycastHit[] _raycastHist;
            Collider[] _colliders = Physics.OverlapSphere(character.transform.position, this.searchRadius);
            List<Item> _items = new List<Item>();
            foreach (Collider _col in _colliders) {
                if (_col.gameObject.GetComponent<Item>() != null) {
                    _items.Add(_col.gameObject.GetComponent<Item>());
                }
            }
            nearbyItems = _items;
            if (nearbyItems.Count > 0)
                return NodeStates.SUCCESS;
            else
                return NodeStates.FAILURE;

            // Physics.SphereCast(character.transform.position, 5.0f, );
        }
        private NodeStates AutoPickup() {
            return NodeStates.FAILURE;
        }
    }

}