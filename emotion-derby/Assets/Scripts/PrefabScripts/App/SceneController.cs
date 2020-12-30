using UnityEngine.SceneManagement;
using Common;

public class SceneController : SingletonMonoBehaviour<SceneController>
{
  public enum SCENE_NAME
  {
    [StringValue("Title")]
    Title,
  }

  public void LoadScene(SCENE_NAME sceneName)
  {
    SceneManager.LoadScene(sceneName.GetStringValue());
  }
}
