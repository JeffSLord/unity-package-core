using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    [RequireComponent(typeof(Camera))]
    public class FloatController : MonoBehaviour {

        public Camera floatCam;
        public float moveSpeed = 10.0f;
        public Vector3 moveDir;
        public GameObject rotateTarget;
        // Start is called before the first frame update
        void Start() {
            floatCam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update() {
            InputHandler();
        }
        private void InputHandler() {
            Vector3 _movement = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) {
                _movement += floatCam.transform.forward;
            }
            if (Input.GetKey(KeyCode.S)) {
                _movement += -1 * floatCam.transform.forward;
            }
            if (Input.GetKey(KeyCode.D)) {
                _movement += floatCam.transform.right;
            }
            if (Input.GetKey(KeyCode.A)) {
                _movement += -1 * floatCam.transform.right;
            }
            _movement.y = -1 * Input.mouseScrollDelta.y;
            MovementHandler(_movement);
            if (Input.GetKeyDown(KeyCode.Q)) {
                transform.RotateAround(rotateTarget.transform.position, Vector3.up, 45);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                transform.RotateAround(rotateTarget.transform.position, Vector3.up, -45);
            }

        }
        private void MovementHandler(Vector3 movement) {
            movement = movement.normalized;
            moveDir = movement;
            floatCam.transform.position += movement * Time.deltaTime * moveSpeed;
        }
    }
}