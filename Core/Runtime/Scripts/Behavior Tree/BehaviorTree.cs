using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    // [RequireComponent(typeof(Character2D))]
    public class BehaviorTree : MonoBehaviour {
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
        public Node urgentNode;
        public NodeStates urgentState;
        public bool isUrgentRunning;
        // low priority (jobs, etc)
        public Node minorNode;
        public NodeStates minorState;
        public bool isMinorRunning;

        void Awake() {
            // this.context = new Dictionary<string, object>();
            this.context = new Context();
        }
        void Start() {
            if (isGlobalRunning) {
                RunBT();
            }
        }
        public void RunBT() {
            StopAllCoroutines();
            StartCoroutine(Execute());
        }
        public void SetManualNode(Node node) {
            if (manualPriorityNode != null) {
                StopAllCoroutines();
                isMnaualRunning = false;
            }
            manualPriorityNode = node;
            if (manualPriorityNode != null) {
                isMnaualRunning = true;
                StartCoroutine(Execute());
            }
        }
        private IEnumerator Execute() {
            while (true) {
                isUrgentRunning = true;
                isMinorRunning = true;
                isMnaualRunning = true;
                Debug.Log("BT Tick");
                // manual task
                if (manualPriorityNode != null) {
                    isUrgentRunning = false;
                    isMinorRunning = false;
                    manualPriorityState = manualPriorityNode.Evaluate();
                    switch (manualPriorityState) {
                        case NodeStates.SUCCESS:
                            isUrgentRunning = true;
                            isMinorRunning = true;
                            SetManualNode(null);
                            break;
                        case NodeStates.FAILURE:
                            isUrgentRunning = true;
                            isMinorRunning = true;
                            SetManualNode(null);
                            break;
                        case NodeStates.RUNNING:
                            isUrgentRunning = false;
                            isMinorRunning = false;
                            break;
                        default:
                            break;
                    }
                } else {
                    // high priority
                    if (isUrgentRunning && urgentNode != null) {
                        urgentState = urgentNode.Evaluate();
                        switch (urgentState) {
                            case NodeStates.SUCCESS:
                                isMinorRunning = true;
                                break;
                            case NodeStates.FAILURE: // 
                                isMinorRunning = true;
                                break;
                            case NodeStates.RUNNING:
                                isMinorRunning = false;
                                break;
                            default:
                                break;
                        }
                    }
                    // low priority
                    if (isMinorRunning && minorNode != null) {
                        minorState = minorNode.Evaluate();
                        switch (minorState) {
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
                yield return new WaitForSeconds(tickRate);
            }
        }
    }
}