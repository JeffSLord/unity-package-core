using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BehaviorTree))]
    public class Character : WorldObject {
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        public Selectable currentSelection;
        public BehaviorTree bt;
        public CharacterData characterData;
        public float fov;
        public float voiceRange;
        public int faction;

        protected override void Awake() {

        }
        protected override void Start() {
            base.Start();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            bt = GetComponent<BehaviorTree>();
            SetBtContext();
        }
        protected void SetBtContext() {
            bt.SetContext<Character>("character", this);
            bt.SetContext<float>("moveSpeed", characterData.moveSpeed);
            bt.SetContext<float>("stoppingDistance", characterData.stoppingDistance);
            bt.SetContextList<Waypoint>("waypoints", GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().waypoints);
            bt.urgentNode = EnemyBT.EnemyDetectionNode(bt.context);
        }
        protected override void Select0(GameObject selector, int option = 0) {
            Debug.Log("Actual override is working? can this work?");
        }

        public void SetCharacterDestination(Vector3 position, float stoppingDistance) {
            bt.SetContext("targetPosition", position);
            bt.SetContext("stoppingDistance", stoppingDistance);
            bt.SetManualNode(MoveBT.MoveToPoint(bt.context));

        }
    }
}