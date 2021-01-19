using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core
{
    [System.Serializable]
    public class Character
    {
        public static List<Character> AllCharacters { get; set; } = new List<Character>();
        // public CharacterBehavior Behavior{get;set;}
        public CharacterBehavior Behavior;
        // public int ID{get;set;}
        public int ID;
        // public string Name{get;set;}
        public string Name;
        // public BehaviorTree BT{get;set;}
        public BehaviorTree BT;
        // public List<WorkPriority> WorkPriority{get;set;}
        // public List<WorkPriority> WorkPriority;
        // public Settlement Settlement{get;set;}
        // public Settlement Settlement;
        // public int Faction{get;set;}
        public int Faction;
        // public int Age {get;set;}
        public int Age;
        // public bool IsPlayer{get;set;}
        public bool IsPlayer;
        // public List<int> Enemies;
        public List<int> EnemyIDsDetected;
        public List<int> EnemyIDsInMelee;
        public List<int> EnemyIDsInRange;
        // public Context Context;
        public NavMeshAgent NavMeshAgent;
        public Animator Animator;
        public Vector3 MovePosition;
        public Transform MoveTransform;
        public float MoveSpeed;
        public float StoppingDistance;
        public float Hunger;
        public float MaxHunger;
        public float Energy;
        public float MaxEnergy;
        public float Health;
        public float MaxHealth;
        public bool OnQuest;
        public float AttackRange;
        public bool IsFighter;
        public float AttackCooldown;
        public float MaxAttackCooldown;
        public float BlockCooldown;
        public float MaxBlockCooldown;
        public bool KnockedOut;
        public bool IsAttacking;
        public bool CanMove;
        public bool CanAttack;
        public bool CanBlock;
        public bool CanDodge;
        // public BehaviorTree BT;


        public Character(CharacterBehavior behavior, bool isPlayer = false)
        {
            this.Behavior = behavior;
            this.ID = Character.AllCharacters.Count;
            Character.AllCharacters.Add(this);
            this.Faction = 1;

            this.EnemyIDsInMelee = new List<int>();
            this.EnemyIDsInRange = new List<int>();
            this.EnemyIDsDetected = new List<int>();

            this.StoppingDistance = 2.0f;
            this.MoveSpeed = 3.5f;

            this.Hunger = 100.0f;
            this.MaxHunger = 100.0f;

            this.Energy = 100.0f;
            this.MaxEnergy = 100.0f;

            this.Health = 100.0f;
            this.MaxHealth = 100.0f;

            this.OnQuest = false;

            this.AttackRange = 2.0f;

            this.IsFighter = true;

            this.AttackCooldown = 0.0f;
            this.MaxAttackCooldown = 2.0f;
            this.BlockCooldown = 0.0f;
            this.MaxBlockCooldown = 3.0f;
            this.KnockedOut = false;
            this.IsAttacking = false;
            this.CanMove = true;
            this.CanAttack = true;
            this.CanBlock = true;
            this.CanDodge = true;



            // this.Context = new Context(this.ID);
            // this.BT = new BehaviorTree();
            // this.VisibleCharacterIDs = new List<int>();
            // this.WorkPriority = new List<WorkPriority>{
            //     new WorkPriority(WorkType.FARM, 1),
            //     new WorkPriority(WorkType.HAUL, 1)
            // };
            this.IsPlayer = isPlayer;
        }
        public static Character GetCharacter(int id)
        {
            return Character.AllCharacters[id];
        }

        public bool IsEnemy(Character character)
        {
            return character.Faction != this.Faction;
        }
        public Character FindClosestEnemy()
        {
            if (EnemyIDsDetected.Count != 0)
            {
                Character _closest = null;
                float _dist = -1.0f;
                for (int i = 0; i < EnemyIDsDetected.Count; i++)
                {
                    if(Character.AllCharacters[EnemyIDsDetected[i]].KnockedOut){
                        continue;
                    }
                    float _currDist = Vector3.Distance(this.Behavior.transform.position, Character.AllCharacters[EnemyIDsDetected[i]].Behavior.transform.position);
                    if (i == 0)
                    {
                        _dist = _currDist;
                        _closest = Character.AllCharacters[EnemyIDsDetected[i]];
                    }
                    else
                    {
                        if (_currDist < _dist)
                        {
                            _dist = _currDist;
                            _closest = Character.AllCharacters[EnemyIDsDetected[i]];
                        }
                    }

                }
                return _closest;
            }
            return null;
        }

        public AttackResult Attack(Character c){
            // this.CanMove = false;
            // this.IsAttacking = true;
            this.AttackCooldown = this.MaxAttackCooldown;
            StopMovement();
            AttackResult _res = c.OnAttacked(this);
            this.Animator.Play("Attack1");
            if(_res == AttackResult.Hit){
                c.Damage(3.0f);
            }
            return _res;

        }
        public AttackResult OnAttacked(Character c){
            // try to block
            if(this.BlockCooldown <= 0 && this.CanBlock){
                this.BlockCooldown = this.MaxBlockCooldown;
                Debug.Log("Attack Blocked!");
                this.Animator.Play("RollLeft");
                return AttackResult.Blocked;
            } else{
                // probably calculated if it will miss or not dependent on skill
                return AttackResult.Hit;
            }
        }
        public void Damage(float dmg){
            this.Health -= dmg;
            if(this.Health <= 0.0f){
                this.KnockedOut = true;
                this.Behavior.gameObject.active = false;
            }
        }

        public void StopMovement(){
            this.NavMeshAgent.Stop();
            this.NavMeshAgent.ResetPath();
        }

    }
}



// 