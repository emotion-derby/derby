using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Prefabs.Ball
{
  public class BallController : MonoBehaviour
  {
    private int _lifeTimeSec = 60;
    private DateTime _createdTime;

    private void Start()
    {
      _createdTime = DateTime.Now;
    }

    public bool isHitToBat { get; private set; } = false;
    public bool isHitGround { get; private set; } = false;
    public bool isNotified = false;
    // Update is called once per frame
    private void Update()
    {
      if ((DateTime.Now - this._createdTime).Seconds >= this._lifeTimeSec)
      {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        return;
      }
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.collider.name == "Ground" && this._lifeTimeSec > 1)
      {
        App.AudioController.Instance.PlayAudio(App.AudioController.AUDIO_NAME.KODUTUMI).Forget();
        this.isHitGround = true;
        this._createdTime = DateTime.Now;
        this._lifeTimeSec = 1;
      }
      else if (collision.collider.name == "MeetCircle")
      {
        App.AudioController.Instance.PlayAudio(App.AudioController.AUDIO_NAME.MUTI).Forget();
        this.isHitToBat = true;
      }
      else
      {
        App.AudioController.Instance.PlayAudio(App.AudioController.AUDIO_NAME.KODUTUMI).Forget();
      }
    }
  }
}
