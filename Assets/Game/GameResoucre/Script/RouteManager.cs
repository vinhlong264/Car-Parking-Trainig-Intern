using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public LineDrawer lineDrawer;

    private List<Route> routes = new List<Route>();
    private int totalRoute;
    private int carToDestination;

    private void OnEnable()
    {
        GameManager.Instance.OnResetGame += OnResetHandler;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnResetGame -= OnResetHandler;
    }


    private void Start()
    {
        totalRoute = GetComponentsInChildren<Route>().Length;
        lineDrawer = GameManager.Instance.lineDrawer;
    }

    public void OnParkEnterDestination()
    {
        carToDestination++;
        if(carToDestination == routes.Count)
        {
            GameManager.Instance.OnYouWIn?.Invoke(); // kích hoạt delegate You Win 
        }
    }

    public void RegisterRoute(Route _route) // đăng kí các route điều khiển car
    {
        routes.Add(_route);
        if (routes.Count == totalRoute)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
        foreach (var i in routes)
        {
            i.Car.OnMoveCar(i.points.ToArray());
        }
    }

    private void OnResetHandler()
    {
        if (this.routes.Count == 0) return;

        if(this.routes.Count == 1)
        {
            this.routes[0].ResetLevel();
        }

        if(this.routes.Count > 1)
        {
            foreach (var i in this.routes)
            {
                i.ResetLevel();
            }
        }

        this.routes.Clear();
    }

}
