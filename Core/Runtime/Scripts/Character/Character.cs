using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    [System.Serializable]
    public class Character
    {
        // public static int idCount = 0;
        public static List<Character> AllCharacters {get;set;} = new List<Character>();
        public CharacterBehavior Behavior{get;set;}
        public int ID{get;set;}
        [SerializeField]
        public string Name{get;set;}
        // public float MoveSpeed{get;set;}
        // public float StoppingDistance{get;set;}
        public BehaviorTree BT{get;set;}
        public List<WorkPriority> WorkPriority{get;set;}
        public Settlement Settlement{get;set;}
        public int Faction{get;set;}
        // public float Fov{get;set;}
        // public float VoiceRange{get;set;}
        // public float NoiseRange{get;set;}
        // public List<int> VisibleCharacterIDs{get;set;}
        public int Age {get;set;}
        public bool IsPlayer{get;set;}
        
        public Character(CharacterBehavior behavior, bool isPlayer=false){
            this.Behavior = behavior;
            this.ID = Character.AllCharacters.Count;
            Character.AllCharacters.Add(this);
            // this.MoveSpeed = 1.0f;
            // this.StoppingDistance = 1.0f;
            this.Faction = 1;
            // this.Fov = 90;
            // this.VoiceRange = 20;
            // this.NoiseRange = 20;
            this.BT = new BehaviorTree();
            // this.VisibleCharacterIDs = new List<int>();
            this.WorkPriority = new List<WorkPriority>{
                new WorkPriority(WorkType.FARM, 1),
                new WorkPriority(WorkType.HAUL, 1)
            };
            this.IsPlayer = isPlayer;
        }
        public static Character GetCharacter(int id){
            return Character.AllCharacters[id];
        }
        // public Character(){
        // }
        
        public bool IsEnemy(Character character) {
            return character.Faction != this.Faction;
        }
        
    }
}