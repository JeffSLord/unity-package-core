using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class MoveBT {
        // public Vector3 targetPosition;
        // public NavMeshAgent navMeshAgent;
        // public Character character;
        // public float stoppingDistance;

        // public MoveBT(Character character, Vector3 position = new Vector3(), float stoppingDistance = 1.0f) {
        //     this.navMeshAgent = character.navMeshAgent;
        //     this.character = character;
        //     this.targetPosition = position;
        //     this.stoppingDistance = stoppingDistance;
        //     character.animator.SetBool("IsMoving", true);
        // }

        private static NodeStates SetDestination(BehaviorContext context) {
            context.navMeshAgent.destination = context.targetPosition;
            context.navMeshAgent.stoppingDistance = context.stoppingDistance;
            return NodeStates.SUCCESS;
        }
        public static Node SetDestinationNode() {
            return new TaskContextNode(SetDestination, "Set Destination");
        }
        private static NodeStates CheckDestinationReached(BehaviorContext context) {
            if (Vector3.Distance(navMeshAgent.transform.position, targetPosition) <= stoppingDistance + 0.05) {
                character.animator.SetBool("IsMoving", false);
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.RUNNING;
            }
        }
        public static Node CheckDestinationReachedNode() {
            return new TaskContextNode(CheckDestinationReached, "Check Destination Reached");
        }

        public static Node MoveSequence() {
            return new Sequence(new List<Node> {
                SetDestinationNode(),
                CheckDestinationReachedNode(),
                TurnNode()
            });
        }
        // private NodeStates Turn() {
        //     this.character.transform.LookAt(targetPosition);
        //     return NodeStates.SUCCESS;
        // }
        private static NodeStates Turn(BehaviorContext context) {
            Vector3 _targetDir = targetPosition - character.transform.position;
            float _angle = Mathf.Atan2(_targetDir.y, _targetDir.x) * Mathf.Rad2Deg;
            character.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            return NodeStates.SUCCESS;
        }
        public static Node TurnNode() {
            return new TaskContextNode(Turn, "Turn");
        }
    }
}