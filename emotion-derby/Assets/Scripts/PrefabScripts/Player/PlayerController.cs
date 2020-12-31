using UnityEngine;

namespace Player
{
  public class PlayerController : MonoBehaviour
  {
    private Animator _animator;

    private void Start()
    {
      this._animator = this.gameObject.GetComponent<Animator>();
    }
    public void Kick()
    {
      this._animator.SetTrigger("Kick");
    }
  }
}
