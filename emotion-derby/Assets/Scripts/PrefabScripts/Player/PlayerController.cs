using UnityEngine;

namespace Prefabs.Player
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] private GameObject _meetCircle;
    private Animator _animator;

    public GameObject meetCircle
    {
      get
      {
        return this._meetCircle;
      }
    }

    private void Start()
    {
      this._animator = this.gameObject.GetComponent<Animator>();
    }
    public void Kick()
    {
      App.AudioController.Instance.PlayRandomManVoice();
      this._animator.SetTrigger("Kick");
    }
  }
}
