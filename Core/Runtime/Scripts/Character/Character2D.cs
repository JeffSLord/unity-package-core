using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character2D : Character {
        protected override void Start() {
            base.Start();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }
    }
}