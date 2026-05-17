using UnityEngine;

public class DDOL : MonoBehaviour {

	//TODO: Note script order execution needs updating, this goes first before all else
	private static DDOL instance;

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}
}
