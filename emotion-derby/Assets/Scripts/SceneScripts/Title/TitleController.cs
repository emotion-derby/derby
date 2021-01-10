using Prefabs.App;
using Cysharp.Threading.Tasks;

namespace Scene.Title
{
  public class TitleController : Common.SceneControllerBase
  {
    private void Start()
    {
      ScoreData.Instance.Clear();
    }
    public void OnPushStartButton()
    {
      AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON).Forget();
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.StageSelect);
    }
  }
}
