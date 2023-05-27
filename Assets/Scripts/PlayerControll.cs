using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerControll : MonoBehaviour
{
    //Movement
    public float speed;
    public float turnspeed;
    private float HorizontalInput;
    private float ForwardInput;
    //Coins
    public float coins=0;
    public TMP_Text coinCounterText;
    //Animation
    Animator enemyAnimator;
    Animator enemyAnimator1;
    Animator enemyAnimator2;
    Animator enemyAnimator3;
    Animator animator;
    //Health
    public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthBar;
     private bool canTakeDamage = true;
    private float cooldownTimer = 0f;
    public float cooldownDuration = 0.75f;


    // Start is called before the first frame update
    void Start()
    {

        //Enemy Animator
        enemyAnimator= GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
        enemyAnimator1= GameObject.FindGameObjectWithTag("Enemy1").GetComponent<Animator>();
        enemyAnimator2= GameObject.FindGameObjectWithTag("Enemy2").GetComponent<Animator>();
        enemyAnimator3= GameObject.FindGameObjectWithTag("Enemy3").GetComponent<Animator>();

        //Player Animator
        animator=GetComponent<Animator>();
        Debug.Log(animator);
        
        //UI
        coinCounterText.text="COINS: 0";
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        HorizontalInput= Input.GetAxis("Horizontal");
        ForwardInput= Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward*Time.deltaTime*speed*ForwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime*turnspeed*HorizontalInput);
        coinCounterText.text="COINS: "+coins;

        //Check Walking
        if (Input.GetKey("w")){
            animator.SetBool("IsWalking",true);
            speed=1f;
            turnspeed=60f;
        }
        if (!Input.GetKey("w")){
            animator.SetBool("IsWalking",false);
        }

        //Check Running
        if (Input.GetKey("w") && Input.GetKey("left shift")){
            animator.SetBool("IsRunning",true);
            speed=5f;
            turnspeed=75f;

        }
        if (!Input.GetKey("w") || !Input.GetKey("left shift")){
            animator.SetBool("IsRunning",false);

        }

        //Check Attack
        if (Input.GetKey(KeyCode.Mouse0)){
            animator.SetBool("Attack",true);
        }
        if (!Input.GetKey(KeyCode.Mouse0)){
            animator.SetBool("Attack",false);
        }
        if (!canTakeDamage)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canTakeDamage = true;
            }
        }

        //HealthBar

        if(currentHealth<=0){
            SceneManager.LoadScene("LostMenu");   
        }

        if(currentHealth<100 && Input.GetKey("h") && coins>=300){
            healthBar.SetHealth(maxHealth);
            currentHealth=maxHealth;
            coins-=300;
        }


         GameObject Boss = GameObject.FindGameObjectWithTag("Enemy3");
        if (Boss != null)
        {
            Vector3 directionToEnemy = transform.position - Boss.transform.position;
            directionToEnemy.y = 0f; // Ignore vertical difference if not needed

            if (Vector3.Dot(Boss.transform.forward, directionToEnemy.normalized) > 0.5f && Vector3.Distance(transform.position, Boss.transform.position) <= 2.0f && enemyAnimator3.GetBool("Attack") && canTakeDamage)
            {
                currentHealth -= 30;
                healthBar.SetHealth(currentHealth);
                canTakeDamage = false;
                cooldownTimer = cooldownDuration;
            }
        }



    }

        private void OnCollisionStay(Collision other) {
            if(other.gameObject.CompareTag("Door") && Input.GetKey("f"))
            {
                
                Destroy(other.gameObject);
            }

            if(other.gameObject.CompareTag("Openable") && Input.GetKey("f"))
            {
            Destroy(other.gameObject);
            }
            if(other.gameObject.CompareTag("Coin") && Input.GetKey("e"))
            {
              Destroy(other.gameObject); 
              coins+=100;
            }
            
            if(other.gameObject.CompareTag("Enemy") && enemyAnimator.GetBool("Attack"))
		{
			currentHealth-=10;
            healthBar.SetHealth(currentHealth);
		}

          if(other.gameObject.CompareTag("Enemy1") && enemyAnimator1.GetBool("Attack"))
		{
			currentHealth-=10;
            healthBar.SetHealth(currentHealth);
		}

          if(other.gameObject.CompareTag("Enemy2") && enemyAnimator2.GetBool("Attack"))
		{
			currentHealth-=20;
            healthBar.SetHealth(currentHealth);
		}

          if(other.gameObject.CompareTag("TeleportIn") && Input.GetKey("t")){
            GameObject teleportExit= GameObject.FindGameObjectWithTag("TeleportOut");
            transform.position = teleportExit.transform.position;
        }
        

        }


}
