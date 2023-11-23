using UnityEngine;



public class MoveWithCam : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        // Rotate the object to match the camera's Y-axis rotation
        transform.rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
    }

}