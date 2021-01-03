using UnityEngine;
using System;
using Prefabs.App;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Scene.Batting
{
  public class BattingController : MonoBehaviour
  {
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _world;
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private Transform _homeBase;
    [SerializeField] private GameObject _batterBox;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pitcher;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private GameObject _notify;
    [SerializeField] private int _remainBalls = 25;
    [SerializeField] private int _goalBalls = 15;

    private Prefabs.Score.ScoreController _remainScoreController;
    private Prefabs.Player.PlayerController _playerController;
    private GameObject _currentBall = null;

    private CancellationTokenSource _cancelToken = new CancellationTokenSource();

    private void Start()
    {
      BallCameraController.Instance.gameObject.SetActive(false);
      this._scoreBoard.transform.Find("Goal").GetComponent<Prefabs.Score.ScoreController>().SetScore(this._goalBalls);
      this._remainScoreController = this._scoreBoard.transform.Find("Remain").GetComponent<Prefabs.Score.ScoreController>();
      this._remainScoreController.SetScore(this._remainBalls);
      this._playerController = this._player.GetComponent<Prefabs.Player.PlayerController>();

      UniTask.Void(async () =>
      {
        await UniTask.Delay(3000);
        Prefabs.Pitcher.PitcherController pitcherController = this._pitcher.GetComponent<Prefabs.Pitcher.PitcherController>();
        while (true)
        {
          if (this._cancelToken.IsCancellationRequested) return;
          await UniTask.WaitUntil(() =>
          {
            if (this._currentBall != null)
            {
              return !this._currentBall.gameObject.activeSelf && pitcherController.isReady;
            }
            return pitcherController.isReady;
          });
          if (this._remainBalls <= 0)
          {
            SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Result);
            return;
          }
          await UniTask.Delay(1000);
          this._currentBall = Instantiate(this._ballPrefab, this._world.transform);
          pitcherController.StartPitching(this._currentBall);
          this._remainScoreController.SetScore(--this._remainBalls);
        }
      });
    }

    private void Update()
    {
      this.SetPlayerPos();
      this.Notify();

      if (InputController.OnTouchOrClickScreen())
      {
        _player.GetComponent<Prefabs.Player.PlayerController>().Kick();
      }
    }

    private void Notify()
    {
      void _Notify(GameObject gameObject)
      {
        if (!gameObject.activeSelf)
        {
          gameObject.SetActive(true);
          UniTask.Void(async () =>
          {
            await UniTask.Delay(3000);
            gameObject.SetActive(false);
          });
        }
      }

      if (this._currentBall)
      {
        Prefabs.Ball.BallController ballController = this._currentBall.GetComponent<Prefabs.Ball.BallController>();
        if (ballController.isNotified) return;
        if (ballController.isHitGround)
        {
          if (!ballController.isHitToBat)
          {
            _Notify(this._notify.transform.Find("Strike").gameObject);
            ballController.isNotified = true;
            return;
          }
          else
          {
            Vector3 homeBaseVerticalVec = (this._homeBase.position + new Vector3(0f, 0f, 1f)).normalized;
            Vector3 ballVec = (this._currentBall.transform.position - this._homeBase.position).normalized;

            float innerProduct = Math.Abs(homeBaseVerticalVec.x * ballVec.x + homeBaseVerticalVec.z * ballVec.z);
            if (innerProduct >= Math.Cos(Math.PI / 4) && this._currentBall.transform.position.z >= this._homeBase.position.z)
            {
              _Notify(this._notify.transform.Find("Hit").gameObject);
            }
            else
            {
              _Notify(this._notify.transform.Find("Faul").gameObject);
            }
            ballController.isNotified = true;
          }
        }
      }
    }

    private void SetPlayerPos()
    {
      Vector3 mousePos = InputController.GetMousePos();

      Ray mousePosRay = this._mainCamera.ScreenPointToRay(mousePos);
      RaycastHit hit;

      if (this._groundCollider.Raycast(mousePosRay, out hit, 100f))
      {
        for (int i = 0; i < 100; i++)
        {
          Vector3 p = mousePosRay.GetPoint(i);
          if (Math.Abs(p.y - this._playerController.meetCircle.transform.position.y) <= 0.15f)
          {
            Vector3 playerPos = this._player.transform.position;
            playerPos.x = p.x - (this._playerController.meetCircle.transform.position.x - playerPos.x);
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

    private void OnDestroy()
    {
      this._cancelToken.Cancel();
    }
  }
}
