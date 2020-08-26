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
                    Select0();
                }
                if (Input.GetMouseButtonDown(1)) {
                    Select1();
                }
                if (Input.GetMouseButton(1)) {
                    Select1();
                }
            }
        }
        // shouldn't affect current character?
        private void Select0() {
            GameObject _hitObject = playerRaycast.hitObject;
            if (_hitObject != null) {
                Selectable _hitSelectable = _hitObject.GetComponentInParent<Selectable>();
                if (_hitSelectable != null) {
                    _hitSelectable.Select0(this.gameObject);
                }
            }
        }
        // will affect current character
        private void Select1() {
            GameObject _hitObject = playerRaycast.hitObject;
            if (playerRaycast.hitObject != null) {
                // if currently selecting something (doing something), stop
                if (character.currentSelect != null) {
                    character.currentSelect.Deselect(this.gameObject);
                    character.currentSelect = null;
                }
                Selectable _hitSelectable = _hitObject.GetComponentInParent<Selectable>();
                if (_hitSelectable != null) {
                    _hitSelectable.Select1(this.gameObject, 1);
                    character.currentSelect = _hitSelectable;
                } else {
                    MoveBT moveContext = new MoveBT(character, playerRaycast.hitLocation, 1.0f);
                    character.behaviorTree.SetNode(moveContext.MoveSequence());
                }
            }
        }
    }
}