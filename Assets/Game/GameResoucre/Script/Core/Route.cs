using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private LineRender _lineRender;
    [SerializeField] private Park _park;

    [Header("Color infor")]
    [SerializeField] private Color _lineColor;

    private LineDrawer _lineDrawer;
    private RouteManager _routeManager;
    public List<Vector3> points { get; private set; }

    public Car Car { get => _car; }
    public LineRender LineRender { get => _lineRender; }
    public RouteManager _RouteManager { get => _routeManager; }

    private void Awake()
    {
        _routeManager = GetComponentInParent<RouteManager>();
    }

    void Start()
    {
        _lineDrawer = _routeManager.lineDrawer;

        _lineDrawer.OnParkLinked += OnParkLinkedHandler;
    }

    private void OnParkLinkedHandler(Route route, List<Vector3> path)
    {
        if (path.Count == 0 || route != this) return;
        points = path;
       _routeManager.RegisterRoute(this); // đăng kí car di chuyển khi vẽ đến đích
    }

    public void ResetLevel()
    {
        this._car.ResetValueDefalut();
        this._lineRender.ClearLine();
    }

#if UNITY_2022_3
    private void OnDrawGizmos()
    {
        _lineRender.setColor(_lineColor);
        _park.setColor(_lineColor);
    }
#endif
}
