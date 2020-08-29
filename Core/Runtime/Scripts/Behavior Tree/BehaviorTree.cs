using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class BehaviorTree : MonoBehaviour {
        public float tickRate = 0.25f;
        public Node currentNode;
        public NodeStates currentNodeState;
        public bool isRunning;

        public Node highPriorityNode;
        public NodeStates highPriorityState;
        public Node lowPriorityNode;
        public NodeStates lowPriorityState;
        public Node manualPriorityNode;
        public bool isLowRunning;
        public bool isMnaualRunning;

        public void SetNode(Node node) {
            if (currentNode != null) {
                StopAllCoroutines();
                isRunning = false;
            }
            currentNode = node;
            if (currentNode != null) {
                isRunning = true;
                StartCoroutine(Execute());
            }
        }

        private IEnumerator Execute() {
            while (true) {
                Debug.Log("BT Tick");
                highPriorityState = highPriorityNode.Evaluate();
                switch (highPriorityState) {
                    case NodeStates.SUCCESS:
                        break;
                    case NodeStates.FAILURE:
                        isLowRunning = false;
                        break;
                    default:
                        break;
                }

                // while (isLowRunning) {
                if (isLowRunning) {
                    lowPriorityState = currentNode.Evaluate();
                    switch (lowPriorityState) {
                        case NodeStates.RUNNING:
                            break;
                        case NodeStates.SUCCESS:
                            SetNode(null);
                            break;
                        case NodeStates.FAILURE:
                            SetNode(null);
                            break;
                        default:
                            SetNode(null);
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