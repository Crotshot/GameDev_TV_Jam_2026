using UnityEngine;

public class PacketPad : MonoBehaviour {

	[SerializeField] float[] angles;
	[SerializeField] float launchDistance;
	[SerializeField] Material redLight, greenLight;
	int currentAngleIndex, targetAngleIndex;
	float rotationTimeTotal, rotationTimeCurrent;

	Transform swivel, arrowsParent;
	Animator padAnimator;
	bool active = false, rotating;
	Quaternion currentAngle, targetAngle;

	private void Awake() {
		currentAngleIndex = 0;
		targetAngleIndex = 0;
		
		arrowsParent = transform.GetChild(0);
		angles = new float[arrowsParent.childCount];

		for (int i = 0; i < angles.Length; i++) {
			angles[i] = arrowsParent.GetChild(i).gameObject.transform.localEulerAngles.y;

			Renderer renderer = arrowsParent.GetChild(i).GetComponent<Renderer>();
			Material[] mats = renderer.materials;
			mats[1] = i == 0 ? greenLight : redLight;
			renderer.materials = mats;
		}

		padAnimator = GetComponent<Animator>();
		currentAngle = Quaternion.Euler(0, angles[currentAngleIndex] + transform.eulerAngles.y, 0);
	}

	private void Update() {
		if (rotating) {
			//Rotaion is from current to target index, calcualted via animation % completion from LaunchComplete to ResetPad.
			rotationTimeCurrent += Time.deltaTime * padAnimator.speed;
			if (rotationTimeCurrent < rotationTimeTotal) {
				swivel.transform.rotation = Quaternion.Slerp(currentAngle, targetAngle, rotationTimeCurrent / rotationTimeTotal);
			}
			//else {
			//	swivel.transform.rotation = targetAngle;
			//	rotating = false;
			//}
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

	public void LaunchComplete() {
		//Debug.Log("LaunchComplete!");
		padAnimator.speed = 2.0f;
		targetAngleIndex = currentAngleIndex + 1 == angles.Length ? 0 : currentAngleIndex + 1;

		for (int i = 0; i < 2; i++) {
			Renderer renderer = arrowsParent.GetChild(i == 0 ? targetAngleIndex : currentAngleIndex).GetComponent<Renderer>();
			Material[] mats = renderer.materials;
			mats[1] = i == 0 ? greenLight : redLight;
			renderer.materials = mats;
		}

		targetAngle = Quaternion.Euler(0, angles[targetAngleIndex] + transform.eulerAngles.y, 0);
		rotating = true;

		AnimatorStateInfo state = padAnimator.GetCurrentAnimatorStateInfo(0);
		AnimationClip clip = padAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
		float normalizedTime = state.normalizedTime % 1f;
		float currentFrame = normalizedTime * clip.length * clip.frameRate;
		float totalFrames = clip.length * clip.frameRate;

		//Debug.Log(
		//	$"LaunchComplete Frame: {Mathf.FloorToInt(currentFrame)} / " +
		//	$"{Mathf.FloorToInt(totalFrames)}" + "\n" +
		//	$"Framerate: " + clip.frameRate + "\n" +
		//	$"Time remaining: " + ((totalFrames - currentFrame) / clip.frameRate)
		//);

		rotationTimeTotal = (totalFrames - currentFrame) / clip.frameRate;
		rotationTimeCurrent = 0;

		
	}

	public void ResetPad() {
		//Debug.Log("ResetPad!");
		active = false;
		rotating = false;
		swivel.transform.rotation = targetAngle;
		currentAngleIndex = targetAngleIndex;
		currentAngle = Quaternion.Euler(0, angles[currentAngleIndex] + transform.eulerAngles.y, 0);
	}

	public void AssignSwivel(Transform swivel) {
		this.swivel = swivel;
	}
}
