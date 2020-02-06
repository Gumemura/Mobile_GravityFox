using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverte_gravidade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
    	if(transform.Find("Player").gameObject.GetComponent<Input_player>().morreu() == false){
	        foreach(Touch touch in Input.touches){
				if (touch.phase == TouchPhase.Began){
					GetComponent<Rigidbody2D>().gravityScale *= -1;
				}
			}
    	}
    }
}
