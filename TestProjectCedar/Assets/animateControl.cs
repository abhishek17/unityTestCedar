using UnityEngine;
using System.Collections;

public class animateControl : MonoBehaviour {

	// Use this for initialization
    Animator animComp;
	void Start () {
        animComp = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spriteClick()
    {
        Debug.Log("sprite sheet clicked");
        animComp.Play("click");
        
    }
}
