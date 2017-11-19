using UnityEngine;
using System.Collections;
using System.Text;

public class knobDrag : MonoBehaviour {

	// Use this for initialization
    public float movementScalar = 50.0f;
    private GameObject networkObj;
    private UdpSocketManagerUsageExample networkScr;
	void Start () {
        networkObj = GameObject.Find("networkObject");
        networkScr = (UdpSocketManagerUsageExample)networkObj.GetComponent(typeof(UdpSocketManagerUsageExample));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void drag()
    {
        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            transform.Rotate(-Vector3.forward * Time.deltaTime * movementScalar, Space.World);
            print("Mouse moved left");
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving right
            transform.Rotate(Vector3.forward * Time.deltaTime * movementScalar, Space.World);
            print("Mouse moved right");
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            //Code for action on mouse moving up
            transform.Rotate(-Vector3.forward * Time.deltaTime * movementScalar, Space.World);
            print("Mouse moved up");
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            //Code for action on mouse moving down
            transform.Rotate(Vector3.forward * Time.deltaTime * movementScalar, Space.World);
            print("Mouse moved down");
        }

    }

    public void mouseUp()
    {
        //send it over network
        if (!networkScr._udpSocketManager.isInitializationCompleted())
        {
            return;
        }
        if (!networkScr._isListenPortLogged)
        {
            Debug.Log("UdpSocketManager, listen port: " + networkScr._udpSocketManager.getListenPort());
            networkScr._isListenPortLogged = true;
        }
        string knobMovementMessage = "Knob Moved to " + transform.eulerAngles.z +" degrees";
        networkScr._udpSocketManager.send(Encoding.UTF8.GetBytes(knobMovementMessage));
        Debug.Log("UdpSocketManager, Sent" + knobMovementMessage);
    }
}
