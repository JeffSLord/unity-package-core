using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Pickupable))]
    public class Resource : MonoBehaviour, ISelectable {
        public void Deselect(GameObject selector) {
            selector.GetComponent<PlayerController>().character.currentSelect = null;
        }

        public void Select(GameObject selector, int option = 0) {
            if (option == 1) {
                PlayerController _playerController = selector.GetComponent<PlayerController>();
                if (_playerController != null) {
                    _playerController.character.currentSelect = this;
                    GetComponent<Pickupable>().Pickup(_playerController);
                }
            }
        }

        public void SelectGui() {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}