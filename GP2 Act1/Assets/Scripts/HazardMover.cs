using UnityEngine;

public class HazardMover : MonoBehaviour
{
    public Transform pointA, pointB;

    public float travelDuration;
    private float timer;
    private bool movingtoA = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HazardZone"))
        {
            Debug.Log("Target Drone entered the hazard zone!");

            GameManager.Instance.DeductScore(1);
        }
    }

    void Update()
    {
        Vector3 start, target;

        timer += Time.deltaTime;
        float time = Mathf.Clamp01(timer / travelDuration);

        if (movingtoA)
        {
            start = pointB.position;
            target = pointA.position;
        }
        else
        {
            start = pointA.position;
            target = pointB.position;
        }
        
        transform.position = Vector3.Lerp(start, target, time);

        if (time >= 1f)
        {
            movingtoA = !movingtoA;
            timer = 0f;
        }
    }
}
