using System.Collections;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Lord.Core.PlayerRaycast))]
    [RequireComponent(typeof(Lord.Core.BuilderController))]
    public class PlayerController : MonoBehaviour {
        private PlayerRaycast playerRaycast;
        private BuilderController builderController;
        private FloatController floatController;
        public Character character;
        private void Start() {
            playerRaycast = GetComponent<PlayerRaycast>();
            builderController = GetComponent<BuilderController>();
            floatController = GetComponentInChildren<FloatController>();

            builderController.enabled = false;
        }
        private void Update() {
            InputHandler();
        }
        private void InputHandler() {
            if (Input.GetKeyDown(KeyCode.B)) {
                if (builderController.enabled) {
                    Destroy(builderController.ghostObject);
                }
                builderController.enabled = !builderController.enabled;
            }
            if (!builderController.enabled) {
                if (Input.GetMouseButtonDown(0)) {
                    if (playerRaycast.hitObject != null) {
                        if (playerRaycast.hitObject.GetComponent<ISelectable>() != null) {
                            playerRaycast.hitObject.GetComponent<ISelectable>().Select(this.gameObject);
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1)) {
                    if (playerRaycast.hitObject != null) {
                        if (playerRaycast.hitObject.GetComponent<ISelectable>() != null) {
                            playerRaycast.hitObject.GetComponent<ISelectable>().Select(this.gameObject, 1);
                        } else {
                            character.navMeshAgent.destination = playerRaycast.hitLocation;
                        }
                    }
                }
            }
        }
    }
}