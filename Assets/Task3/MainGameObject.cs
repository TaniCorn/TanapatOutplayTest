using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class MainGameObject : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float distanceToPointThreshold = 0.5f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (points == null)
        {
            Debug.LogError("No point has been specified");
        }
        else
        {
            foreach (Transform t in points)
            {
                t.parent = null;
            }
            StartCoroutine(MoveToPoint(points));
        }
    }

    private IEnumerator MoveToPoint(List<Transform> pointsToMoveTo)
    {
        while (pointsToMoveTo.Count > 0)
        {
            Vector3 direction = (pointsToMoveTo[0].position - this.transform.position);
            Vector3 directionLastFrame = direction.normalized;

            rb.velocity = direction.normalized * speed;

            while ((Vector3.Distance(this.transform.position, pointsToMoveTo[0].position) >= distanceToPointThreshold) && 
                ((pointsToMoveTo[0].position - this.transform.position).normalized == directionLastFrame))
            {
                directionLastFrame = (pointsToMoveTo[0].position - this.transform.position).normalized;
                yield return null;
            }

            rb.velocity = Vector3.zero;
            pointsToMoveTo.Remove(pointsToMoveTo[0]);

            yield return null;
        }
        rb.velocity = Vector3.zero;
        RemoveSelf();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        // Should be obstacles only

        RemoveSelf();
    }

    private void RemoveSelf()
    {
        // Spawn Sound
        // Spawn VFX

        // Destroy(gameObject);
        Debug.Log("Hit");
    }

    [ContextMenu("Create a point")]
    void CreateAPoint()
    {
        GameObject pointGO = new GameObject("Point");
        pointGO.transform.parent = this.transform;
        points.Add(pointGO.transform);
    }
}
