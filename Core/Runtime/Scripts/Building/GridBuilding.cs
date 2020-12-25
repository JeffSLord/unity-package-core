using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class GridBuilding : MonoBehaviour
    {
        public Vector2Int size;
        public Vector2Int position;
        public GameObject wallPrefab;
        public GameObject wallParent;


        public void GenerateBuilding(Vector2Int size, Vector2Int position){
            this.size = size;
            this.position = position;
            for(int i = 0; i < size.x; i++){
                for(int j = 0; j < size.y; j++){
                    if(i==0 || i==size.x-1 || j==0 || j==size.y-1){
                        // GameObject _wall = Instantiate(wallPrefab, new Vector3(i,j,0), Quaternion.identity);
                        GameObject _wall = Instantiate(wallPrefab, new Vector3(0,0,0), Quaternion.identity);
                        _wall.transform.parent = wallParent.transform;
                        _wall.transform.localPosition = new Vector3(i, j, 0);
                    }
                }
            }
        }
        public static Vector2Int GenerateRectBuildingSize(int min=5, int max=10){
            int _x = Random.Range(min, max);
            int _y = Random.Range(min, max);
            return new Vector2Int(_x, _y);
        }

        // public static bool GenerateRectBuilding(Vector2Int size, Vector2Int position){
        //     return false;
        // }
        public static Vector2Int GenerateBuildingPosition(int min=-50, int max=50){
            int _x = Random.Range(min, max);
            int _y = Random.Range(min, max);
            return new Vector2Int(_x, _y);
        }
    }
}