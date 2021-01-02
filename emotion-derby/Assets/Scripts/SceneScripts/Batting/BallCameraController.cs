using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scene.Batting
{
  public class BallCameraController : Common.SingletonMonoBehaviour<BallCameraController>
  {
    private GameObject _ball;
    public void StartFollowBall(GameObject ball)
    {
      this._ball = ball;
      {
        UniTask.Create(async () =>
        {
          await UniTask.DelayFrame(10);
          Debug.Log(this._ball.GetComponent<Rigidbody>().velocity.z);
          if (this._ball.GetComponent<Rigidbody>().velocity.z > 0)
          {
            await UniTask.Delay(300);
            Time.timeScale = 2f;
            this.gameObject.SetActive(true);
          }
          else
          {
            this.gameObject.SetActive(true);
          }
        });
      }
    }

    public void Update()
    {
      if (!this._ball?.gameObject.activeSelf ?? this._ball == null)
      {
        this._ball = null;
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
      }
      else
      {
        this.transform.position = this._ball.transform.position + new Vector3(0f, 10f, -10f);
        this.transform.LookAt(this._ball.transform);
      }
    }
  }
}
