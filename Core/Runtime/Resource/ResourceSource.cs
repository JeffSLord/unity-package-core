using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    public class ResourceSource : MonoBehaviour, ISelectable {
        public Resource resource;
        public float resourcesLeft;
        public float harvestLeft;
        public Transform harvestPoint;
        public Transform resourceSpawnPoint;
        public float harvestDistance;
        public List<Character> assignedCharacters;
        public bool harvestRunning = false;
        public int charactersInRange = 0;
        // Start is called before the first frame update
        void Start() {
            assignedCharacters = new List<Character>();
        }

        // Update is called once per frame
        void Update() {

        }
        public void AssignCharacter(Character character) {
            Debug.Log("Begin Harvesting");
            character.navMeshAgent.destination = harvestPoint.position;
            character.navMeshAgent.stoppingDistance = harvestDistance;
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

        public void Select(GameObject selector, int option = 0) {
            if (option == 1) {
                PlayerController _playerController = selector.GetComponent<PlayerController>();
                if (_playerController != null) {
                    AssignCharacter(_playerController.character);
                }
            }
        }

        public void Deselect(GameObject selector) {
            UnassignCharacter(selector.GetComponent<PlayerController>().character);
        }

        public void SelectGui() {
            throw new System.NotImplementedException();
        }
        private IEnumerator HarvestCoroutine() {
            harvestRunning = true;
            while (harvestRunning) {
                yield return new WaitForSeconds(1.0f);
                foreach (Character _char in assignedCharacters) {
                    if (Vector3.Distance(_char.transform.position, harvestPoint.position) <= harvestDistance) {
                        harvestLeft = harvestLeft - 1;
                        resourcesLeft = resourcesLeft - 1;
                        Instantiate(resource, resourceSpawnPoint.position, Quaternion.identity);
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