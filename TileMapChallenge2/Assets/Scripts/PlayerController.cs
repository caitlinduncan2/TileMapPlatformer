using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    private bool facingRight = true;

    private int score;
    private int lives;
    public Vector3 localScale;

   


    Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        SetScoreText();
        SetLivesText();
        winText.text = "";
        loseText.text = "";
        anim = GetComponent<Animator>();
        transform.localScale += new Vector3(0.1F, 0, 0);


    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 2);
        }
      

    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetScoreText();
            SetLivesText();
        }
        if (score == 4)
        {
            transform.position = new Vector3(0f, (float)13.49, 3.0f);
            GameObject.Find("Main Camera").transform.position = new Vector3(0.0f, 16.02f, -10.48f );
            lives = 3;
            SetLivesText();

        }

    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 8)
        {
            winText.text = "You Win!";

            FindObjectOfType<AudioManager>().Play("PlayerWin");
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            loseText.text = "You Lose!";
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }


        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}