using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Blink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	InvokeRepeating("blink_text", 1, .7f);
    }

    // Update is called once per frame
    void Update()
    {

    }

	void blink_text(){
    	gameObject.SetActive(gameObject.activeSelf == false);
	}
}
