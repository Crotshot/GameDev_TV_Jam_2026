using UnityEngine;

public class PacketSpawner : MonoBehaviour {

#if UNITY_EDITOR
	public bool testSpawnPacket;
#endif

	private void Awake() {
		
	}

	void Start() {
		
	}

	void Update() {
#if UNITY_EDITOR
		if (testSpawnPacket) {
			GameObject packet = ObjectPool.FetchObject(ObjectPool.ObjectType.Packet, transform.position, Quaternion.identity);
			packet.GetComponent<Packet>().Launch(transform.position + transform.forward * 5f);
		}

#endif
	}
}
