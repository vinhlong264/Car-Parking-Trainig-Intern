using System.Collections;
using System.Collections.Generic;
using CarParking.Game.Manager;
using UnityEngine;
namespace CarParking.Game.Entity
{
    public class Route : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private DrawLine _lineRender;
        [SerializeField] private Park _park;

        [Header("Color infor")]
        [SerializeField] private Color _lineColor;

        private DrawHandler _drawHandler;
        private RouteManager _routeManager;
        public List<Vector3> points { get; private set; }

        public Car Car { get => _car; }
        public DrawLine LineRender { get => _lineRender; }
        public RouteManager _RouteManager { get => _routeManager; }

        private void Awake()
        {
            _routeManager = GetComponentInParent<RouteManager>();
        }

        void Start()
        {
            _drawHandler = _routeManager.lineDrawer;

            _drawHandler.OnParkLinked += OnParkLinkedHandler;
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

}
