using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BehaviorTreeBehavior))]
    public class CharacterBehavior : WorldObject {
        public Character character;
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        public Selectable currentSelection;
        public BehaviorTreeBehavior btBehavior;
        [SerializeField]
        private bool isPlayer;

        protected override void Awake() {
            character = new Character(this, isPlayer=this.isPlayer);
            Debug.Log("Character ID: " + character.ID);
        }
        protected override void Start() {
            base.Start();
            navMeshAgent = GetComponent<NavMeshAgent>();
            // navMeshAgent.speed = character.MoveSpeed;
            animator = GetComponent<Animator>();
            btBehavior = GetComponent<BehaviorTreeBehavior>();

            btBehavior.behaviorTree.Context.CharacterId = this.character.ID;

            // btBehavior.behaviorTree.Context.SetContext<Character>("character", this.character);
            // btBehavior.behaviorTree.Context.SetContext<CharacterBehavior>("characterBehavior", this);
            // btBehavior.behaviorTree.Context.SetContext<float>("moveSpeed", this.character.MoveSpeed);
            // btBehavior.behaviorTree.Context.SetContext<float>("stoppingDistance", this.character.StoppingDistance);
            // btBehavior.behaviorTree.Context.SetContextList<Waypoint>("waypoints", GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().waypoints);
            // btBehavior.behaviorTree.Context.SetContextList<WorkPriority>("workPriority", this.character.WorkPriority);
            // btBehavior.behaviorTree.Context.SetContext<Settlement>("settlement", GameObject.FindGameObjectWithTag("Settlement").GetComponent<SettlementBehavior>().settlement);
            // btBehavior.behaviorTree.HighPriorityNode = EnemyBT.EnemyDetectionNode(btBehavior.behaviorTree.Context);
            // btBehavior.behaviorTree.LowPriorityNode = WorkBT.Work(btBehavior.behaviorTree.Context);
        }
        protected override void Select0(GameObject selector, int option = 0) {
            Debug.Log("Actual override is working? can this work?");
        }
        public void SetCharacterDestination(Vector3 position, float stoppingDistance) {
            character.BT.Context.SetContext("targetPosition", position);
            character.BT.Context.SetContext("stoppingDistance", stoppingDistance);
            character.BT.SetManualNode(MoveBT.MoveToPoint(character.BT.Context));

        }

        public void SetVisibility(bool isVisible){
            if(!this.isPlayer){
                SpriteRenderer[] _renderers = GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer _ren in _renderers){
                    if(isVisible){
                        _ren.enabled = true;
                    }
                    else{
                        _ren.enabled = false;
                    }
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("TEST TEST TEST TEST" + other.gameObject.name);
        }
    }
}