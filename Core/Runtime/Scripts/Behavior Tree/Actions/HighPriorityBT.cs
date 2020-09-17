// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Lord.Core {
//     public class HighPriorityBT {

//         private BehaviorContext context;
//         public HighPriorityBT(BehaviorContext context) {
//             this.context = context;
//         }

//         private NodeStates IsEnemyVisible(BehaviorContext context) {
//             // just check if it is within a specific range for now
//             // add sight later
//             switch (context.isEnemyVisible) {
//                 case (true):
//                     Debug.Log("Enememy is visible");
//                     return NodeStates.SUCCESS;
//                 case (false):
//                     return NodeStates.FAILURE;
//                 default:
//                     return NodeStates.FAILURE;
//             }
//         }
//         public Node IsEnemyVisibleNode() {
//             return new TaskContextNode(IsEnemyVisible, "Check Enemies Visible");
//         }
//         private NodeStates IsAlertedOfEnemy(BehaviorContext context) {
//             return NodeStates.FAILURE;
//         }
//         public Node IsAlertedOfEnemyNode() {
//             return new TaskContextNode(IsAlertedOfEnemy, "Check Enemies Alert");
//         }
//         private NodeStates IsHungry(BehaviorContext context) {
//             return NodeStates.SUCCESS;
//         }
//         public Node IsHungryNode() {
//             return new TaskContextNode(IsHungry, "Check Hungry");
//         }
//         public Node IsEnemyDetectedNode() {
//             return new ParallelSequence(new List<Node> {
//                 IsEnemyVisibleNode(),
//                 IsAlertedOfEnemyNode()
//             });
//         }
//         public Node EnemyDetectionNode() {
//             MoveBT _moveBT = new MoveBT(context.character, new Vector3(0, 0, 0));
//             return new Sequence(new List<Node> {
//                 IsEnemyDetectedNode(),

//             });
//         }
//         public Node HighPriorityNode() {
//             return new ParallelSelector(new List<Node> {
//                 EnemyDetectionNode(),
//                 // IsEnemyVisibleNode(),
//                 // IsAlertedOfEnemyNode(),
//                 IsHungryNode()
//             });
//         }
//         private NodeStates Main(BehaviorContext context) {
//             return NodeStates.SUCCESS;
//         }
//         public Node MainNode() {
//             return new TaskContextNode(Main);
//         }
//     }
// }