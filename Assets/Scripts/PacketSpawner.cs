using UnityEngine;

public class PacketSpawner : MonoBehaviour {

#if UNITY_EDITOR
	public bool testSpawnPacket;
#endif
	Transform destination;

	private void Awake() {
		destination = transform.GetChild(0);
	}

	void Start() {
		
	}

	void Update() {
#if UNITY_EDITOR
		if (testSpawnPacket) {
			GameObject packet = ObjectPool.FetchObject(ObjectPool.ObjectType.Packet, transform.position, Quaternion.identity);
			//packet.GetComponent<Packet>().Launch(transform.position + transform.forward * 5f);

			packet.GetComponent<Packet>().Launch(transform.position, destination.position);
			testSpawnPacket = false;
		}
#endif
	}
}
