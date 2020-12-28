using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    [System.Serializable]
    public class WorkPriority
    {
        public WorkType workType;
        public int priority;
        public WorkPriority(WorkType workType, int priority){
            this.workType = workType;
            this.priority = priority;
        }
    }
}