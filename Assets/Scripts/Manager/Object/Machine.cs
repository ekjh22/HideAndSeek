using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Machine
{
    #region Base Machine Settings
    
    /// <summary>
    /// 머신의 상태
    /// </summary>
    public enum State : int {

        Idle,
        Move,
        Attack,
        Dead,
        NONE = 99
    }

    /// <summary>
    /// 머신의 스탯
    /// </summary>
    [Serializable]
    public class Stats {

        /// <summary>
        /// 이동속도
        /// </summary>
        public float Speed { set; get; }
        
        /// <summary>
        /// 현재 체력
        /// </summary>
        public int CurHP { set; get; }
        
        /// <summary>
        /// 최대 체력
        /// </summary>
        public int MaxHP { set; get; }

        public Stats(float _speed = 5f, int _hp = 3) {

            Speed = _speed;
            CurHP = MaxHP = _hp;
        }

        /// <summary>
        /// 현재 체력이 0인가 ?
        /// </summary>
        /// <returns></returns>
        public bool isDead() { return CurHP <= 0; }
    }
    
    #endregion

    public class Machine : MonoBehaviour {

        protected Stats _stats = null;
        public    State _state = State.NONE;

        void Awake() => Init();
        void Start() => StartFSM();

        void Update() {

            if (_stats.isDead())
                _state = State.Dead;
        }

        /// <summary>
        /// 머신을 초기화하는 함수
        /// </summary>
        /// <param name="_speed">이동속도</param>
        /// <param name="_hp">체력</param>
        virtual protected void Init() {

            _stats = new Stats();
            _state = State.Idle;
        }

        /// <summary>
        /// 머신의 Idle 이벤트를 수행중일 때 호출하는 함수
        /// </summary>
        virtual protected void IdleEvent() { }

        /// <summary>
        /// 머신의 Move 이벤트를 수행중일 때 호출하는 함수
        /// </summary>
        virtual protected void MoveEvent() { }

        /// <summary>
        /// 머신의 Attack 이벤트를 수행중일 때 호출하는 함수
        /// </summary>
        virtual protected void AttackEvent() { }

        /// <summary>
        /// 머신의 Dead 이벤트를 수행중일 때 호출하는 함수
        /// </summary>
        virtual protected void DeadEvent() { }

        IEnumerator Idle() {

            // Start
            yield return null;

            while (_state == State.Idle) {

                // Update
                Debug.Log("Idle !");

                IdleEvent();
                yield return null;
            }

            // End
            NextState();
        }

        IEnumerator Move() {

            // Start
            yield return null;

            while (_state == State.Move) {

                // Update
                Debug.Log("Move !");

                MoveEvent();
                yield return null;
            }

            // End
            NextState();
        }

        IEnumerator Attack() {

            // Start
            yield return null;

            while (_state == State.Attack) {

                // Update
                Debug.Log("Attack !");

                AttackEvent();
                yield return null;
            }

            // End
            NextState();
        }

        void StartFSM() {

            Debug.Log("FSM Start !");

            if (_state == State.NONE)
                _state = State.Idle;

            StartCoroutine(_state.ToString());
        }

        void NextState() {

            if (_state != State.NONE)
                StartCoroutine(_state.ToString());
            else if (_state == State.Dead)
                DeadEvent();
        }
    }
}