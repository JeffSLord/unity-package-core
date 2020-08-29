using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class BehaviorContext {
        public bool isEnemyVisible;
        public bool isAlertedOfEnemy;
        public bool isWorking;
        public bool isHungry;
        public List<Character> enemiesInRange;

        public BehaviorContext() {
            this.isEnemyVisible = false;
            this.isAlertedOfEnemy = false;
            this.isWorking = false;
            this.isHungry = false;
            this.enemiesInRange = new List<Character>();
        }
    }
}