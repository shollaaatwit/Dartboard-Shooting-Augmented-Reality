using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerCollider : MonoBehaviour
{
    public bool isCooldownActive = true;
    public bool anchorMoved = false;
    public AudioSource buttonAudio;

    public PositionTracker dl;
    public GameObject sphere;

    public GameObject text1;
    public GameObject text2;
    void OnEnable()
    {
        dl = FindObjectOfType<PositionTracker>();
        sphere = GameObject.Find("[BuildingBlock] DartBoard");

        if(dl.returnPosition() != sphere.transform.position)
        {
            anchorMoved = true;
        }


        buttonAudio = GetComponent<AudioSource>();
        StartCoroutine(DisableCooldown());
    }

    void Update()
    {
        if(dl.returnPosition() != sphere.transform.position)
        {
            anchorMoved = true;
        }

        if(anchorMoved)
        {
            if(text1 != null)
            {
                text2.SetActive(false);
                text1.SetActive(true);
            }
        }
        else
        {
            if(text1 != null)
            {
                text2.SetActive(true);
                text1.SetActive(false);
            }            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isCooldownActive && anchorMoved)
        {
            Debug.Log("mboop");    
            gameObject.GetComponent<Toggle>().isOn = true;
            // buttonAudio.Play();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!isCooldownActive && anchorMoved)
        {
            buttonAudio.Play();
        }        
    }

    
    private IEnumerator DisableCooldown()
    {
        yield return new WaitForSeconds(2f);
        isCooldownActive = false;
    }
}
