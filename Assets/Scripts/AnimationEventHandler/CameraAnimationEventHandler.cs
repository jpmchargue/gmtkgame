using UnityEngine;

public class CameraAnimationEventHandler : MonoBehaviour
{
    public GameObject mainGameLoop;
    public void OnAnimationEnd()
    {
        mainGameLoop.SetActive(true);
    }
}
