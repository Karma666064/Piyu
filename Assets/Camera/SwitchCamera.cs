using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private Animator anim;
    public enum Cam { Cam_1, Cam_2, Cam_3 };
    public Cam camType;

    public GameObject stateDrivenCamera;

    public bool activeCam1 = true;
    public bool activeCam2;
    public bool activeCam3;
    public bool camCanChange;

    private void Start()
    {
        anim = stateDrivenCamera.GetComponent<Animator>();
    }

    private void Update()
    {
        if (activeCam1 && camCanChange)
        {
            anim.Play("Cam First");
            camCanChange = false;
        }
        else if (activeCam2 && camCanChange)
        {
            anim.Play("Cam Second");
            camCanChange = false;
        }
        else if (activeCam3 && camCanChange)
        {
            anim.Play("Cam Third");
            camCanChange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (camType)
            {
                case Cam.Cam_1:
                    activeCam1 = true;
                    activeCam2 = false;
                    activeCam3 = false;
                    camCanChange = true;
                    break;
                case Cam.Cam_2:
                    activeCam1 = false;
                    activeCam2 = true;
                    activeCam3 = false;
                    camCanChange = true;
                    break;
                case Cam.Cam_3:
                    activeCam1 = false;
                    activeCam2 = false;
                    activeCam3 = true;
                    camCanChange = true;
                    break;
            }
        }
    }
}
