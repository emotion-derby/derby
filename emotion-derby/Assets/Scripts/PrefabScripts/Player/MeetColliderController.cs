using UnityEngine;

namespace Prefabs.Player
{
  public class MeetColliderController : MonoBehaviour
  {
    [SerializeField] private Animator _kick;
    private void OnCollisionEnter(Collision collision)
    {
      Rigidbody ball = collision.gameObject.GetComponent<Rigidbody>();
      float power = _kick.GetFloat("Power");
      Vector3 impulse = collision.impulse;
      impulse.x *= power;
      impulse.y *= power * 0.8f;
      impulse.z *= power * 1.2f;
      ball.AddForce(impulse, ForceMode.Impulse);
      Scene.Batting.BallCameraController.Instance.StartFollowBall(collision.gameObject);
    }
  }
}
