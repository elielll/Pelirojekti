using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform position_A, position_B;
    public float speed;
    public Transform startPosition;

    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = startPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == position_A.position)
        {
            nextPosition = position_B.position;
        }
        if (transform.position == position_B.position)
        {
            nextPosition = position_A.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }
    //piirt‰‰ apuviivan alku ja loppupisteen v‰lille
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position_A.position, position_B.position);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
