using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class MoveBT {
        private static NodeStates SetDestination(Context context) {
            Character _character;
            float _stoppingDistance;
            Vector3 _targetPosition;
            float _moveSpeed;
            if(!context.data.TryGetValue<Character>("character", out _character)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<Vector3>("targetPosition", out _targetPosition)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<float>("stoppingDistance", out _stoppingDistance)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<float>("moveSpeed", out _moveSpeed)){
                return NodeStates.FAILURE;
            }
            _character.navMeshAgent.destination = _targetPosition;
            _character.navMeshAgent.stoppingDistance = _stoppingDistance;
            _character.navMeshAgent.speed = _moveSpeed;
            return NodeStates.SUCCESS;
        }
        public static Node SetDestinationNode(Context context) {
            return new TaskContextNode(SetDestination, context, "Set Destination");
        }
        private static NodeStates CheckDestinationReached(Context context) {
            Character _character;
            float _stoppingDistance;
            Vector3 _targetPosition;
            if(!context.data.TryGetValue<Character>("character", out _character)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<Vector3>("targetPosition", out _targetPosition)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<float>("stoppingDistance", out _stoppingDistance)){
                return NodeStates.FAILURE;
            }
            if (Vector3.Distance(_character.navMeshAgent.transform.position, _targetPosition) <= _stoppingDistance + 0.05) {
                _character.animator.SetBool("IsMoving", false);
                return NodeStates.SUCCESS;
            } else {
                return NodeStates.RUNNING;
            }
        }
        public static Node CheckDestinationReachedNode(Context context) {
            return new TaskContextNode(CheckDestinationReached, context, "Check Destination Reached");
        }
        private static NodeStates SetTargetTransform(Context context) {
            Character _character;
            Transform _transform;
            if(!context.data.TryGetValue<Character>("character", out _character)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<Transform>("targetTransform", out _transform)){
                return NodeStates.FAILURE;
            }
            context.SetContext<Vector3>("targetPosition", _transform.position);
            return NodeStates.SUCCESS;
        }
        public static Node SetTargetTransformNode(Context context) {
            return new TaskContextNode(SetTargetTransform, context, "Set transform position");
        }
        private static NodeStates Turn(Context context) {
            Character _character;
            Vector3 _targetPosition;
            if(!context.data.TryGetValue<Vector3>("targetPosition", out _targetPosition)){
                return NodeStates.FAILURE;
            }
            if(!context.data.TryGetValue<Character>("character", out _character)){
                return NodeStates.FAILURE;
            }
            Vector3 _targetDir = _targetPosition - _character.transform.position;
            float _angle = Mathf.Atan2(_targetDir.y, _targetDir.x) * Mathf.Rad2Deg;
            _character.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            return NodeStates.SUCCESS;
        }
        public static Node TurnNode(Context context) {
            return new TaskContextNode(Turn, context, "Turn");
        }
        private static NodeStates SelectRandomWaypoint(Context context) {
            List<Waypoint> _waypoints;
            if (context.data.TryGetValue<List<Waypoint>>("waypoints", out _waypoints)) {
                Debug.Log("waypoint count " + _waypoints.Count);
                System.Random _rand = new System.Random();
                int _index = _rand.Next(_waypoints.Count);
                Debug.Log(_index);
                context.SetContext<Waypoint>("waypoint", _waypoints[_index]);
                context.SetContext<Vector3>("targetPosition", _waypoints[_index].transform.position);
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
        public static Node SelectRandomWaypointNode(Context context) {
            return new TaskContextNode(SelectRandomWaypoint, context, "Selecting random waypoint");
        }
        public static Node MoveToPoint(Context context) {
            return new SequenceSeries(new List<Node> {
                SetDestinationNode(context),
                CheckDestinationReachedNode(context),
                TurnNode(context)
            });
        }
        public static Node MoveToPointParallel(Context context) {
            return new SequenceParallel(new List<Node> {
                SetDestinationNode(context),
                CheckDestinationReachedNode(context),
                TurnNode(context)
            });
        }

        public static Node FollowTransform(Context context) {
            return new SequenceParallel(new List<Node> {
                SetTargetTransformNode(context),
                MoveToPointParallel(context)
            });
        }
        public static Node MoveToRandomWaypoint(Context context) {
            return new SequenceSeries(new List<Node> {
                SelectRandomWaypointNode(context),
                MoveToPointParallel(context)
            });
        }
    }
}