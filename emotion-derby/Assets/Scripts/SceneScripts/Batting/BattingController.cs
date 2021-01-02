using UnityEngine;
using App;
using System;

namespace Scene.Batting
{
  public class BattingController : MonoBehaviour
  {
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _meetCircle;

    private void Update()
    {
      this.SetPlayerPos();

      if (App.InputController.OnTouchOrClickScreen())
      {
        _player.GetComponent<Player.PlayerController>().Kick();
      }
    }

    private void SetPlayerPos()
    {
      Vector3 mousePos = App.InputController.GetMousePos();

      Ray mousePosRay = this._mainCamera.ScreenPointToRay(mousePos);
      RaycastHit hit;

      Collider groundCollider = this._ground.GetComponent<Collider>();
      if (groundCollider.Raycast(mousePosRay, out hit, 100f))
      {
        for (int i = 0; i < 100; i++)
        {
          Vector3 p = mousePosRay.GetPoint(i);
          if (Math.Abs(p.y - this._meetCircle.transform.position.y) <= 0.15f)
          {
            Vector3 playerPos = this._player.transform.position;
            playerPos.x = p.x - (this._meetCircle.transform.position.x - playerPos.x);
            playerPos.z = p.z;

            this._player.transform.position = playerPos;
            return;
          }
        }
      }
    }

    public void OnPushToResultButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Result);
    }
  }
}
