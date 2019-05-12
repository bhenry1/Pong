using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Disable obsolete API warning.
#pragma warning disable 0618
public class Player_Controller : NetworkBehaviour 
{
//n = numofplayers

  	[SyncVar]
    Color myColor;

	public float playerSpeed = 10;



	public override void OnStartServer()
	{

		myColor = Random.ColorHSV();
        GetComponent<Renderer>().material.color = myColor;


		int numOfPlayers = GameObject.Find("Netman").GetComponent<NetworkManager>().numPlayers;
		print(numOfPlayers);
		if(numOfPlayers == 2 ) 
		{
			//call the resert ball method
			GameObject.Find("Ball").GetComponent<Ball>().resetBall();


		}

		if(isServer)
		{
			
		}

		if(isLocalPlayer)
		{

		}

	}

	  public override void OnStartClient()
    {
        GetComponent<Renderer>().material.color = myColor;
        //bod = GetComponent<Rigidbody>();
    }

	



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame rouplhy 60fps
	void Update () 
	{

	

		if(isLocalPlayer)
		{

		
		Vector3 cur_pos = transform.position; //Cur Pos of the paddle
		float vertical = Input.GetAxis("Vertical"); //Gets pos of the players vertical input pos
		//If V = 1, the controller is all the way to the top
		//If V = =1 the controller is all the way to the bottom

		cur_pos.y += vertical * playerSpeed * Time.deltaTime;

		//4.5 is the top wall pos, -2.5 is the bottom pos
		if(cur_pos.y >=  4.5f ) cur_pos.y = 4.5f;
		else if(cur_pos.y <= -2.5f ) cur_pos.y = -2.5f;

		
		transform.position = cur_pos;
		}
	}
}
