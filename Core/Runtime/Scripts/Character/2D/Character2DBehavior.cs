using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character2DBehavior : CharacterBehavior {
        protected override void Start() {
            base.Start();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            // navMeshAgent.
        }
        // private bool LineOfSight(Transform target) {
        //     RaycastHit hit;
        //     if (Vector3.Angle(target.position - transform.position, transform.forward) <= base.character.Fov &&
        //         Physics.Linecast(transform.position, target.position, out hit) &&
        //         hit.collider.transform == target) {
        //         return true;
        //     }
        //     return false;
        // }
        // public bool IsEnemy(CharacterBehavior characterBehavior) {
        //     return characterBehavior.character.faction != this.faction;
        // }
    }
}