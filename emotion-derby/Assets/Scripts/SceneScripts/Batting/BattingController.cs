using UnityEngine;
using App;
using System;

namespace Scene.Batting
{
  public class BattingController : MonoBehaviour
  {
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private GameObject _batterBox;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _meetCircle;

    private void Start()
    {
      BallCameraController.Instance.gameObject.SetActive(false);
    }

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

      if (this._groundCollider.Raycast(mousePosRay, out hit, 100f))
      {
        for (int i = 0; i < 100; i++)
        {
          Vector3 p = mousePosRay.GetPoint(i);
          if (Math.Abs(p.y - this._meetCircle.transform.position.y) <= 0.15f)
          {
            Vector3 playerPos = this._player.transform.position;
            playerPos.x = p.x - (this._meetCircle.transform.position.x - playerPos.x);
            playerPos.z = p.z;

            Rect batterBoxRect = GetBatterBoxRect();
            if (playerPos.x < batterBoxRect.x) playerPos.x = batterBoxRect.x;
            if (playerPos.x > batterBoxRect.x + batterBoxRect.width) playerPos.x = batterBoxRect.x + batterBoxRect.width;
            if (playerPos.z > batterBoxRect.y) playerPos.z = batterBoxRect.y;
            if (playerPos.z < batterBoxRect.y - batterBoxRect.height) playerPos.z = batterBoxRect.y - batterBoxRect.height;

            this._player.transform.position = playerPos;
            return;
          }
        }
      }
    }

    private Rect GetBatterBoxRect()
    {
      float x = this._batterBox.transform.Find("Left").position.x;
      float z = this._batterBox.transform.Find("Top").position.z;
      float w = this._batterBox.transform.Find("Right").position.x - x;
      float h = z - this._batterBox.transform.Find("Bottom").position.z;

      return new Rect(x, z, w, h);
    }

    public void OnPushToResultButton()
    {
      SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Result);
    }
  }
}
