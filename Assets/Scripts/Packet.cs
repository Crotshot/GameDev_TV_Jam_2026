using Unity.VisualScripting;
using UnityEngine;

public class Packet : MonoBehaviour {

	Vector3 startPoint, destination, nextPos, currentPos;
	float travelP, height = 5f, travelTime = 1.2f, currentTime = 0f;
	bool travelling;
	AnimationCurve curve;

	private void Start() {
		curve = _Globals._PacketCurve;
	}

	private void OnEnable() {
		currentPos = transform.position;
		currentTime = 0;
	}

	private void Update() {
		if (currentTime < travelTime && travelling) {
			Traverse();
			currentTime += Time.deltaTime;
			travelP += curve.Evaluate(currentTime/ travelTime); //For now just using time, should gradually slow down and speed relative to a curve perhaps
		}
	}

	private void Traverse() {
		nextPos = new Vector3(startPoint.x * (1 - travelP) + destination.x * travelP,  //x
									  4 * (1 - travelP) * travelP * height + (startPoint.y + destination.y) / 2f, //y
									  startPoint.z * (1 - travelP) + destination.z * travelP); //z}

		transform.position = currentPos;
		transform.LookAt(nextPos);
		currentPos = nextPos;
	}

	public void Launch(Vector3 destination) {
		destination = this.destination;
		travelP = 0f;
		travelling = true;
	}

	private void OnCollisionEnter(Collision collision) {
		//Any collision will destroy a packet
		//Make kaboom here
		travelling = false;
		ObjectPool.PoolObject(gameObject, ObjectPool.ObjectType.Packet);
	}

}
