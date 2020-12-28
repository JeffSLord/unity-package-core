using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
namespace Lord.Core
{
    public class SettlementBehavior : MonoBehaviour
    {
        
        public NavMeshSurface2d surface;
        public GameObject wallParent;
        public GameObject roadParent;
        public GameObject floorParent;
        public Tilemap floorTileMap;
        public Tilemap wallTileMap;
        public TileBase floorTile;
        public TileBase wallTile;
        public TileBase roadTile;
        public Settlement settlement;
        public GameObject characterPrefab;
        public GameObject buildingPrefab;
        public GameObject farmPrefab;
        public GameObject roadPrefab;
        public GameObject wallPrefab;
        public GameObject floorPrefab;

        void Start(){
            this.settlement = new Settlement();
            PlaceSettlement();
        }
        private IEnumerator UpdateNavmesh(){
            int _count = 0;
            while(_count < 2){
                surface.UpdateNavMesh(surface.navMeshData);
                _count++;
                yield return new WaitForEndOfFrame();
            }
        }
        void PlaceSettlement(){
            PlaceRoads();
            PlaceWalls();
            PlaceFloors();
            StartCoroutine(UpdateNavmesh());
        }

        void PlaceObject(GameObject prefab, Vector2Int pos, GameObject parent){
            GameObject _go = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            _go.transform.parent = parent.transform;
            _go.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        }
        void PlaceRoads(){
            foreach (Vector2Int _pos in settlement.roadPositionsList){
                // PlaceObject(roadPrefab, _pos, roadParent);
                floorTileMap.SetTile(new Vector3Int(_pos.x, _pos.y, 0), roadTile);
                // GameObject _go = GameObject.Instantiate(roadPrefab, Vector3.zero, Quaternion.identity);
                // _go.transform.parent = roadParent.transform;
                // _go.transform.localPosition = new Vector3(_pos.x, _pos.y, 0);
            }
        }
        void PlaceWalls(){
            foreach (Vector2Int _pos in settlement.wallPositionsList){
                // PlaceObject(wallPrefab, _pos, wallParent);
                wallTileMap.SetTile(new Vector3Int(_pos.x, _pos.y, 0), wallTile);
                // GameObject _go = GameObject.Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
                // _go.transform.parent = wallParent.transform;
                // _go.transform.localPosition = new Vector3(_pos.x, _pos.y, 0)
            }
            surface.UpdateNavMesh(surface.navMeshData);
        }
        void PlaceFloors(){
            foreach (Vector2Int _pos in settlement.buildingPositionsList){
                // PlaceObject(floorPrefab, _pos, floorParent);
                floorTileMap.SetTile(new Vector3Int(_pos.x, _pos.y, 0), floorTile);
                // floorTileMap.SetTile()
                
                // GameObject.Instantiate(floorPrefab, new Vector3(_pos.x, _pos.y, 0), Quaternion.identity);
            }
        }
        void InitBuildings(){
            for(int i=0; i<settlement.housePositions.Count; i++){
                GridBuilding _gridBuilding = new GridBuilding(settlement.houseSizes[i], settlement.housePositions[i]);
                GridBuildingBehavior _gridBuildingBehavior = GameObject.Instantiate(this.buildingPrefab).GetComponent<GridBuildingBehavior>();
                _gridBuildingBehavior.gridBuilding = _gridBuilding;
                _gridBuildingBehavior.GenerateBuilding();
            }
            for(int i=0; i<settlement.farmPositions.Count; i++){
                GridBuilding _gridBuilding = new GridBuilding(new Vector2Int(1,1), settlement.farmPositions[i]);
                GameObject _farm = GameObject.Instantiate(this.farmPrefab);
                settlement.farms.Add(new WorkAssignment(_farm));
            }
        }
        
    }
}