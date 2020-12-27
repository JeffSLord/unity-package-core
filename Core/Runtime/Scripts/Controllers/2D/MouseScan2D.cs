using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Lord.Core {
    public class MouseScan2D : MonoBehaviour {
        public bool debugging;
        public Camera cam;
        public Tilemap tilemap;
        public Vector3 mousePosition;
        public Vector3Int tilePosition;
        public Tile tile;
        private Tile lastTile;
        private Color lastColor;

        public GameObject hitGameObject;

        // Update is called once per frame
        void Update() {
            Process();
            if (debugging)
                Log();
        }
        private void Process() {
            Vector3 _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * cam.transform.position.z);
            mousePosition = cam.ScreenToWorldPoint(_mousePos);
            tilePosition = tilemap.WorldToCell(mousePosition);
            tile = tilemap.GetTile<Tile>(tilePosition);

            // ignore Layer 9
            RaycastHit2D _hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, ~(1 << 9));
            if (_hit.collider != null) {
                hitGameObject = _hit.collider.gameObject;
            } else {
                hitGameObject = null;
            }

        }
        private void Log() {
            Debug.Log("Mouse Position: " + mousePosition);
            Debug.Log("Tile Position: " + tilePosition);
            tilemap.SetTileFlags(tilePosition, TileFlags.None);
            tilemap.SetColor(tilePosition, Color.red);
        }
    }

}