using UnityEngine;
using System.Collections;

public class basicScr : MonoBehaviour {   //MonoBehavior makes the class inherit the gameobject class. This gives all kinds of functionalities from scripting, for example, access to different components and their input control

    public Light myLight;
	// Use this for initialization
	void Start () {
        
	}
    void MyFunction()
    {
        myLight.enabled = !myLight.enabled;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space pressed");
            MyFunction();
        }
	}
}
