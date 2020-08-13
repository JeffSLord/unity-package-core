using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class Pickupable : MonoBehaviour {
        public bool running = false;
        public float pickupDistance = 1.0f;

        public void Pickup(Character character) {
            running = true;
            StartCoroutine(PickupCoroutine(character));
        }
        public void Pickup(PlayerController playerController) {
            Pickup(playerController.character);
        }

        private IEnumerator PickupCoroutine(Character character) {
            while (running) {
                if (character.currentSelect != GetComponent<ISelectable>()) {
                    StopAllCoroutines();
                    yield break;
                }
                if (Vector3.Distance(character.transform.position, this.transform.position) <= pickupDistance) {
                    //! replace this with pickup
                    Destroy(this.gameObject);
                }
                character.navMeshAgent.destination = this.transform.position;
                yield return new WaitForSeconds(0.25f);
            }
        }

    }
}