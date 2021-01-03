using UnityEngine;

namespace Prefabs.Pitcher
{
  public class PitcherController : MonoBehaviour
  {
    [SerializeField] private Vector3 _pitchingOffset;
    [SerializeField] private Vector3 _pitchingForce;

    private Animator _animator;
    private GameObject _ball = null;

    public bool isReady { get; private set; } = true;
    // Start is called before the first frame update
    private void Start()
    {
      this._animator = this.gameObject.GetComponent<Animator>();
    }

    public void StartPitching(GameObject ball)
    {
      this._ball = ball;
      // 実際に投球するまで地面に当たらない位置に固定＆重力加速度をストップ
      this._ball.transform.position = new Vector3(0, 100, 0);
      this._ball.GetComponent<Rigidbody>().isKinematic = true;
      this.isReady = false;
      this._animator.SetTrigger("Pitching");
    }

    public void PitchingFromAnimation()
    {
      this._ball.GetComponent<Rigidbody>().isKinematic = false;
      this._ball.transform.position = this.transform.position + this._pitchingOffset;
      this._ball.GetComponent<Rigidbody>().AddForce(this._pitchingForce, ForceMode.Impulse);
      this.isReady = true;
    }
  }
}
