using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public class WorkBT : MoveBT {
        public GameObject workObject;
        public Vector3 workPosition;
        public Character character;
        public WorkBT(Character character, GameObject workObject, Vector3 workPosition):
            base(character.navMeshAgent, workPosition) {
                this.character = character;
                this.workObject = workObject;
                this.workPosition = workPosition;
            }

        public NodeStates WorkResourceSource() {
            // Play animation?
            if (workObject != null) {
                // Debug.Log("Starting work part");
                Debug.Log(workObject.name);
                ResourceSource _resourceSource = workObject.GetComponent<ResourceSource>();
                _resourceSource.AssignCharacter(character);
                return NodeStates.RUNNING;
            } else {
                Debug.Log("Work is SUCCESS");
                return NodeStates.SUCCESS;
            }
        }
        public ActionNode WorkResourceSourceNode() {
            return new ActionNode(WorkResourceSource);
        }
        public NodeStates PickupResource() {
            return NodeStates.FAILURE;
            // WOD_ResourceSource _wodResourceSource = workObject.GetComponent<ResourceSource>().worldObjectData;

            // _wodResourceSource.spawn

            // Item _item = workObject.GetComponent<ResourceSource>().worldObjectData.
        }
        public Sequence WorkResourceSourceSequence() {
            ResourceSource _resourceSource = workObject.GetComponent<ResourceSource>();
            WOD_ResourceSource _wod = (WOD_ResourceSource) _resourceSource.worldObjectData;
            stoppingDistance = _wod.harvestDistance;
            return new Sequence(new List<Node> {
                MoveToPointNode(),
                WorkResourceSourceNode()
            });
        }
    }

}