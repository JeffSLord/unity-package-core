using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    // [RequireComponent(typeof(Character2D))]
    public class BehaviorTree : MonoBehaviour {
        // context
        // public BehaviorContext context;
        public Dictionary<string, object> contextDict;
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

        void Start() {
            // this.context = new BehaviorContext(this.GetComponent<Character>());
            this.contextDict = new Dictionary<string, object>();
            // this.SetContext("character", this.GetComponent<Character>());
            this.SetContext<Character>("character", this.GetComponent<Character>());
            this.isUrgentRunning = true;
            this.isMinorRunning = true;
            // this.urgentNode = 
            StartCoroutine(Execute());
        }

        public T SetContext<T>(string name, T obj) {
            contextDict[name] = obj;
            // T _val;
            // if (contextDict.TryGetValue<T>(name, out _val)) {
            //     contextDict[name] = obj;
            // }
            return (T) contextDict[name];
        }
        public List<T> SetContextList<T>(string name, List<T> obj) {
            List<T> _val;
            if (contextDict.TryGetValue<List<T>>(name, out _val)) {
                contextDict[name] = ((List<T>) contextDict[name]).Union(obj).ToList();
            } else {
                SetContext<List<T>>(name, obj);
            }
            return (List<T>) contextDict[name];
        }
        public void RemoveContext<T>(string name) {
            T _val;
            if (contextDict.TryGetValue<T>(name, out _val)) {
                contextDict.Remove(name);
            }
        }
        // Return true if fully removed, false if list still has elements
        public bool RemoveContextList<T>(string name, List<T> obj) {
            T _val;
            if (contextDict.TryGetValue<T>(name, out _val)) {
                List<T> _l1 = ((List<T>) contextDict[name]).Except(obj).ToList();
                if (_l1.Count > 0) {
                    contextDict[name] = _l1;
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

                yield return new WaitForSeconds(tickRate);
                // }
            }
        }

        // private NodeStates 
    }
}