using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject joker;
    public GameObject lion;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip coinSound;
    public float speed;
    public float jumpForce = 350f;
    public bool isLeft = false;
    public bool isRight = false;

    private int hp = 1;
    private AudioSource playerAudio;
    private Animator jokerAnima;
    private Animator lionAnima;
    private bool isDead = false;
    private Rigidbody2D playerRigid;
    private MoveCam cam;
    private int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 0;
        playerAudio = GetComponent<AudioSource>();
        cam = mainCamera.GetComponent<MoveCam>();
        playerRigid = GetComponent<Rigidbody2D>();
        jokerAnima = joker.GetComponent<Animator>();
        lionAnima = lion.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        jokerAnima.SetBool("Run", false);
        lionAnima.SetBool("Run", false);
        jokerAnima.SetBool("BackRun", false);
        lionAnima.SetBool("BackRun", false);
        isLeft = false;
        isRight = false;

        if (playerRigid.velocity.y < -1.875f)
        {
            jumpCount = 0;
        }

        speed = 0f;
        cam.speed = 0f;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (mainCamera.gameObject.transform.position.x <= -115.8f || mainCamera.gameObject.transform.position.x >= 0f)
        {
            cam.speed = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && isDead == false)
        {
            isRight = true;
            if (jokerAnima.GetBool("Jump") == false)
            {
                jokerAnima.SetBool("Run", true);
                lionAnima.SetBool("Run", true);
            }
            else
            {
                jokerAnima.SetBool("Run", false);
                lionAnima.SetBool("Run", false);
            }
            if (mainCamera.gameObject.transform.position.x > -115.8f)
            {
                if (mainCamera.gameObject.transform.position.x < 0f && transform.position.x < -5f)
                {
                    speed = -3f;
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                else
                {
                    cam.speed = 3f;
                }
            }

            if (mainCamera.gameObject.transform.position.x <= -115.8f)
            {
                cam.speed = 0f;
                if (transform.position.x < 8f)
                {
                    speed = -3f;
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) && isDead == false)
        {
            isLeft = true;
            if (jokerAnima.GetBool("Jump") == false)
            {
                jokerAnima.SetBool("BackRun", true);
                lionAnima.SetBool("BackRun", true);
            }
            else
            {
                jokerAnima.SetBool("BackRun", false);
                lionAnima.SetBool("BackRun", false);
            }

            if (mainCamera.gameObject.transform.position.x < 0f)
            {
                if (mainCamera.gameObject.transform.position.x > -115.8f && transform.position.x > -5f)
                {
                    speed = 2f;
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
                else
                {
                    cam.speed = -2f;
                }
            }

            if (mainCamera.gameObject.transform.position.x >= 0f)
            {
                cam.speed = 0f;
                if (transform.position.x > -8f)
                {
                    speed = 2f;
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isDead == false && jumpCount == 0)
        {
            playerAudio.clip = jumpSound;
            playerAudio.Play();
            jokerAnima.SetBool("Jump", true);
            lionAnima.SetBool("Jump", true);
            playerRigid.velocity = Vector2.zero;
            playerRigid.AddForce(new Vector2(0, jumpForce));
            jumpCount += 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isDead == false && playerRigid.velocity.y > -1.875f)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jokerAnima.SetBool("Jump", false);
            lionAnima.SetBool("Jump", false);
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            playerAudio.clip = coinSound;
            playerAudio.Play();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            hp -= 1;
            if (hp <= 0 && isDead == false)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        playerAudio.clip = deathSound;
        playerAudio.Play();
        playerRigid.velocity = Vector2.zero;
        isDead = true;
        jokerAnima.SetTrigger("Die");
        lionAnima.SetTrigger("Die");
    }
}
