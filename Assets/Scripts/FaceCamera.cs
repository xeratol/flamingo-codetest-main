using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        transform.forward = transform.position - Camera.main.transform.position;
    }
}
