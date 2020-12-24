using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core
{
    public class Job
    {
        public string name;
        public GameObject jobGO;

        public Job(string name, GameObject jobGO){
            this.name = name;
            this.jobGO = jobGO;
        }
    }
}