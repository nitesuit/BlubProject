using UnityEngine;
using System.Collections;

public class GearScript : MonoBehaviour
{
    public bool RotateClockwise = true;
    void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        var direction = transform.forward;
        if (RotateClockwise) direction *= -1;
        transform.RotateAround(transform.position, direction, Time.deltaTime * 45f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
