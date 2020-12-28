using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core{
    [System.Serializable]
    public class Settlement
    {
        public static System.Random Rand = new System.Random();
        // public List<Character> characters;
        public static List<Settlement> allSettlements = new List<Settlement>();
        public int ID{get;set;}
        public List<GameObject> houses;
        public List<WorkAssignment> farms;
        
        // public List<Vector2Int> housePositions;
        // public List<Vector2Int> houseSizes;
        public List<Vector2Int> farmPositions;
        public int settlementRadius;
        public List<Vector2Int> roadPositionsList;
        public Dictionary<Vector2Int, bool> roadPositionsDict;
        public List<Vector2Int> buildingPositionsList;
        public Dictionary<Vector2Int, bool> buildingPositionsDict;
        public List<Vector2Int> wallPositionsList;

        public Settlement(){
            this.ID = Settlement.allSettlements.Count;
            Settlement.allSettlements.Add(this);
            this.houses = new List<GameObject>();
            this.farms = new List<WorkAssignment>();
            // this.housePositions = new List<Vector2Int>();
            // this.houseSizes = new List<Vector2Int>();
            this.farmPositions = new List<Vector2Int>();
            this.settlementRadius = 50;
            this.roadPositionsList = new List<Vector2Int>();
            this.roadPositionsDict = new Dictionary<Vector2Int, bool>();
            this.buildingPositionsList = new List<Vector2Int>();
            this.buildingPositionsDict = new Dictionary<Vector2Int, bool>();
            this.wallPositionsList = new List<Vector2Int>();
            GenerateSettlement();
        }
        public static Settlement GetSettlement(int id){
            return Settlement.allSettlements[id];
        }

        private void GenerateSettlement(){
            GenerateRoadPositions();
            GenerateBuildingPositions();
            GenerateFarmPositions();
        }

        // private void GenerateBuildings(){
            // Vector2Int _buildingPosition;
            // Vector2Int _buildingSize;
            // for (int i = 0; i < 5; i++){
            //     bool _success = false;
            //     while (!_success){
            //         _buildingSize = GridBuilding.GenerateRectBuildingSize();
            //         _buildingPosition = GridBuilding.GenerateBuildingPosition();
            //         if(GridBuildingManager.instance.CheckValidRectPlacement(_buildingSize, _buildingPosition)){
            //             _success = true;
            //             GridBuildingManager.instance.BuildRect(_buildingSize, _buildingPosition);
            //             this.housePositions.Add(_buildingPosition);
            //             this.houseSizes.Add(_buildingSize);
            //         }
            //     }           
            // }
            // for(int i = 0; i < 5; i++){
            //     _buildingPosition = GridBuilding.GenerateBuildingPosition();
            //     this.farmPositions.Add(_buildingPosition);
            //     // GameObject _farm = GameObject.Instantiate(farmPrefab, new Vector3(_buildingPosition.x, _buildingPosition.y, 0), Quaternion.identity);
            //     // farms.Add(new WorkAssignment(_farm));
            // }
        // }
        private void GenerateBuildingPositions(){
            for (int i = 0; i < 10; i++){
                bool _searching = true;
                int _count = 0;
                while(_searching && _count < 10){
                    Vector2Int _buildingPosition = GridBuilding.GenerateBuildingPosition();
                    Vector2Int _buildingSize = GridBuilding.GenerateRectBuildingSize(10, 20);
                    Direction _direction = (Direction)Settlement.Rand.Next(0,4);
                    if(IsValidBuilding(_buildingPosition, _buildingSize, _direction)){
                        GenerateBuildingPosition(_buildingPosition, _buildingSize, _direction);
                        _searching = false;
                    }
                    _count++;
                }
            }
        }
        private void GenerateBuildingPosition(Vector2Int startPos, Vector2Int size, Direction direction=Direction.NORTH){
            for (int i = 0; i < size.x; i++){
                for(int j = 0; j < size.y; j++){
                    Vector2Int _offset = new Vector2Int(i,j);
                    Vector2Int _currentPosition = GenerateNextBuildingPosition(startPos, _offset, direction);
                    buildingPositionsList.Add(_currentPosition);
                    buildingPositionsDict.Add(_currentPosition, true);
                    if(i==0 || i==size.x-1 || j==0 || j==size.y-1){
                        switch (direction){
                            case Direction.NORTH:
                                if(j==0 && i==size.x/2){
                                    continue;
                                }
                                break;
                            case Direction.SOUTH:
                                goto case Direction.NORTH;
                            case Direction.EAST:
                                if(i==0 && j==size.y/2){
                                    continue;
                                }
                                break;
                            case Direction.WEST:
                                goto case Direction.EAST;
                            default:
                                break;
                        }
                        wallPositionsList.Add(_currentPosition);
                    }
                }
            }
        }
        private Vector2Int GenerateNextBuildingPosition(Vector2Int startPos, Vector2Int offset, Direction direction){
            Vector2Int _position;
            switch (direction){
                case Direction.NORTH:
                    _position = new Vector2Int(startPos.x + offset.x, startPos.y + offset.y);
                    break;
                case Direction.EAST:
                    _position = new Vector2Int(startPos.x + offset.x, startPos.y + offset.y);
                    break;
                case Direction.SOUTH:
                    _position = new Vector2Int(startPos.x + offset.x, startPos.y - offset.y);
                    break;
                case Direction.WEST:
                    _position = new Vector2Int(startPos.x - offset.x, startPos.y - offset.y);
                    break;
                default:
                    _position = startPos;
                    break;
            }
            return _position;
        }

        private void GenerateRoadPositions(){
            for(int i=0;i<4;i++){
                GenerateRoadPosition(new Vector2Int(0,0), settlementRadius, (Direction)i);
            }
        }
        private void GenerateRoadPosition(Vector2Int startPos, int length, Direction direction, float spawnPercent=0.1f){
            for (int i = 0; i < length; i++){
                Vector2Int _currentPosition;
                switch (direction){
                    case Direction.NORTH:
                        _currentPosition = new Vector2Int(startPos.x, startPos.y + i);
                        break;
                    case Direction.SOUTH:
                        _currentPosition = new Vector2Int(startPos.x, startPos.y - i);
                        break;
                    case Direction.EAST:
                        _currentPosition = new Vector2Int(startPos.x + i, startPos.y);
                        break;
                    case Direction.WEST:
                        _currentPosition = new Vector2Int(startPos.x - i, startPos.y);
                        break;
                    default:
                        _currentPosition = startPos;
                        break;
                }
                roadPositionsList.Add(_currentPosition);
                if(Settlement.Rand.NextDouble() < spawnPercent){
                    List<Direction> _directions = new List<Direction>();
                    if(direction == Direction.NORTH || direction == Direction.SOUTH){
                        _directions.Add(Direction.EAST);
                        _directions.Add(Direction.WEST);
                    }
                    else{
                        _directions.Add(Direction.NORTH);
                        _directions.Add(Direction.SOUTH);
                    }
                    Direction _direction = _directions[Settlement.Rand.Next(_directions.Count)];
                    GenerateRoadPosition(_currentPosition, length/2, _direction, spawnPercent=0.0f);
                }
            }
        }

        private void GenerateFarmPositions(){
            for (int i = 0; i < 10; i++){
                bool _searching = true;
                int _count = 0;
                while(_searching && _count < 10){
                    Vector2Int _buildingPosition = GridBuilding.GenerateBuildingPosition();
                    Vector2Int _buildingSize = new Vector2Int(1,1);
                    Direction _direction = (Direction)Settlement.Rand.Next(0,4);
                    if(IsValidBuilding(_buildingPosition, _buildingSize, _direction)){
                        GenerateFarmPosition(_buildingPosition, _buildingSize);
                        _searching = false;
                    }
                    _count++;
                }
            }
        }
        private void GenerateFarmPosition(Vector2Int pos, Vector2Int size){
            farmPositions.Add(pos);
        }
        private bool IsValidBuilding(Vector2Int startPos, Vector2Int size, Direction direction){
            // alter startpos and size so that buildings do not spawn right next to each other
            // startPos = new Vector2Int(startPos.x-1, startPos.y-1);
            // size = new Vector2Int(size.x+2, size.y+2);
            for (int i = -1; i < size.x + 1; i++){
                for(int j = -1; j < size.y + 1; j++){
                    Vector2Int _currentPosition = GenerateNextBuildingPosition(startPos, new Vector2Int(i,j), direction);
                    bool _exists;
                    if(buildingPositionsDict.TryGetValue(_currentPosition, out _exists)){
                        return false;
                    }
                }
            }
            return true;
        }
    }
}