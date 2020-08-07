using System.Collections;
using UnityEngine;

namespace Lord.Core {
    [RequireComponent(typeof(Lord.Core.PlayerRaycast))]
    public class PlayerControl : MonoBehaviour {
        private PlayerRaycast playerRaycast;
        private void Start() {
            playerRaycast = GetComponent<PlayerRaycast>();
        }
        private void Update() {

        }
    }
}