using UnityEngine;
using Prefabs.App;
using Cysharp.Threading.Tasks;

namespace Scene.StageSelect
{
  public class StageSelectController : MonoBehaviour
  {
    public void OnPushReturnTitleButton()
    {
      UniTask.Void(async () =>
      {
        await AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON);
        SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Title);
      });
    }

    public void OnPushStage1Button()
    {
      UniTask.Void(async () =>
      {
        await AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON);
        SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Batting);
      });
    }
  }
}
