using UnityEngine;
using App;

namespace Scene.Batting
{
  public class BattingController : MonoBehaviour
  {
    public void OnPushToResultButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Result);
    }
  }
}
