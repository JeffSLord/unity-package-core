using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core{
    [System.Serializable]
    public class BehaviorTree
    {
        // public Context Context{get;set;}
        public Context Context;
        // public bool IsGlobalRunning{get;set;}
        public bool IsGlobalRunning;
        // how often to execute node
        // public float TickRate{get;set;}
        public float TickRate;
        // manual priority (player override)
        // public Node ManualPriorityNode{get;set;}
        public Node ManualPriorityNode;
        // public NodeStates ManualPriorityState{get;set;}
        public NodeStates ManualPriorityState;
        // public bool IsMnaualRunning{get;set;}
        public bool IsMnaualRunning;
        // high priority (enemy detection, hunger)
        // public Node HighPriorityNode{get;set;}
        public Node HighPriorityNode;
        // public NodeStates HighPriorityState{get;set;}
        public NodeStates HighPriorityState;
        // public bool IsHighPriorityRunning{get;set;}
        public bool IsHighPriorityRunning;
        // low priority (jobs, etc)
        // public Node LowPriorityNode{get;set;}
        public Node LowPriorityNode;
        // public NodeStates LowPriorityState{get;set;}
        public NodeStates LowPriorityState;
        // public bool IsLowPriorityRunning{get;set;}
        public bool IsLowPriorityRunning;

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