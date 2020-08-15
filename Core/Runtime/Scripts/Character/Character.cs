using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Selectable))]
    public class Character : WorldObjectSelect {
        public NavMeshAgent navMeshAgent;
        public Selectable currentSelect;

        // Start is called before the first frame update
        protected override void Start() {
            base.Start();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void OnCollisionEnter(Collision collision) {
            Debug.Log(collision.collider.name);
            // console.log(collision.collider.)
        }

        protected override void Select0(GameObject selector, int option = 0) {
            Debug.Log("Actual override is working? can this work?");
        }
    }
}