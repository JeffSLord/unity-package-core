using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class Repeater : Node {
        private Node node;
        public Repeater(Node node) {
            this.node = node;
        }
        public override NodeStates Evaluate() {
            switch (node.Evaluate()) {
                default : return NodeStates.RUNNING;
                // break;
            }
        }
    }
}