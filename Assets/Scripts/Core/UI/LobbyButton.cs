using System.Threading.Tasks;

namespace HideAndSeek
{
    public class LobbyButton : ActionButton {

        public override sealed void OnClick() {

            //Task.Run(() => Game._instance.ChangeState(Scene.Ready));
            Game._instance.ChangeState(Scene.Ready);
        }
    }
}