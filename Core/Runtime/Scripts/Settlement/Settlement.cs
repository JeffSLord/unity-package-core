using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core{
    [System.Serializable]
    public class Settlement
    {
        public static System.Random Rand = new System.Random();
        // public List<Character> characters;
        public List<GameObject> houses;
        public List<WorkAssignment> farms;
        
        public List<Vector2Int> housePositions;
        public List<Vector2Int> houseSizes;
        public List<Vector2Int> farmPositions;

        public Settlement(){
            this.houses = new List<GameObject>();
            this.farms = new List<WorkAssignment>();
            this.housePositions = new List<Vector2Int>();
            this.houseSizes = new List<Vector2Int>();
            this.farmPositions = new List<Vector2Int>();
            InitSettlement();
        }

        void InitSettlement(){
            Vector2Int _buildingPosition;
            Vector2Int _buildingSize;
            for (int i = 0; i < 5; i++){
                bool _success = false;
                while (!_success){
                    _buildingSize = GridBuilding.GenerateRectBuildingSize();
                    _buildingPosition = GridBuilding.GenerateBuildingPosition();
                    if(GridBuildingManager.instance.CheckValidRectPlacement(_buildingSize, _buildingPosition)){
                        _success = true;
                        GridBuildingManager.instance.BuildRect(_buildingSize, _buildingPosition);
                        this.housePositions.Add(_buildingPosition);
                        this.houseSizes.Add(_buildingSize);
                    }
                }           
            }
            for(int i = 0; i < 5; i++){
                _buildingPosition = GridBuilding.GenerateBuildingPosition();
                this.farmPositions.Add(_buildingPosition);
                // GameObject _farm = GameObject.Instantiate(farmPrefab, new Vector3(_buildingPosition.x, _buildingPosition.y, 0), Quaternion.identity);
                // farms.Add(new WorkAssignment(_farm));
            }     
        }
    }

}