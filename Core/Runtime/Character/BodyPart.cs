using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour {
    public Collider[] bodyColliders;
    // Start is called before the first frame update
    void Start() {
        bodyColliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update() {

    }
}