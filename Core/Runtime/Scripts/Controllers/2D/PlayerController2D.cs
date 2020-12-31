using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(MouseScan2D))]
    public class PlayerController2D : MonoBehaviour {
        public Character2DBehavior character;
        public MouseScan2D mouseScan;
        public Camera camera;
        public float followMouseSpeed;
        public float scrollSpeed;

        void Start() {
            mouseScan = GetComponent<MouseScan2D>();
            this.camera = GetComponent<Camera>();
        }
        void Update() {
            InputHandler();
        }
        void LateUpdate() {
            this.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, this.transform.position.z);
        }

        private void InputHandler() {
            if (Input.GetMouseButtonDown(0)) {
                MouseHandler(0);
            }
            if (Input.GetMouseButton(1)) {
                MouseHandler(1);
            }
            ScrollHandler();
        }

        private void ScrollHandler(){
            float _fov = camera.fieldOfView;
            _fov -= Input.mouseScrollDelta.y * scrollSpeed;
            _fov = Mathf.Clamp(_fov, 40, 120);
            camera.fieldOfView = _fov;
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
                    // if mouse click hit object
                    if (_hitObject != null) {
                        // if currently selecting something (doing something), stop doing it
                        if (character.currentSelection != null) {
                            character.currentSelection.Deselect(this.gameObject);
                            character.currentSelection = null;
                        }
                        // if clicked thing is selectable
                        Selectable _hitSelectable = _hitObject.GetComponentInParent<Selectable>();
                        if (_hitSelectable != null) {
                            _hitSelectable.Select1(this.gameObject, 1);
                            character.currentSelection = _hitSelectable;
                        } else { // if clicked thing is not selectable
                            character.SetCharacterDestination(mouseScan.mousePosition, 1.0f);
                        }
                    } else { // if mouse click did not hit object
                        character.SetCharacterDestination(mouseScan.mousePosition, 1.0f);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}