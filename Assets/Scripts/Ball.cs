using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D rbplayer1;
    public Rigidbody2D rbplayer2;
    public Rigidbody2D rbicetandog;
    public Rigidbody2D rbicebrowndog;
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI scoreText;
    public int score1;
    public int score2;
    
    public GameObject goal;
    public GameObject player1;
    public GameObject player2;
    public GameObject confetti_left;
    public GameObject confetti_right;
    public GameObject rightgoaltrigger;
    public GameObject leftgoaltrigger;
    public GameObject spaceBG;
    public GameObject grassBG;
    public GameObject skyBG;
    public GameObject iceBG;
    public GameObject wingsp1;
    public GameObject wingsp2;
    public GameObject spaceGround;
    public GameObject grassGround;
    public GameObject iceGround;
    public GameObject IceBrownDog;
    public GameObject IceTanDog;




    public Animator player1animator;
    public Animator player2animator;
    public PlayerMovement player1script;
    public BrownDogMovement player2script;
    

    

    void Start()
    {
        score1 = 0;
        score2 = 0;
        UpdateScore();

        player1.transform.position= new Vector3(-7,2);
        player2.transform.position=new Vector3(7,2);  //tp players

        
        
    } //SCOREBOARD
    
    void UpdateScore()
    {
        scoreText.text = score1.ToString() + " - " + score2.ToString();
    }
    
    
    
    private void OnCollisionEnter2D(Collision2D collision) //COLLISIONS FOR THE NET
    {
        if(collision.gameObject.tag == "LeftGoal")
        {
            gameObject.SetActive(false);
            score2 = score2 + 1;
            goal.SetActive(true);
            confetti_left.SetActive(true);
            UpdateScore();
            HideEventName();
            Reset();
            
        }
            if(collision.gameObject.tag == "RightGoal")
        {
            
            gameObject.SetActive(false);
            score1 = score1 + 1;
            goal.SetActive(true);
            confetti_right.SetActive(true);
            UpdateScore();
            HideEventName();
            Reset();
            
            

        }

        void HideEventName()
        {
           
        }

    void Reset() //RESETS THE GOALS + ANY ALTERCATIONS MADE TO PLAYERS
    {
        rightgoaltrigger.SetActive(false);      //reset goal triggers and ball
        leftgoaltrigger.SetActive(false);
        gameObject.SetActive(true); 
        

       



        StartCoroutine(ResetStage());
        
        
    }
    IEnumerator ResetStage()
    {
            //reset phase (hide goal sign, bring back goal triggers)
        yield return new WaitForSeconds(3f);
        goal.SetActive(false);
        rightgoaltrigger.SetActive(true);
        leftgoaltrigger.SetActive(true);
        rb.velocity = Vector2.zero;
        confetti_left.SetActive(false);
        confetti_right.SetActive(false);
        transform.position = new Vector3(0,3,0);


                                    //this is the default blue of the ball
        Color DefaultBallColor = new Color(0.1f, 0.54f, 0.81f); 
        spriteRenderer.color = DefaultBallColor;

        

        wingsp1.SetActive(false); //turns off wings used in fly event
        wingsp2.SetActive(false);

        
        player1.SetActive(true);
        player2.SetActive(true);

        
        IceBrownDog.transform.position= new Vector3(7,20);
        IceTanDog.transform.position=new Vector3(-7,20);
        rbicetandog.velocity = Vector2.zero;            //ignore all this bullshit
        rbicebrowndog.velocity = Vector2.zero;             //fix for a dumb ice dog bug




        rbplayer1.gravityScale = 4f;        //gravity
        rbplayer2.gravityScale = 4f;
        rb.gravityScale = .5f;
        player1animator.speed = 1f;   //animation playback speed
        player2animator.speed = 1f;
        player1script.player1jumpcooldown = .75f;   //jump cooldown to .75
        player2script.player2jumpcooldown = .75f;
        player1script.moveSpeed = 9f;
        player2script.moveSpeed = 9f;


        grassGround.SetActive(true);
        spaceGround.SetActive(false); 
        iceGround.SetActive(false);

        grassBG.SetActive(true);   //turn on default background/ground and turn off all others
        spaceBG.SetActive(false);
        iceBG.SetActive(false);
        
        
        
        
        
        ChooseEvent();
        //teleport players and ball back to place
        
    }
    void ChooseEvent()
    {
        System.Random rnd = new System.Random();
        int randomevent  = rnd.Next(1, 5);
        // creates a random number between 1 and 4 to decide the next event
        if (randomevent == 2)
        {
            LowGravityEvent();
            Debug.Log("started grav event (rolled a 2)");
        }
        else if (randomevent == 3)
        {
            
            FlyEvent();
            Debug.Log("started fly event (rolled a 3)");
        }
        else if (randomevent == 4)
        {
            IceEvent();
            Debug.Log("started ice event(rolled a 4)");

        }
        else
        {
            DefaultEvent();
            Debug.Log("started default event (rolled a 1)");
        }
        
    }
    
    void DefaultEvent()
    {
        player1.transform.position= new Vector3(-7,2);
        player2.transform.position=new Vector3(7,2);
        
    }
    
    void LowGravityEvent() //all conditions for the low gravity event
    {
        grassBG.SetActive(false);
        grassGround.SetActive(false);
        spaceBG.SetActive(true);
        spaceGround.SetActive(true);

        
        spriteRenderer.color = Color.gray;  //change background to space and change ball color to gray

        player1.transform.position= new Vector3(-7,2);
        player2.transform.position=new Vector3(7,2);  //tp players
       
        rbplayer1.gravityScale = 2f;  //change gravity of players + ball
        rbplayer2.gravityScale = 2f;
        rb.gravityScale = .3f;

        player1animator.speed = .5f;    //slow down animation speed to look more natural
        player2animator.speed = .5f;

        player1script.player1jumpcooldown = 1.5f;   //raise jump cooldown so players cant jump midair
        player2script.player2jumpcooldown = 1.5f;
        player1script.moveSpeed = 6f;       //slow movespeed to look more "low grav"
        player2script.moveSpeed = 6f;
    }
    
    void FlyEvent()
    {   
        player1.transform.position= new Vector3(-7,2);
        player2.transform.position=new Vector3(7,2);

        player1script.player1jumpcooldown = 0f;   //lowers cooldown to allow players to fly
        player2script.player2jumpcooldown = 0f;
        
        wingsp1.SetActive(true);
        wingsp2.SetActive(true);

        grassBG.SetActive(false);
        skyBG.SetActive(true);

    }
    
    void IceEvent()
    {
        player1.transform.position= new Vector3(7,20);
        player2.transform.position=new Vector3(-7,20);
                    //ignore all this bullshit
                     //fix for a dumb ice dog bug
       
       
       IceBrownDog.SetActive(true);
       IceTanDog.SetActive(true);


        IceBrownDog.transform.position= new Vector3(7,2);
        IceTanDog.transform.position=new Vector3(-7,2);

        iceBG.SetActive(true);
        grassBG.SetActive(false);
        grassGround.SetActive(false);
        iceGround.SetActive(true);

        spriteRenderer.color = Color.white;

    


    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
    }

