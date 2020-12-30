using UnityEngine;
using App;

namespace Scene.Title
{
  public class TitleController : MonoBehaviour
  {
    public void OnPushStartButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.StageSelect);
    }
  }
}
