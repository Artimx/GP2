using UnityEngine;

public class OrbitalTarget : MonoBehaviour
{
    public Transform pointA, pointB;

    public float travelDuration;
    private float timer;
    private bool movingToB = true;

    private bool inHazardZone = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 start, target;

        timer += Time.deltaTime;
        float time = Mathf.Clamp01(timer / travelDuration);

        if (movingToB)
        {
            start = pointA.position;
            target = pointB.position;
        }
        else
        {
            start = pointB.position;
            target = pointA.position;
        }

        transform.position = Vector3.Lerp(start, target, time);

        if (time >= 1f)
        {
            movingToB = !movingToB;
            timer = 0f;
        }
    }

    private void OnEnable()
    {
        Debug.Log("Target Drone Enabled.");
    }

    private void OnDisable()
    {
        Debug.Log("Target Drone Disabled.");
    }

    private void OnMouseDown()
    {
        if (inHazardZone)
        {
            Debug.Log("Target Drone Clicked in Hazard Zone!");
            GameManager.Instance.DeductScore(1);
        }
        else
        {
            Debug.Log("Target Drone Clicked!");
            GameManager.Instance.AddScore(1);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTERED: " + other.gameObject.name);

        if (other.CompareTag("HazardZone"))
        {
            inHazardZone = true;
            Debug.Log("ENTERED HAZARD ZONE");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TRIGGER EXITED: " + other.gameObject.name);

        if (other.CompareTag("HazardZone"))
        {
            inHazardZone = false;
            Debug.Log("EXITED HAZARD ZONE");
        }
    }
}
