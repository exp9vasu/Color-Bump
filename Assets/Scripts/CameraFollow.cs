using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject ball;
	public Vector3 offset;
	//public float lerpRate;
	public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        offset =  transform.position - ball.transform.position;
		gameOver = false;
    }

    // Update is called once per frame
    void Update()    {
        if (!gameOver){
			Follow ();
		}
    }
	
	void Follow()
	{
		transform.position = ball.transform.position + offset;
	}
}
