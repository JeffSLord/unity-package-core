using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public static class EnemyBT {
        private static NodeStates IsEnemyVisible(Dictionary<string, object> context) {
            // just check if it is within a specific range for now
            // add sight later
            bool _isEnemyVisible;
            context.TryGetValue<bool>("isEnemyVisible", _isEnemyVisible);
            switch (_isEnemyVisible) {
                case (true):
                    Debug.Log("Enememy is visible");
                    return NodeStates.SUCCESS;
                case (false):
                    return NodeStates.FAILURE;
                default:
                    return NodeStates.FAILURE;
            }
        }
        public static Node IsEnemyVisibleNode() {
            return new TaskContextNode(IsEnemyVisible, "Check Enemies Visible");
        }
        private static NodeStates IsAlertedOfEnemy(BehaviorContext context) {
            return NodeStates.FAILURE;
        }
        public static Node IsAlertedOfEnemyNode() {
            return new TaskContextNode(IsAlertedOfEnemy, "Check Enemies Alert");
        }
        public static Node IsEnemyDetectedNode() {
            return new ParallelSequence(new List<Node> {
                EnemyBT.IsEnemyVisibleNode(),
                EnemyBT.IsAlertedOfEnemyNode()
            });
        }
        public static Node EnemyDetectionNode() {
            // MoveBT _moveBT = new MoveBT(context.character, new Vector3(0, 0, 0));
            return new Sequence(new List<Node> {
                EnemyBT.IsEnemyDetectedNode(),

            });
        }
    }
}