using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {

    public static class WorkBT {
        private static NodeStates GetWorkAssignment(Context context){
            List<WorkPriority> _workPriorities;
            if(!context.data.TryGetValue<List<WorkPriority>>("workPriority", out _workPriorities)){
                return NodeStates.FAILURE;
            }
            Settlement _settlement;
            if(!context.data.TryGetValue<Settlement>("settlement", out _settlement)){
                return NodeStates.FAILURE;
            }
            Character _character;
            if(!context.data.TryGetValue<Character>("character", out _character)){
                return NodeStates.FAILURE;
            }
            foreach (WorkPriority _priority in _workPriorities){
                if(_priority.priority > 0){
                    switch (_priority.workType){
                        case WorkType.FARM:
                            foreach (WorkAssignment workAssignment in _settlement.farms){
                                if(workAssignment.IsAvailable()){
                                    workAssignment.AssignCharacter(_character);
                                    context.SetContext<WorkAssignment>("workAssignment", workAssignment);
                                    context.SetContext<Vector3>("targetPosition", workAssignment.workObject.transform.position);
                                    return NodeStates.SUCCESS;
                                }
                            }
                            break;
                        default:
                            break;
                    }   
                }
            }
            return NodeStates.FAILURE;
        }
        public static Node GetWorkAssignmentNode(Context context){
            return new TaskContextNode(GetWorkAssignment, context, "Get work assignment");
        }
        
        private static NodeStates WorkAssignment(Context context){
            // if working running
            // if not working success
            WorkAssignment _workAssignment;
            context.data.TryGetValue<WorkAssignment>("workAssignment", out _workAssignment);
            GameObject.Destroy(_workAssignment.workObject);
            return NodeStates.SUCCESS;
        }
        public static Node WorkAssignmentNode(Context context){
            return new TaskContextNode(WorkAssignment, context, "Work the assigned work.");
        }

        public static Node Work(Context context){
            return new SequenceSeries(new List<Node>{
                WorkBT.GetWorkAssignmentNode(context),
                MoveBT.MoveToPoint(context),
                WorkBT.WorkAssignmentNode(context)
            });
        }

        // private static NodeStates WorkResourceSource(Context context) {
        //     ResourceSource _resourceSource;
        //     Character _character;
        //     if (context.data.TryGetValue<ResourceSource>("resourceSource", out _resourceSource)) {
        //         if (context.data.TryGetValue<Character>("character", out _character)) {
        //             if (_resourceSource != null) {
        //                 _resourceSource.AssignCharacter(_character);
        //                 return NodeStates.RUNNING;
        //             } else {
        //                 _character.animator.Rebind();
        //                 return NodeStates.SUCCESS;
        //             }
        //         }
        //     }
        //     return NodeStates.FAILURE;
        // }
        // public static Node WorkResourceSourceNode(Context context) {
        //     return new TaskContextNode(WorkResourceSource, context, "Work Resource Source");
        // }
        // private static NodeStates WorkResourceSourceAnim(Context context) {
        //     Character _character;
        //     if (context.data.TryGetValue<Character>("character", out _character)) {
        //         _character.animator.Play("Attack", 0);
        //         return NodeStates.SUCCESS;
        //     } else { return NodeStates.FAILURE; }
        // }
        // public static Node WorkResourceSourceAnimNode(Context context) {
        //     return new TaskContextNode(WorkResourceSourceAnim, context, "Animation");
        // }
        // private NodeStates WorkResourceSourceFinish() {
        //     if (workObject)
        //         this.character.animator.Rebind();
        //     return NodeStates.SUCCESS;
        // }
        // public TaskNode WorkResourceSourceFinishNode() {
        //     return new TaskNode(WorkResourceSourceFinishNode, "Work Finsished");
        // }
        // public static Node WorkResourceSourceSequence(Context context) {
        //     // ResourceSource _resourceSource = workObject.GetComponent<ResourceSource>();
        //     // WOD_ResourceSource _wod = (WOD_ResourceSource) _resourceSource.worldObjectData;
        //     // this.stoppingDistance = _wod.harvestDistance;
        //     return new SequenceSeries(new List<Node> {
        //         MoveBT.MoveToPoint(context),
        //         MoveBT.TurnNode(context),
        //         WorkResourceSourceAnimNode(context),
        //         WorkResourceSourceNode(context)
        //     });
        // }
    }

}