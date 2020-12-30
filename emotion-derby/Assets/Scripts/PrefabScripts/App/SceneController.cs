using UnityEngine.SceneManagement;
using Common;

namespace App
{
  public class SceneController : SingletonMonoBehaviour<SceneController>
  {
    public enum SCENE_NAME
    {
      [StringValue("Title")]
      Title,
      [StringValue("StageSelect")]
      StageSelect,
      [StringValue("Batting")]
      Batting,
      [StringValue("Result")]
      Result,
    }

    public void LoadScene(SCENE_NAME sceneName)
    {
      SceneManager.LoadScene(sceneName.GetStringValue());
    }
  }
}
