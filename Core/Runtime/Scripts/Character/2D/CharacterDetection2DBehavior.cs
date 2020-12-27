using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class CharacterDetection2DBehavior : MonoBehaviour {
        public CharacterBehavior characterBehavior;

        void Start(){
            // this.characterBehavior = GetComponent<CharacterBehavior>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            // Debug.Log("Collider: " + other.gameObject.name);
            CharacterCollider2D _col = other.gameObject.GetComponent<CharacterCollider2D>();
            if (_col != null) {
                Character _char = _col.character2DBehavior.character;
                List<Character> _charInRange = new List<Character> { _char };
                // Debug.Log(_char.gameObject.name);
                if (_char.IsEnemy(characterBehavior.character)) { // Character is enemy
                    characterBehavior.character.bt.context.SetContextList<Character>("enemiesInRange", _charInRange);
                    characterBehavior.character.bt.context.SetContext<bool>("isEnemyVisible", true);

                    characterBehavior.character.bt.context.SetContext<Transform>("targetTransform", _col.character2DBehavior.transform);

                    Debug.Log("CHARACTER IS AN ENEMY");

                } else { // Character is not enemy
                    Debug.Log("CHARACTER IS NOT AN ENEMY");
                    characterBehavior.character.bt.context.SetContextList<Character>("friendliesInRange", _charInRange);
                }
                // if (Vector3.Distance (transform.position, player.position) < sightReach && Vector3.Angle (target.position - transform.position, transform.forward) <= fov)
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            // Character2D _char = other.gameObject.GetComponentInParent<Character2D>();
            CharacterCollider2D _col = other.gameObject.GetComponent<CharacterCollider2D>();
            if (_col != null) {
                Character _char = _col.character2DBehavior.character;
                List<Character> _charLeftRange = new List<Character> { _char };
                if (_char.IsEnemy(characterBehavior.character)) {
                    if (characterBehavior.character.bt.context.RemoveContextList<Character>("enemiesInRange", _charLeftRange)) {
                        characterBehavior.character.bt.context.SetContext("isEnemyVisible", false);
                    }
                } else {
                    characterBehavior.character.bt.context.RemoveContextList<Character>("friendliesInRange", _charLeftRange);
                    // character.bt.context.friendliesInRange.Remove(_char);
                }
            }
        }
    }
}