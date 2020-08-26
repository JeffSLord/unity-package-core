using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class BehaviorTree : MonoBehaviour {
        public Node currentNode;
        public NodeStates currentNodeState;
        public bool running;

        public void SetNode(Node node) {
            if (currentNode != null) {
                StopAllCoroutines();
                running = false;
            }
            currentNode = node;
            if (currentNode != null) {
                running = true;
                StartCoroutine(Execute());
            }
        }

        private IEnumerator Execute() {
            Debug.Log("BT Tick");
            while (running) {
                currentNodeState = currentNode.Evaluate();
                switch (currentNodeState) {
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
                yield return new WaitForSeconds(0.25f);
            }
        }

        // private NodeStates 
    }
}