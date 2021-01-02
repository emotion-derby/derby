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
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        return;
      }
      this._lifeTimeFrame--;
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.collider.name == "Ground" && this._lifeTimeFrame > 60)
      {
        this._lifeTimeFrame = 60;
      }
    }
  }
}
