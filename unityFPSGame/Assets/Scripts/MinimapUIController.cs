using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUIController : MonoBehaviour
{
    public Terrain terrainObject;
    public GameObject viewPortObject;
    public GameObject minimapObject;
    public GameObject playerObject;

    private RectTransform viewPortObjectRectTransform;
    private RectTransform minimapObjectRectTransform;
    public RectTransform playerPosObjectRectTransform;

    private Vector2 playerMinimapPosition;
    private float minimapZoomRatioX;
    private float minimapZoomRatioY;

    private void Awake()
    {
        viewPortObjectRectTransform = viewPortObject.GetComponent<RectTransform>();
        minimapObjectRectTransform = minimapObject.GetComponent<RectTransform>();

        minimapZoomRatioX = minimapObjectRectTransform.sizeDelta.x / terrainObject.terrainData.size.x;
        minimapZoomRatioY = minimapObjectRectTransform.sizeDelta.y / terrainObject.terrainData.size.z;
    }

    private void Update()
    {
        SetPlayerMinimapPosition();
        SetZeroPositionUI();
    }

    private void SetPlayerMinimapPosition()
    {
        playerMinimapPosition.x = playerObject.transform.position.x * minimapZoomRatioX;
        playerMinimapPosition.y = playerObject.transform.position.z * minimapZoomRatioY;

        playerPosObjectRectTransform.anchoredPosition = playerMinimapPosition;
    }

    private void SetZeroPositionUI()
    {
        // playerPosObject가 이동한 만큼만 이동해주면 되므로
        // 중앙을 예시로 잡고 playerPosObject가 400,400 이라면 맵의 위치는 0,0 / 440, 440 이라면 맵의 위치는 -40, -40이 되어야 하므로
        // 두 예시를 기준으로 식을 세우면 playerPosObject의 위치 - (미니맵 이미지의 사이즈의 절반(좌우로 나뉘므로)) 의 반대 값이 미니맵의 위치가 됨
        minimapObjectRectTransform.anchoredPosition = -(playerPosObjectRectTransform.anchoredPosition - (minimapObjectRectTransform.sizeDelta / 2));

        if (Mathf.Abs(minimapObjectRectTransform.anchoredPosition.x) >= 200.0f)
        {
                                                                      // 해당 미니맵이 최대한 움직일 수 있는 x 좌표의 범위 * 해당 나의 x 좌표 부호를 곱해주면.
            minimapObjectRectTransform.anchoredPosition = new Vector2((minimapObjectRectTransform.sizeDelta.x - viewPortObjectRectTransform.sizeDelta.x) / 2 * Mathf.Sign(minimapObjectRectTransform.anchoredPosition.x),
                                                                       minimapObjectRectTransform.anchoredPosition.y);
        }

        if(Mathf.Abs(minimapObjectRectTransform.anchoredPosition.y) >= 200.0f)
        {
            // 해당 미니맵이 최대한 움직일 수 있는 x 좌표의 범위 * 해당 나의 x 좌표 부호를 곱해주면.
            minimapObjectRectTransform.anchoredPosition = new Vector2(minimapObjectRectTransform.anchoredPosition.x,
                                                                       (minimapObjectRectTransform.sizeDelta.y - viewPortObjectRectTransform.sizeDelta.y) / 2 * Mathf.Sign(minimapObjectRectTransform.anchoredPosition.y));
        }
    }
}
