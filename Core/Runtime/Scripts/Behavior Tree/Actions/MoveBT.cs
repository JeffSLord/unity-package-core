using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class MoveBT {
        private static NodeStates SetDestination(Dictionary<string, object> context) {
            Character _character;
            float _stoppingDistance;
            Vector3 _targetPosition;
            if (context.TryGetValue<Character>("character", out _character)) {
                if (context.TryGetValue<Vector3>("targetPosition", out _targetPosition)) {
                    if (context.TryGetValue<float>("stoppingDistance", out _stoppingDistance)) {
                        _character.navMeshAgent.destination = _targetPosition;
                        _character.navMeshAgent.stoppingDistance = _stoppingDistance;
                        // _character.transform.rotation = Quaternion.LookRotation(_character.navMeshAgent.desiredVelocity, Vector3.forward);
                        return NodeStates.SUCCESS;
                    }
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node SetDestinationNode(Dictionary<string, object> context) {
            return new TaskContextNode(SetDestination, context, "Set Destination");
        }
        private static NodeStates CheckDestinationReached(Dictionary<string, object> context) {
            Character _character;
            float _stoppingDistance;
            Vector3 _targetPosition;
            if (context.TryGetValue<Character>("character", out _character)) {
                if (context.TryGetValue<Vector3>("targetPosition", out _targetPosition)) {
                    if (context.TryGetValue<float>("stoppingDistance", out _stoppingDistance)) {
                        if (Vector3.Distance(_character.navMeshAgent.transform.position, _targetPosition) <= _stoppingDistance + 0.05) {
                            _character.animator.SetBool("IsMoving", false);
                            return NodeStates.SUCCESS;
                        } else {
                            return NodeStates.RUNNING;
                        }
                    }
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node CheckDestinationReachedNode(Dictionary<string, object> context) {
            return new TaskContextNode(CheckDestinationReached, context, "Check Destination Reached");
        }
        private static NodeStates SetTransformPosition(Dictionary<string, object> context) {
            Character _character;
            Transform _transform;
            if (context.TryGetValue<Character>("character", out _character)) {
                if (context.TryGetValue<Transform>("targetTransform", out _transform)) {
                    _character.bt.SetContext<Vector3>("targetPosition", _transform.position);
                    return NodeStates.SUCCESS;
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node SetTransformPositionNode(Dictionary<string, object> context) {
            return new TaskContextNode(SetTransformPosition, context, "Set transform position");
        }
        private static NodeStates Turn(Dictionary<string, object> context) {
            Character _character;
            Vector3 _targetPosition;
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
        public static Node MoveToPoint(Dictionary<string, object> context) {
            return new SequenceSeq(new List<Node> {
                SetDestinationNode(context),
                CheckDestinationReachedNode(context),
                TurnNode(context)
            });
        }
        public static Node MoveToPointUpdate(Dictionary<string, object> context) {
            return new SequencePar(new List<Node> {
                SetDestinationNode(context),
                CheckDestinationReachedNode(context),
                TurnNode(context)
            });
        }

        public static Node FollowTransform(Dictionary<string, object> context) {
            return new SequencePar(new List<Node> {
                SetTransformPositionNode(context),
                MoveToPointUpdate(context)
            });
        }

    }
}