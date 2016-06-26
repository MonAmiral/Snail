using UnityEngine;
using System.Collections;

public class Player : Entity 
{
	public float MovementSpeed;

	public float Acceleration;
	private Vector3 Movement;

	public Vector3 Forward
	{
		get
		{
			Vector3 forwardVector = this.transform.forward;
			forwardVector.y = 0f;
			return forwardVector.normalized;
		}
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void FixedUpdate()
	{
		float verticalInput = Input.GetAxis("Vertical");

		if (Mathf.Abs(verticalInput) < 0.1f)
		{
			this.Movement = Vector3.Slerp(this.Movement, Vector3.zero, Time.fixedDeltaTime * this.Acceleration * 0.2f);
		}
		else if (verticalInput > 0)
		{
			this.Movement = Vector3.Slerp(this.Movement, this.Forward * this.MovementSpeed * verticalInput, Time.fixedDeltaTime * this.Acceleration);
		}
		else
		{
			this.Movement = Vector3.Slerp(this.Movement, this.Forward * this.MovementSpeed * verticalInput * 0.3f, Time.fixedDeltaTime * this.Acceleration);
		}

		float horizontalInput = Input.GetAxis("Horizontal");
		this.transform.eulerAngles += Vector3.up * horizontalInput * Time.fixedDeltaTime * 180;

		this.Rigidbody.angularVelocity *= 0.8f;
		this.Rigidbody.velocity = this.Movement;
	}
}
