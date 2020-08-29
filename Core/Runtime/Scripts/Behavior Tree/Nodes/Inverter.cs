using System.Collections;
using UnityEngine;

namespace Lord.Core {
    public class Inverter : Node {
        /* Child node to evaluate */
        private Node node;

        // public Node node {
        //     get { return node; }
        // }

        /* The constructor requires the child node that this inverter  decorator
         * wraps*/
        public Inverter(Node node) {
            this.node = node;
        }

        /* Reports a success if the child fails and
         * a failure if the child succeeeds. Running will report
         * as running */
        public override NodeStates Evaluate() {
            switch (node.Evaluate()) {
                case NodeStates.FAILURE:
                    nodeState = NodeStates.SUCCESS;
                    return nodeState;
                case NodeStates.SUCCESS:
                    nodeState = NodeStates.FAILURE;
                    return nodeState;
                case NodeStates.RUNNING:
                    nodeState = NodeStates.RUNNING;
                    return nodeState;
            }
            nodeState = NodeStates.SUCCESS;
            return nodeState;
        }
    }
}