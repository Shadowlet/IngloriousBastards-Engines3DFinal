using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;

    private GameObject scoreManager;
    private GameObject abilityUI;
    private GameObject gameManager;

    public Animator anim_player;

    public AudioSource audio;
    public AudioClip audioClip;

    public float moveForce;
    public float dashForce;
    public float jumpForce;
    public float rotationForce;

    public float dashTimer;

    public bool isGrounded;
    public bool isDashing;
    public bool canDash;

    public Vector3 thing;
    private Vector3 moveDirection;
    private Vector3 characterRotation;

    //--DEFAULT VALUES--//
    public float acceleration = 200f;         
    public float airAcceleration = 200f;     
    public float maxSpeed = 6.4f;             
    public float maxAirSpeed = 0.6f;          
    public float friction = 8f;               // How fast the player decelerates on the ground

    [SerializeField]
    private GameObject camObj;

    [SerializeField]
    private LayerMask groundLayers;

    private float lastJumpPress = -1f;
    private float jumpPressDuration = 0.1f;

    private int comboCount;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        //Debug.Log(scoreManager.transform.GetChild(0));
        //scoreManager.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        Debug.Log(scoreManager);
        abilityUI = GameObject.FindGameObjectWithTag("UIManager");
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody>();

        //characterRotation = new Vector3(this.transform.rotation.x, Input.GetAxis("Horizontal") * rotationForce, this.transform.rotation.z);
    }


    void Update()
    {
        //Time survived.
        scoreManager.GetComponent<ScoreManager>().timeSurvived += Time.deltaTime;
        abilityUI.GetComponent<AbilityUI>().TimeTextUI.text = "Time: " + Time.time.ToString("F2");

        dashTimer -= Time.deltaTime;
        thing = Rb.velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastJumpPress = Time.time;

            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isGrounded && canDash)
            {
                isDashing = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canDash = true;
            dashTimer = 0.25f;
        }

        DoMovement();
        if (dashTimer > 0)
        {
            Dash();
        }
        else if (dashTimer <= 0)
        {
            isDashing = false;
        }   
        GetComponent<TrailRenderer>().enabled = isDashing;

        //StopMovement();
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Get player velocity
        Vector3 playerVelocity = GetComponent<Rigidbody>().velocity;
        // Slow down if on ground
        playerVelocity = Friction(playerVelocity);
        // Air/ground movement
        playerVelocity += Movement(input, playerVelocity);

        // New velocity
        GetComponent<Rigidbody>().velocity = playerVelocity;




        //Rb.velocity = new Vector3(Rb.velocity.x, Mathf.Max(-5, Rb.velocity.y), Rb.velocity.z);
    }

    /// <summary>
    /// Moves the player according to the input. (THIS IS WHERE THE STRAFING MECHANIC HAPPENS)
    /// </summary>
	private Vector3 Movement(Vector2 input, Vector3 velocity)
    {

        //Different acceleration values for ground and air
        float curAccel = acceleration;
        if (!isGrounded)
            curAccel = airAcceleration;

        //Ground speed
        float curMaxSpeed = maxSpeed;

        //Air speed
        if (!isGrounded)
            curMaxSpeed = maxAirSpeed;

        //Get rotation input + camera rotation and make them into vectors
        Vector3 cameraRotation = new Vector3(0f, camObj.transform.rotation.eulerAngles.y, 0f);
        Vector3 inputVelocity = Quaternion.Euler(cameraRotation) *
                                new Vector3(input.x * curAccel, 0f, input.y * curAccel);

        //Ignore vertical
        Vector3 alignedInputVelocity = new Vector3(inputVelocity.x, 0f, inputVelocity.z) * Time.deltaTime;

        //Get current velocity
        Vector3 currentVelocity = new Vector3(velocity.x, 0f, velocity.z);

        //How close the current speed to max velocity is (1 = not moving, 0 = at/over max speed)
        float max = Mathf.Max(0f, 1 - (currentVelocity.magnitude / curMaxSpeed));

        //How perpendicular the input to the current velocity is (0 = 90°)
        float velocityDot = Vector3.Dot(currentVelocity, alignedInputVelocity);

        //Scale the input to the max speed
        Vector3 modifiedVelocity = alignedInputVelocity * max;

        //The more perpendicular the input is, the more the input velocity will be applied
        Vector3 correctVelocity = Vector3.Lerp(alignedInputVelocity, modifiedVelocity, velocityDot);

        //Apply jump
        correctVelocity += GetJumpVelocity(velocity.y);

        //Return
        return correctVelocity;
    }

    /// <summary>
    /// Calculates the velocity with which the player is accelerated up when jumping
    /// </summary>
    /// <returns>Additional jump velocity for the player</returns>
	private Vector3 GetJumpVelocity(float yVelocity)
	{
		Vector3 jumpVelocity = Vector3.zero;

		if(Time.time < lastJumpPress + jumpPressDuration && yVelocity < jumpForce && CheckGround())
		{
			lastJumpPress = -1f;
			jumpVelocity = new Vector3(0f, jumpForce - yVelocity, 0f);
		}

		return jumpVelocity;
	}

    /// <summary>
    /// Slows down the player if on ground
    /// </summary>
    /// <param name="currentVelocity">Velocity of the player</param>
    /// <returns>Modified velocity of the player</returns>
	private Vector3 Friction(Vector3 currentVelocity)
    {
        isGrounded = CheckGround();
        float speed = currentVelocity.magnitude;

        if (!isGrounded || Input.GetButton("Jump") || speed == 0f)
            return currentVelocity;

        float drop = speed * friction * Time.deltaTime;
        return currentVelocity * (Mathf.Max(speed - drop, 0f) / speed);
    }


    void DoMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded)
            {
                moveDirection = new Vector3(0, 0, 1);
                moveDirection = transform.TransformDirection(moveDirection);
                Rb.AddForce(moveDirection * moveForce);

                if (Rb.velocity.z > (Vector3.forward.z * moveForce))
                {
                    Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, (Vector3.forward.z * moveForce));
                }
            }
            else if(!isGrounded)
            {

            }
            
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (isGrounded)
            {
                moveDirection = new Vector3(0, 0, -1);
                moveDirection = transform.TransformDirection(moveDirection);
                Rb.AddForce(moveDirection * moveForce);

                if (Rb.velocity.z < (Vector3.forward.z * moveForce))
                {
                    Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, (Vector3.back.z * moveForce));
                }
            }
            else if (!isGrounded)
            {
                
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") != 0)
        {

            /*if (isGrounded)
            {
                Vector3 moveDirection = new Vector3(1, 0, 0);
                moveDirection = transform.TransformDirection(moveDirection);
                Rb.AddForce(moveDirection * moveForce);

                if (Rb.velocity.x > (Vector3.right.x * moveForce))
                {
                    Rb.velocity = new Vector3((Vector3.right.x * moveForce), Rb.velocity.y, Rb.velocity.z);
                    // new Vector3(x, y, z);
                }
            }*/
            characterRotation = new Vector3(this.transform.rotation.x, (Input.GetAxis("Horizontal") / 4 * rotationForce), this.transform.rotation.z);
            Rb.transform.Rotate(characterRotation);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") != 0)
        {
            /*if (isGrounded)
            {
                Vector3 moveDirection = new Vector3(-1, 0, 0);
                moveDirection = transform.TransformDirection(moveDirection);
                Rb.AddForce(moveDirection * moveForce);


                if (Rb.velocity.x < (Vector3.left.x * moveForce))
                {
                    Rb.velocity = new Vector3((Vector3.left.x * moveForce), Rb.velocity.y, Rb.velocity.z);
                }
            }*/
            characterRotation = new Vector3(this.transform.rotation.x, (Input.GetAxis("Horizontal") / 4 * rotationForce), this.transform.rotation.z);
            Rb.transform.Rotate(characterRotation);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            Rb.AddForce(Vector3.up * jumpForce);
            anim_player.SetTrigger("JUMP");
        }
        else if (!isGrounded)
        {
            abilityUI.GetComponent<AbilityUI>().SetAbilitiesInactive();
            comboCount = 0;
            Rb.AddForce((Vector3.up * jumpForce * 4) + Rb.velocity);
            anim_player.SetTrigger("JUMP");
        }
    }
    
    void BounceOffBalloon()
    {
        // Add to balloon count
        int count = scoreManager.GetComponent<ScoreManager>().balloonCount++;
        comboCount++;
/*        int i = scoreManager.GetComponent<ScoreManager>().balloonCount++;
*/        abilityUI.GetComponent<AbilityUI>().BalloonTextUI.text = "Balloons Hit: " + count.ToString();

        audio.PlayOneShot(audioClip);
        abilityUI.GetComponent<AbilityUI>().SetAbilitiesActive();
        anim_player.SetTrigger("JUMP");

        Rb.velocity = new Vector3 (Rb.velocity.x, 0, Rb.velocity.z);
        Rb.AddForce(Vector3.up * 500);
    }

    void Dash()
    {
        //Rb.velocity = new Vector3(0, 0, 0);

        //dashTimer = 0.25f;
        if (isDashing)
        {
            comboCount = 0;

            abilityUI.GetComponent<AbilityUI>().SetAbilitiesInactive();

            moveDirection = new Vector3(0, 0, 1); // 1,1,1 breaks this. Record it.
            moveDirection = transform.TransformDirection(moveDirection);
            Rb.AddForce(moveDirection * dashForce);
            //isDashing = false;
        }
    }


    void StopMovement()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, 0.0f);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            Rb.velocity = new Vector3(0.0f, Rb.velocity.y, Rb.velocity.z);
        }


    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isDashing = false;
            canDash = false;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SpikeFloor" || collision.gameObject.tag == "KillZone")
        {
            scoreManager.GetComponent<ScoreManager>().comboCount = comboCount;
            gameManager.GetComponent<GameManager>().PlayerDeath();
            anim_player.SetTrigger("DEATH");

            // Destroy(this);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            // canDash = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Balloon")
        {
            Debug.Log("HIT");
            BounceOffBalloon();
            Destroy(other.gameObject);
        }
    }

    private bool CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool output = Physics.Raycast(ray, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayers);
        return output;
    }
}
