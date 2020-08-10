using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Character : MonoBehaviour, ISelectable {
        public NavMeshAgent navMeshAgent;

        // Start is called before the first frame update
        void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update() {

        }

        public void Deselect(GameObject selector) {
            throw new System.NotImplementedException();
        }

        public void Select(GameObject selector, int option = 0) {
            if (option == 0) { // left click
                ShowSelectGui();
            } else if (option == 1) { // right click
                // if (selector.GetComponent<Character>)
            }
            // throw new System.NotImplementedException();
        }
        public void ShowSelectGui() {
            Debug.Log("Show Select GUI");
            // throw new System.NotImplementedException();
        }
    }
}