using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;
    void Update()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }
    
}
