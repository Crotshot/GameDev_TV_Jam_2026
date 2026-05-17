using Unity.VisualScripting;
using UnityEngine;

public class Packet : MonoBehaviour {

	Vector3 startPoint, destination, nextPos, currentPos;
	float travelP, height = 5f, travelTime = 0.8f, currentTime = 0f;
	bool travelling;
	AnimationCurve curve;
	Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody>();	
	}

	private void Start() {
		curve = _Globals._PacketCurve;
		travelP = 0;
	}

	private void OnEnable() {
		currentPos = transform.position;
		currentTime = 0;
	}

	private void Update() {
		if (currentTime < travelTime && travelling) {
			Traverse();
			currentTime += Time.deltaTime;
			travelP = curve.Evaluate(currentTime / travelTime);
			//Debug.Log("CT: " + currentTime + " - TT: " + travelTime + " - TP: " + travelP);
		}
		//else if (currentTime > travelTime && travelling) {
		//	//Debug.Log("Desitination Reached");
		//	transform.position = destination;
		//	travelling = false;
		//}
	}

	private void Traverse() {
		nextPos = new Vector3(startPoint.x * (1 - travelP) + destination.x * travelP,  //x
									  4 * (1 - travelP) * travelP * height + (startPoint.y + destination.y) / 2f, //y //TODO Remake this to better support verticality
									  startPoint.z * (1 - travelP) + destination.z * travelP); //z}

		transform.position = currentPos;
		transform.LookAt(nextPos);
		currentPos = nextPos;
	}

	public void Launch(Vector3 startPoint, Vector3 destination) {
		travelP = 0;
		this.startPoint = startPoint;
		this.destination = destination;
		currentTime = 0;
		travelling = true;
		transform.position = startPoint;
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	private void OnCollisionEnter(Collision collision) {
		//Any collision will destroy a packet
		//Make kaboom here
		travelling = false;
		ObjectPool.PoolObject(gameObject, ObjectPool.ObjectType.Packet);
	}

}
