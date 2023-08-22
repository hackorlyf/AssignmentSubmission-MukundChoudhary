using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircles = 5;
    public int maxCircles = 10;

    private List<GameObject> circles = new List<GameObject>();
    private Vector3 lineStart;
    private Vector3 lineEnd;
    private bool isDrawing = false;

    private LineRenderer lineRenderer;

    private void Start()
    {
        SpawnCircles();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            lineStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineStart.z = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            lineEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineEnd.z = 0;
            
            lineRenderer.SetPosition(0, lineStart);
            lineRenderer.SetPosition(1, lineEnd);
            RemoveIntersectedCircles();

            // Update line renderer positions
            
        }
    }

    private void SpawnCircles()
    {
        int numCircles = Random.Range(minCircles, maxCircles + 1);
        for (int i = 0; i < numCircles; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-8f, 8f),
                Random.Range(-4.5f, 4.5f),
                0f
            );
            GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circles.Add(newCircle);
        }
    }

    private void RemoveIntersectedCircles()
{
    List<GameObject> circlesToRemove = new List<GameObject>();

    foreach (GameObject circle in circles)
    {
        if (IsCircleIntersected(circle))
        {
            circlesToRemove.Add(circle);
        }
    }

    // Remove the intersected circles from the circles list and destroy them
    foreach (GameObject circleToRemove in circlesToRemove)
    {
        circles.Remove(circleToRemove);
        Destroy(circleToRemove);
    }
}

    private bool IsCircleIntersected(GameObject circle)
{
    CircleCollider2D circleCollider = circle.GetComponent<CircleCollider2D>();
    return Physics2D.Linecast(lineStart, lineEnd, 1 << circle.layer) && circleCollider.bounds.IntersectRay(new Ray(lineStart, lineEnd - lineStart));
}

}

