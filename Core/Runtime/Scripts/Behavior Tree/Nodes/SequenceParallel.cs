using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class SequenceParallel : Node {
        private List<Node> nodeList = new List<Node>();
        public SequenceParallel(List<Node> nodes) {
            this.nodeList = nodes;
        }

        /* If any child node returns a failure, the entire node fails. Whence all 
         * nodes return a success, the node reports a success. */
        public override NodeStates Evaluate() {
            bool anyChildRunning = false;
            foreach (Node node in nodeList) {
                Debug.Log("[BT SEQUENCE PARALLEL] " + node.nodeName + "-" + node.nodeState);
                switch (node.Evaluate()) {
                    case NodeStates.FAILURE:
                        this.nodeState = NodeStates.FAILURE;
                        return this.nodeState;
                    case NodeStates.SUCCESS:
                        continue;
                    case NodeStates.RUNNING:
                        anyChildRunning = true;
                        continue;
                    default:
                        this.nodeState = NodeStates.SUCCESS;
                        return this.nodeState;
                }
            }
            this.nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
            return nodeState;
        }
    }
}