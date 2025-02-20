using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 lastDragPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastDragPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 dragDelta = Input.mousePosition - lastDragPosition;
            MovePlatform(dragDelta.x);
            lastDragPosition = Input.mousePosition;
        }
    }

    void MovePlatform(float delta)
    {
        float movementSpeed = 0.01f; // �������� ���� ���������
        float newPosition = transform.position.x + delta * movementSpeed;

        // ��� ������
        float screenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        float screenLimit = screenWidth / 2f - 0.5f; // ������� ������ �� ��� ������

        // ��������� ���� ��������� �� ������ ������
        newPosition = Mathf.Clamp(newPosition, -screenLimit, screenLimit);

        // ������ ���� ������� ���������
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
}
