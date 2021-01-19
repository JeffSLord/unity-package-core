using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class MoveBT {
        private static NodeStates SetDestinationPosition(Context context) {
            Character _character = Character.GetCharacter(context.CharacterId);
            Vector3 _targetPosition = context.Character.MovePosition;
            _character.NavMeshAgent.destination = _targetPosition;
            return NodeStates.SUCCESS;
        }

        private static NodeStates SetMoveSpeed(Context context){
            Character _character = Character.GetCharacter(context.CharacterId);
            float _moveSpeed = context.Character.MoveSpeed;
            _character.NavMeshAgent.speed = _moveSpeed;
            return NodeStates.SUCCESS; 
        }

        private static NodeStates SetStoppingDistance(Context context){
            Character _character = Character.GetCharacter(context.CharacterId);
            float _stoppingDistance = context.Character.StoppingDistance;
            _character.NavMeshAgent.stoppingDistance = _stoppingDistance;
            return NodeStates.SUCCESS;
        }
        private static NodeStates CheckDestinationReached(Context context) {
            Character _character = context.Character;
            float _stoppingDistance = context.Character.StoppingDistance;
            Vector3 _targetPosition = context.Character.MovePosition;
            Debug.Log("DISTANCE: " + Vector3.Distance(_character.Behavior.transform.position, _targetPosition));
            if(Vector3.Distance(_character.Behavior.transform.position, _targetPosition) <= _stoppingDistance + 0.05) {
                // _character.Animator.SetBool("IsMoving", false);
                return NodeStates.SUCCESS;
            } else{
                return NodeStates.RUNNING;
            }
        }

        private static NodeStates SetDestinationTransform(Context context) {
            Character _character = context.Character;
            Transform _transform = context.Character.MoveTransform;
            context.Character.MovePosition = _transform.position;
            return NodeStates.SUCCESS;
        }
        private static NodeStates Turn(Context context) {
            Character _character = context.Character;
            Vector3 _targetPosition = context.Character.MovePosition;
            Vector3 _targetDir = _targetPosition - _character.Behavior.transform.position;
            float _angle = Mathf.Atan2(_targetDir.y, _targetDir.x) * Mathf.Rad2Deg;
            _character.Behavior.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            return NodeStates.SUCCESS;
        }
        private static NodeStates SelectRandomWaypoint(Context context) {
            List<Waypoint> _waypoints;
            if (!context.data.TryGetValue<List<Waypoint>>("waypoints", out _waypoints)) {
                return NodeStates.FAILURE;
            }
            // Debug.Log("waypoint count " + _waypoints.Count);
            System.Random _rand = new System.Random();
            int _index = _rand.Next(_waypoints.Count);
            // Debug.Log(_index);
            context.SetContext<Waypoint>("waypoint", _waypoints[_index]);
            context.SetContext<Vector3>("targetPosition", _waypoints[_index].transform.position);
            return NodeStates.SUCCESS;
        }
        public static Node MoveToPoint(Context context) {
            return new SequenceSeries(
                new List<Node> {
                    new TaskContextNode(SetDestinationPosition, context, "set destination"),
                    new TaskContextNode(CheckDestinationReached, context, "check destination reached"),
                    new TaskContextNode(Turn, context, "Turn")
                }
            );
        }
        public static Node MoveToPointParallel(Context context) {
            return new SequenceParallel(
                new List<Node> {
                    new TaskContextNode(SetDestinationPosition, context, "Set destination")
                    ,new TaskContextNode(CheckDestinationReached, context, "Check destination reached")
                    // ,new TaskContextNode(Turn, context, "Turn")
                }
            );
        }

        public static Node FollowTransform(Context context) {
            return new SequenceParallel(
                new List<Node> {
                    new TaskContextNode(SetDestinationTransform, context, "Set target"),
                    MoveToPointParallel(context)
                }
            );
        }
        public static Node MoveToRandomWaypoint(Context context) {
            return new SequenceSeries(
                new List<Node> {
                    new TaskContextNode(SelectRandomWaypoint, context, "Select random waypoint"),
                    MoveToPointParallel(context)
                }
            );
        }
    }
}