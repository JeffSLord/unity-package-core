using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData/Character", order = 1)]
public class CharacterData : ScriptableObject {
    public float moveSpeed = 1.0f;
    public float stoppingDistance = 1.0f;
}