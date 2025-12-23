using UnityEngine;

/// <summary>
/// A simple FPP camera rotation script.
/// Horizontal rotation is applied to the root object.
/// Vertical rotation is applied to the camera.
/// </summary>
public class FirstPersonCameraRotation : MonoBehaviour
{
    public float Sensitivity {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 camRotation = Vector2.zero;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    Transform rootTransform;

    void Start()
    {
        rootTransform = transform.root; // assign once for efficiency
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Accumulate mouse input
        camRotation.x += Input.GetAxis(xAxis) * sensitivity;   // yaw
        camRotation.y += Input.GetAxis(yAxis) * sensitivity;   // pitch
        camRotation.y = Mathf.Clamp(camRotation.y, -yRotationLimit, yRotationLimit);

        // Apply horizontal rotation (yaw) to the root object
        rootTransform.localRotation = Quaternion.Euler(0f, camRotation.x, 0f);

        // Apply vertical rotation (pitch) to the camera itself
        transform.localRotation = Quaternion.Euler(-camRotation.y, 0f, 0f);
    }
}