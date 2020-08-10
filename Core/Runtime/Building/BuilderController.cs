using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Lord.Core.PlayerRaycast))]
    public class BuilderController : MonoBehaviour {
        public GameObject buildRoot;
        private PlayerRaycast playerRaycast;
        public bool debug = true;
        public GameObject debugCubePrefab;
        public GameObject debugBuildObject;
        public GameObject buildObject;
        public GameObject debugCube;
        public GameObject ghostObject;
        public Material buildGood;
        public Material buildBad;
        private GameObject lastBuilt;
        public int buildRotation;
        public Vector3Int buildLocation;
        public Vector3 rayDir;

        // Start is called before the first frame update
        void Start() {
            playerRaycast = GetComponent<PlayerRaycast>();
            buildRotation = 0;
        }

        // Update is called once per frame
        void Update() {
            buildLocation = BuildLocation(playerRaycast.hitLocation);
            InputHandler();
            if (ghostObject != null) {
                Destroy(ghostObject);
            }
            if (playerRaycast.hitObject != null) {
                Vector3 _target = new Vector3(buildLocation.x, buildLocation.y, buildLocation.z);
                // debugCube = Instantiate(debugCubePrefab, _target, Quaternion.identity);
                // debugCube.transform.Rotate(new Vector3(0, buildRotation, 0));
                ghostObject = Instantiate(buildObject, _target, Quaternion.identity);
                ghostObject.transform.Rotate(new Vector3(0, buildRotation, 0));
                Renderer[] _renderers = ghostObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer _renders in _renderers) {
                    _renders.material = buildGood;
                }
                Collider[] _colliders = ghostObject.GetComponentsInChildren<Collider>();
                foreach (Collider _collider in _colliders) {
                    Destroy(_collider);
                }
            }
            // if (debug) {
            //     if (debugCube != null)
            //         Destroy(debugCube);
            //     if (playerRaycast.hitObject != null) {
            //         Vector3 _target = new Vector3(buildLocation.x, buildLocation.y, buildLocation.z);
            //         debugCube = Instantiate(debugCubePrefab, _target, Quaternion.identity);
            //         debugCube.transform.Rotate(new Vector3(0, buildRotation, 0));

            //         Renderer[] _renderers = debugCube.GetComponentsInChildren<Renderer>();
            //         foreach (Renderer _renders in _renderers) {
            //             _renders.material = buildGood;
            //         }
            //     }
            // }
        }
        private void InputHandler() {
            if (Input.GetMouseButtonDown(0)) {
                BuildObject(buildLocation);
            }
            if (Input.GetKeyDown(KeyCode.Z)) {
                CalculateRotation(-90);
            }
            if (Input.GetKeyDown(KeyCode.C)) {
                CalculateRotation(90);
            }
        }
        private void CalculateRotation(int amount) {
            int _rotation = buildRotation += amount;
            if (_rotation == 360 || _rotation == -360)
                _rotation = 0;
            buildRotation = _rotation;
        }
        private void BuildObject(Vector3Int buildLocation) {
            GameObject _exists;
            BuildManager.instance.buildGrid.TryGetValue(buildLocation, out _exists);
            if (_exists) {
                Debug.LogWarning("Building already exists in this location.");
            } else {
                lastBuilt = Instantiate(buildObject);

                lastBuilt.transform.position = buildLocation + new Vector3(0, 0, 0);
                lastBuilt.transform.Rotate(new Vector3(0, buildRotation, 0));
                BuildManager.instance.buildGrid.Add(buildLocation, lastBuilt);
            }
        }
        private Vector3Int BuildLocation(Vector3 point) {
            rayDir = playerRaycast.ray.direction;
            Vector3 _subtractedPoint = point - (playerRaycast.ray.direction * 0.5f);
            int _xint = Mathf.RoundToInt(_subtractedPoint.x);
            int _yint = 0;
            // int _yint = Mathf.RoundToInt(_subtractedPoint.y);
            int _zint = Mathf.RoundToInt(_subtractedPoint.z);
            Vector3Int _buildPoint = new Vector3Int(_xint, _yint, _zint);
            return _buildPoint;
        }
        void OnGUI() {
            // Make a background box
            GUI.Box(new Rect(10, Screen.height - 50, 100, 100), "Builder Menu");

            // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
            if (GUI.Button(new Rect(20, Screen.height - 70, 80, 20), "Level 1")) {
                // Application.LoadLevel(1);
            }

            // Make the second button.
            if (GUI.Button(new Rect(20, Screen.height - 40, 80, 20), "Level 2")) {
                // Application.LoadLevel(2);
            }
        }
    }
}