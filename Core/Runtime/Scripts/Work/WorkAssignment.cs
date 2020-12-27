using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class WorkAssignment
    {
        public GameObject workObject;
        public CharacterBehavior assignedCharacter;

        public WorkAssignment(GameObject workObject){
            this.workObject = workObject;
        }

        public bool IsAvailable(){
            if(this.assignedCharacter == null){
                return true;
            }
            else{
                return false;
            }
        }
        public bool AssignCharacter(CharacterBehavior character){
            this.assignedCharacter = character;
            return true;
        }
        public bool UnassignCharacter(){
            this.assignedCharacter = null;
            return true;
        }
    }
}