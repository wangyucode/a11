using System.Runtime.InteropServices;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    float rotationSpeed = Screen.width * 0.0125f;

    private int lastAction = 0;

    private Vector3 deltaVector = Vector3.zero;

    private Vector2 startPos;

    public Material material;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;

    public Transform DNATrans;
    public Transform text1;
    public Transform text2;
    public Transform text3;
    public Transform text4;
    public Transform text5;
    public Transform t1Top;
    public Transform t1DNA;
    public Transform t2Top;
    public Transform t2DNA;
    public Transform t3Top;
    public Transform t3DNA;
    public Transform t4Top;
    public Transform t4DNA;
    public Transform t5Top;
    public Transform t5DNA;

    public LineRenderer line1;
    public LineRenderer line2;
    public LineRenderer line3;
    public LineRenderer line4;
    public LineRenderer line5;

    [DllImport("__Internal")]
    private static extern void ShowLink(string link);

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(t1Top.position, t1DNA.position);
        Gizmos.DrawLine(t2Top.position, t2DNA.position);
        Gizmos.DrawLine(t3Top.position, t3DNA.position);
        Gizmos.DrawLine(t4Top.position, t4DNA.position);
        Gizmos.DrawLine(t5Top.position, t5DNA.position);
    }

    void Update()
    {
        material.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat1.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat2.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat3.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat4.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);

        if (lastAction == 0) transform.Rotate(Vector3.up * Time.deltaTime * 8f, Space.Self);

        line1.SetPositions(new Vector3[] { t1Top.position, t1DNA.position });
        line2.SetPositions(new Vector3[] { t2Top.position, t2DNA.position });
        line3.SetPositions(new Vector3[] { t3Top.position, t3DNA.position });
        line4.SetPositions(new Vector3[] { t4Top.position, t4DNA.position });
        line5.SetPositions(new Vector3[] { t5Top.position, t5DNA.position });

        if (Input.touches.Length == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    if (lastAction == 0 || lastAction == 1)
                    {
                        deltaVector = touch.deltaPosition;
                        Vector3 rotateVector = new Vector3(0, deltaVector.x, 0) * Time.deltaTime * rotationSpeed;
                        transform.Rotate(rotateVector, Space.Self);
                        lastAction = 1;
                    }
                    break;
                case TouchPhase.Ended:
                    lastAction = 0;
                    float distance = Vector2.Distance(touch.position, startPos);
                    if (distance < 10)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            ShowLink(hit.collider.gameObject.name);
                        }
                    }
                    break;
            }

        }

    }

}
