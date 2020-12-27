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
            this.bt = new BehaviorTree();
            this.workPriority = new List<WorkPriority>{
                new WorkPriority("farm", 1),
                new WorkPriority("haul", 1)
            };
        }
        public bool IsEnemy(Character character) {
            return character.faction != this.faction;
        }
    }
}