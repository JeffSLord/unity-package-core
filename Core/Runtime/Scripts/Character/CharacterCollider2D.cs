using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(PolygonCollider2D))]
    public class CharacterCollider2D : MonoBehaviour {
        public Character2D character;

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Collider: " + other.gameObject.name);
            // Character2D _char = other.gameObject.GetComponentInParent<Character2D>();
            CharacterCollider2D _col = other.gameObject.GetComponent<CharacterCollider2D>();
            if (_col != null) {
                Character2D _char = _col.character;
                Debug.Log(_char.gameObject.name);
                if (_char.IsEnemy(character)) {
                    character.bt.context.enemiesInRange.Add(_char);
                    character.bt.context.enemiesDetected.Add(_char);
                    character.bt.context.isEnemyVisible = true;
                } else {
                    character.bt.context.friendliesInRange.Add(_char);
                }
                // if character is bad, add to enemies
                // if (Vector3.Distance (transform.position, player.position) < sightReach && Vector3.Angle (target.position - transform.position, transform.forward) <= fov)
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            // Character2D _char = other.gameObject.GetComponentInParent<Character2D>();
            CharacterCollider2D _col = other.gameObject.GetComponent<CharacterCollider2D>();
            if (_col != null) {
                Character2D _char = _col.character;
                if (_char.IsEnemy(character)) {
                    character.bt.context.enemiesInRange.Remove(_char);
                    character.bt.context.enemiesDetected.Remove(_char);
                    character.bt.context.isEnemyVisible = true;
                } else {
                    character.bt.context.friendliesInRange.Remove(_char);
                }
                // if character is bad, add to enemies
                // if (Vector3.Distance (transform.position, player.position) < sightReach && Vector3.Angle (target.position - transform.position, transform.forward) <= fov)
            }
        }
    }
}