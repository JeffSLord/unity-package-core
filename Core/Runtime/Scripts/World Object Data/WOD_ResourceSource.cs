using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [CreateAssetMenu(fileName = "WOD_ResourceSource", menuName = "ScriptableObjects/WorldObjectData/ResourceSource", order = 1)]

    public class WOD_ResourceSource : WorldObjectData {

        // public string objectName;
        // public string objectDescription;
        public GameObject spawnableResource;
        // public float resourcesLeft;
        public float totalHarvest;
        public float totalWork;
        // public Transform harvestPoint;
        // public Transform resourceSpawnPoint;
        public float harvestDistance;
        // public List<Character> assignedCharacters;
        // public bool harvestRunning = false;
        // public int charactersInRange = 0;

    }
}