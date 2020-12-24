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
        public Node highPriorityNode;
        public NodeStates highPriorityState;
        public bool isHighPriorityRunning;
        // low priority (jobs, etc)
        public Node lowPriorityNode;
        public NodeStates lowPriorityState;
        public bool isLowPriorityRunning;

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
                this.isMnaualRunning = false;
            }
            manualPriorityNode = node;
            if (manualPriorityNode != null) {
                this.isMnaualRunning = true;
                StartCoroutine(Execute());
            }
        }
        private IEnumerator Execute() {
            while (true) {
                this.isHighPriorityRunning = true;
                this.isLowPriorityRunning = true;
                this.isMnaualRunning = true;
                Debug.Log("BT Tick");
                // manual task
                if (manualPriorityNode != null) {
                    this.isHighPriorityRunning = false;
                    this.isLowPriorityRunning = false;
                    this.manualPriorityState = manualPriorityNode.Evaluate();
                    switch (manualPriorityState) {
                        case NodeStates.SUCCESS:
                            this.isHighPriorityRunning = true;
                            this.isLowPriorityRunning = true;
                            SetManualNode(null);
                            break;
                        case NodeStates.FAILURE:
                            this.isHighPriorityRunning = true;
                            this.isLowPriorityRunning = true;
                            SetManualNode(null);
                            break;
                        case NodeStates.RUNNING:
                            this.isHighPriorityRunning = false;
                            this.isLowPriorityRunning = false;
                            break;
                        default:
                            break;
                    }
                } else {
                    // high priority
                    if (isHighPriorityRunning && highPriorityNode != null) {
                        this.highPriorityState = highPriorityNode.Evaluate();
                        switch (highPriorityState) {
                            case NodeStates.SUCCESS:
                                this.isLowPriorityRunning = true;
                                break;
                            case NodeStates.FAILURE: // 
                                this.isLowPriorityRunning = true;
                                break;
                            case NodeStates.RUNNING:
                                this.isLowPriorityRunning = false;
                                break;
                            default:
                                break;
                        }
                    }
                    // low priority
                    if (isLowPriorityRunning && lowPriorityNode != null) {
                        this.lowPriorityState = lowPriorityNode.Evaluate();
                        switch (lowPriorityState) {
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