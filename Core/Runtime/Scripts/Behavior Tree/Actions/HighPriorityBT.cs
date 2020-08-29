using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class HighPriorityBT {
        public BehaviorContext context;

        private NodeStates AreEnemiesVisible() {
            // just check if it is within a specific range for now
            // add sight later
            switch (context.isEnemyVisible) {
                case (true):
                    return NodeStates.SUCCESS;
                case (false):
                    return NodeStates.FAILURE;
                default:
                    return NodeStates.FAILURE;
            }
        }
        public TaskNode AreEnemiesVisibleNode() {
            return new TaskNode(AreEnemiesVisible, "Check Enemies Visible");
        }
        private NodeStates IsAlertedOfEnemies() {
            return NodeStates.FAILURE;
        }
        public TaskNode IsAlertedOfEnemiesNode() {
            return new TaskNode(IsAlertedOfEnemies, "Check Enemies Alert");
        }
        private NodeStates IsHungry() {
            return NodeStates.SUCCESS;
        }
        public TaskNode IsHungryNode() {
            return new TaskNode(IsHungry, "Check Hungry");
        }

        public ParallelSequence HighPriorityNode() {
            return new ParallelSequence(new List<Node> {
                AreEnemiesVisibleNode(),
                IsAlertedOfEnemiesNode(),
                IsHungryNode()
            });
        }
    }
}