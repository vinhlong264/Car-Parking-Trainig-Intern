using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour, IDrawHandler
{
    private RaycastDectector dectector;
    [SerializeField] private LayerMask WhatIsMask;
    private Route currentRoute;
    private LineRender currentLine;

    public System.Action<Route , List<Vector3>> OnParkLinked;

    void Start()
    {
        dectector = new RaycastDectector(WhatIsMask);
    }


    public void OnBeginDrawHandler()
    {
        if (!GetInfor().isContact)
        {
            return;
        }

        bool isCar = GetInfor().colider.TryGetComponent(out Car _car);
        if (isCar)
        {
            Debug.Log("Touch car");
            currentRoute = _car._route;
            if (currentRoute != null)
            {
                currentLine = currentRoute.LineRender;
                currentLine.InitializeLine();
            }
        }
    }

    public void OnDrawHandler()
    {
        if (currentRoute == null)
        {
            return ;
        }

        if (!GetInfor().isContact) return;


        Vector3 newPoint = GetInfor().point;
        currentLine.addLinePoint(newPoint);

        bool isPark = GetInfor().colider.TryGetComponent(out Park _park);
        if (isPark)
        {
            Route routePark = _park._route;
            if (currentRoute == routePark)
            {
                currentLine.addLinePoint(GetInfor().transform.position);
            }
            else
            {
                currentLine.ClearLine();
            }

            OnEndDrawHandler();
        }

    }

    public void OnEndDrawHandler()
    {
        if (currentRoute == null) return;

        if (!GetInfor().isContact)
        {
            currentLine.ClearLine();
            return;
        }

        bool isPark = GetInfor().colider.TryGetComponent(out Park _park);
        if (currentLine.PointCount < 2 || !isPark)
        {
            currentLine.ClearLine();
            return;
        }
        else
        {
            OnParkLinked?.Invoke(currentRoute , currentLine.ListPoints);
        }
        ResetDraw();
        
    }

    private void ResetDraw()
    {
        currentRoute = null;
        currentLine = null;
    }

    private RaycastDectector.RaycastInfor GetInfor()
    {
        return dectector.GetInforRaycast();
    }
}
