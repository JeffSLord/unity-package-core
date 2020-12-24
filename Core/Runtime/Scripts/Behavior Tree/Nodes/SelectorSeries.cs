using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class SelectorSeries : Node {
        /** The child nodes for this selector */
        protected List<Node> nodeList = new List<Node>();

        /** The constructor requires a list of child nodes to be 
         * passed in*/
        public SelectorSeries(List<Node> nodes) {
            this.nodeList = nodes;
        }

        /* If any of the children reports a success, the selector will
         * immediately report a success upwards. If all children fail,
         * it will report a failure instead.*/
        public override NodeStates Evaluate() {
            foreach (Node node in nodeList) {
                switch (node.Evaluate()) {
                    case NodeStates.FAILURE:
                        continue;
                    case NodeStates.SUCCESS:
                        nodeState = NodeStates.SUCCESS;
                        return nodeState;
                    case NodeStates.RUNNING:
                        nodeState = NodeStates.RUNNING;
                        return nodeState;
                    default:
                        continue;
                }
            }
            nodeState = NodeStates.FAILURE;
            return nodeState;
        }
    }
}