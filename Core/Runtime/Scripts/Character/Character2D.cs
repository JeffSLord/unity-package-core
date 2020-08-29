using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character2D : Character {
        protected override void Start() {
            base.Start();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }

        private void OnTriggerEnter(Collider other) {
            Character _char = other.gameObject.GetComponent<Character>();
            if (_char != null) {
                // if character is bad, add to enemies
                // if (Vector3.Distance (transform.position, player.position) < sightReach && Vector3.Angle (target.position - transform.position, transform.forward) <= fov)
            }
        }
        private void OnTriggerExit(Collider other) {

        }
        private bool LineOfSight(Transform target) {
            RaycastHit hit;
            if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov &&
                Physics.Linecast(transform.position, target.position, out hit) &&
                hit.collider.transform == target) {
                return true;
            }
            return false;
        }
    }
}