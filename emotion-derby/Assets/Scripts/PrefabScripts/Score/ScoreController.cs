using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prefabs.Score
{
  public class ScoreController : MonoBehaviour
  {
    [SerializeField] private List<Sprite> _digitSprites;
    [SerializeField] private Image _digit10Image;
    [SerializeField] private Image _digit1Image;

    private void Start()
    {
      this._digit10Image.sprite = this._digitSprites[0];
      this._digit1Image.sprite = this._digitSprites[0];
    }

    public void SetScore(int score)
    {
      int digit10Num = (int)(score / 10);
      if (digit10Num > 9)
      {
        throw new System.Exception($"score must be less than 99. [score={score}]");
      }
      this._digit10Image.sprite = this._digitSprites[digit10Num];
      this._digit1Image.sprite = this._digitSprites[score % 10];
    }
  }
}
