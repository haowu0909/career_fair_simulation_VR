using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map : MonoBehaviour
{
    public GameObject left_cotroller;
    public GameObject miniMap;
    public InputActionProperty map_button; //X
    bool ifMap = false;

    // Start is called before the first frame update
    void Start()
    {
        map_button.action.performed += mapSwitch;
    }

    private void OnDestroy()
    {
        map_button.action.performed -= mapSwitch;

    }

    // Update is called once per frame
    void Update()
    {
        if (ifMap)
        {
            miniMap.SetActive(true);
            Vector3 controller_pos = left_cotroller.transform.position;
            miniMap.transform.position = new Vector3(controller_pos.x, controller_pos.y + 0.1f, controller_pos.z);
            //var rotationVector = miniMap.transform.rotation.eulerAngles;
            //rotationVector.y = 180;
            //miniMap.transform.rotation = Quaternion.Euler(rotationVector);
            miniMap.transform.parent = left_cotroller.transform;
        }
        else
        {
            miniMap.SetActive(false);
        }
    }

    public void mapSwitch(InputAction.CallbackContext context)
    {
        ifMap = !ifMap;
    }
}
