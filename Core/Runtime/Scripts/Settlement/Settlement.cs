using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lord.Core
{
    public class Settlement: MonoBehaviour
    {
        public int numCharacters;
        public int numFarms;
        public List<Character> characters;
        public List<GameObject> houses;
        public List<GameObject> farms;
        public GameObject characterPrefab;
        public GameObject housePrefab;
        public GameObject farmPrefab;
        

        void Start(){
            InitSettlement();

        }

        void InitSettlement(){
            numCharacters = Random.Range(5, 10);
            for (int i = 0; i < 20; i++)
            {
                bool _success = false;
                Vector2Int _buildingPosition;
                Vector2Int _buildingSize;
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
                    }
                }
                


                // houses.Add(_house);
                // GameObject _character = Instantiate(characterPrefab, new Vector3(_posx, _posy, 0), Quaternion.identity);
                // characters.Add(_character.GetComponent<Character>());
            }
        }

        void InitCharacter(){

        }

        void InitBuilding(){

        }
        
    }
}