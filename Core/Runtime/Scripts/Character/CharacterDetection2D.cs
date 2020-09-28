using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class CharacterDetection2D : MonoBehaviour {
        public Character character;
        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Collider: " + other.gameObject.name);
            CharacterCollider2D _col = other.gameObject.GetComponent<CharacterCollider2D>();
            if (_col != null) {
                Character2D _char = _col.character;
                List<Character> _charInRange = new List<Character> { _char };
                Debug.Log(_char.gameObject.name);
                if (_char.IsEnemy(character)) { // Character is enemy
                    character.bt.SetContextList<Character>("enemiesInRange", _charInRange);
                    character.bt.SetContext<bool>("isEnemyVisible", true);
                    Debug.Log("CHARACTER IS AN ENEMY");

                } else { // Character is not enemy
                    Debug.Log("CHARACTER IS NOT AN ENEMY");
                    character.bt.SetContextList<Character>("friendliesInRange", _charInRange);
                }
                // if (Vector3.Distance (transform.position, player.position) < sightReach && Vector3.Angle (target.position - transform.position, transform.forward) <= fov)
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            // Character2D _char = other.gameObject.GetComponentInParent<Character2D>();
            CharacterCollider2D _col = other.gameObject.GetComponent<CharacterCollider2D>();
            if (_col != null) {
                Character2D _char = _col.character;
                List<Character> _charLeftRange = new List<Character> { _char };
                if (_char.IsEnemy(character)) {
                    if (character.bt.RemoveContextList<Character>("enemiesInRange", _charLeftRange)) {
                        character.bt.SetContext("isEnemyVisible", false);
                    }
                } else {
                    character.bt.RemoveContextList<Character>("friendliesInRange", _charLeftRange);
                    // character.bt.context.friendliesInRange.Remove(_char);
                }
            }
        }
    }
}