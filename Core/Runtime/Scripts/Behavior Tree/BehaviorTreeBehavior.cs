using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    // [RequireComponent(typeof(Character2D))]
    public class BehaviorTreeBehavior : MonoBehaviour {
        public BehaviorTree behaviorTree;

        void Awake() {

        }
        void Start() {
            behaviorTree = GetComponent<CharacterBehavior>().character.bt;
            if (behaviorTree.isGlobalRunning) {
                RunBT();
            }
        }
        public void RunBT() {
            StopAllCoroutines();
            StartCoroutine(Execute());
        }
        private IEnumerator Execute() {
            while (true) {
                behaviorTree.isHighPriorityRunning = true;
                behaviorTree.isLowPriorityRunning = true;
                behaviorTree.isMnaualRunning = true;
                Debug.Log("BT Tick");
                // manual task
                if (behaviorTree.manualPriorityNode != null) {
                    behaviorTree.isHighPriorityRunning = false;
                    behaviorTree.isLowPriorityRunning = false;
                    behaviorTree.manualPriorityState = behaviorTree.manualPriorityNode.Evaluate();
                    switch (behaviorTree.manualPriorityState) {
                        case NodeStates.SUCCESS:
                            behaviorTree.isHighPriorityRunning = true;
                            behaviorTree.isLowPriorityRunning = true;
                            behaviorTree.SetManualNode(null);
                            break;
                        case NodeStates.FAILURE:
                            behaviorTree.isHighPriorityRunning = true;
                            behaviorTree.isLowPriorityRunning = true;
                            behaviorTree.SetManualNode(null);
                            break;
                        case NodeStates.RUNNING:
                            behaviorTree.isHighPriorityRunning = false;
                            behaviorTree.isLowPriorityRunning = false;
                            break;
                        default:
                            break;
                    }
                } else {
                    // high priority
                    if (behaviorTree.isHighPriorityRunning && behaviorTree.highPriorityNode != null) {
                        behaviorTree.highPriorityState = behaviorTree.highPriorityNode.Evaluate();
                        switch (behaviorTree.highPriorityState) {
                            case NodeStates.SUCCESS:
                                behaviorTree.isLowPriorityRunning = true;
                                break;
                            case NodeStates.FAILURE: // 
                                behaviorTree.isLowPriorityRunning = true;
                                break;
                            case NodeStates.RUNNING:
                                behaviorTree.isLowPriorityRunning = false;
                                break;
                            default:
                                break;
                        }
                    }
                    // low priority
                    if (behaviorTree.isLowPriorityRunning && behaviorTree.lowPriorityNode != null) {
                        behaviorTree.lowPriorityState = behaviorTree.lowPriorityNode.Evaluate();
                        switch (behaviorTree.lowPriorityState) {
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
                yield return new WaitForSeconds(behaviorTree.tickRate);
            }
        }
    }
}