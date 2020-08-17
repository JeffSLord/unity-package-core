using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class InventorySlot {
        public WOD_Item itemData;
        public int itemCount;

        public InventorySlot(WOD_Item wod_Item, int itemCount) {
            this.itemData = wod_Item;
            this.itemCount = itemCount;
        }

        public void AddToSlot(WOD_Item itemData, int count) {
            if (this.itemData == itemData) {
                this.itemCount += count;
            } else {
                this.itemData = itemData;
                this.itemCount = count;
            }
        }
    }
}