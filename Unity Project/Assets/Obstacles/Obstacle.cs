using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour 
{
	public Transform Exit;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			LevelDesigner.Instance.OnRoomEntered(this);
		}
	}
}
