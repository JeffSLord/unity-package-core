using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class CharacterController2D : MonoBehaviour {
        // Start is called before the first frame update
        // public CharacterBehavior characterBehavior;
        public Transform characterTransform;
        public MouseScan2D mouseScan;
        public float mouseFollowSpeed;
        public float movementSpeed;
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            FollowMouse();
            MovementHandler();
        }
        private void MovementHandler() {
            Vector3 _move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            // Debug.Log(_move);
            characterTransform.position += _move * movementSpeed * Time.deltaTime;
        }
        private void FollowMouse() {
            Vector3 _dir = Input.mousePosition - mouseScan.cam.WorldToScreenPoint(characterTransform.position);
            float _angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            // character.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            Quaternion _q = Quaternion.Euler(new Vector3(0, 0, _angle));
            characterTransform.rotation = Quaternion.RotateTowards(characterTransform.rotation, _q, mouseFollowSpeed * Time.deltaTime);
        }

    }
}