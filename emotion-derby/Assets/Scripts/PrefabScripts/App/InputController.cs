using UnityEngine;

namespace App
{
  public class InputController : Common.SingletonMonoBehaviour<InputController>
  {
    public static bool OnTouchOrClickScreen()
    {
      if (Input.GetMouseButtonDown(0))
      {
        return true;
      }
      return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }
  }
}
