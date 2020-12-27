using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    [System.Serializable]
    public class Character
    {
        public string name;
        public float moveSpeed;
        public float stoppingDistance;
        public BehaviorTree bt;
        public List<WorkPriority> workPriority;
        public Settlement settlement;
        public int faction;
        public float fov;
        public float voiceRange;
        public float noiseRange;

        public Character(){
            this.moveSpeed = 1.0f;
            this.stoppingDistance = 1.0f;
            this.faction = 1;
            this.fov = 90;
            this.voiceRange = 20;
            this.noiseRange = 20;
            bt = new BehaviorTree();
        }

        private void InitBT(){
            bt.context.SetContext<Character>("character", this);
            bt.context.SetContext<float>("moveSpeed", this.moveSpeed);
            bt.context.SetContext<float>("stoppingDistance", this.stoppingDistance);
            bt.context.SetContextList<Waypoint>("waypoints", GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().waypoints);
            bt.context.SetContextList<WorkPriority>("workPriority", workPriority);
            bt.highPriorityNode = EnemyBT.EnemyDetectionNode(bt.context);
            // bt.lowPriorityNode = MoveBT.MoveToRandomWaypoint(bt.context);
            bt.lowPriorityNode = WorkBT.Work(bt.context);

            //! TEMP
            bt.context.SetContext<Settlement>("settlement", GameObject.FindGameObjectWithTag("Settlement").GetComponent<Settlement>());
        }
        public bool IsEnemy(Character character) {
            return character.faction != this.faction;
        }
    }
}