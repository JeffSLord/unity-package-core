using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    // [RequireComponent(typeof(Character2D))]
    public class BehaviorTreeBehavior : MonoBehaviour {
        public BehaviorTree behaviorTree;
        void Start() {
            behaviorTree = GetComponent<CharacterBehavior>().character.BT;
            if (behaviorTree.IsGlobalRunning) {
                RunBT();
            }
        }
        public void RunBT() {
            StopAllCoroutines();
            StartCoroutine(Execute());
        }
        private IEnumerator Execute() {
            while (behaviorTree.IsGlobalRunning) {
                behaviorTree.IsHighPriorityRunning = true;
                behaviorTree.IsLowPriorityRunning = true;
                behaviorTree.IsMnaualRunning = true;
                Debug.Log("BT Tick");
                // manual task
                if (behaviorTree.ManualPriorityNode != null) {
                    behaviorTree.IsHighPriorityRunning = false;
                    behaviorTree.IsLowPriorityRunning = false;
                    behaviorTree.ManualPriorityState = behaviorTree.ManualPriorityNode.Evaluate();
                    switch (behaviorTree.ManualPriorityState) {
                        case NodeStates.SUCCESS:
                            behaviorTree.IsHighPriorityRunning = true;
                            behaviorTree.IsLowPriorityRunning = true;
                            behaviorTree.SetManualNode(null);
                            break;
                        case NodeStates.FAILURE:
                            behaviorTree.IsHighPriorityRunning = true;
                            behaviorTree.IsLowPriorityRunning = true;
                            behaviorTree.SetManualNode(null);
                            break;
                        case NodeStates.RUNNING:
                            behaviorTree.IsHighPriorityRunning = false;
                            behaviorTree.IsLowPriorityRunning = false;
                            break;
                        default:
                            break;
                    }
                } else {
                    // high priority
                    if (behaviorTree.IsHighPriorityRunning && behaviorTree.HighPriorityNode != null) {
                        behaviorTree.HighPriorityState = behaviorTree.HighPriorityNode.Evaluate();
                        switch (behaviorTree.HighPriorityState) {
                            case NodeStates.SUCCESS:
                                behaviorTree.IsLowPriorityRunning = true;
                                break;
                            case NodeStates.FAILURE: // 
                                behaviorTree.IsLowPriorityRunning = true;
                                break;
                            case NodeStates.RUNNING:
                                behaviorTree.IsLowPriorityRunning = false;
                                break;
                            default:
                                break;
                        }
                    }
                    // low priority
                    if (behaviorTree.IsLowPriorityRunning && behaviorTree.LowPriorityNode != null) {
                        behaviorTree.LowPriorityState = behaviorTree.LowPriorityNode.Evaluate();
                        switch (behaviorTree.LowPriorityState) {
                            case NodeStates.RUNNING:
                                break;
                            case NodeStates.SUCCESS:
                                break;
                            case NodeStates.FAILURE:
                                break;
                            default:
                                break;
                        }
                    }
                }
                yield return new WaitForSeconds(behaviorTree.TickRate);
            }
        }
    }
}