using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour, ISelectable {
        public NavMeshAgent navMeshAgent;
        public ISelectable currentSelect;

        // Start is called before the first frame update
        void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update() {

        }

        public void Deselect(GameObject selector) {
            // throw new System.NotImplementedException();
        }

        public void Select(GameObject selector, int option = 0) {
            if (option == 0) { // left click
                SelectGui();
            } else if (option == 1) { // right click
                // if (selector.GetComponent<Character>)
            }
            // throw new System.NotImplementedException();
        }
        public void SelectGui() {
            Debug.Log("Show Select GUI");
            // throw new System.NotImplementedException();
        }

        void OnCollisionEnter(Collision collision) {
            Debug.Log(collision.collider.name);
            // console.log(collision.collider.)
        }
    }
}