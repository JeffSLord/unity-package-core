using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public static class CombatBT
    {
        private static NodeStates IsHealthLessThan25(Context context){
            float _healthPercent;
            if(!context.data.TryGetValue<float>("healthPercent", out _healthPercent)){
                return NodeStates.FAILURE;
            }
            if(_healthPercent > 0.25f){
                return NodeStates.FAILURE;
            }
            else{
                return NodeStates.SUCCESS;
            }
        }

        private static NodeStates IsEnemyInMeleeRange(Context context){
            return NodeStates.FAILURE;
        }

    }
}