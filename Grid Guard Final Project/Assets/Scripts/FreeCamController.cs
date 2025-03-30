using UnityEngine;

public class FreeCamController : MonoBehaviour
{
    public float moveSpeed = 10f;         
    public float rotationSpeed = 200f;    
    public Transform boundary;            // CameraBoundary gameobject

    private Vector3 boundaryMin;
    private Vector3 boundaryMax;

    void Start()
    {
        if(boundary != null)
        {
            // Get BoxCollider component from the boundary and compute min/max bounds
            BoxCollider bc = boundary.GetComponent<BoxCollider>();
            Vector3 center = boundary.position + bc.center;
            Vector3 size = bc.size;
            boundaryMin = center - size * 0.5f;
            boundaryMax = center + size * 0.5f;
        }
        else
        {
            Debug.LogError("Boundary not assigned to FreeCamController.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // input for horizontal and vertical movement (WASD and arrow keys)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // Calculate movement direction basedd on camera orientation
        Vector3 moveDir = transform.right * h + transform.forward * v;
        Vector3 newPos = transform.position + moveDir * moveSpeed * Time.deltaTime;

        // Spacebar/Lshift vertical movement
        if (Input.GetKey(KeyCode.Space))
        {
            // disregard orientation. move up
            newPos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // disregard orientation. move down
            newPos.y -= moveSpeed * Time.deltaTime;
        }

        // clamp new position within boundary limits
        if (boundary != null)
        {
            newPos.x = Mathf.Clamp(newPos.x, boundaryMin.x, boundaryMax.x);
            newPos.y = Mathf.Clamp(newPos.y, boundaryMin.y, boundaryMax.y);
            newPos.z = Mathf.Clamp(newPos.z, boundaryMin.z, boundaryMax.z);
        }

        transform.position = newPos;
    }

    void HandleRotation()
    {
        // Mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        
        // Rotate around Y axis globally and X axis locally
        transform.Rotate(Vector3.up, mouseX, Space.World);
        transform.Rotate(Vector3.right, mouseY, Space.Self);
    }
}
