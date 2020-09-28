using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [System.Serializable]
    public class BehaviorContext {
        public Character character;

        // character detection
        public bool isEnemyVisible;
        public bool isAlertedOfEnemy;
        public List<Character> enemiesInRange;
        public List<Character> enemiesDetected;
        public List<Character> friendliesInRange;

        // work
        public bool isWorking;
        public ResourceSource resourceSource;

        // hunger
        public bool isHungry;

        // movement
        public Vector3 targetPosition;
        public NavMeshAgent navMeshAgent;
        public float stoppingDistance;

        // item
        public Item item;
        public List<Item> nearbyItems;
        public float itemSearchRadius;

        public BehaviorContext() {
            this.character = null;
            // character
            this.isEnemyVisible = false;
            this.isAlertedOfEnemy = false;
            this.enemiesInRange = new List<Character>();
            this.enemiesDetected = new List<Character>();
            this.friendliesInRange = new List<Character>();
            // work
            this.isWorking = false;
            // hunger
            this.isHungry = false;
            // movement 
            this.targetPosition = Vector3.zero;
            this.navMeshAgent = null;
            this.stoppingDistance = 1.0f;

        }
        public BehaviorContext(Character character) : this() {
            this.character = character;
            this.navMeshAgent = this.character.navMeshAgent;
        }
    }
}