using UnityEngine;

namespace Scene.Common
{
  public class SceneControllerBase : MonoBehaviour
  {
    private void Awake()
    {
#if UNITY_EDITOR
      if (Prefabs.App.SceneController.Instance == null)
      {
        GameObject appPrefab = Resources.Load<GameObject>("Prefabs/App");
        DontDestroyOnLoad(Instantiate(appPrefab));
      }
#endif
    }
  }
}
