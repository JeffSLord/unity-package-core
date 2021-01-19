using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public static class CharacterBT
    {
        public static Node CharacterBehavior(Context context){
            return new SequenceParallel(
                new List<Node>{
                    new Inverter(EnemyBT.EnemyHandler(context))
                    // EnemyBT.EnemyHandler(context)
                    ,new Inverter(new TaskContextNode(NeedsBT.IsHungry, context, "Is Hungry"))
                    ,new Inverter(new TaskContextNode(NeedsBT.IsTired, context, "Is Tired"))
                    // new SequenceSeries(
                    //     new List<Node>{

                    //     }
                    // )
                }
            );
        }
    }
}