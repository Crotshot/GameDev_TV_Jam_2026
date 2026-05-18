using UnityEngine;

public class PacketPadTrigger : MonoBehaviour {


	private PacketPad parentPad;

	private void Awake() {
		parentPad = transform.parent.parent.GetComponent<PacketPad>();
		parentPad.AssignSwivel(transform);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Packet packet) != null) { 
			parentPad.TriggerEntered(packet);
		}
	}

}