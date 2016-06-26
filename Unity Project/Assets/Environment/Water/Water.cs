using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour 
{
	[SerializeField]
	private Vector3 relativeDirection;
	public AnimationCurve Speed;

	public Vector3 GetDirection()
	{
		return this.transform.TransformDirection(this.relativeDirection) * this.Speed.Evaluate(Time.time);
	}

	public void OnDrawGizmosSelected()
	{
		Debug.DrawRay(this.transform.position, this.GetDirection());
	}
}
