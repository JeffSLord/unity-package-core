using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public static class EnemyBT {
        private static NodeStates IsEnemyVisible(Context context) {
            // just check if it is within a specific range for now
            // add sight later
            bool _isEnemyVisible;
            if (context.data.TryGetValue<bool>("isEnemyVisible", out _isEnemyVisible)) {
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
        public static Node IsEnemyVisibleNode(Context context) {
            return new TaskContextNode(IsEnemyVisible, context, "Check Enemies Visible");
        }
        private static NodeStates IsAlertedOfEnemy(Context context) {
            return NodeStates.FAILURE;
        }
        public static Node IsAlertedOfEnemyNode(Context context) {
            return new TaskContextNode(IsAlertedOfEnemy, context, "Check Enemies Alert");
        }
        public static Node IsEnemyDetectedNode(Context context) {
            return new SelectorParallel(new List<Node> {
                EnemyBT.IsEnemyVisibleNode(context),
                EnemyBT.IsAlertedOfEnemyNode(context)
            });
        }
        public static Node EnemyDetectionNode(Context context) {
            // MoveBT _moveBT = new MoveBT(context.character, new Vector3(0, 0, 0));
            return new SequenceParallel(new List<Node> {
                EnemyBT.IsEnemyDetectedNode(context),
                MoveBT.FollowTransform(context)
            });
        }
    }
}