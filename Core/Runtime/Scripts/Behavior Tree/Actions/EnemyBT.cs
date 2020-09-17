using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public static class EnemyBT {
        private static NodeStates IsEnemyVisible(Dictionary<string, object> context) {
            // just check if it is within a specific range for now
            // add sight later
            bool _isEnemyVisible;
            if (context.TryGetValue<bool>("isEnemyVisible", out _isEnemyVisible)) {
                switch (_isEnemyVisible) {
                    case (true):
                        Debug.Log("Enememy is visible");
                        return NodeStates.SUCCESS;
                    case (false):
                        return NodeStates.FAILURE;
                    default:
                        return NodeStates.FAILURE;
                }
            } else {
                return NodeStates.FAILURE;
            }

        }
        public static Node IsEnemyVisibleNode(Dictionary<string, object> context) {
            return new TaskContextNode(IsEnemyVisible, context, "Check Enemies Visible");
        }
        private static NodeStates IsAlertedOfEnemy(Dictionary<string, object> context) {
            return NodeStates.FAILURE;
        }
        public static Node IsAlertedOfEnemyNode(Dictionary<string, object> context) {
            return new TaskContextNode(IsAlertedOfEnemy, context, "Check Enemies Alert");
        }
        public static Node IsEnemyDetectedNode(Dictionary<string, object> context) {
            return new ParallelSequence(new List<Node> {
                EnemyBT.IsEnemyVisibleNode(context),
                EnemyBT.IsAlertedOfEnemyNode(context)
            });
        }
        public static Node EnemyDetectionNode(Dictionary<string, object> context) {
            // MoveBT _moveBT = new MoveBT(context.character, new Vector3(0, 0, 0));
            return new Sequence(new List<Node> {
                EnemyBT.IsEnemyDetectedNode(context),

            });
        }
    }
}