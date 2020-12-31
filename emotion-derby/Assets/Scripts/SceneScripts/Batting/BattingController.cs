using UnityEngine;
using App;

namespace Scene.Batting
{
  public class BattingController : MonoBehaviour
  {
    [SerializeField] private GameObject _player;
    public void OnPushToResultButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Result);
    }

    private void Update()
    {
      if (App.InputController.OnTouchOrClickScreen())
      {
        _player.GetComponent<Player.PlayerController>().Kick();
      }
    }
  }
}
