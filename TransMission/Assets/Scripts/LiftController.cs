using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 0.1F;
    private float startTime;
    private float journeyLength;
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

        if (transform.position == endMarker.position)
        {
            startTime = Time.time;
            Transform temp = endMarker;
            endMarker = startMarker;
            startMarker = temp;
        }
    }
}