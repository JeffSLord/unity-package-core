using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lord.Core {
    public class Stage : MonoBehaviour {
        public List<Waypoint> waypoints;
        void Awake() {
            waypoints = new List<Waypoint>();
        }
        void Start() {
            waypoints = GetComponentsInChildren<Waypoint>().ToList();
        }
    }
}