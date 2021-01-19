using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public static class NeedsBT{
        public static NodeStates IsHungry(Context context){
            Debug.Log("Checking Hunger");
            if(context.Character.Hunger <= 20.0f){
                return NodeStates.SUCCESS;
            } else{
                return NodeStates.FAILURE;
            }
        }
        public static NodeStates IsTired(Context context){
            Debug.Log("Checking Energy");
            if(context.Character.Energy <= 20.0f){
                return NodeStates.SUCCESS;
            } else{
                return NodeStates.FAILURE;
            }
        }
    }
}