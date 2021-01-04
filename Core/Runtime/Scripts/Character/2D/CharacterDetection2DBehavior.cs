using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(CharacterBehavior))]
    public class CharacterDetection2DBehavior : MonoBehaviour {
        public CharacterDetection2D characterDetection;
        public CharacterBehavior characterBehavior;
        void Start(){
            this.characterDetection = new CharacterDetection2D(this);
            this.characterBehavior = GetComponent<CharacterBehavior>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            CharacterCollider2DBehavior _col = other.gameObject.GetComponent<CharacterCollider2DBehavior>();
            if (_col != null) {
                bool _setVisibility = this.characterBehavior.character.IsPlayer;
                characterDetection.OnCharacterEnterVision(_col.character2DBehavior.character, _setVisibility);
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            CharacterCollider2DBehavior _col = other.gameObject.GetComponent<CharacterCollider2DBehavior>();
            if (_col != null) {
                bool _setVisibility = this.characterBehavior.character.IsPlayer;
                characterDetection.OnCharacterExitVision(_col.character2DBehavior.character, _setVisibility);
            }
        }
    }
}