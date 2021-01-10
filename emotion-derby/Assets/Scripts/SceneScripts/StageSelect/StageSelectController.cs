using UnityEngine;
using Prefabs.App;
using Cysharp.Threading.Tasks;

namespace Scene.StageSelect
{
  public class StageSelectController : MonoBehaviour
  {
    public void OnPushReturnTitleButton()
    {
      AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON).Forget();
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Title);
    }

    public void OnPushStage1Button()
    {
      AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON).Forget();
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Batting);

    }
  }
}
