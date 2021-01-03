using UnityEngine;
using Prefabs.App;

namespace Scene.Result
{
  public class ResultController : MonoBehaviour
  {
    public void OnPushReturnTitleButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Title);
    }
  }
}
