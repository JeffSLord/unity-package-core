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
        }
        protected override void Start() {
            base.Start();
            navMeshAgent = GetComponent<NavMeshAgent>();
            // navMeshAgent.speed = character.MoveSpeed;
            animator = GetComponent<Animator>();
            btBehavior = GetComponent<BehaviorTreeBehavior>();

            btBehavior.behaviorTree.context.SetContext<Character>("character", this.character);
            btBehavior.behaviorTree.context.SetContext<CharacterBehavior>("characterBehavior", this);
            btBehavior.behaviorTree.context.SetContext<float>("moveSpeed", this.character.MoveSpeed);
            btBehavior.behaviorTree.context.SetContext<float>("stoppingDistance", this.character.StoppingDistance);
            btBehavior.behaviorTree.context.SetContextList<Waypoint>("waypoints", GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().waypoints);
            btBehavior.behaviorTree.context.SetContextList<WorkPriority>("workPriority", this.character.WorkPriority);
            btBehavior.behaviorTree.context.SetContext<Settlement>("settlement", GameObject.FindGameObjectWithTag("Settlement").GetComponent<SettlementBehavior>().settlement);
            btBehavior.behaviorTree.highPriorityNode = EnemyBT.EnemyDetectionNode(btBehavior.behaviorTree.context);
            btBehavior.behaviorTree.lowPriorityNode = WorkBT.Work(btBehavior.behaviorTree.context);
        }
        protected override void Select0(GameObject selector, int option = 0) {
            Debug.Log("Actual override is working? can this work?");
        }
        public void SetCharacterDestination(Vector3 position, float stoppingDistance) {
            character.BT.context.SetContext("targetPosition", position);
            character.BT.context.SetContext("stoppingDistance", stoppingDistance);
            character.BT.SetManualNode(MoveBT.MoveToPoint(character.BT.context));

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