using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;

    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
