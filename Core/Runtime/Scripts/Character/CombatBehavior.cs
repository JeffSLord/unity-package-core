using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core
{
public class CombatBehavior : MonoBehaviour
{
    public Combat combat;
    // Start is called before the first frame update
    void Awake(){
        this.combat = new Combat();
    }
    // void Start()
    // {
    //     this.combat = new Combat();
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     if(combat.Enemies.Count > 0){
    //         Character _char = Character.GetCharacter(combat.Enemies[0]);
    //         this.GetComponent<NavMeshAgent>().SetDestination(_char.Behavior.transform.position);
    //         this.GetComponent<NavMeshAgent>().stoppingDistance = 5.0f; // Attack distance
    //         if(Vector3.Distance(this.transform.position, _char.Behavior.transform.position) <= 5.0f){
    //             Debug.Log("ATTACK: " + _char.Name);
    //         }
    //     }
    // }
}
}