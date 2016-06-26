using UnityEngine;
using System.Collections;

public class Limb : MonoBehaviour 
{
	public Rigidbody Rigidbody;
	private Vector3 lastPosition;

	private void Awake()
	{
		this.lastPosition = this.transform.position;
	}

	private void FixedUpdate()
	{
		Vector3 movement = this.lastPosition - this.transform.position;
		this.Rigidbody.AddForce(movement * 100);
		this.lastPosition = this.transform.position;
	}
}
