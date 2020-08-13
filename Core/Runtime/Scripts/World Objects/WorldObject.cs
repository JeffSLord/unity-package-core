using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Rigidbody))]
    public class WorldObject : MonoBehaviour {
        public WorldObjectData worldObjectData;
    }
}