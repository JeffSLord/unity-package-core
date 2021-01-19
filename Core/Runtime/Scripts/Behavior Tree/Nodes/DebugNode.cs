using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class DebugNode : Node
    {
        public string message;
        public DebugNode(string message){
            this.message = message;
        }

        public override NodeStates Evaluate() {
            Debug.Log(message);
            return NodeStates.SUCCESS;
        }   
    }
}