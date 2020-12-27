using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lord.Core
{
    public class Settlement: MonoBehaviour
    {
        // public int numCharacters;
        // public int numFarms;
        public List<CharacterBehavior> characters;
        public List<GameObject> houses;
        public List<WorkAssignment> farms;
        public GameObject characterPrefab;
        public GameObject housePrefab;
        public GameObject farmPrefab;
        
        void Awake(){
            houses = new List<GameObject>();
            farms = new List<WorkAssignment>();
        }

        void Start(){
            InitSettlement();

        }

        void InitSettlement(){
            // numCharacters = Random.Range(5, 10);
            Vector2Int _buildingPosition;
            Vector2Int _buildingSize;
            for (int i = 0; i < 5; i++)
            {
                bool _success = false;
                Debug.Log("GENERATING BUILDING: " + i.ToString());
                while (!_success)
                {
                    // int _posx = Random.Range(-100,100);
                    // int _posy = Random.Range(-100,100);
                    // GameObject _house = Instantiate(housePrefab, new Vector3(_posx, _posy, 0), Quaternion.identity); 
                    _buildingSize = GridBuilding.GenerateRectBuildingSize();
                    _buildingPosition = GridBuilding.GenerateBuildingPosition();
                    if(GridBuildingManager.instance.CheckValidRectPlacement(_buildingSize, _buildingPosition)){
                        _success = true;
                        GridBuildingManager.instance.BuildRect(_buildingSize, _buildingPosition);
                        GridBuilding _house = Instantiate(housePrefab, new Vector3(_buildingPosition.x, _buildingPosition.y,0), Quaternion.identity).GetComponent<GridBuilding>();
                        _house.GenerateBuilding(_buildingSize, _buildingPosition);
                        houses.Add(_house.gameObject);
                    }
                }           
            }
            for(int i = 0; i < 5; i++){
                _buildingPosition = GridBuilding.GenerateBuildingPosition();
                GameObject _farm = Instantiate(farmPrefab, new Vector3(_buildingPosition.x, _buildingPosition.y, 0), Quaternion.identity);
                farms.Add(new WorkAssignment(_farm));
            }     
        }

        void InitCharacter(){

        }

        void InitBuilding(){

        }
        
    }
}