using UnityEngine;
using System;

namespace Ball
{
  public class BallController : MonoBehaviour
  {
    private int _lifeTimeSec = 60;
    private DateTime _createdTime;

    private void Start()
    {
      _createdTime = DateTime.Now;
    }
    // Update is called once per frame
    private void Update()
    {
      Debug.Log((DateTime.Now - this._createdTime).Seconds);
      if ((DateTime.Now - this._createdTime).Seconds >= this._lifeTimeSec)
      {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        return;
      }
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.collider.name == "Ground" && this._lifeTimeSec > 2)
      {
        this._createdTime = DateTime.Now;
        this._lifeTimeSec = 2;
      }
    }
  }
}
