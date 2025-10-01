using UnityEngine;

public class LavaArea : MonoBehaviour
{
    private float speed = 0.5f;

    private void Update()
    {
        //Vector3 direction = isFacingRight ? Vector3.right : Vector3.left;
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}
