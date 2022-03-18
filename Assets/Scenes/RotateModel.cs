using System.Runtime.InteropServices;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    float rotationSpeed = Screen.width * 0.0125f;

    public float xMat = 0.03f;
    public float yMat = 0.02f;

    private int lastAction = 0;

    private Vector3 deltaVector = Vector3.zero;

    private Vector2 startPos;

    public Material material;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Transform DNATrans;

    [DllImport("__Internal")]
    private static extern void ShowLink(string link);

    void Update()
    {
        material.mainTextureOffset = new Vector2(Time.time * xMat, Time.time * yMat);
        mat1.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat2.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat3.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);
        mat4.mainTextureOffset = new Vector2(Time.time * 0.005f, Time.time * 0.009f);

        if (lastAction == 0) DNATrans.Rotate(Vector3.up * Time.deltaTime * 8f, Space.Self);

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
                        DNATrans.Rotate(Vector3.up * deltaVector.x * Time.deltaTime * rotationSpeed, Space.Self);
                        lastAction = 1;
                    }
                    break;
                case TouchPhase.Ended:
                    lastAction = 0;
                    break;
            }

        }

    }

    public void Clicked(string name){
        ShowLink(name);
    }

}
