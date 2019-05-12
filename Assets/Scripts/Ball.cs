using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

#pragma warning disable 0618


public class Ball : NetworkBehaviour 
{
	Rigidbody my_rbody;


	Text scoreLabel;
	Text speedLabel;

	
	Text winnerLabel; 

	[SyncVar] 
	public float ballSpeed = 10;

	
	[SyncVar]
	int playerScore;
	[SyncVar]
	int enemyScore;

	[SyncVar]
	string headerText;

	[SyncVar]
	string subtext;



	public AudioSource phit;


	public AudioSource whit;


	public AudioSource pscore;

	public AudioSource escore;

	public AudioSource gOver;


	public AudioSource victory;









	// Use this for initialization
	void Start () 
	{
		my_rbody = GetComponent<Rigidbody>();
		scoreLabel = GameObject.Find("Scoreboard").GetComponent<Text>();
		scoreLabel.text = "0:0";
		speedLabel = GameObject.Find("Speed").GetComponent<Text>();
		speedLabel.text = "Speed: " + ballSpeed;
		winnerLabel = GameObject.Find("Winner").GetComponent<Text>();
		phit = GameObject.Find("PaddleFX").GetComponent<AudioSource>();
		whit = GameObject.Find("WallFX").GetComponent<AudioSource>();
		pscore = GameObject.Find("PScoreFX").GetComponent<AudioSource>();
		escore = GameObject.Find("EScoreFX").GetComponent<AudioSource>();
		gOver = GameObject.Find("GameOverFX").GetComponent<AudioSource>();
		victory = GameObject.Find("VictoryFX").GetComponent<AudioSource>();







		// Invoke("resetBall", 2);
		//print(my_rbody.velocity.magnitude);
	}

	public void resetBall()
	{
		Random rand = new Random();
		float angle = Mathf.Round(Random.Range(-4f, 4f));

		transform.position = new Vector3(0, 0, 0);
		my_rbody.velocity = (new Vector3(-1, angle, 0)).normalized * ballSpeed;
		speedLabel.text = "Speed: " + ballSpeed;


	}


	void OnCollisionEnter(Collision collision) 
	{
		if(collision.collider.name == "PlayerPaddle(Clone)")
		{
			phit.Play();
			my_rbody.velocity = 1.10f * my_rbody.velocity;

			//Speed up ball by 10%
			//CmdIncBallSpeed();
			
			//speedLabel.text = "Speed: " + Mathf.Round(my_rbody.velocity.magnitude);

		}

		//Sound effects
		if(collision.collider.name == "EnemyPaddle")  phit.Play();
		if(collision.collider.name == "TopWall")  whit.Play();
		if(collision.collider.name == "BottomWall")  whit.Play();
		if(collision.collider.name == "RightWall")  whit.Play();
		if(collision.collider.name == "LeftWall")  whit.Play();


		if(collision.collider.name == "LeftWall" || collision.collider.name == "RightWall")
		{
			if(collision.collider.name == "LeftWall")
			{
				pscore.Play();
				playerScore++;


				// CmdplayerScrored();
				// RpcUpdateScore();
				if(playerScore >= 5) 
				{
					victory.Play();
					//winnerLabel.text = "You Win!!";
 					Time.timeScale = 0; 
				}
				
			}
			if(collision.collider.name == "RightWall")
			{
				escore.Play();
				enemyScore++;
				
				// CmdenemyScored();
				// RpcUpdateScore();

				if(enemyScore >= 5) 
				{
					gOver.Play();
					//winnerLabel.text = "Game Over!";
					Time.timeScale = 0; 


				}

			}

			//scoreLabel.text = enemyScore + ":" + playerScore;
			//RpcUpdateScore();

			//Stop ball then rest after 1 second
			my_rbody.velocity = Vector3.zero;
			Invoke("resetBall", 1);

		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isServer)
		{
			headerText = enemyScore + ":" + playerScore;
			print(headerText);
			subtext = "Speed: " + Mathf.Round(my_rbody.velocity.magnitude);

			if(playerScore == 5) winnerLabel.text = "You Win!!";
			if(enemyScore == 5) winnerLabel.text = "You Lose!!";

		}		

		if(isClient && !isServer)
		{
			if(playerScore == 5) winnerLabel.text = "You Lose!!";
			if(enemyScore == 5) winnerLabel.text = "You Win!!";
		}

		if(headerText != null) 	scoreLabel.text = headerText;
		if(subtext != null)  speedLabel.text = subtext;

	



	}

	// [Command]
	// void CmdIncBallSpeed()
	// {
	// 		my_rbody.velocity = 1.10f * my_rbody.velocity;
	// }

	// [Command]
	// void CmdplayerScrored()
	// {
	// 	playerScore++;
	// }

	// [Command]
	// void CmdenemyScored()
	// {
	// 	enemyScore++;
	// }


	// [ClientRpc]
	// void RpcUpdateScore()
	// {
	// 		scoreLabel.text = enemyScore + ":" + playerScore;
	// }
}
