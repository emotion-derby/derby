using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Pitcher
{
  public class PitcherController : MonoBehaviour
  {
    [SerializeField] private GameObject _world;
    [SerializeField] private GameObject _ball;

    private CancellationTokenSource _cancelToken;
    // Start is called before the first frame update
    void Start()
    {
      this._cancelToken = new CancellationTokenSource();
      UniTask.Create(async () =>
      {
        while (true)
        {
          if (this._cancelToken.IsCancellationRequested) return;
          GameObject ball = Instantiate(this._ball, this._world.transform);
          ball.transform.position = this.transform.position + new Vector3(0f, 1.0f, -2f);
          ball.GetComponent<Rigidbody>().AddForce(0, 3f, -50f, ForceMode.Impulse);
          await UniTask.Delay(2 * 1000);
        }
      });
    }

    void OnDestroy()
    {
      this._cancelToken.Cancel();
    }
  }
}
