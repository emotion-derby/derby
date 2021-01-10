using UnityEngine;
using System;
using Prefabs.App;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Scene.Batting
{
  public class BattingController : Common.SceneControllerBase
  {
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private GameObject _treesContainer;
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private Transform _homeBase;
    [SerializeField] private GameObject _batterBox;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pitcher;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private GameObject _notify;
    [SerializeField] private Prefabs.Score.FlyingDistanceDisplayController _flyingDistanceController;
    [SerializeField] private int _remainBalls;
    [SerializeField] private int _goalBalls;

    private Prefabs.Score.ScoreController _remainScoreController;
    private Prefabs.Score.ScoreController _homeRunScoreController;
    private Prefabs.Player.PlayerController _playerController;
    private GameObject _currentBall = null;
    private int _homeRunCount = 0;
    private List<String> _resultList = new List<String>();
    private List<int> _flyingDistances = new List<int>() { 0 };

    private CancellationTokenSource _cancelToken = new CancellationTokenSource();

    private bool isInitialized = false;

    private void Start()
    {
      // 木を生やす
      for (int i = 0; i < 50; i++)
      {
        GameObject tree = Instantiate(this._treePrefab, this._treesContainer.transform);
        tree.transform.position = new Vector3(UnityEngine.Random.Range(-1000, 1000f), 4, UnityEngine.Random.Range(250f, 1000f));
      }

      BallCameraController.Instance.gameObject.SetActive(false);
      UniTask.Void(async () =>
      {
        // WebGL版で何故かスコア系の値が入らないので暫定対処
        await UniTask.DelayFrame(1);
        if (this._cancelToken.IsCancellationRequested) return;
        this._scoreBoard.transform.Find("Goal").GetComponent<Prefabs.Score.ScoreController>().SetScore(this._goalBalls);
        this._remainScoreController = this._scoreBoard.transform.Find("Remain").GetComponent<Prefabs.Score.ScoreController>();
        this._homeRunScoreController = this._scoreBoard.transform.Find("Homerun").GetComponent<Prefabs.Score.ScoreController>();
        this._remainScoreController.SetScore(this._remainBalls);
        this._playerController = this._player.GetComponent<Prefabs.Player.PlayerController>();
        this.isInitialized = true;
      });

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
            this.CalcScoreData();
            SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Result);
            this._cancelToken.Cancel();
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
      if (this.isInitialized)
      {
        this.SetPlayerPos();
        this.Notify();
        this.DisplayFlyingDistance();

        if (InputController.OnTouchOrClickScreen())
        {
          _player.GetComponent<Prefabs.Player.PlayerController>().Kick();
        }
      }
    }

    private void DisplayFlyingDistance()
    {
      if (this._currentBall)
      {
        Prefabs.Ball.BallController ballController = this._currentBall.GetComponent<Prefabs.Ball.BallController>();
        if (!ballController.isHitGround)
        {
          this._flyingDistanceController.SetDistance(this.GetFlyingDistance());
        }
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
            if (this._cancelToken.IsCancellationRequested) return;
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
            AudioController.Instance.PlayOneShotAudio(AudioController.AUDIO_NAME.FUGOUKAKU).Forget();
            _Notify(this._notify.transform.Find("Strike").gameObject);
            this._resultList.Add("Strike");
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
              int d = this.GetFlyingDistance();
              this._flyingDistances.Add(d);
              if (d >= 500)
              {
                AudioController.Instance.PlayOneShotAudio(AudioController.AUDIO_NAME.KANSEI_BIG).Forget();
                _Notify(this._notify.transform.Find("HomeRun").gameObject);
                this._homeRunScoreController.SetScore(++this._homeRunCount);
                this._resultList.Add("HomeRun");
              }
              else
              {
                AudioController.Instance.PlayOneShotAudio(AudioController.AUDIO_NAME.KANSEI_MIDDLE).Forget();
                _Notify(this._notify.transform.Find("Hit").gameObject);
                this._resultList.Add("Hit");
              }
            }
            else
            {
              AudioController.Instance.PlayOneShotAudio(AudioController.AUDIO_NAME.FUGOUKAKU).Forget();
              _Notify(this._notify.transform.Find("Faul").gameObject);
              this._resultList.Add("Faul");
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

    private int GetFlyingDistance()
    {
      if (this._currentBall)
      {
        Prefabs.Ball.BallController ballController = this._currentBall.GetComponent<Prefabs.Ball.BallController>();
        if (ballController.isHitToBat)
        {
          Vector3 distance = this._currentBall.transform.position - this._homeBase.position;
          Vector2 distance2d = new Vector2(distance.x, distance.z);
          return (int)distance2d.magnitude;
        }
      }
      return 0;
    }

    private void CalcScoreData()
    {
      ScoreData.Instance.isSuccess = this._homeRunCount >= this._goalBalls;
      ScoreData.Instance.homeRunCount = this._homeRunCount;
      int continuousHomerunCount = 0;
      int tmp = 0;
      this._resultList.ForEach(str =>
      {
        if (str == "HomeRun")
        {
          tmp++;
        }
        else
        {
          if (tmp > continuousHomerunCount) continuousHomerunCount = tmp;
          tmp = 0;
        }
      });
      ScoreData.Instance.continuousHomeRunCount = continuousHomerunCount;
      ScoreData.Instance.maxFlyingDistance = this._flyingDistances.Max();
      ScoreData.Instance.totalFlyingDistance = this._flyingDistances.Sum();
    }

    private Rect GetBatterBoxRect()
    {
      float x = this._batterBox.transform.Find("Left").position.x;
      float z = this._batterBox.transform.Find("Top").position.z;
      float w = this._batterBox.transform.Find("Right").position.x - x;
      float h = z - this._batterBox.transform.Find("Bottom").position.z;

      return new Rect(x, z, w, h);
    }

    private void OnDestroy()
    {
      this._cancelToken.Cancel();
    }
  }
}
