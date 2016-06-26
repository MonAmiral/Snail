using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour 
{
	public Transform Target;
	private Vector3 offset;

	private void LateUpdate()
	{
		this.transform.position = Vector3.Lerp(this.transform.position, this.Target.position + this.offset, 0.5f);
	}

	private void Awake()
	{
		this.offset = this.transform.position - this.Target.position;
	}
}
