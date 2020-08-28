using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Lord.Core {
    public class MouseScan : MonoBehaviour {
        public Camera cam;
        public bool tileMode;
        public bool debug = true;
        public Ray ray;
        private RaycastHit hit;
        public Vector3 hitLocation;
        public Vector3 hitLocationRound;
        public GameObject hitObject;
        public float interactionDistance = 100.0f;
        public Tilemap tilemap;
        public Vector3 tilePosition;
        public Tile tile;

        void Update() {
            ProcessRaycast();
        }
        private void ProcessRaycast() {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, interactionDistance)) {
                hitObject = hit.collider.gameObject;
                hitLocation = hit.point;
                hitLocationRound = RoundHit(hitLocation);

            } else {
                hitObject = null;
            }
            if (debug) {
                Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green, 0.1f);
                if (hitObject != null) {
                    Debug.Log(hitObject);
                }
            }
            if (tileMode) {
                tilePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                tilePosition.z = 0;
                tilemap.GetTile(tilemap.WorldToCell(tilePosition));
            }
        }
        private int MyRound(float val) {
            if (val % 0.5f == 0)
                return Mathf.CeilToInt(val);
            else
                return Mathf.FloorToInt(val);
        }
        private Vector3 RoundHit(Vector3 point) {
            float _x = Mathf.Round(point.x);
            float _y = 0.0f;
            float _z = Mathf.Round(point.z);
            Vector3 _roundedPoint = new Vector3(_x, _y, _z);
            return _roundedPoint;
        }
    }
}