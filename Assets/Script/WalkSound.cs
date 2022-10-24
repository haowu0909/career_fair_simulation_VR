using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionProperty walkAction;
    public AudioSource walkingsound;
    float time = 0.0f;
    void Start()
    {
        walkingsound.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 joystickValue = walkAction.action.ReadValue<Vector2>();
        time += Time.deltaTime;
        if(time >= 1 && joystickValue.x >= 0.05f)
        {
            walkingsound .Play();
            time = 0.0f;
        }
        

    }
}
