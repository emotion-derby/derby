using UnityEngine;

namespace Prefabs.App
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

    public static Vector3 GetMousePos()
    {
      return Input.mousePosition;
    }
  }
}
