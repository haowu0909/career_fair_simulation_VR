using UnityEngine;
using UnityEngine.InputSystem;

public class GraspGrabber : Grabber
{
    public InputActionProperty grabAction;

    Grabbable currentObject;
    Grabbable grabbedObject;


    //midpoint
    public GameObject midpoint_cube;
    public GameObject left_cotroller;
    public GameObject right_cotroller;
    private float lastDistance ;

    public InputActionProperty triangle_grab;
    bool ifTriangle = false;
    void Start()
    {
        lastDistance = 0;
        grabbedObject = null;
        currentObject = null;

        grabAction.action.performed += Grab;
        grabAction.action.canceled += Release;
        triangle_grab.action.performed += triangleSwitch;
    }

    private void OnDestroy()
    {
        grabAction.action.performed -= Grab;
        grabAction.action.canceled -= Release;
        triangle_grab.action.performed -= triangleSwitch;

    }

    // Update is called once per frame
    void Update()
    {
        
        //midpoint_cube.active = true;
        


        if (grabbedObject && ifTriangle)
        {
            midpoint_cube.active = true;
            Vector3 betweenCotrollers = left_cotroller.transform.position + right_cotroller.transform.position;
            float curDis = Vector3.Distance(left_cotroller.transform.position, right_cotroller.transform.position);
            float lift_rate = curDis / lastDistance;

            Vector3 pos = betweenCotrollers / 2;
            pos.y = lift_rate * pos.y;
            midpoint_cube.transform.position = pos;

            grabbedObject.transform.parent = midpoint_cube.transform;
            grabbedObject.transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    public void triangleSwitch(InputAction.CallbackContext context)
    {
        ifTriangle = !ifTriangle;
    }

    public override void Grab(InputAction.CallbackContext context)
    {
        if (currentObject && grabbedObject == null)
        {
            if (currentObject.GetCurrentGrabber() != null)
            {
                currentObject.GetCurrentGrabber().Release(new InputAction.CallbackContext());
            }

            grabbedObject = currentObject;
            grabbedObject.SetCurrentGrabber(this);

            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }

            grabbedObject.transform.parent = this.transform;
            midpoint_cube.active = false;
            //Vector3 distance = left_cotroller.transform.position - right_cotroller.transform.position;
            lastDistance = Vector3.Distance(left_cotroller.transform.position, right_cotroller.transform.position);
        }



    }

    public override void Release(InputAction.CallbackContext context)
    {
        if (grabbedObject)
        {
            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            }

            grabbedObject.SetCurrentGrabber(null);
            grabbedObject.transform.parent = null;
            grabbedObject = null;
            midpoint_cube.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentObject == null && other.GetComponent<Grabbable>())
        {
            currentObject = other.gameObject.GetComponent<Grabbable>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentObject)
        {
            if (other.GetComponent<Grabbable>() && currentObject.GetInstanceID() == other.GetComponent<Grabbable>().GetInstanceID())
            {
                currentObject = null;
            }
        }
    }
}
