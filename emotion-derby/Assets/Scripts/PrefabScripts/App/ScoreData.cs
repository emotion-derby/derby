namespace Prefabs.App
{
  public class ScoreData : Common.SingletonMonoBehaviour<ScoreData>
  {
    public bool isSuccess;
    public int homeRunCount;
    public int continuousHomeRunCount;
    public int maxFlyingDistance;
    public int totalFlyingDistance;

    public void Clear()
    {
      this.isSuccess = false;
      this.homeRunCount = 0;
      this.continuousHomeRunCount = 0;
      this.maxFlyingDistance = 0;
      this.totalFlyingDistance = 0;
    }
  }
}
