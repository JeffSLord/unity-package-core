﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Lord.Core {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BehaviorTree))]
    public class Character : WorldObject {
        public UnityEngine.AI.NavMeshAgent navMeshAgent;
        public Animator animator;
        public Selectable currentSelection;
        public BehaviorTree bt;
        public float fov;
        public float voiceRange;
        public int faction;
        protected override void Start() {
            base.Start();
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            animator = GetComponent<Animator>();
            bt = GetComponent<BehaviorTree>();
        }
        protected override void Select0(GameObject selector, int option = 0) {
            Debug.Log("Actual override is working? can this work?");
        }
    }
}