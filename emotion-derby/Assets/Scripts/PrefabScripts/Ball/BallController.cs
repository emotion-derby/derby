using UnityEngine;

namespace Ball
{
  public class BallController : MonoBehaviour
  {
    private int _lifeTimeFrame = 600;

    // Update is called once per frame
    void Update()
    {
      if (this._lifeTimeFrame <= 0)
      {
        Destroy(this.gameObject);
        return;
      }
      this._lifeTimeFrame--;
    }
  }
}
