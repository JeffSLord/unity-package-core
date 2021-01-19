using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core{
    public class CharacterDetection2D{
        public CharacterDetection2DBehavior Behavior{get;set;}
        public List<int> VisibleCharacterIDs{get;set;}
        public float VoiceRange{get;set;}
        public float NoiseRange{get;set;}
        public float Fov{get;set;}

        public CharacterDetection2D(CharacterDetection2DBehavior behavior){
            this.Behavior = behavior;
            this.VisibleCharacterIDs = new List<int>();
            this.VoiceRange = 20;
            this.NoiseRange = 20;
            this.Fov = 160;
        }

        public void OnCharacterEnterVision(Character otherCharacter, bool setVisibility){
            VisibleCharacterIDs.Add(otherCharacter.ID);
            if(setVisibility){
                otherCharacter.Behavior.SetVisibility(true);
            }
            //! Behavior Tree
            // if(Behavior.characterBehavior.character.IsEnemy(otherCharacter)){
            //     Behavior.characterBehavior.character.BT.Context.SetContextList<Character>("enemiesInRange", new List<Character>{otherCharacter});
            //     Behavior.characterBehavior.character.BT.Context.SetContext<bool>("isEnemyVisible", true);
            // }
        }
        public void OnCharacterExitVision(Character otherCharacter, bool setVisibility){
            VisibleCharacterIDs.Remove(otherCharacter.ID);
            if(setVisibility){
                otherCharacter.Behavior.SetVisibility(false);
            }
            //! Behavior Tree
            // if(Behavior.characterBehavior.character.IsEnemy(otherCharacter)){
            //     Behavior.characterBehavior.character.BT.Context.RemoveContextList<Character>("enemiesInRange", new List<Character>{otherCharacter});
            //     if(VisibleCharacterIDs.Count == 0){
            //         Behavior.characterBehavior.character.BT.Context.SetContext<bool>("isEnemyVisible", true);
            //     }
            // }
        }
    }
}
