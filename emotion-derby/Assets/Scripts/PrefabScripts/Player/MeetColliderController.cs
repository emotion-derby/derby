using UnityEngine;

namespace Player
{
  public class MeetColliderController : MonoBehaviour
  {
    [SerializeField] private Animator _kick;
    private void OnCollisionEnter(Collision collision)
    {
      Rigidbody ball = collision.gameObject.GetComponent<Rigidbody>();
      float power = _kick.GetFloat("Power");
      ball.AddForce(collision.impulse * power, ForceMode.Impulse);
      Scene.Batting.BallCameraController.Instance.StartFollowBall(collision.gameObject);
    }
  }
}
