using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    public GameObject rig;
    public GameObject google;
    public GameObject fb;
    public GameObject linkedin;
    public GameObject qq;
    public GameObject mmm;


    public GameObject initial;

    public void initial_tp()
    {
        rig.transform.position = initial.transform.position;
    }

    public void google_tp()
    {
        rig.transform.position = google.transform.position;
    }

    public void fb_tp()
    {
        rig.transform.position = fb.transform.position;
    }

    public void linkedin_tp()
    {
        rig.transform.position = linkedin.transform.position;
    }

    public void qq_tp()
    {
        rig.transform.position = qq.transform.position;
    }

    public void mmm_tp()
    {
        rig.transform.position = mmm.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
