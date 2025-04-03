using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Vector3 moveDir;
    public Vector3 rotDir;
    public Vector3 camRot;
    public float moveSpeed;
    public float rotSpeed;
    public Transform cam;
    public float sprint;
    public float sprintSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        transform.Translate(moveDir * Time.deltaTime * moveSpeed * sprint);
        rotDir.y = Input.GetAxis("Mouse X");
        camRot.x = Input.GetAxis("Mouse Y");
        transform.Rotate(rotDir * Time.deltaTime * rotSpeed);
        cam.Rotate(-camRot * Time.deltaTime * rotSpeed);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = sprintSpeed;
        }
        else
        {
            sprint = 1;
        }
    }
}
