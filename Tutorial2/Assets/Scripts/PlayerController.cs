using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text lifesText;
    public Text loseText;
    public Text winText;

    private int scoreValue;
    private int lives;
    

    private GameObject coin;

    private SpriteRenderer mySpriteRenderer;

    Animator anim;


    void Awake(){
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Start(){
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives = 3;
        scoreValue = 0;

        loseText.text = "";
        winText.text = "";
        SetLifesText();
        
        
        coin = GameObject.FindGameObjectWithTag("Coin");
        anim = GetComponent<Animator>();
    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.D)){

            anim.SetInteger("State", 2);

            if (mySpriteRenderer != null)
            {
                
                mySpriteRenderer.flipX = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.D))
        {

            anim.SetInteger("State", 0);

            if (mySpriteRenderer != null)
            {

                mySpriteRenderer.flipX = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            anim.SetInteger("State", 2);

            if (mySpriteRenderer != null)
            {

                mySpriteRenderer.flipX = true;
            }

        }

        if (Input.GetKeyUp(KeyCode.A))
        {

            anim.SetInteger("State", 0);

            if (mySpriteRenderer != null)
            {

                mySpriteRenderer.flipX = true;
            }

        }

       

    }


    void FixedUpdate(){
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.collider.tag == "Coin"){
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            collision.collider.gameObject.SetActive(false);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(-31.0f, 1.0f);
                rd2d.velocity = Vector2.zero;
                lives = 3;
                SetLifesText();
                //Debug.Log("run");
            }
        }

        else if (collision.collider.tag == "Enemy"){
            collision.collider.gameObject.SetActive(false);
            lives = lives - 1;
            SetLifesText();

        }

        if (scoreValue == 8)
        {

            winText.text = "You Win! Game made by Nick Hennessy";
            AudioListener.pause = true;

            
            
        }






    }

    private void OnCollisionStay2D(Collision2D collision){

        if (collision.collider.tag == "Ground"){


            if (Input.GetKey(KeyCode.W)){

                //anim.SetInteger("State", 3);
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

            else if (collision.collider.tag == "Ground")
            {
               

            }
        }
    }


    

    void SetLifesText()
    {

        lifesText.text = "Lives: " + lives.ToString();

        if (lives == 0)
        {
            loseText.text = "You Lose!";
            // DestroyPlayer();
            Destroy(gameObject);

        }

        
    }

}
