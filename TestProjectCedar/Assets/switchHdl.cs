using UnityEngine;
using System.Collections;
using System.Text;

public class switchHdl : MonoBehaviour {

    float endPosY;
    bool currentlyON;
    public float animationSpeedScalar;
    RectTransform rectTranformObj;
    private GameObject networkObj;
    private UdpSocketManagerUsageExample networkScr;
    // Use this for initialization
    void Start()
    {
        endPosY = -50.0f;//off
        currentlyON = false;
        rectTranformObj = this.gameObject.GetComponent<RectTransform>();
        networkObj = GameObject.Find("networkObject");
        networkScr = (UdpSocketManagerUsageExample)networkObj.GetComponent(typeof(UdpSocketManagerUsageExample));
        //transform.position = new Vector3(transform.position.x, 50.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        animate();
    }

    public void onSwitchClick()
    {
        Debug.Log("Switch clicked ");
        currentlyON = !currentlyON;
        endPosY = -endPosY;

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
        string switchClickMessage = "Switch is switched ";
        if(currentlyON)
        {
            switchClickMessage += "on";
        }
        else
        {
            switchClickMessage += "off";
        }
        networkScr._udpSocketManager.send(Encoding.UTF8.GetBytes(switchClickMessage));
        Debug.Log("UdpSocketManager, Sent" + switchClickMessage);
    }

    public void animate()
    {
        

        if (currentlyON && rectTranformObj.localPosition.y < endPosY)
        {
            rectTranformObj.localPosition = new Vector3(rectTranformObj.localPosition.x, rectTranformObj.localPosition.y + animationSpeedScalar * Time.deltaTime, rectTranformObj.localPosition.z);
        }
        else if (!currentlyON && rectTranformObj.localPosition.y > endPosY)
        {
            rectTranformObj.localPosition = new Vector3(rectTranformObj.localPosition.x, rectTranformObj.localPosition.y - animationSpeedScalar * Time.deltaTime, rectTranformObj.localPosition.z);
        }

    }
}
