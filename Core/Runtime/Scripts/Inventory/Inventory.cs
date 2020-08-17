using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class Inventory {
        public InventorySlot[] inventorySlots;
        public int inventoryCount;

        public Inventory(int inventoryCount) {
            this.inventoryCount = inventoryCount;
            inventorySlots = new InventorySlot[this.inventoryCount];
        }

        public bool AddItem(WOD_Item itemData, int count = 1) {
            int _existingId = FindExistingSlot(itemData);
            if (_existingId != -1) {
                AddToSlot(_existingId, itemData, count);
                return true;
            } else {
                int _emptyId = FindEmptySlot();
                if (_emptyId != -1) {
                    AddToSlot(_emptyId, itemData, count);
                    return true;
                }
            }
            return false;
        }
        public bool AddItem(Item item, int count = 1) {
            bool _added = AddItem((WOD_Item) item.worldObjectData, count);
            if (_added) {
                GameObject.Destroy(item);
                return true;
            } else {
                return false;
            }

        }
        private int FindExistingSlot(WOD_Item itemData) {
            for (int i = 0; i < inventorySlots.Length - 1; i++) {
                if (inventorySlots[i].itemData == itemData) {
                    return i;
                }
            }
            return -1;
        }
        private int FindEmptySlot() {
            for (int i = 0; i < inventorySlots.Length - 1; i++) {
                if (inventorySlots[i].itemData == null) {
                    return i;
                }
            }
            return -1;
        }
        private void AddToSlot(int slot, WOD_Item itemData, int count) {
            inventorySlots[slot].AddToSlot(itemData, count);
        }

    }
}