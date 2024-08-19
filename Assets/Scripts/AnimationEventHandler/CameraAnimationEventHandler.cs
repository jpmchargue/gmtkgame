using UnityEngine;

public class CameraAnimationEventHandler : MonoBehaviour
{
    public GameObject mainGameLoop;
    public void OnAnimationEnd()
    {
        Debug.Log("ENTERING CAMERA");
        mainGameLoop.SetActive(true);
    }
}
