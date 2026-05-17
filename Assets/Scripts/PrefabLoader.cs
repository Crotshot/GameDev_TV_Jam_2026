using UnityEngine;

public class PrefabLoader : MonoBehaviour {

	//TODO: Note script order execution needs updating, this goes second after DDOL
	[SerializeField] private GameObject PacketPrefab;
	[SerializeField] private AnimationCurve PacketCurve;

	private void Awake() {
		ObjectPool.PacketPrefab = PacketPrefab;
		_Globals._PacketCurve = PacketCurve;
	}
}
