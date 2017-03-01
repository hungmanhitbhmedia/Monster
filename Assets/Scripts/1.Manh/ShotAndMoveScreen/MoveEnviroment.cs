using UnityEngine;
using System.Collections;

public class MoveEnviroment : MonoBehaviour
{
    float perspectiveZoomSpeed = 0.35f;        // The rate of change of the field of view in perspective mode.
    float orthoZoomSpeed = 0.005f;
    float x;
    float y;
    public bool isMove;
    float dragSpeed = 1;
    private Vector3 dragOrigin;
    bool cameraDragging;
    Vector3 screenSpace;
    Vector3 offset;

    Vector3 curScreenSpace;
    Vector3 curPosition;

    public GameObject cameraB;
    Vector3 click1;
    Vector3 click2;
    Vector3 move;
    Vector3 positionB;
    Vector3 tg;


    void Start()
    {

    }
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    click1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    positionB = cameraB.transform.position;

                    ConfirmMove();
                    break;
                case TouchPhase.Moved:
                    click2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    move = click2 - click1;
                    tg = positionB - new Vector3(move.x * 3.2f * cameraB.GetComponent<Camera>().orthographicSize, move.y * 2 * cameraB.GetComponent<Camera>().orthographicSize, move.z);
                    if (tg.x > x && tg.x < -x && tg.y > y && tg.y < -y)
                    {
                        cameraB.transform.position = tg;
                    }
                    break;
                case TouchPhase.Ended:
                    break;
            }
            if (Input.touchCount == 2)
            {

                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (cameraB.GetComponent<Camera>().orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    cameraB.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                    // Make sure the orthographic size never drops below zero.
                    cameraB.GetComponent<Camera>().orthographicSize = Mathf.Clamp(cameraB.GetComponent<Camera>().orthographicSize, 1f, 3.5f);
                    MoveWhenZoom();

                }
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    cameraB.GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    cameraB.GetComponent<Camera>().fieldOfView = Mathf.Clamp(cameraB.GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
                }
                if (touchZero.phase == TouchPhase.Ended)
                {
                    // screenSpace = Camera.main.WorldToScreenPoint(touchOne.position);
                    //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, screenSpace.z));
                    click1 = Camera.main.ScreenToViewportPoint(touchOne.position);
                    positionB = cameraB.transform.position;
                    ConfirmMove();
                }
                if (touchOne.phase == TouchPhase.Ended)
                {
                    //screenSpace = Camera.main.WorldToScreenPoint(touchZero.position);
                    //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touchZero.position.x, touchZero.position.y, screenSpace.z));
                    click1 = Camera.main.ScreenToViewportPoint(touchZero.position);
                    positionB = cameraB.transform.position;
                    ConfirmMove();
                }
            }
        }
    }
    //tính toán khoảng di chuyển trên màn hình để không bị lệch khi zoom.
    void ConfirmMove()
    {
        x = -18.8f - 1.57f * (3.5f - cameraB.GetComponent<Camera>().orthographicSize);
        y = -11.3f - (3.5f - cameraB.GetComponent<Camera>().orthographicSize);
    }
    void MoveWhenZoom()
    {
        float posX = cameraB.transform.position.x;
        float posY = cameraB.transform.position.y;

        float posMoveX = -18.8f - 1.57f * (3.5f - cameraB.GetComponent<Camera>().orthographicSize);
        float posMoveY = -11.3f - (3.5f - cameraB.GetComponent<Camera>().orthographicSize);

        if (Mathf.Abs(posX) > Mathf.Abs(posMoveX))
        {
            if (posX > 0)
            {
                cameraB.transform.position = new Vector3(-posMoveX, cameraB.transform.position.y, cameraB.transform.position.z);
            }
            else
            {
                cameraB.transform.position = new Vector3(posMoveX, cameraB.transform.position.y, cameraB.transform.position.z);
            }
        }
        if (Mathf.Abs(posY) > Mathf.Abs(posMoveY))
        {
            if (posY > 0)
            {
                cameraB.transform.position = new Vector3(cameraB.transform.position.x, -posMoveY, cameraB.transform.position.z);
            }
            else
            {
                cameraB.transform.position = new Vector3(cameraB.transform.position.x, posMoveY, cameraB.transform.position.z);
            }
        }
    }
}
