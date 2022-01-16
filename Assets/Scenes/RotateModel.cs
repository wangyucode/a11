using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateModel : MonoBehaviour
{
    float rotationSpeed = 8;
    float zoomSpeed = 40;

    private float oldDistance;

    private int lastAction = 0;

    private Vector3 deltaVector = Vector3.zero;
    private float passTime = 0;

    private float lerpTime = 1f;

    private Vector2 startPos;
    private string log = "";

    private float loadSceneTime = -1;
    private float loadSceneDelayTime = 1f;

    private string loadSceneName;


    private float smoth = 5;

    public Transform cubeCameraTrans;
    public Transform sphereCameraTrans;
    public Transform capsuleCameraTrans;
    public Transform returnCameraTrans;


    [DllImport("__Internal")]
    private static extern void ShowLink(string link);
    void Awake()
    {
        log = SceneManager.GetActiveScene().name;
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        if (loadSceneTime > 0)
        {
            MoveCamera();

            if (Time.time > loadSceneTime)
            {
                LoadScene();
            }
            return;
        }
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
                        Vector3 rotateVector = new Vector3(-deltaVector.y, deltaVector.x, 0) * Time.deltaTime * rotationSpeed;
                        transform.Rotate(rotateVector, Space.World);
                        lastAction = 1;
                        passTime = 0;
                    }
                    break;
                case TouchPhase.Ended:
                    float distance = Vector2.Distance(touch.position, startPos);
                    if (distance < 10)
                    {
                        log = "clicked";
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            log = hit.collider.gameObject.name;
                            OnClicked(hit.collider.gameObject.name);
                        }
                    }
                    else
                    {
                        log = "unclicked";
                    }
                    break;
            }

        }
        else if (Input.touches.Length == 2)
        {
            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[2].phase == TouchPhase.Moved)
            {
                float newDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                if (newDistance > oldDistance)
                {
                    Camera.main.fieldOfView -= zoomSpeed * Time.deltaTime;
                }
                else
                {
                    Camera.main.fieldOfView += zoomSpeed * Time.deltaTime;
                }
                oldDistance = newDistance;
                lastAction = 2;
                passTime = lerpTime;
            }
        }
        else if (Input.touches.Length == 0)
        {
            if (passTime < lerpTime)
            {
                deltaVector = Vector2.Lerp(deltaVector, Vector2.zero, Time.deltaTime * smoth);
                Vector3 rotateVector = new Vector3(-deltaVector.y, deltaVector.x, 0) * Time.deltaTime * rotationSpeed;
                transform.Rotate(rotateVector, Space.World);
                passTime += Time.deltaTime;
            }
            lastAction = 0;
        }

    }

    private void MoveCamera()
    {
        Transform targetTransform = Camera.main.gameObject.transform;
        switch (loadSceneName)
        {
            case "CubeScene":
                targetTransform = cubeCameraTrans;
                break;
            case "CapsuleScene":
                targetTransform = capsuleCameraTrans;
                break;
            case "SphereScene":
                targetTransform = sphereCameraTrans;
                break;
            case "MainScene":
                targetTransform = returnCameraTrans;
                break;

        }
        Vector3 position = Camera.main.gameObject.transform.position;
        Camera.main.gameObject.transform.position = Vector3.Lerp(position, targetTransform.position, Time.deltaTime * smoth);
        Quaternion rotation = Camera.main.gameObject.transform.rotation;
        Camera.main.gameObject.transform.rotation = Quaternion.Lerp(rotation, targetTransform.rotation, Time.deltaTime * smoth);
    }

    private void OnClicked(string name)
    {
        loadSceneTime = Time.time + loadSceneDelayTime;
        switch (name)
        {
            case "Cube":
                loadSceneName = "CubeScene";
                break;
            case "Capsule":

                loadSceneName = "CapsuleScene";
                break;
            case "Sphere":
                loadSceneName = "SphereScene";
                break;

            default:
                loadSceneTime = -1;
                ShowLink(name);
                break;
        }
    }

    void LoadScene()
    {
        log = "LoadCubeScene";
        SceneManager.LoadScene(loadSceneName);
    }

    public void back()
    {
        loadSceneTime = Time.time + loadSceneDelayTime;
        loadSceneName = "MainScene";
    }
}
