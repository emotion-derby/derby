using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prefabs.Score
{
  public class FlyingDistanceDisplayController : MonoBehaviour
  {
    [SerializeField] private List<Sprite> _digitSprites;
    [SerializeField] private Image _digit1000Image;
    [SerializeField] private Image _digit100Image;
    [SerializeField] private Image _digit10Image;
    [SerializeField] private Image _digit1Image;
    // Start is called before the first frame update
    void Start()
    {
      this._digit1000Image.sprite = this._digitSprites[0];
      this._digit100Image.sprite = this._digitSprites[0];
      this._digit10Image.sprite = this._digitSprites[0];
      this._digit1Image.sprite = this._digitSprites[0];
    }

    public void SetDistance(int distance)
    {
      int digit1000Num = (int)(distance / 1000);
      if (digit1000Num > 9)
      {
        Debug.LogError($"distance must be less than 9999. [distance={distance}]");
        distance = 9999;
      }
      this._digit1000Image.sprite = this._digitSprites[digit1000Num];

      int digit100Num = (int)((distance / 100) % 10);
      this._digit100Image.sprite = this._digitSprites[digit100Num];

      int digit10Num = (int)((distance / 10) % 10);
      this._digit10Image.sprite = this._digitSprites[digit10Num];

      this._digit1Image.sprite = this._digitSprites[distance % 10];
    }
  }
}
