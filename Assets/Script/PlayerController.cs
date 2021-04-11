using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	private float horizontalRotation;

	private float verticalRotation;

	[Header("Bounding")]
	[SerializeField] private float verticalRotationBound;


	[Header("Movement")]
	[SerializeField] private Vector2 walkingSpeed;

	[SerializeField] private Vector2 runningSpeed;

	private bool isRunning;

	[SerializeField] private float jumpStrength;

	private Vector3 playerMovement;

	private CharacterController cc;

	[Header("Weapon")]

	[SerializeField] private float weaponRange;
	[SerializeField] private float WeaponDamage;
	[SerializeField] private int maxAmmo;
	[SerializeField] private float reloadDelay;
	private int curAmmo;
	private bool isReloading;

	public GameObject bulletHole;


	void Start()
	{
		cc = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		curAmmo = maxAmmo;
	}

	// Update is called once per frame
	void Update()
	{
		GettingInputs();
		CamRotating();
		Movement();
	}

	private void FixedUpdate()
	{

	}


	void CamRotating()
	{
		verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationBound, verticalRotationBound);

		transform.Rotate(0, horizontalRotation, 0);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
	}

	void GettingInputs()
	{
		//Mouse
		horizontalRotation = Input.GetAxis("Mouse X");
		verticalRotation -= Input.GetAxis("Mouse Y");

		//Movement
		playerMovement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

		if (Input.GetKey(KeyCode.LeftShift) && cc.isGrounded)
		{
			isRunning = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			isRunning = false;
		}

		//Jump
		if (cc.isGrounded && Input.GetButton("Jump"))
		{

			playerMovement.y += jumpStrength;
		}

	}

	void Movement()
	{
		//moving around 
		if (!isRunning)
		{
			playerMovement.x = playerMovement.x * walkingSpeed.x;
			playerMovement.z = playerMovement.z * walkingSpeed.y;
		}

		else
		{
			playerMovement.x = playerMovement.x * runningSpeed.x;
			playerMovement.z = playerMovement.z * runningSpeed.y;
		}

		if (!cc.isGrounded)
		{
			playerMovement.y += Physics.gravity.y;
		}


		//resolving everything
		cc.Move(playerMovement * Time.deltaTime);
		//Gravity


	}


}