using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class Sequence : Node {
        /** Chiildren nodes that belong to this sequence */
        private List<Node> m_nodes = new List<Node>();
        private int currentIndex;
        private int previousIndex;
        private int iterations;
        /** Must provide an initial set of children nodes to work */
        public Sequence(List<Node> nodes) {
            m_nodes = nodes;
            currentIndex = 0;
            previousIndex = 0;
            iterations = 0;
        }

        /* If any child node returns a failure, the entire node fails. Whence all 
         * nodes return a success, the node reports a success. */
        // public override NodeStates Evaluate() {
        //     bool anyChildRunning = false;
        //     foreach (Node node in m_nodes) {
        //         Debug.Log("Sequence node state: " + node.nodeName + "-" + node.nodeState);
        //         switch (node.Evaluate()) {
        //             case NodeStates.FAILURE:
        //                 m_nodeState = NodeStates.FAILURE;
        //                 return m_nodeState;
        //             case NodeStates.SUCCESS:
        //                 continue;
        //             case NodeStates.RUNNING:
        //                 anyChildRunning = true;
        //                 continue;
        //             default:
        //                 m_nodeState = NodeStates.SUCCESS;
        //                 return m_nodeState;
        //         }
        //     }
        //     m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        //     return m_nodeState;
        // }

        public override NodeStates Evaluate() {
            bool _runningIteration = true;
            previousIndex = currentIndex;
            while (_runningIteration) {
                Node node = m_nodes[currentIndex];
                switch (node.Evaluate()) {
                    case NodeStates.FAILURE:
                        m_nodeState = NodeStates.FAILURE;
                        break;
                    case NodeStates.SUCCESS:
                        m_nodeState = currentIndex + 1 == m_nodes.Count ? NodeStates.SUCCESS : NodeStates.RUNNING;
                        currentIndex += 1;
                        break;
                    case NodeStates.RUNNING:
                        m_nodeState = NodeStates.RUNNING;
                        break;
                    default:
                        m_nodeState = NodeStates.SUCCESS;
                        break;
                }
                if (currentIndex == previousIndex) {
                    Debug.Log("Sequence (" + (currentIndex + 1) + "/" + m_nodes.Count + ")" + " " + node.nodeState + "-" + node.nodeName);
                    _runningIteration = false;
                } else {
                    previousIndex = currentIndex;
                    Debug.Log("Sequence (" + currentIndex + "/" + m_nodes.Count + ")" + " " + node.nodeState + "-" + node.nodeName);
                    if (currentIndex == m_nodes.Count) {
                        _runningIteration = false;
                    }
                }
                if (iterations >= 100) {
                    _runningIteration = false;
                    Debug.LogError("INFINITE LOOP DETECTED");
                }
                iterations += 1;
            }
            return m_nodeState;
        }
    }
}