using System.Threading.Tasks;

namespace HideAndSeek
{
    public class DeadButton : ActionButton {

        public override sealed void OnClick() {

            Task.Run(() => Game._instance.ChangeState(Scene.Lobby));
        }
    }
}