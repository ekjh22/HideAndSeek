using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Machine;
using System.Threading.Tasks;

namespace HideAndSeek
{
    /// <summary>
    /// 맵의 구간
    /// </summary>
    public enum Lap : int {

        Start,
        Lap1, Lap2,
        Lap3, Lap4,
        Goal,
        NONE = 99
    }

    public class PlayerMachine : Machine {

        [SerializeField] private Lap _lap = Lap.NONE;

        Vector3   _moveDir = Vector3.zero;
        Transform _offset  = null;
        Transform _target  = null;

        protected override sealed void Init() {

            base.Init();

            _lap = Lap.Start;

            _offset = GameObject.Find("Player@Offset").transform;
            _target = GameObject.FindGameObjectWithTag("Enemy").transform;
        }

        protected override sealed void IdleEvent() {

            base.IdleEvent();

            if (Game._instance._state != Scene.Game)
                return;

            if (Input.GetMouseButtonDown(0)) {

                _state = State.Move;
                ResetLap();
            }
        }

        protected override sealed void MoveEvent() {

            base.MoveEvent();

            if (Game._instance._state != Scene.Game)
                return;

            if (Input.GetMouseButton(0)) {

                transform.Translate(_moveDir * _stats.Speed * Time.deltaTime);
                _offset.LookAt(_target);

                _offset.rotation = Quaternion.Euler(new Vector3(0f, _offset.rotation.eulerAngles.y, 0f));
            }
            else {

                _state = State.Idle;
                _moveDir = Vector3.zero;
            }
        }

        protected override sealed void AttackEvent() {

            base.AttackEvent();
        }

        protected override sealed void DeadEvent() {

            base.DeadEvent();

            if (Game._instance._state != Scene.Game)
                return;

            Task.Run(() => Game._instance.ChangeState(Scene.Dead));
        }

        void OnEnable() {

            transform.position = new Vector3(5f, 0.5f, -5f);
        }

        void OnTriggerEnter(Collider other) {

            if (other.CompareTag("Attack")) { // 공격 받음

                _stats.CurHP -= 1;
            }
            else if (other.CompareTag("Wall")) { // 벽에 숨음

                
            }
            else if (other.CompareTag("Lap")) { // 구간 도착

                _lap += 1;
                ResetLap();
            }
        }

        void OnTriggerExit(Collider other) {

            if (other.CompareTag("Wall")) { // 벽에서 나감

            }
        }

        void ResetLap()  {

            switch (_lap) {
                case Lap.Lap1:
                    _moveDir = Vector3.left;
                    break;
                case Lap.Lap2:
                    _moveDir = Vector3.forward;
                    break;
                case Lap.Lap3:
                    _moveDir = Vector3.right;
                    break;
                case Lap.Lap4:
                    _moveDir = Vector3.back;
                    break;
                case Lap.Goal:
                    _lap = Lap.Lap1;
                    ResetLap();
                    break;
                default:
                    break;
            }
        }
    }
}