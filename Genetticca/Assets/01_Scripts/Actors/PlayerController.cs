using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Vars
    //// Tweakable fields
    [Header("Configuración salto")]
    public float GRAVITY_CONSTANT = 20F;
    public float multiplierGravityOnJump = 0.5F;
    public double timeJumpUp = 0.4D;
    public float jumpInitialHeight = 3.5F;
    public float climbForce = 4F;

    public double timeBetweenJumpWall = 0.75D;
    public float wallSlideMaxSpeed = -3F;
    [Header("Configuración movimiento")]
    public float speed = 3.5F;

    [Header("Configuración dash")]
    public float dashAccelerateMultiplier = 2F;
    public double timeDashAccelerate = 0.3D;
    public double timeBetweenDash = 1D;
    public int numDashInAir = 1;
    public float damageDist = -5.0f;
    [Header("Configuración espada")]
    public SwordAbility swordA;
    [Header("Testing")]
    public float m_directonProjectileMagnitude;

	public Animator myAnim;

    //	public float dashDecelerateMultiplayer = 0.2F;
    //	public double timeDashDecelerate = 0.06D;

    double timeDiferencia = 0D;


    CharacterController m_myCharacterC;
    Vector3 m_inputMove;
    float m_input3rdAxis;

    Vector3 m_move;
    public Vector3 m_directonProjectile;
    float m_directionMove = 1;
    float m_lastDirectionMove = 1;
    float m_currentGravity;
    double m_timeStartJump;
    double m_timeStartJumpWall;
    double m_timeStartDashAccelerate;
    bool m_isDashing;
    bool m_isSliding;
    float m_slideDirection;
    int m_currentNumDashes;
    bool m_isLastTimeGrounded;
    bool m_is3rdAxisInUseR;
	bool isWalking;
	bool isJumping;
	bool isDashing;
	bool JumpUp;
    //private ShooterController shooterC;
    private ShooterController shooterC;
    #endregion
    void Start()
    {
        shooterC = transform.root.GetComponentInChildren<ShooterController>();
        //shooterC = transform.root.GetComponentInChildren<ProjectileAbility>();
        m_myCharacterC = GetComponentInChildren<CharacterController>();
        GRAVITY_CONSTANT /= 200;
        m_currentGravity = GRAVITY_CONSTANT;
        m_inputMove = Vector3.zero;
        m_input3rdAxis = 0F;
        m_move = Vector3.zero;
        m_timeStartJump = 0D;
        m_timeStartJumpWall = 0D;
        m_is3rdAxisInUseR = false;
        m_slideDirection = 1F;
        m_timeStartDashAccelerate = -1D;
        m_isDashing = false;
        m_currentNumDashes = 0;
        m_isLastTimeGrounded = false;
        swordA.Initialize(transform.root.gameObject);
		isWalking = false;
		isJumping = false;
		isDashing = false;
		JumpUp = false;
		myAnim.SetBool("isWalking", isWalking);
		myAnim.SetBool("isJumping", isJumping);
		myAnim.SetBool("isDashing", isDashing);
		myAnim.SetBool("JumpUp", JumpUp);
	}

    void FixedUpdate()
    {
        m_isSliding = false;
        //TODO MVO: No me gusta esta comparacion pq puede que collisione con otras cosas que no sean suelo. 
        //Deberia controllarse desde OnControllerColliderHit pero no lo he conseguido.
        if ((m_myCharacterC.collisionFlags & CollisionFlags.Sides) != 0)
            m_isSliding = true;
        getInputControls();
        if (m_myCharacterC.isGrounded)
        {
            m_currentNumDashes = numDashInAir;
        }
        //SALTO
        if (m_myCharacterC.isGrounded && Input.GetButton("Jump"))
        {
            jump();

        }
        else
        {

            if (m_myCharacterC.velocity.y > 0 && m_inputMove.y > 0.1 && (Time.time - m_timeStartJump < timeJumpUp))
            {
                m_currentGravity = GRAVITY_CONSTANT * multiplierGravityOnJump;
            }
            else
            {
                m_currentGravity = GRAVITY_CONSTANT;

            }
        }

        //MOVE
        if (Mathf.Abs(m_inputMove.x) > 0.1F)
        {
            m_directionMove = Mathf.Sign(m_inputMove.x);
            if(m_directionMove!=m_lastDirectionMove)
                transform.Rotate(new Vector3 (0,180,0));
            m_lastDirectionMove = m_directionMove;
			isWalking = true;
            if (!m_isSliding || (m_isSliding && m_directionMove != m_slideDirection))
                m_move.x = speed * m_directionMove;

        }
        else
        {
            if (m_myCharacterC.isGrounded)
                m_move.x = 0;
				isWalking = false;
        }
        if (!m_myCharacterC.isGrounded)
            m_move.y -= m_currentGravity;

        dash();
        m_myCharacterC.Move(m_move * Time.deltaTime);

        //DISPARAR
        m_directonProjectileMagnitude = m_directonProjectile.magnitude;//TODO BORRAR
        if (m_directonProjectile != Vector3.zero && m_directonProjectile.magnitude > 0.3f)
        {
            shooterC.ShootWithDirection(m_directonProjectile);
            if (shooterC.getIsBursting())
                shooterC.setDriection(m_directonProjectile);
            // Debug.Log("disparar direccion= "+m_directonProjectile.x.ToString()+", "+ m_directonProjectile.y.ToString());
        }
        //ATAQUE ESPADA
        if (Input.GetButton("SwordAttack"))
        {
            
            swordA.TriggerAbility();

        }

		//ANIMACION SALTO
		if (!m_myCharacterC.isGrounded) {
			isJumping = true;
			//Debug.Log("JUMPANIM");
			if(m_move.y > 0) {
				JumpUp = true;
				//Debug.Log("JUMPANIMUP");
			} else if(m_move.y < 0){
				JumpUp = false;
				//Debug.Log("JUMPANIMDOWN");
			}
		} else {
			isJumping = false;
		}

		Debug.Log("isWalking" + isWalking);
		Debug.Log("isJumping" + isJumping);
		Debug.Log("isDashing" + isDashing);
		Debug.Log("JumpUp" + JumpUp);

		myAnim.SetBool("isWalking", isWalking);
		myAnim.SetBool("isJumping", isJumping);
		myAnim.SetBool("isDashing", isDashing);
		myAnim.SetBool("JumpUp", JumpUp);

    }
    //CONTROLES
    #region InputControls
    void getInputControls()
    {
        m_inputMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), 0);
        if (Mathf.Abs(Input.GetAxis("FireX")) >= 0.3F || Mathf.Abs(Input.GetAxis("FireY")) >= 0.3F)
        {
            m_directonProjectile = new Vector3(Input.GetAxis("FireX"), Input.GetAxis("FireY"), 0);
        }
        else
        {
            m_directonProjectile = Vector3.zero;
        }
        if (!m_isDashing && m_currentNumDashes > 0)
        {
            getThirdAxis();
        }
    }


    void getThirdAxis()
    {
        m_input3rdAxis = Input.GetAxis("Dash");

        if (m_input3rdAxis == 1)
        {
            if (m_is3rdAxisInUseR == false)
            {
                m_is3rdAxisInUseR = true;
            }
        }
        if (m_input3rdAxis == 0)
        {
            m_is3rdAxisInUseR = false;
        }

    }
    #endregion
    //MECANICAS
    #region MECANICAS
    void dash()
    {
		
        //inicio Dash
        if (m_is3rdAxisInUseR)
        {
            if (m_timeStartDashAccelerate == -1D)
            {
                m_timeStartDashAccelerate = Time.time;
                m_isDashing = true;
				isDashing = true;
                m_currentNumDashes--;

                if (m_isSliding)
                    m_directionMove = m_slideDirection;
            }
            //en medio del dash
            timeDiferencia = Time.time - m_timeStartDashAccelerate;

            if (m_timeStartDashAccelerate != -1D)
            {
                if (Time.time - m_timeStartDashAccelerate < timeDashAccelerate)
                {
                    m_move.x = m_directionMove * speed * dashAccelerateMultiplier;
                    m_move.y = 0;
                    m_currentGravity = GRAVITY_CONSTANT;
                }
                else
                {
                    //fin del dash
                    if (!m_myCharacterC.isGrounded)
                        m_move.x = m_directionMove * speed;
                    if (Time.time - m_timeStartDashAccelerate > timeBetweenDash)
                    {
                        m_is3rdAxisInUseR = false;
                        m_timeStartDashAccelerate = -1D;
                        m_isDashing = false;
						isDashing = false;
                    }
                }
            }
        }
    }

    void jump()
    {
        m_move.y = jumpInitialHeight;
        if (m_isSliding && Mathf.Sign(m_inputMove.x) != m_slideDirection)
            m_move.y = climbForce;

        m_is3rdAxisInUseR = false;
        m_timeStartDashAccelerate = -1D;
        m_isDashing = false; m_currentGravity = GRAVITY_CONSTANT;
        m_timeStartJump = Time.time;
        m_currentNumDashes = numDashInAir;
        m_isLastTimeGrounded = false;

        Debug.Log("JUMP");
    }



    public void getDamage()
    {
        //Destroy (gameObject);
        m_move.x = damageDist;
        m_move.y = 0;

        m_myCharacterC.Move(m_move * Time.deltaTime);
    }
    #endregion

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!m_myCharacterC.isGrounded && hit.normal.y < 0.1F && m_myCharacterC.velocity.y < 0)
        {
            m_isSliding = true;
            m_slideDirection = hit.normal.x;
            if (m_move.y < wallSlideMaxSpeed)
                m_move.y = wallSlideMaxSpeed;
            if (Input.GetButton("Jump") && Time.time - m_timeStartJumpWall > timeBetweenJumpWall)
            {
                m_directionMove = m_slideDirection;
                if (Mathf.Sign(m_inputMove.x) != m_slideDirection)
                    m_move.x = 0;
                else
                    m_move.x = m_directionMove * speed;
                m_timeStartJumpWall = Time.time;
                jump();
            }
            if (m_is3rdAxisInUseR && !m_isDashing)
            {
                m_directionMove = m_slideDirection;
                dash();
            }


        }
        if (m_myCharacterC.isGrounded && !m_isLastTimeGrounded)
        {
            m_isLastTimeGrounded = true;
            m_timeStartJump = 0D;
            m_is3rdAxisInUseR = false;
            m_timeStartDashAccelerate = -1D;
            m_isDashing = false; m_timeStartJumpWall = 0D;
            m_currentGravity = GRAVITY_CONSTANT;
            m_currentNumDashes = numDashInAir;
        }
    }
}
//TODO MV MEJORAS:
//- Idea arq. pasar todo a maquina de estados (ej. https://www.youtube.com/watch?v=I0sbUsQruIs&list=PLLH3mUGkfFCV_qhwvkiUSJXpX2xDcEkWd)
// Diferenciar entre salto pared i climb (con tiempos diferentes y valores diferentes)
