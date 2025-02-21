using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject Player;
    public static float mouseSensitivity = 2f;
    private float cameraVerticalRotation = 0f;
    private float cameraHorizontalRotation = 0f;

    void Update()
    {
        NormalControls();
        transform.position = Player.transform.position;
    }

    void NormalControls()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        cameraHorizontalRotation += inputX;

        Quaternion rotation = Quaternion.Euler(cameraVerticalRotation, cameraHorizontalRotation, 0);
        transform.rotation = rotation;

        transform.position = Player.transform.position;
    }
}
