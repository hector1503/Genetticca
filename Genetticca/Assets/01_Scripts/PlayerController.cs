using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//// Tweakable fields
	[Header("Configuración salto")]	
	public float GRAVITY_CONSTANT = 20F;
	public float multiplierGravityOnJump = 0.5F;
	public double timeJumpUp = 0.4D;
	public float jumpInitialHeight = 3.5F; 


	[Header("Configuración movimiento")]	
	public float speed = 3.5F;

	[Header("Configuración dash")]	
	public float dashAccelerateMultiplier=2F;
	public double timeDashAccelerate = 0.3D;
	public double timeBetweenDash = 1D;
//	public float dashDecelerateMultiplayer = 0.2F;
//	public double timeDashDecelerate = 0.06D;

	double timeDiferencia=0D;


	CharacterController m_myCharacterC;
	Vector3 m_inputMove;
	float m_input3rdAxis;

	Vector3 m_move;
	float m_direction = 1;

	float m_currentGravity;
	double m_timeStartJump;

	double m_timeStartDashAccelerate;
	bool m_canDash;



	bool m_is3rdAxisInUseR;


	void Start ()
	{
		m_myCharacterC = GetComponentInChildren<CharacterController> ();
		m_currentGravity = GRAVITY_CONSTANT;
		m_inputMove = Vector3.zero;
		m_input3rdAxis = 0F;
		m_move = Vector3.zero;
		m_timeStartJump = 0D;
		m_is3rdAxisInUseR = false;

		m_timeStartDashAccelerate = -1;
		m_canDash = true;
	}
	
	void FixedUpdate ()
	{
		getInputControls ();

		//SALTO
		if (m_myCharacterC.isGrounded && Input.GetButton("Jump")) {
			m_move.y = jumpInitialHeight * Time.deltaTime;
			m_currentGravity = GRAVITY_CONSTANT;
			m_timeStartJump = Time.time;
			Debug.Log("JUMP");
		
		} else {
			
			if (m_myCharacterC.velocity.y>0 && m_inputMove.y > 0.1 && (Time.time-m_timeStartJump < timeJumpUp)) {
				m_currentGravity = GRAVITY_CONSTANT * multiplierGravityOnJump;
			} else {
				m_currentGravity = GRAVITY_CONSTANT;

			}
		}


		if (Mathf.Abs (m_inputMove.x) > 0.1F) {
			m_direction = m_inputMove.x /Mathf.Abs (m_inputMove.x);
			m_move.x = speed * m_direction * Time.deltaTime ;
		} else {
			if (m_myCharacterC.isGrounded)
				m_move.x = 0;
		}

		dash ();

		m_myCharacterC.Move (m_move);

	}


	void getInputControls()
	{
		m_inputMove = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Jump"), 0);

		if (m_canDash) {
			getThirdAxis ();
		}
	}

	void getThirdAxis()
	{
		m_input3rdAxis = Input.GetAxis ("Fire2");

		if (m_input3rdAxis > 0) {
			if (m_is3rdAxisInUseR == false) {
				m_is3rdAxisInUseR = true;
			}
		}
		if (m_input3rdAxis == 0) {
			m_is3rdAxisInUseR = false;
		}
	  
	}

	void dash()
	{
		
		//inicio Dash
		if (m_is3rdAxisInUseR) {
			if (m_timeStartDashAccelerate == -1) {
				m_timeStartDashAccelerate = Time.time;
				m_canDash = false;
			} 
			//en medio del dash
			timeDiferencia = Time.time - m_timeStartDashAccelerate;

			if (m_timeStartDashAccelerate != -1) {
				if (Time.time - m_timeStartDashAccelerate < timeDashAccelerate) {
					m_move.x = m_direction * speed * dashAccelerateMultiplier * Time.deltaTime;
					m_move.y = 0;
				} else {
					//fin del dash
					if (Time.time - m_timeStartDashAccelerate >= timeDashAccelerate)
						m_currentGravity = GRAVITY_CONSTANT;
					m_move.x = m_direction * speed * Time.deltaTime;
					m_move.y -= m_currentGravity / 2 * Time.deltaTime;
					if (Time.time - m_timeStartDashAccelerate > timeBetweenDash) {
						m_is3rdAxisInUseR = false;
						m_timeStartDashAccelerate = -1;
						m_canDash = true;
					}
				}
			}
		} else {
			m_move.y -= m_currentGravity / 2 * Time.deltaTime;
		}
	}

}
//TODO MV MEJORAS:
//- Idea arq. pasar todo a maquina de estados (ej. https://www.youtube.com/watch?v=I0sbUsQruIs&list=PLLH3mUGkfFCV_qhwvkiUSJXpX2xDcEkWd)

