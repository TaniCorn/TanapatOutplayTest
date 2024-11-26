using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof (ParticleSystem))]
[RequireComponent (typeof (MeshRenderer))]
public class MainGameObject : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField][Tooltip("Points to travel to")] List<Transform> points;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float distanceToPointThreshold = 0.5f;

    [Header("References")]
    AudioSource deathAudio;
    ParticleSystem deathParticle;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        deathAudio = GetComponent<AudioSource>();
        deathParticle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        this.transform.position = Vector3.zero;
        rb.useGravity = false;
        
        if (points == null)
        {
            Debug.LogError("No point has been specified");
            RemoveSelf();
        }
        else
        {
            // Unparent points so that points do not move with this object
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
            // Change velocity to new point
            Vector3 direction = (pointsToMoveTo[0].position - this.transform.position);
            Vector3 directionLastFrame = direction.normalized;
            rb.velocity = direction.normalized * speed;

            // First condition is to check if we have reached close enough to the point
            // Second condition is to check if we have moved past the point
            while ((Vector3.Distance(this.transform.position, pointsToMoveTo[0].position) >= distanceToPointThreshold) && 
                ((pointsToMoveTo[0].position - this.transform.position).normalized == directionLastFrame))
            {
                directionLastFrame = (pointsToMoveTo[0].position - this.transform.position).normalized;
                yield return null;
            }

            // Reached point, will stop if no points are left
            rb.velocity = Vector3.zero;
            pointsToMoveTo.Remove(pointsToMoveTo[0]);
            yield return null;
        }

        RemoveSelf();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        // Should be obstacles only
        RemoveSelf();
    }

    private void RemoveSelf()
    {
        StopCoroutine("MoveToPoint");
        GetComponent<MeshRenderer>().enabled = false;
        deathAudio.Play();
        deathParticle.Play();

        Destroy(this.gameObject, 5.0f);
    }

    [ContextMenu("Create a point")]
    void CreateAPoint()
    {
        GameObject pointGO = new GameObject("Point");
        pointGO.transform.parent = this.transform;
        points.Add(pointGO.transform);
    }
}
