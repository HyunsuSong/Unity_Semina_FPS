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
        // playerPosObject�� �̵��� ��ŭ�� �̵����ָ� �ǹǷ�
        // �߾��� ���÷� ��� playerPosObject�� 400,400 �̶�� ���� ��ġ�� 0,0 / 440, 440 �̶�� ���� ��ġ�� -40, -40�� �Ǿ�� �ϹǷ�
        // �� ���ø� �������� ���� ����� playerPosObject�� ��ġ - (�̴ϸ� �̹����� �������� ����(�¿�� �����Ƿ�)) �� �ݴ� ���� �̴ϸ��� ��ġ�� ��
        minimapObjectRectTransform.anchoredPosition = -(playerPosObjectRectTransform.anchoredPosition - (minimapObjectRectTransform.sizeDelta / 2));

        if (Mathf.Abs(minimapObjectRectTransform.anchoredPosition.x) >= 200.0f)
        {
                                                                      // �ش� �̴ϸ��� �ִ��� ������ �� �ִ� x ��ǥ�� ���� * �ش� ���� x ��ǥ ��ȣ�� �����ָ�.
            minimapObjectRectTransform.anchoredPosition = new Vector2((minimapObjectRectTransform.sizeDelta.x - viewPortObjectRectTransform.sizeDelta.x) / 2 * Mathf.Sign(minimapObjectRectTransform.anchoredPosition.x),
                                                                       minimapObjectRectTransform.anchoredPosition.y);
        }

        if(Mathf.Abs(minimapObjectRectTransform.anchoredPosition.y) >= 200.0f)
        {
            // �ش� �̴ϸ��� �ִ��� ������ �� �ִ� x ��ǥ�� ���� * �ش� ���� x ��ǥ ��ȣ�� �����ָ�.
            minimapObjectRectTransform.anchoredPosition = new Vector2(minimapObjectRectTransform.anchoredPosition.x,
                                                                       (minimapObjectRectTransform.sizeDelta.y - viewPortObjectRectTransform.sizeDelta.y) / 2 * Mathf.Sign(minimapObjectRectTransform.anchoredPosition.y));
        }
    }
}
