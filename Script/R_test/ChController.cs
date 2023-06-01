using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChController : MonoBehaviourPun
{
	public static GameObject LocalPlayerInstance = null;
	public static Action OnPlayerEnterPortal;
	
	public float speed = 6f;
	public float mouseSensitivity =5f;
	public float jumpSpeed = 5f;

	private Animator anim;

	private float rotationLeftRight;
	private float verticalRotation;
	private float forwardspeed;
	private float sideSpeed;
	private float verticalVelocity; 
	private Vector3 speedCombined;
	private CharacterController cc;

	private Camera cam;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		cam = GetComponentInChildren<Camera> ();
		cc = GetComponent<CharacterController> ();
		Cursor.visible = false;

		if (!photonView.IsMine)
		{
			cam.gameObject.SetActive(false);
		}
		anim.SetInteger("State", 0);
	}

    private void Awake()
    {
		if (photonView.IsMine)
		{
			ChController.LocalPlayerInstance = this.gameObject;
		}
	}

    // Update is called once per frame
    void Update () {
		if (photonView.IsMine == false)
		{
			return;
		}

		rotationLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotationLeftRight,0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -60f, 60f);
		cam.transform.localRotation = Quaternion.Euler (verticalRotation, 0,0);
		
		forwardspeed = Input.GetAxis ("Vertical") * speed;
		sideSpeed = Input.GetAxis ("Horizontal") * speed;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			forwardspeed *= 2f;
			if(cc.isGrounded && anim.GetInteger("State") == 1)
				anim.SetInteger("State", 2);
			Debug.Log("State = " + anim.GetInteger("State"));
		}
		else if(cc.isGrounded && forwardspeed != 0 || sideSpeed != 0)
        {
			anim.SetInteger("State", 1);
			Debug.Log("State = " + anim.GetInteger("State"));
		}
		else if(forwardspeed == 0 && sideSpeed == 0 && cc.isGrounded)
        {
			anim.SetInteger("State", 0);
			Debug.Log("State = " + anim.GetInteger("State"));
		}

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
			anim.SetInteger("State", 3);
			Debug.Log("State = "+anim.GetInteger("State"));
		}

		speedCombined = new Vector3 (sideSpeed, verticalVelocity, forwardspeed);

		speedCombined = transform.rotation * speedCombined;

		cc.Move (speedCombined * Time.deltaTime);
	}
}
