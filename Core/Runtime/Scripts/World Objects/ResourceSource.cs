using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class ResourceSource : WorldObject {
        // public Resource resource;
        public float resourcesLeft;
        // public float resourcesHarvested;
        public float workLeft;
        // public float workDone;
        public Transform harvestPoint;
        public Transform resourceSpawnPoint;
        // public float harvestDistance;
        public List<Character> assignedCharacters;
        public bool harvestRunning = false;
        public int charactersInRange = 0;
        // Start is called before the first frame update
        private WOD_ResourceSource wod_ResourceSource;
        protected override void Start() {
            base.Start();
            assignedCharacters = new List<Character>();
            wod_ResourceSource = (WOD_ResourceSource) worldObjectData;
            resourcesLeft = wod_ResourceSource.totalResources;
            workLeft = wod_ResourceSource.totalWork;
        }
        public void AssignCharacter(Character character) {
            Debug.Log("Begin Harvesting");
            character.navMeshAgent.destination = harvestPoint.position;
            character.navMeshAgent.stoppingDistance = wod_ResourceSource.harvestDistance;
            if (!assignedCharacters.Contains(character)) {
                assignedCharacters.Add(character);
            }
            if (!harvestRunning) {
                StartCoroutine(HarvestCoroutine());
            }
        }
        public void UnassignCharacter(Character character) {
            if (assignedCharacters.Contains(character)) {
                assignedCharacters.Remove(character);
                character.currentSelection = null;
            }
            if (assignedCharacters.Count == 0) {
                harvestRunning = false;
                StopAllCoroutines();
            }
        }
        private void ResourceDepleted() {
            foreach (Character character in assignedCharacters) {
                character.currentSelection = null;
            }
            // remove navmesh obstocle
            Destroy(this.gameObject);
        }

        // protected override void Select0(GameObject selector, int option = 0) {

        // }
        protected override void Select1(GameObject selector, int option = 0) {
            PlayerController _playerController = selector.GetComponent<PlayerController>();
            if (_playerController != null) {
                // AssignCharacter(_playerController.character);
                // WorkBT workBT = new WorkBT(_playerController.character, this.gameObject, this.harvestPoint.position);
                // _playerController.character.bt.context.resourceSource = this;
                BehaviorTree _bt = _playerController.character.bt;
                _bt.SetContext("resourceSource", this);
                _bt.SetManualNode(WorkBT.WorkResourceSourceSequence(_bt.context));
            } else if (selector.GetComponent<Character>() != null) {
                // AssignCharacter(selector.GetComponent<Character>());
            }
        }
        protected override void Deselect(GameObject selector, int option = 0) {
            UnassignCharacter(selector.GetComponent<PlayerController>().character);
        }

        private IEnumerator HarvestCoroutine() {
            harvestRunning = true;
            while (harvestRunning) {
                yield return new WaitForSeconds(1.0f);
                foreach (Character _char in assignedCharacters) {
                    if (Vector3.Distance(_char.transform.position, harvestPoint.position) <= wod_ResourceSource.harvestDistance) {
                        workLeft = workLeft - 1;
                        resourcesLeft = resourcesLeft - 1;
                        GameObject _instantiated = Instantiate(wod_ResourceSource.spawnableResource, resourceSpawnPoint.position, Quaternion.identity);
                        _instantiated.transform.parent = BuildManager.instance.itemRoot;
                        if (resourcesLeft == 0) {
                            harvestRunning = false;
                            ResourceDepleted();
                            yield break;
                        }
                    }
                }
            }
        }
    }
}