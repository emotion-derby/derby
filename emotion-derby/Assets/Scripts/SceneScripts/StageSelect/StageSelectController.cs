using UnityEngine;
using App;

namespace Scene.StageSelect
{
  public class StageSelectController : MonoBehaviour
  {
    public void OnPushReturnTitleButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Title);
    }

    public void OnPushStage1Button()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Batting);
    }
  }
}
