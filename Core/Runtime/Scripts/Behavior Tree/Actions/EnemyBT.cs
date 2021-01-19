using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public static class EnemyBT {
        private static NodeStates IsEnemyDetected(Context context) {
            if(context.Character.EnemyIDsDetected.Count > 0){
                return NodeStates.SUCCESS;
            } else{
                return NodeStates.FAILURE;
            }
        }
        private static NodeStates IsAlertedOfEnemy(Context context) {
            return NodeStates.FAILURE;
        }
        private static NodeStates SetEnemyTransform(Context context){
            context.Character.MoveTransform = Character.AllCharacters[context.Character.EnemyIDsDetected[0]].Behavior.transform;
            return NodeStates.SUCCESS;
        }
        public static Node IsEnemyDetectedCheck(Context context) {
            return new SelectorParallel(
                new List<Node> {
                    new TaskContextNode(EnemyBT.IsEnemyDetected, context, "Is enemy detected?"),
                    new TaskContextNode(EnemyBT.IsAlertedOfEnemy, context, "Is alerted of enemy?")
                }
            );
        }
        public static Node EnemyDetection(Context context) {
            return new SequenceSeries(
                new List<Node> {
                    new TaskContextNode(EnemyBT.IsEnemyDetected, context, "Is enemy detected?"),
                    new TaskContextNode(EnemyBT.SetEnemyTransform, context, "Set Enemy Transform."),
                    new DebugNode("TEST DEBUG PRINT"),
                    MoveBT.FollowTransform(context),
                    new DebugNode("ATTACK HERE")
                }
            );
        }

        public static Node EnemyHandler(Context context){
            return new SequenceSeries(
                new List<Node>{
                    EnemyBT.EnemyDetection(context)
                }
            );
        }
    }
}