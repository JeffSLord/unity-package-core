using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(MouseScan2D))]
    public class PlayerController2D : MonoBehaviour {
        public Character2D character;
        public MouseScan2D mouseScan;

        void Start() {
            mouseScan = GetComponent<MouseScan2D>();
        }
        void Update() {
            InputHandler();
        }
        private void InputHandler() {
            if (Input.GetMouseButtonDown(0)) {
                MouseHandler(0);
            }
            // if (Input.GetMouseButtonDown(1)) {
            //     MouseHandler(1);
            // }
            if (Input.GetMouseButton(1)) {
                MouseHandler(1);
            }
        }
        private void MouseHandler(int mouseButton) {
            GameObject _hitObject;
            switch (mouseButton) {
                case 0: // left click
                    _hitObject = mouseScan.hitGameObject;
                    if (_hitObject != null) {
                        Selectable _hitSelectable = _hitObject.GetComponentInParent<Selectable>();
                        if (_hitSelectable != null) {
                            _hitSelectable.Select0(this.gameObject);
                        }
                    }
                    break;
                case 1: // right click
                    _hitObject = mouseScan.hitGameObject;
                    if (_hitObject != null) {
                        // if currently selecting something (doing something), stop
                        if (character.currentSelection != null) {
                            character.currentSelection.Deselect(this.gameObject);
                            character.currentSelection = null;
                        }
                        Selectable _hitSelectable = _hitObject.GetComponentInParent<Selectable>();
                        if (_hitSelectable != null) {
                            _hitSelectable.Select1(this.gameObject, 1);
                            character.currentSelection = _hitSelectable;
                        } else {
                            MoveBT moveContext = new MoveBT(character, mouseScan.mousePosition, 1.0f);
                            character.behaviorTree.SetNode(moveContext.MoveSequence());
                        }
                    } else {
                        MoveBT moveContext = new MoveBT(character, mouseScan.mousePosition, 1.0f);
                        character.behaviorTree.SetNode(moveContext.MoveSequence());
                    }
                    break;
                default:
                    break;
            }
        }
    }
}