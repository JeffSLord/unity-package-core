using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BehaviorTreeBehavior))]
    public class CharacterBehavior : WorldObject
    {
        public Character character;
        public Selectable currentSelection;
        [SerializeField]
        private bool isPlayer;
        private Vector3 lastPosition;

        protected override void Awake()
        {
            character = new Character(this, isPlayer = this.isPlayer);
            Debug.Log("Character ID: " + character.ID);
        }
        protected override void Start()
        {
            base.Start();
            character.NavMeshAgent = GetComponent<NavMeshAgent>();
            character.Animator = GetComponent<Animator>();
            character.BT = GetComponent<BehaviorTreeBehavior>().BT;
            character.Rigidbody = GetComponent<Rigidbody>();

            // Test
            // character.BT.HighPriorityNode = CharacterBT.CharacterBehavior(character.BT.Context);
        }
        protected override void Select0(GameObject selector, int option = 0)
        {
            Debug.Log("Actual override is working? can this work?");
        }
        public void SetCharacterDestination(Vector3 position, float stoppingDistance)
        {
            character.BT.Context.SetContext("targetPosition", position);
            character.BT.Context.SetContext("stoppingDistance", stoppingDistance);
            character.BT.SetManualNode(MoveBT.MoveToPoint(character.BT.Context));

        }

        public void SetVisibility(bool isVisible)
        {
            if (!this.isPlayer)
            {
                SpriteRenderer[] _renderers = GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer _ren in _renderers)
                {
                    if (isVisible)
                    {
                        _ren.enabled = true;
                    }
                    else
                    {
                        _ren.enabled = false;
                    }
                }
            }
        }
        // private void OnTriggerEnter2D(Collider2D other) {
        //     Debug.Log("TEST TEST TEST TEST" + other.gameObject.name);
        // }

        void Update()
        {
            // this.character.Animator.SetBool("IsMoving", this.character.Rigidbody.velocity.magnitude > 0);
            if(this.transform.position != lastPosition){
                this.character.Animator.SetBool("IsMoving", true);
            } else{
                this.character.Animator.SetBool("IsMoving", false);
            }
            lastPosition = this.transform.position;
            if (character.OnQuest)
            {
                // quest behavior
            }
            // enemies detected
            if (character.EnemyIDsDetected.Count > 0)
            {
                // fight or flee?
                if (character.IsFighter)
                {
                    Character _closestEnemy = character.FindClosestEnemy();
                    if (_closestEnemy != null)
                    {
                        transform.rotation = Quaternion.LookRotation((_closestEnemy.Behavior.transform.position - transform.position), Vector3.up);
                        float _distance = Vector3.Distance(this.transform.position, _closestEnemy.Behavior.transform.position);
                        // are you close enough to attack?
                        if (_distance < character.AttackRange)
                        {
                            character.StopMovement();
                            // are your attacks on cooldown?
                            if (character.AttackCooldown <= 0.0f && character.CanAttack)
                            {
                                // try to attack!
                                Debug.Log("I am attacking!");
                                this.character.Attack(_closestEnemy);
                            }
                            else
                            {
                                // wait wait for cooldown
                            }
                        }
                        else
                        {
                            // you aren't close enough, so move closer
                            if(this.character.CanMove){
                                this.character.NavMeshAgent.SetDestination(_closestEnemy.Behavior.transform.position);
                            }else{
                                this.character.StopMovement();
                            }
                        }
                    }

                }
                else
                {
                    // flee run away
                }
                // fight

                // flee
                // run away
            }
            if (character.AttackCooldown > 0)
            {
                character.AttackCooldown -= Time.deltaTime;
            }
            if (character.BlockCooldown > 0)
            {
                character.BlockCooldown -= Time.deltaTime;
            }
            // hungry
            else if ((character.Hunger / character.MaxHunger) < 0.25f)
            {
                Debug.Log("I am Hungry");
                // is there food in your inventory?
                // is there food somewhere else?
                // go buy food?
            }
            // tired, sleep
            else if ((character.Energy / character.MaxEnergy) < 0.25f)
            {
                Debug.Log("I am tired");
                // go to sleep
            }

        }
    }
}