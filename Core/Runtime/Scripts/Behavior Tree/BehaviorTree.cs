using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core{
    [System.Serializable]
    public class BehaviorTree
    {
        public bool IsGlobalRunning{get;set;}
        // public Dictionary<string, object> context;
        // how often to execute node
        public Context Context{get;set;}
        public float TickRate{get;set;}
        // manual priority (player override)
        public Node ManualPriorityNode{get;set;}
        public NodeStates ManualPriorityState{get;set;}
        public bool IsMnaualRunning{get;set;}
        // high priority (enemy detection, hunger)
        public Node HighPriorityNode{get;set;}
        public NodeStates HighPriorityState{get;set;}
        public bool IsHighPriorityRunning{get;set;}
        // low priority (jobs, etc)
        public Node LowPriorityNode{get;set;}
        public NodeStates LowPriorityState{get;set;}
        public bool IsLowPriorityRunning{get;set;}

        public BehaviorTree(){
            this.Context = new Context();
            this.IsGlobalRunning = true;
            this.TickRate = 0.25f;
        }

        public void SetManualNode(Node node) {
            if (ManualPriorityNode != null) {
                // StopAllCoroutines();
                this.IsMnaualRunning = false;
            }
            ManualPriorityNode = node;
            if (ManualPriorityNode != null) {
                this.IsMnaualRunning = true;
                // StartCoroutine(Execute());
            }
        }
    }
}