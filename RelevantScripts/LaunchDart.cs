using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;


public class LaunchDart : MonoBehaviour
{
    public OVRInput.Controller _controller;
    public OVRInput.Button _button;
    public Grabbable _grabbable;
    private Rigidbody dartRigidbody;
    public Vector3 velocity;
    public float speed = 0;
    Vector3 releaseVelocity;
    Vector3 releaseAngularVelocity ;
    public AudioSource audioSource;


    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("ShootingAudioSource").GetComponent<AudioSource>();
        dartRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _grabbable = GetComponentInChildren<Grabbable>();
        // if(_grabbable.SelectingPoints.Count > 0)
        // {

        // }
        dartRigidbody.isKinematic = false;
        if (_grabbable.SelectingPoints.Count > 0)
        {
            releaseAngularVelocity = OVRInput.GetLocalControllerAngularVelocity(_controller);
            // Follow the controller's position and rotation
            transform.position = OVRInput.GetLocalControllerPosition(_controller);
            transform.rotation = OVRInput.GetLocalControllerRotation(_controller);
            // releaseVelocity = OVRInput.GetLocalControllerVelocity(_controller);
            // releaseAngularVelocity = OVRInput.GetLocalControllerAngularVelocity(_controller);
            velocity = OVRInput.GetLocalControllerVelocity(_controller);
            speed = velocity.magnitude;
            // dartRigidbody.isKinematic = false;
            // Release the dart when the trigger is released
            if (OVRInput.GetUp( _button, _controller))
            {
                Debug.Log("Released button!");
                dartRigidbody.isKinematic = false;
                ReleaseDart();
                dartRigidbody.useGravity = true;
                transform.parent = null;
                audioSource.Play();
                OVRInput.SetControllerVibration(0.1f, 0.1f,OVRInput.Controller.RTouch);
                Destroy(gameObject, 5f);
            }
            transform.rotation = Quaternion.LookRotation(dartRigidbody.velocity);
            if(dartRigidbody.isKinematic == false)
            {
                Destroy(gameObject, 5f);
            }
        }
        // if (_grabbable.SelectingPoints.Count == 0 && OVRInput.GetUp( _button, _controller) && flag == true)
        // {
        //     flag = false;
        //     Debug.Log("Released button!");
        //     dartRigidbody.isKinematic = false;
        //     dartRigidbody.useGravity = true;
        //     ReleaseDart();
        // }

    }

    void ReleaseDart()
    {
        dartRigidbody.isKinematic = false;

        // releaseVelocity = OVRInput.GetLocalControllerVelocity(_controller);
        // releaseAngularVelocity = OVRInput.GetLocalControllerAngularVelocity(_controller);
        releaseVelocity = OVRInput.GetLocalControllerVelocity(_controller);
        releaseAngularVelocity = OVRInput.GetLocalControllerAngularVelocity(_controller);
        dartRigidbody.velocity = releaseVelocity;
        dartRigidbody.angularVelocity = releaseAngularVelocity;
        Vector3 propulsionDirection = transform.forward;
        dartRigidbody.AddForce(propulsionDirection * speed, ForceMode.Impulse);
        Debug.Log($"Dart released with velocity: {releaseVelocity} and angular velocity: {releaseAngularVelocity}");
    }
}
