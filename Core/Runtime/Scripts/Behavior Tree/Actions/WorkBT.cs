﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class WorkBT {
        // public GameObject workObject;
        // public Vector3 workPosition;
        // public WorkBT(Character character, GameObject workObject, Vector3 workPosition):
        //     base(character, workPosition) {
        //         this.workObject = workObject;
        //         this.workPosition = workPosition;
        //     }

        private static NodeStates WorkResourceSource(Dictionary<string, object> context) {
            ResourceSource _resourceSource;
            Character _character;
            if (context.TryGetValue<ResourceSource>("resourceSource", out _resourceSource)) {
                if (context.TryGetValue<Character>("character", out _character)) {
                    if (_resourceSource != null) {
                        _resourceSource.AssignCharacter(_character);
                        return NodeStates.RUNNING;
                    } else {
                        _character.animator.Rebind();
                        return NodeStates.SUCCESS;
                    }
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node WorkResourceSourceNode(Dictionary<string, object> context) {
            return new TaskContextNode(WorkResourceSource, context, "Work Resource Source");
        }
        private static NodeStates WorkResourceSourceAnim(Dictionary<string, object> context) {
            Character _character;
            if (context.TryGetValue<Character>("character", out _character)) {
                _character.animator.Play("Attack", 0);
                return NodeStates.SUCCESS;
            } else { return NodeStates.FAILURE; }
        }
        public static Node WorkResourceSourceAnimNode(Dictionary<string, object> context) {
            return new TaskContextNode(WorkResourceSourceAnim, context, "Animation");
        }
        // private NodeStates WorkResourceSourceFinish() {
        //     if (workObject)
        //         this.character.animator.Rebind();
        //     return NodeStates.SUCCESS;
        // }
        // public TaskNode WorkResourceSourceFinishNode() {
        //     return new TaskNode(WorkResourceSourceFinishNode, "Work Finsished");
        // }
        public static Node WorkResourceSourceSequence(Dictionary<string, object> context) {
            // ResourceSource _resourceSource = workObject.GetComponent<ResourceSource>();
            // WOD_ResourceSource _wod = (WOD_ResourceSource) _resourceSource.worldObjectData;
            // this.stoppingDistance = _wod.harvestDistance;
            return new SequenceSeq(new List<Node> {
                MoveBT.MoveToPoint(context),
                MoveBT.TurnNode(context),
                WorkResourceSourceAnimNode(context),
                WorkResourceSourceNode(context)
            });
        }
    }

}