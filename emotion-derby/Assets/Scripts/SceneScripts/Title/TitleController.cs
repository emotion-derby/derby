using UnityEngine;
using Prefabs.App;
using Cysharp.Threading.Tasks;

namespace Scene.Title
{
  public class TitleController : MonoBehaviour
  {
    private void Start()
    {
      ScoreData.Instance.Clear();
    }
    public void OnPushStartButton()
    {
      UniTask.Void(async () =>
      {
        await AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON);
        SceneController.Instance.LoadScene(SceneController.SCENE_NAME.StageSelect);
      });
    }
  }
}
