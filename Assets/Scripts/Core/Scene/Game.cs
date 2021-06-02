using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace HideAndSeek
{
    public enum Scene : int {

        Lobby,
        Ready, Game,
        Dead,
        NONE = 99
    }

    public class Game : MonoBehaviour {

        public static Game  _instance = null;
        public        Scene _state    = Scene.NONE;

        [SerializeField] private GameObject _lobbyUI   = null;
        [SerializeField] private GameObject _gameUI    = null;
        [SerializeField] private GameObject _deadUI    = null;

        [SerializeField] private Text _broadcastText   = null;

        void Awake() => _instance = this;

        void Start() => _state = Scene.Lobby;

        void SettingScene() {

            switch (_state) {

                case Scene.Lobby:
                    _gameUI.SetActive(false);
                    _deadUI.SetActive(false);
                    _lobbyUI.SetActive(true);
                    break;
                case Scene.Ready:
                    _lobbyUI.SetActive(false);

                    StartCoroutine(Broadcast("READY..."));
                    StartCoroutine(Broadcast("START !", 3));

                    //Task.Run(() => Broadcast("READY..."));
                    //Task.Run(() => Broadcast("START !", 3000));

                    //Task.Run(() => ChangeState(Scene.Game, 3500));
                    break;
                case Scene.Game:
                    _gameUI.SetActive(true);
                    break;
                case Scene.Dead:
                    _deadUI.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 화면에 공지를 내보내는 함수
        /// </summary>
        /// <param name="_msg">공지 내용</param>
        /// <param name="_duration">대기 시간</param>
         public IEnumerator Broadcast(string _msg, int _duration = 1) {

            yield return new WaitForSeconds(_duration); 

            _broadcastText.gameObject.SetActive(false);

            _broadcastText.text = _msg;

            _broadcastText.gameObject.SetActive(true);

            //await Task.Delay(_duration);

            //await Task.Run(() => {

            //    Debug.Log(_msg);

            //    _broadcastText.gameObject.SetActive(false);

            //    _broadcastText.text = _msg;

            //    _broadcastText.gameObject.SetActive(true);
            //});
        }

        /// <summary>
        /// 현재 씬의 상태를 바꾸는 함수
        /// </summary>
        /// <param name="_state">상태 종류</param>
        public void ChangeState(Scene _state, int _duration = 1) {

            //await Task.Delay(_duration);

            //await Task.Run(() => {

            //    this._state = _state;
            //    SettingScene();
            //});

            this._state = _state;
            SettingScene();
        }
    }
}