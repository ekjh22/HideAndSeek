using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek
{
    public class PlayerUI : MonoBehaviour {

        [SerializeField] private Text _scoreObj = null;
        [SerializeField] private Text _hpObj    = null;

        void LateUpdate() {

            SettingScore();
            SettingHP();
        }

        void SettingScore() {

        }

        void SettingHP() {
        
        }
    }
}