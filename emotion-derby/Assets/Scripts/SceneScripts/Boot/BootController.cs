using UnityEngine;

public class BootController : MonoBehaviour
{
  [SerializeField] private GameObject App;
  void Start()
  {
    DontDestroyOnLoad(App);
    SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Title);
  }
}
