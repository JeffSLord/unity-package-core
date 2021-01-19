using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    // [RequireComponent(typeof(Character2D))]
    public class BehaviorTreeBehavior : MonoBehaviour {
        public BehaviorTree BT;

        void Awake(){
            BT = new BehaviorTree();
        }
        void Start() {
            BT.Context.SetCharacter(GetComponent<CharacterBehavior>().character.ID);
            if (BT.IsGlobalRunning) {
                RunBT();
            }
        }
        public void RunBT() {
            StopAllCoroutines();
            StartCoroutine(Execute());
        }
        private IEnumerator Execute() {
            while (BT.IsGlobalRunning) {
                BT.IsHighPriorityRunning = true;
                BT.IsLowPriorityRunning = true;
                BT.IsMnaualRunning = true;
                Debug.Log("BT Tick");
                // manual task
                if (BT.ManualPriorityNode != null) {
                    BT.IsHighPriorityRunning = false;
                    BT.IsLowPriorityRunning = false;
                    BT.ManualPriorityState = BT.ManualPriorityNode.Evaluate();
                    switch (BT.ManualPriorityState) {
                        case NodeStates.SUCCESS:
                            BT.IsHighPriorityRunning = true;
                            BT.IsLowPriorityRunning = true;
                            BT.SetManualNode(null);
                            break;
                        case NodeStates.FAILURE:
                            BT.IsHighPriorityRunning = true;
                            BT.IsLowPriorityRunning = true;
                            BT.SetManualNode(null);
                            break;
                        case NodeStates.RUNNING:
                            BT.IsHighPriorityRunning = false;
                            BT.IsLowPriorityRunning = false;
                            break;
                        default:
                            break;
                    }
                } else {
                    // high priority
                    if (BT.IsHighPriorityRunning && BT.HighPriorityNode != null) {
                        BT.HighPriorityState = BT.HighPriorityNode.Evaluate();
                        switch (BT.HighPriorityState) {
                            case NodeStates.SUCCESS:
                                BT.IsLowPriorityRunning = true;
                                break;
                            case NodeStates.FAILURE: // 
                                BT.IsLowPriorityRunning = true;
                                break;
                            case NodeStates.RUNNING:
                                BT.IsLowPriorityRunning = false;
                                break;
                            default:
                                break;
                        }
                    }
                    // low priority
                    if (BT.IsLowPriorityRunning && BT.LowPriorityNode != null) {
                        BT.LowPriorityState = BT.LowPriorityNode.Evaluate();
                        switch (BT.LowPriorityState) {
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
                yield return new WaitForSeconds(BT.TickRate);
            }
        }
    }
}