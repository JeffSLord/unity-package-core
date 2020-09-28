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

            // rotate towards mouse
            Vector3 _dir = Input.mousePosition - mouseScan.cam.WorldToScreenPoint(character.transform.position);
            float _angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            // character.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            Quaternion _q = Quaternion.Euler(new Vector3(0, 0, _angle));
            character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, _q, 400.0f * Time.deltaTime);
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