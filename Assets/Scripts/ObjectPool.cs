using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool {

	public static GameObject PacketPrefab, RatPrefab;
	static List<GameObject> activePackets = new List<GameObject>(), pooledPackets = new List<GameObject>();
	static List<GameObject> activeRats = new List<GameObject>(), pooledRats = new List<GameObject>();

	public enum ObjectType {
		Packet,
		Rat
	}

	public static GameObject FetchObject(ObjectType objType, Vector3 position, Quaternion rotation) {
		GameObject obj = null;

		switch (objType) {//Enable phsyics and script when grabbing pooled objects
			case ObjectType.Packet:
				if (pooledPackets.Count > 0) {
					//Debug.Log("Grabbing Packet!");
					obj = pooledPackets[0];
					activePackets.Add(obj);
					pooledPackets.RemoveAt(0);

					obj.transform.position = position;
					obj.transform.rotation = rotation;
					obj.SetActive(true);
					obj.GetComponent<Packet>().enabled = true;
					obj.GetComponent<Collider>().enabled = true;
					obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				}
				else {
					//Debug.Log("Creating Packet!");
					obj = Object.Instantiate(PacketPrefab, new Vector3(0, -10, 0), Quaternion.identity);
				}
				break;
			case ObjectType.Rat:
				if (pooledRats.Count > 0) {
					obj = pooledRats[0];
					activeRats.Add(obj);
					pooledRats.RemoveAt(0);

					obj.transform.position = position;
					obj.transform.rotation = rotation;
					obj.SetActive(true);
					obj.GetComponent<Enemy>().enabled = true;
					obj.GetComponent<Collider>().enabled = true;
					obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				}
				else {
					obj = Object.Instantiate(RatPrefab, new Vector3(0, -10, 0), Quaternion.identity);
				}
				break;
			default:
				Debug.Log("Fetch Object fell out of the pool!");
				break;
		}
		
		return obj;
	}

	public static void PoolObject(GameObject obj, ObjectType objType) {//Disable phsyics and script
		switch (objType) {//Enable phsyics and script when grabbing pooled objects
			case ObjectType.Packet:
				//Debug.Log("Pooling Packet!");
				obj.GetComponent<Packet>().enabled = false;
				pooledPackets.Add(obj);
				break;
			case ObjectType.Rat:
				Debug.Log("Pooling Rat!");
				obj.GetComponent<Enemy>().enabled = false;
				pooledRats.Add(obj);
				break;
			default:
				Debug.Log("Object swallowed by the abyss!");
				break;
		}
		obj.GetComponent<Collider>().enabled = false;
		Rigidbody rb = obj.GetComponent<Rigidbody>();
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		obj.transform.position = new Vector3(0, -10, 0);
		obj.SetActive(false);
	}
}