using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour 
{

public float enemySpeed = 10;

	GameObject ball;

	// Use this for initialization
	void Start () 
	{
	  ball = GameObject.Find("Ball");

	}
	
	// Update is called once per frame rouplhy 60fps
	void Update () 
	{
		Vector3 cur_pos = transform.position; 
		float ballyPos = ball.transform.position.y;
		//Math.clamp

		if(cur_pos.y >=  4.3f ) cur_pos.y = 4.3f;
		if(cur_pos.y <= -2.3f ) cur_pos.y = -2.3f;

		if(ballyPos > cur_pos.y)
		{
		cur_pos.y +=  enemySpeed * Time.deltaTime;

		}

		
		else if(ballyPos < cur_pos.y)
		{
		cur_pos.y -=  enemySpeed * Time.deltaTime;

		}
		//Cur Pos of the paddle
		//float vertical = Input.GetAxis("Vertical"); //Gets pos of the players vertical input pos
		//If V = 1, the controller is all the way to the top
		//If V = =1 the controller is all the way to the bottom

		//cur_pos.y += vertical * playerSpeed * Time.deltaTime;

		//4.5 is the top wall pos, -2.5 is the bottom pos
	




		
		transform.position = cur_pos;
	}
}
