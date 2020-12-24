using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class GridBuildingManager : MonoBehaviour
    {
        public static GridBuildingManager instance;
        public Dictionary<Vector2Int, bool> availableGrid;
        // Start is called before the first frame update
        void Start()
        {
            if (instance != null && instance != this) {
                Destroy(this.gameObject);
            } else {
                instance = this;
                availableGrid = new Dictionary<Vector2Int, bool>();
            }
        }

        public bool CheckValidRectPlacement(Vector2Int size, Vector2Int position){
            for (int i = position.x; i < position.x + size.x; i++){
                for(int j = position.y; j < position.y + size.y; j++){
                    bool _existing;
                    if(availableGrid.TryGetValue(new Vector2Int(i,j), out _existing)){
                        return false;
                    }
                }
            }
            return true;
        }

        public void BuildRect(Vector2Int size, Vector2Int position){
            for (int i = position.x; i < position.x + size.x; i++){
                for(int j = position.y; j < position.y + size.y; j++){
                    availableGrid.Add(new Vector2Int(i,j), true);
                }
            }
        }
        public void RemoveRect(Vector2Int size, Vector2Int position){
            for (int i = position.x; i < position.x + size.x; i++){
                for(int j = position.y; j < position.y + size.y; j++){
                    availableGrid.Remove(new Vector2Int(i,j));
                }
            }
        }
    }
}