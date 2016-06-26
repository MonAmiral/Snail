using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public LayerMask WaterMask;
	public Rigidbody Rigidbody;

	protected Water WaterImIn;
	private Vector3 direction;

	protected virtual void Update()
	{
		if (this.WaterImIn == null)
		{
			this.direction *= 0.99f;
		}
		
		this.transform.position += this.direction * Time.deltaTime;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Water")
		{
			this.WaterImIn = other.GetComponentInParent<Water>();
			this.direction = this.WaterImIn.GetDirection();
		}
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		if (other.tag == "Water")
		{
			if (this.WaterImIn == other.GetComponentInParent<Water>())
			{
				this.WaterImIn = null;
			}
		}
	}

	protected virtual void LateUpdate()
	{
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position + Vector3.up * 5, Vector3.down, out hit, 20f, this.WaterMask))
		{
			Vector3 newUp = Vector3.Slerp(this.transform.up, hit.normal, Time.deltaTime * 5);
			Vector3 newForward = Quaternion.AngleAxis(90, this.transform.right) * newUp;
			this.transform.forward = newForward;
			this.transform.position = hit.point;
		}
	}

	protected virtual void FixedUpdate()
	{
		this.Rigidbody.velocity *= 0.99f;
		this.Rigidbody.angularVelocity *= 0.995f;
	}
}
