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
    [SerializeField] List<Transform> points;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float distanceToPointThreshold = 0.5f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        deathAudio = GetComponent<AudioSource>();
        deathParticle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        if (points == null)
        {
            Debug.LogError("No point has been specified");
        }
        else
        {
            // So points do not move with this object
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
