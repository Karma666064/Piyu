using UnityEngine;

public class OrientationEntity : MonoBehaviour
{
    public bool isFacingRight;

    private void Start()
    {
        SetIsFacingRight();
    }

    private void Update()
    {
        SetIsFacingRight();
    }

    void SetIsFacingRight()
    {
        if (transform.localScale.x > 0) isFacingRight = true;
        else if (transform.localScale.x < 0) isFacingRight = false;
    }
}
