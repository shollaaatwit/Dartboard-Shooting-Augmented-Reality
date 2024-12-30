using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShot : MonoBehaviour
{
    public Vector3 destroyPosition;
    public GameObject dummyDart;
    public AudioSource hitSound;

    void Start()
    {
        hitSound = GameObject.Find("[BuildingBlock] DartBoard").GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        float distance = velocity.magnitude * Time.fixedDeltaTime;

        if (Physics.Raycast(transform.position, velocity.normalized, out RaycastHit hit, distance))
        {
            HandleCollision(hit);
        }
    }

    void HandleCollision(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;

        if (hitObject.CompareTag("Triangle"))
        {
            hitObject.GetComponent<WheelPoints>().UpdatePoints();
            Rigidbody rb = hitObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Updating points.");
            rb.isKinematic = true;
            destroyPosition = transform.position;
            Instantiate(dummyDart, destroyPosition, Quaternion.identity);
            Destroy(gameObject);
            hitSound.Play();
        }
    }
}
