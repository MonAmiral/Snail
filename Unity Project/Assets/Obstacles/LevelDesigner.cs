using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDesigner : MonoBehaviour 
{
	public static LevelDesigner Instance;

	public Obstacle[] RoomPrefabs;
	public Transform[] EntityPrefabs;

	private Vector3 NextRoomLocation;
	private Obstacle LatestRoomPrefab;

	private List<Obstacle> spawnedRooms = new List<Obstacle>();
	private int roomIndex;

	public void OnRoomEntered(Obstacle roomJustEntered)
	{
		bool found = false;
		bool skipped = false;
		for (int i = this.spawnedRooms.Count - 1; i >= 0; i--)
		{
			if (skipped)
			{
				GameObject.Destroy(this.spawnedRooms[i].gameObject);
				this.spawnedRooms.RemoveAt(i);

				this.SpawnARoom();
			}

			if (found)
			{
				skipped = true;
			}

			if (this.spawnedRooms[i] == roomJustEntered)
			{
				found = true;
			}
		}
	}

	private void Awake()
	{
		LevelDesigner.Instance = this;
	}

	private void Start()
	{
		this.SpawnARoom();
		this.SpawnARoom();
		this.SpawnARoom();
		this.SpawnARoom();
	}

	private void SpawnARoom()
	{
		Obstacle newRoomPrefab = this.LatestRoomPrefab;
		while (newRoomPrefab == this.LatestRoomPrefab)
		{
			int index = Random.Range(0, this.RoomPrefabs.Length);
			newRoomPrefab = this.RoomPrefabs[index];
		}

		this.LatestRoomPrefab = newRoomPrefab;

		Obstacle newRoom = GameObject.Instantiate(newRoomPrefab, this.NextRoomLocation, Quaternion.identity) as Obstacle;
		newRoom.transform.parent = this.transform;

		newRoom.transform.name = newRoomPrefab.transform.name + "#" + this.roomIndex;
		roomIndex++;

		this.NextRoomLocation = newRoom.Exit.position;

		this.spawnedRooms.Add(newRoom);

		int numberOfEntities = Random.Range(2, 5);
		for (int i = 1; i <= numberOfEntities; i++)
		{
			Vector3 position = Vector3.Lerp(newRoom.transform.position, this.NextRoomLocation, (float)i / (float)numberOfEntities);
			Transform entityTransform = Instantiate(this.EntityPrefabs[Random.Range(0, this.EntityPrefabs.Length)], position, Quaternion.identity) as Transform;
			entityTransform.parent = this.transform;
			entityTransform.eulerAngles += Vector3.up * Random.Range(0, 360);
		}
	}
}
