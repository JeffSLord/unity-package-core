using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    [System.Serializable]
    public class GridBuilding
    {
        public Vector2Int size;
        public Vector2Int position;

        public GridBuilding(Vector2Int size, Vector2Int position){
            this.size = size;
            this.position = position;
        }

        public void SetSize(Vector2Int size){
            this.size = size;
        }
        public void SetPosition(Vector2Int position){
            this.position = position;
        }

        public static Vector2Int GenerateRectBuildingSize(int min=5, int max=10){
            int _x = Settlement.Rand.Next(min,max);
            int _y = Settlement.Rand.Next(min,max);
            return new Vector2Int(_x, _y);
        }
        public static Vector2Int GenerateBuildingPosition(int min=-15, int max=15){
            int _x = Settlement.Rand.Next(min,max);
            int _y = Settlement.Rand.Next(min,max);
            return new Vector2Int(_x, _y);
        }
    }

    
}