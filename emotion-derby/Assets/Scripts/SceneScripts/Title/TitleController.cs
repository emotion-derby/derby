using UnityEngine;
using Prefabs.App;

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
      AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON);
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.StageSelect);
    }
  }
}
