using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]*/
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;
  /*  [SerializeField] GameObject destroy;*/
    private Animator animator;
    bool isControl = true;
    /*bool isPush = false;*/
   /* public float jumForce = 5;*/
    private Rigidbody rb;
    /*public bool isOnGround = true;*/

    public static Player instance;
    [SerializeField] GameObject panelQuestion;
    [SerializeField] GameObject pQ1;
    [SerializeField] GameObject pQ2;
    [SerializeField] GameObject pQ3;
    [SerializeField] GameObject pQ4;
    [SerializeField] GameObject pQ5;
    [SerializeField] GameObject pQ6;
    [SerializeField] GameObject pQ7;

    [SerializeField] GameObject cH;
    [SerializeField] GameObject cH1;
    [SerializeField] GameObject cH2;
    [SerializeField] GameObject cH3;
    [SerializeField] GameObject cH4;
    [SerializeField] GameObject cH5;
    [SerializeField] GameObject cH6;

    [SerializeField] GameObject gameOver;

    [SerializeField] GameObject cube1;
    [SerializeField] GameObject cube2;
    [SerializeField] GameObject cube3;
    [SerializeField] GameObject cube4;

    [SerializeField] GameObject destroy;

    [SerializeField] int gameLevel;

    bool isAnswer1 = false;
    bool isAnswer2 = false;
    bool isAnswer3 = false;
    bool isAnswer4 = false;

    private void OnEnable()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
       
    }
    private void Update()
    {
        if(isControl)
        {
            float x = joystick.Horizontal;
            float y = joystick.Vertical;

            Vector3 dir = new Vector3(x, 0f, y).normalized;


            if (dir.magnitude >= 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                transform.position += new Vector3(x, 0f, y) * speed * Time.deltaTime;

            }

            if (x != 0 || y != 0)
            {
                animator.SetTrigger("Run");

            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
        if(isAnswer1 == true)
        {
            cube1.SetActive(true);
        }
        if (isAnswer2 == true)
        {
            cube2.SetActive(true);
        }
        if (isAnswer3 == true)
        {
            cube3.SetActive(true);
        }
        if (isAnswer4 == true)
        {
            cube4.SetActive(true);
        }
        /*if (isAnswer1 == true)
        {
            cube1.SetActive(true);
        }*/

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DeathZone")
        {
            gameOver.SetActive(true);
            Destroy(destroy);
            
        }

        if (collision.gameObject.tag == "Question")
        {
            panelQuestion.SetActive(true);          
        }
        else
        {
            panelQuestion.SetActive(false);
          
        }
        if(collision.gameObject.tag == "Q1")
        {
            pQ1.SetActive(true);
           
        }
        else
        {
            pQ1.SetActive(false);
        }
        if (collision.gameObject.tag == "Q2")
        {
            pQ2.SetActive(true);
            
        }
        else
        {
            pQ2.SetActive(false);
        }
        if (collision.gameObject.tag == "Q3")
        {
            pQ3.SetActive(true);
            
        }
        else
        {
            pQ3.SetActive(false);
        }
        if (collision.gameObject.tag == "Q4")
        {
            pQ4.SetActive(true);
           
        }
        else
        {
            pQ4.SetActive(false);
        }
        if (collision.gameObject.tag == "Q5")
        {
            pQ5.SetActive(true);
            
        }
        else
        {
            pQ5.SetActive(false);
        }
        if (collision.gameObject.tag == "Q6")
        {
            pQ6.SetActive(true);
            
        }
        else
        {
            pQ6.SetActive(false);
        }
        if (collision.gameObject.tag == "Q7")
        {
            pQ7.SetActive(true);
            
        }
        else
        {
            pQ7.SetActive(false);
        }

        if(collision.gameObject.tag == "C")
        {
            cH.SetActive(true);
        }
        else
        {
            cH.SetActive(false);
        }
        if (collision.gameObject.tag == "C1")
        {
            cH1.SetActive(true);
        }
        else
        {
            cH1.SetActive(false);
        }
        if (collision.gameObject.tag == "C2")
        {
            cH2.SetActive(true);
        }
        else
        {
            cH2.SetActive(false);
        }
        if (collision.gameObject.tag == "C3")
        {
            cH3.SetActive(true);
        }
        else
        {
            cH3.SetActive(false);
        }
        if (collision.gameObject.tag == "C4")
        {
            cH4.SetActive(true);
        }
        else
        {
            cH4.SetActive(false);
        }
        if (collision.gameObject.tag == "C5")
        {
            cH5.SetActive(true);
        }
        else
        {
            cH5.SetActive(false);
        }
        if (collision.gameObject.tag == "C6")
        {
            cH6.SetActive(true);
        }
        else
        {
            cH6.SetActive(false);
        }
        
    }
    public void Close()
    {
        isControl = false;
    }
    public void Open()
    {
        isControl = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(gameLevel);
    }
    public void Answer1()
    {
        isAnswer1 = true;
    }
    public void Answer2()
    {
        isAnswer2 = true;
    }
    public void Answer3()
    {
        isAnswer3 = true;
    }
    public void Answer4()
    {
        isAnswer4 = true;
    }

}
