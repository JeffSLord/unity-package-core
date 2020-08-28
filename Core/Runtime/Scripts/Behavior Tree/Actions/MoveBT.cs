using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public class MoveBT {
        public Vector3 targetPosition;
        public NavMeshAgent navMeshAgent;
        public Character character;
        public float stoppingDistance;

        public MoveBT(Character character, Vector3 position = new Vector3(), float stoppingDistance = 1.0f) {
            this.navMeshAgent = character.navMeshAgent;
            this.character = character;
            this.targetPosition = position;
            this.stoppingDistance = stoppingDistance;
            character.animator.SetBool("IsMoving", true);
        }

        private NodeStates SetDestination() {
            navMeshAgent.destination = targetPosition;
            navMeshAgent.stoppingDistance = stoppingDistance;
            return NodeStates.SUCCESS;
        }
        public TaskNode SetDestinationNode() {
            return new TaskNode(SetDestination, "Set Destination");
        }
        private NodeStates CheckDestinationReached() {
            if (Vector3.Distance(navMeshAgent.transform.position, targetPosition) <= stoppingDistance + 0.05) {
                character.animator.SetBool("IsMoving", false);
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.RUNNING;
            }
        }
        public TaskNode CheckDestinationReachedNode() {
            return new TaskNode(CheckDestinationReached, "Check Destination Reached");
        }

        public Sequence MoveSequence() {
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
        private NodeStates Turn() {
            // Vector3 vectorToTarget = targetPosition - character.transform.position;
            // Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
            // Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
            // character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, targetRotation, 360);
            // character.transform.Translate(Vector3.right, Space.Self);

            // this.character.transform.LookAt(targetPosition, Vector3.forward);
            Vector3 _target = new Vector3(targetPosition.x, targetPosition.y, 0);
            character.transform.LookAt(_target, Vector3.up);
            // character.transform.eulerAngles = new Vector3(0, 0, character.transform.eulerAngles.z);
            return NodeStates.SUCCESS;
        }
        public TaskNode TurnNode() {
            return new TaskNode(Turn, "Turn");
        }
    }
}