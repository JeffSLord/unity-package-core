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

        private static NodeStates SetDestination(Dictionary<string, object> context) {
            Vector3 _targetPosition;
            if (context.TryGetValue<Vector3>("targetPosition", out _targetPosition)) {
                NavMeshAgent _navMeshAgent;
                if (context.TryGetValue<NavMeshAgent>("navMeshAgent", out _navMeshAgent)) {
                    _navMeshAgent.destination = _targetPosition;
                    float _stoppingDistance;
                    if (context.TryGetValue<float>("stoppingDistance", out _stoppingDistance)) {
                        _navMeshAgent.stoppingDistance = _stoppingDistance;
                    }
                }
                // context.navMeshAgent.destination = context.targetPosition;
                // context.navMeshAgent.stoppingDistance = context.stoppingDistance;
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.FAILURE;
            }

        }
        public static Node SetDestinationNode(Dictionary<string, object> context) {
            return new TaskContextNode(SetDestination, context, "Set Destination");
        }
        private static NodeStates CheckDestinationReached(Dictionary<string, object> context) {
            NavMeshAgent _navMeshAgent;
            float _stoppingDistance;
            Vector3 _targetPosition;
            Character _character;
            if (context.TryGetValue<NavMeshAgent>("navMeshAgent", out _navMeshAgent)) {
                if (context.TryGetValue<Vector3>("targetPosition", out _targetPosition)) {
                    if (context.TryGetValue<float>("stoppingDistance", out _stoppingDistance)) {
                        if (Vector3.Distance(_navMeshAgent.transform.position, _targetPosition) <= _stoppingDistance + 0.05) {
                            if (context.TryGetValue<Character>("character", out _character)) {
                                _character.animator.SetBool("IsMoving", false);
                                return NodeStates.SUCCESS;
                            }
                        }
                    }
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node CheckDestinationReachedNode(Dictionary<string, object> context) {
            return new TaskContextNode(CheckDestinationReached, context, "Check Destination Reached");
        }

        public static Node MoveSequence(Dictionary<string, object> context) {
            return new Sequence(new List<Node> {
                SetDestinationNode(context),
                CheckDestinationReachedNode(context),
                TurnNode(context)
            });
        }
        // private NodeStates Turn() {
        //     this.character.transform.LookAt(targetPosition);
        //     return NodeStates.SUCCESS;
        // }
        private static NodeStates Turn(Dictionary<string, object> context) {
            Vector3 _targetPosition;
            Character _character;
            if (context.TryGetValue<Vector3>("targetPosition", out _targetPosition)) {
                if (context.TryGetValue<Character>("character", out _character)) {
                    Vector3 _targetDir = _targetPosition - _character.transform.position;
                    float _angle = Mathf.Atan2(_targetDir.y, _targetDir.x) * Mathf.Rad2Deg;
                    _character.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
                    return NodeStates.SUCCESS;
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node TurnNode(Dictionary<string, object> context) {
            return new TaskContextNode(Turn, context, "Turn");
        }
    }
}