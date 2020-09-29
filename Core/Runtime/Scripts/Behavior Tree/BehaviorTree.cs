using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    // [RequireComponent(typeof(Character2D))]
    public class BehaviorTree : MonoBehaviour {
        public bool isGlobalRunning;
        public Dictionary<string, object> context;
        // how often to execute node
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
            this.context = new Dictionary<string, object>();
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

        public T SetContext<T>(string name, T obj) {
            context[name] = obj;
            return (T) context[name];
        }
        public List<T> SetContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (context.TryGetValue<List<T>>(name, out _val)) {
                context[name] = ((List<T>) context[name]).Union(obj).ToList();
            } else {
                SetContext<List<T>>(name, obj);
            }
            return (List<T>) context[name];
        }
        public void RemoveContext<T>(string name) {
            T _val;
            if (context.TryGetValue<T>(name, out _val)) {
                context.Remove(name);
            }
        }
        // Return true if fully removed, false if list still has elements
        public bool RemoveContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (context.TryGetValue<List<T>>(name, out _val)) {
                List<T> _l1 = ((List<T>) context[name]).Except(obj).ToList();
                if (_l1.Count > 0) {
                    context[name] = _l1;
                    return false;
                } else {
                    RemoveContext<List<T>>(name);
                    return true;
                }
            } else {
                return true;
            }
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
                }
                // high priority
                else if (isUrgentRunning && urgentNode != null) {
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
                else if (isMinorRunning && minorNode != null) {
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
                yield return new WaitForSeconds(tickRate);
            }
        }

    }
}