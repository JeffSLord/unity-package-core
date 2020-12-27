using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {

    public class BuildManager : MonoBehaviour {
        public Transform worldObjectRoot;
        public Transform resourceSourceRoot;
        public Transform itemRoot;
        public Transform buildSourceRoot;
        public static BuildManager instance;
        public Dictionary<Vector3Int, GameObject> buildGrid;

        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(this.gameObject);
            } else {
                instance = this;
                worldObjectRoot = GameObject.Find("/World Objects").transform;
                resourceSourceRoot = GameObject.Find("/World Objects/Resource Sources").transform;
                itemRoot = GameObject.Find("/World Objects/Items").transform;
            }
        }

        // Start is called before the first frame update
        void Start() {
            buildGrid = new Dictionary<Vector3Int, GameObject>();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}