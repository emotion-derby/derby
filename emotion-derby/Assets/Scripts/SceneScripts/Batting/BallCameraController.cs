using UnityEngine;

namespace Scene.Batting
{
  public class BallCameraController : Common.SingletonMonoBehaviour<BallCameraController>
  {
    private GameObject _ball;
    public void StartFollowBall(GameObject ball)
    {
      this.gameObject.SetActive(true);
      this._ball = ball;
    }

    public void Update()
    {
      if (!this._ball?.gameObject.activeSelf ?? this._ball == null)
      {
        this._ball = null;
        this.gameObject.SetActive(false);
      }
      else
      {
        this.transform.position = this._ball.transform.position + new Vector3(0f, 10f, -10f);
        this.transform.LookAt(this._ball.transform);
      }
    }
  }
}
