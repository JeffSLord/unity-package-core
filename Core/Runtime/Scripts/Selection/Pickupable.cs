using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class Pickupable : MonoBehaviour {
        public bool running = false;
        public float pickupDistance = 1.0f;

        public void Pickup(CharacterBehavior character) {
            running = true;
            StartCoroutine(PickupCoroutine(character));
        }
        public void Pickup(PlayerController playerController) {
            Pickup(playerController.characterBehavior);
        }

        private IEnumerator PickupCoroutine(CharacterBehavior character) {
            while (running) {
                if (character.currentSelection != GetComponent<Selectable>()) {
                    StopAllCoroutines();
                    yield break;
                }
                if (Vector3.Distance(character.transform.position, this.transform.position) <= pickupDistance) {
                    //! replace this with pickup
                    Destroy(this.gameObject);
                }
                character.character.NavMeshAgent.destination = this.transform.position;
                yield return new WaitForSeconds(0.25f);
            }
        }

    }
}