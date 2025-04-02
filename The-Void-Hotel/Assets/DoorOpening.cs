using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    private float openAngle = 90f;
    private float closedAngle = 0f;
    public RaycastHit hit;
    public float doorRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, doorRange))
            {
                print("Raycast...");
                if (hit.collider.gameObject.tag == "Door")
                {
                    print("Open ne noor!");
                    Transform Door = hit.collider.transform;

                    if (Mathf.Approximately(Door.eulerAngles.y, openAngle))
                    {
                        Door.eulerAngles = new Vector3(Door.eulerAngles.x, closedAngle, Door.eulerAngles.z);
                    }
                    else
                    {
                        Door.eulerAngles = new Vector3(Door.eulerAngles.x, openAngle, Door.eulerAngles.z);
                    }
                }
            }
        }
    }
}
