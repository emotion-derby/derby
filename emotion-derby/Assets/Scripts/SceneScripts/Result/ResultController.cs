using UnityEngine;
using Prefabs.App;
using Prefabs.Score;
using Cysharp.Threading.Tasks;

namespace Scene.Result
{
  public class ResultController : Common.SceneControllerBase
  {
    [SerializeField] private GameObject _clearText;
    [SerializeField] private GameObject _failedText;
    [SerializeField] private ScoreController _homeRunScoreController;
    [SerializeField] private ScoreController _continuousHomeRunScoreController;
    [SerializeField] private FlyingDistanceDisplayController _maxFlyingDistanceController;
    [SerializeField] private FlyingDistanceDisplayController _totalFlyingDistanceController;

    private void Start()
    {
      if (ScoreData.Instance.isSuccess)
      {
        AudioController.Instance.PlayOneShotAudio(AudioController.AUDIO_NAME.GOUKAKU).Forget();
        this._clearText.SetActive(true);
      }
      else
      {
        AudioController.Instance.PlayOneShotAudio(AudioController.AUDIO_NAME.FUGOUKAKU).Forget();
        this._failedText.SetActive(true);
      }

      UniTask.Void(async () =>
      {
        await UniTask.DelayFrame(1);
        this._homeRunScoreController.SetScore(ScoreData.Instance.homeRunCount);
        this._continuousHomeRunScoreController.SetScore(ScoreData.Instance.continuousHomeRunCount);
        this._maxFlyingDistanceController.SetDistance(ScoreData.Instance.maxFlyingDistance);
        this._totalFlyingDistanceController.SetDistance(ScoreData.Instance.totalFlyingDistance);
      });
    }

    public void OnPushReturnTitleButton()
    {
        AudioController.Instance.PlayAudio(AudioController.AUDIO_NAME.BUTTON).Forget();
        SceneController.Instance.LoadScene(SceneController.SCENE_NAME.Title);
    }
  }
}
