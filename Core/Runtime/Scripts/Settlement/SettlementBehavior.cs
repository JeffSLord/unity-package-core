using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lord.Core
{
    public class SettlementBehavior : MonoBehaviour
    {
        public Settlement settlement;
        public GameObject characterPrefab;
        public GameObject buildingPrefab;
        public GameObject farmPrefab;

        void Start(){
            this.settlement = new Settlement();
            InitBuildings();
        }

        void InitCharacter(){

        }

        void InitBuildings(){
            for(int i=0; i<settlement.housePositions.Count; i++){
                GridBuilding _gridBuilding = new GridBuilding(settlement.houseSizes[i], settlement.housePositions[i]);
                GridBuildingBehavior _gridBuildingBehavior = GameObject.Instantiate(this.buildingPrefab).GetComponent<GridBuildingBehavior>();
                _gridBuildingBehavior.gridBuilding = _gridBuilding;
                _gridBuildingBehavior.GenerateBuilding();
            }
        }
        
    }
}