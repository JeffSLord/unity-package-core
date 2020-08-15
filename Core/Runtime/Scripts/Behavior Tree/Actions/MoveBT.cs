using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public class MoveBT {
        public Vector3 targetPosition;
        public NavMeshAgent navMeshAgent;
        public float stoppingDistance;

        public MoveBT(NavMeshAgent navMeshAgent, Vector3 position = new Vector3(), float stoppingDistance = 1.0f) {
            this.navMeshAgent = navMeshAgent;
            this.targetPosition = position;
            this.stoppingDistance = stoppingDistance;
        }
        public NodeStates MoveToPoint() {
            navMeshAgent.destination = targetPosition;
            // if(navMeshAgent.)
            if (Vector3.Distance(navMeshAgent.transform.position, targetPosition) < stoppingDistance) {
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.RUNNING;
            }
        }
        public ActionNode MoveToPointNode() {
            return new ActionNode(MoveToPoint);
        }
    }
}