using UnityEngine;

public class PacketPad : MonoBehaviour {

	[SerializeField] float[] angles;
	[SerializeField] float launchDistance;
	int currentAngleIndex, targetAngleIndex;
	float rotationPercentage;

	Transform swivel;
	Animator padAnimator;
	bool active = false, rotating;

	private void Awake() {
		currentAngleIndex = 0;
		targetAngleIndex = 0;
		Transform arrowsParent = transform.GetChild(0);
		angles = new float[arrowsParent.childCount];

		for (int i = 0; i < angles.Length; i++) {
			angles[i] = arrowsParent.GetChild(i).gameObject.transform.localEulerAngles.y;
		}

		padAnimator = GetComponent<Animator>();
	}

	private void Update() {
		if (rotating) { 
			//Rotaion is from current to target index, calcualted via animation % completion from LaunchComplete to ResetPad.



		}
	}

	public void TriggerEntered(Packet packet) {
		if (active)
			return;

		packet.Launch(transform.position + Vector3.up, transform.position + swivel.forward * launchDistance);
		padAnimator.SetTrigger("Launch");
		active = true;
		padAnimator.speed = 3.0f;
	}

	public void ResetPad() {
		//Debug.Log("ResetPad!");
		active = false;
		rotating = false;
		currentAngleIndex = targetAngleIndex;

		swivel.rotation = Quaternion.Euler(0, angles[currentAngleIndex] + transform.eulerAngles.y, 0);
	}

	public void LaunchComplete() {
		//Debug.Log("LaunchComplete!");
		padAnimator.speed = 1.0f;
		targetAngleIndex = currentAngleIndex + 1 == angles.Length ? 0 : currentAngleIndex + 1;
		rotating = true;
	}

	public void AssignSwivel(Transform swivel) {
		this.swivel = swivel;
	}
}
