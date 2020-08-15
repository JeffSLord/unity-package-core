using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class ResourceSource : WorldObjectSelect {
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
                character.currentSelect = null;
            }
            if (assignedCharacters.Count == 0) {
                harvestRunning = false;
                StopAllCoroutines();
            }
        }
        private void ResourceDepleted() {
            foreach (Character character in assignedCharacters) {
                character.currentSelect = null;
            }
            Destroy(this.gameObject);
        }

        // protected override void Select0(GameObject selector, int option = 0) {

        // }
        protected override void Select1(GameObject selector, int option = 0) {
            PlayerController _playerController = selector.GetComponent<PlayerController>();
            if (_playerController != null) {
                // AssignCharacter(_playerController.character);
                WorkBT workBT = new WorkBT(_playerController.character, this.gameObject, this.harvestPoint.position);
                _playerController.character.GetComponent<BehaviorTree>().SetNode(workBT.WorkResourceSourceSequence());
            } else if (selector.GetComponent<Character>() != null) {
                // AssignCharacter(selector.GetComponent<Character>());
            }
        }
        // public void Select(GameObject selector, int option = 0) {
        //     if (option == 1) {
        //         PlayerController _playerController = selector.GetComponent<PlayerController>();
        //         if (_playerController != null) {
        //             AssignCharacter(_playerController.character);
        //         } else if (selector.GetComponent<Character>() != null) {
        //             AssignCharacter(selector.GetComponent<Character>());
        //         }
        //     }
        // }

        // public void Deselect(GameObject selector) {
        // UnassignCharacter(selector.GetComponent<PlayerController>().character);
        // }
        protected override void Deselect(GameObject selector) {
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