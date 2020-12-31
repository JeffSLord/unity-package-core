using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class CharacterDetection2DBehavior : MonoBehaviour {
        public CharacterBehavior characterBehavior;
        public CharacterDetection2D characterDetection;

        void Start(){
            this.characterDetection = new CharacterDetection2D(this);
            this.characterBehavior = GetComponent<CharacterBehavior>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            CharacterCollider2DBehavior _col = other.gameObject.GetComponent<CharacterCollider2DBehavior>();
            if (_col != null) {
                bool _setVisibility = this.characterBehavior.character.IsPlayer;
                characterDetection.OnCharacterEnterVision(_col.character2DBehavior.character, _setVisibility);
                // //! Behavior Tree
                // List<Character> _charInRange = new List<Character> { _char };
                // // Debug.Log(_char.gameObject.name);
                // if (_char.IsEnemy(characterBehavior.character)) { // Character is enemy
                //     characterBehavior.character.BT.context.SetContextList<Character>("enemiesInRange", _charInRange);
                //     characterBehavior.character.BT.context.SetContext<bool>("isEnemyVisible", true);

                //     characterBehavior.character.BT.context.SetContext<Transform>("targetTransform", _col.character2DBehavior.transform);

                //     Debug.Log("CHARACTER IS AN ENEMY");

                // } else { // Character is not enemy
                //     Debug.Log("CHARACTER IS NOT AN ENEMY");
                //     characterBehavior.character.BT.context.SetContextList<Character>("friendliesInRange", _charInRange);
                // }
                // //! End Behavior Tree
                // if (Vector3.Distance (transform.position, player.position) < sightReach && Vector3.Angle (target.position - transform.position, transform.forward) <= fov)
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            CharacterCollider2DBehavior _col = other.gameObject.GetComponent<CharacterCollider2DBehavior>();
            if (_col != null) {
                bool _setVisibility = this.characterBehavior.character.IsPlayer;
                characterDetection.OnCharacterExitVision(_col.character2DBehavior.character, _setVisibility);
                // //! Behavior Tree
                // List<Character> _charLeftRange = new List<Character> { _char };
                // if (_char.IsEnemy(characterBehavior.character)) {
                //     if (characterBehavior.character.BT.context.RemoveContextList<Character>("enemiesInRange", _charLeftRange)) {
                //         characterBehavior.character.BT.context.SetContext("isEnemyVisible", false);
                //     }
                // } else {
                //     characterBehavior.character.BT.context.RemoveContextList<Character>("friendliesInRange", _charLeftRange);
                //     // character.bt.context.friendliesInRange.Remove(_char);
                // }
            }
        }
    }
}