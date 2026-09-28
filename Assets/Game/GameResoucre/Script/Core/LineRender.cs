using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour, IColor
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float pointMinBetween;
    [SerializeField] private float yPointFixed;
    private List<Vector3> listPoints = new List<Vector3>();
    private int pointCount;


    public int PointCount { get => pointCount; }
    public List<Vector3> ListPoints { get => listPoints; }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void InitializeLine()
    {
        gameObject.SetActive(true);
    }

    public void ClearLine()
    {
        gameObject.SetActive(false);
        line.positionCount = 0;
        pointCount = 0;
        listPoints.Clear();
    }

    public void addLinePoint(Vector3 newPoint)
    {
        newPoint.y = yPointFixed;

        if (pointCount >= 1f && Vector3.Distance(newPoint, GetLastPoint()) < pointMinBetween) return;

        listPoints.Add(newPoint);        
        pointCount++;
        line.positionCount = pointCount;
        setPostion(pointCount - 1, newPoint);
    }


    private Vector3 GetLastPoint()
    {
        return line.GetPosition(pointCount - 1);
    }

    public void setPostion(int index, Vector3 pos)
    {
        line.SetPosition(index, pos);
    }

    public void setColor(Color newColor)
    {
        line.sharedMaterials[0].color = newColor;
    }
}
