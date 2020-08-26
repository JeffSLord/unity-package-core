using System.Collections;
using UnityEngine;

namespace Lord.Core {
    [System.Serializable]
    public abstract class Node {

        /* Delegate that returns the state of the node.*/
        public delegate NodeStates NodeReturn();

        /* The current state of the node */
        protected NodeStates m_nodeState;
        protected bool initialized;
        /* Node name here */
        public string nodeName;

        public NodeStates nodeState {
            get { return m_nodeState; }
        }

        /* The constructor for the node */
        public Node() { }

        /* Implementing classes use this method to evaluate the desired set of conditions */
        public abstract NodeStates Evaluate();

        public virtual void Interrupt() {

        }
    }
}