using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class GridBuildingBehavior : MonoBehaviour
    {
        public GridBuilding gridBuilding;
        public GameObject wallPrefab;
        public GameObject parentGO;
        public void GenerateBuilding(){
            this.gameObject.transform.position = new Vector3(gridBuilding.position.x, gridBuilding.position.y, 0);
            for(int i = 0; i < gridBuilding.size.x; i++){
                for(int j = 0; j < gridBuilding.size.y; j++){
                    if(i==0 || i==gridBuilding.size.x-1 || j==0 || j==gridBuilding.size.y-1){
                        // GameObject _wall = Instantiate(wallPrefab, new Vector3(i,j,0), Quaternion.identity);
                        GameObject _wall = Instantiate(wallPrefab, new Vector3(0,0,0), Quaternion.identity);
                        _wall.transform.parent = parentGO.transform;
                        _wall.transform.localPosition = new Vector3(i, j, 0);
                        if(i==gridBuilding.size.x/2 && j == gridBuilding.size.y-1){
                            Destroy(_wall);
                        }
                    }
                }
            }
        }
    }
}