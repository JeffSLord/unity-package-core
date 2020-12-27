using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class BehaviorTree
    {
        public bool isGlobalRunning;
        // public Dictionary<string, object> context;
        // how often to execute node
        public Context context;
        public float tickRate = 0.25f;
        // manual priority (player override)
        public Node manualPriorityNode;
        public NodeStates manualPriorityState;
        public bool isMnaualRunning;
        // high priority (enemy detection, hunger)
        public Node highPriorityNode;
        public NodeStates highPriorityState;
        public bool isHighPriorityRunning;
        // low priority (jobs, etc)
        public Node lowPriorityNode;
        public NodeStates lowPriorityState;
        public bool isLowPriorityRunning;

        public BehaviorTree(){
            this.context = new Context();
        }

        public void SetManualNode(Node node) {
            if (manualPriorityNode != null) {
                // StopAllCoroutines();
                this.isMnaualRunning = false;
            }
            manualPriorityNode = node;
            if (manualPriorityNode != null) {
                this.isMnaualRunning = true;
                // StartCoroutine(Execute());
            }
        }
    }
}