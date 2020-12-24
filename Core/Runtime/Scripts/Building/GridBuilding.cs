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
                        GameObject _wall = Instantiate(wallPrefab, new Vector3(i,j,0), Quaternion.identity);
                        _wall.transform.parent = wallParent.transform;
                    }
                }
            }
        }
    }
}