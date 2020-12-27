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

        protected override void Awake() {
            character = new Character();
        }
        protected override void Start() {
            base.Start();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            btBehavior = GetComponent<BehaviorTreeBehavior>();

            btBehavior.behaviorTree.context.SetContext<Character>("character", this.character);
            btBehavior.behaviorTree.context.SetContext<CharacterBehavior>("characterBehavior", this);
            btBehavior.behaviorTree.context.SetContext<float>("moveSpeed", this.character.moveSpeed);
            btBehavior.behaviorTree.context.SetContext<float>("stoppingDistance", this.character.stoppingDistance);
            btBehavior.behaviorTree.context.SetContextList<Waypoint>("waypoints", GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().waypoints);
            btBehavior.behaviorTree.context.SetContextList<WorkPriority>("workPriority", this.character.workPriority);
            btBehavior.behaviorTree.context.SetContext<Settlement>("settlement", GameObject.FindGameObjectWithTag("Settlement").GetComponent<SettlementBehavior>().settlement);
            btBehavior.behaviorTree.highPriorityNode = EnemyBT.EnemyDetectionNode(btBehavior.behaviorTree.context);
            btBehavior.behaviorTree.lowPriorityNode = WorkBT.Work(btBehavior.behaviorTree.context);
        }
        protected override void Select0(GameObject selector, int option = 0) {
            Debug.Log("Actual override is working? can this work?");
        }
        public void SetCharacterDestination(Vector3 position, float stoppingDistance) {
            character.bt.context.SetContext("targetPosition", position);
            character.bt.context.SetContext("stoppingDistance", stoppingDistance);
            character.bt.SetManualNode(MoveBT.MoveToPoint(character.bt.context));

        }
    }
}