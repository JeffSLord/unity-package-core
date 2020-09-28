using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class SequenceSeq : Node {
        /** Chiildren nodes that belong to this sequence */
        private List<Node> nodeList = new List<Node>();
        private int currentIndex;
        private int previousIndex;
        private int iterations;
        /** Must provide an initial set of children nodes to work */
        public SequenceSeq(List<Node> nodes) {
            this.nodeList = nodes;
            currentIndex = 0;
            previousIndex = 0;
            iterations = 0;
        }

        public override NodeStates Evaluate() {
            bool _runningIteration = true;
            previousIndex = currentIndex;
            while (_runningIteration) {
                Node _node = nodeList[currentIndex];
                switch (_node.Evaluate()) {
                    case NodeStates.FAILURE:
                        this.nodeState = NodeStates.FAILURE;
                        break;
                    case NodeStates.SUCCESS:
                        this.nodeState = currentIndex + 1 == nodeList.Count ? NodeStates.SUCCESS : NodeStates.RUNNING;
                        currentIndex += 1;
                        break;
                    case NodeStates.RUNNING:
                        this.nodeState = NodeStates.RUNNING;
                        break;
                    default:
                        this.nodeState = NodeStates.SUCCESS;
                        break;
                }
                if (currentIndex == previousIndex) { // sequence has not progressed
                    Debug.Log("Sequence (" + (currentIndex + 1) + "/" + nodeList.Count + ")" + " " + _node.nodeState + "-" + _node.nodeName);
                    _runningIteration = false;
                } else { // sequence has progressed
                    previousIndex = currentIndex;
                    Debug.Log("Sequence (" + currentIndex + "/" + nodeList.Count + ")" + " " + _node.nodeState + "-" + _node.nodeName);
                    if (currentIndex == nodeList.Count) { // no more states in sequence
                        _runningIteration = false;
                        currentIndex = 0; // reset in case of repeater
                    }
                }
                if (iterations >= 100) {
                    _runningIteration = false;
                    Debug.LogError("INFINITE LOOP DETECTED");
                }
                iterations += 1;
            }
            return nodeState;
        }
    }
}