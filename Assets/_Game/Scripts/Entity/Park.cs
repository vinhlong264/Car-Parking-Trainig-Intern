using System.Collections;
using System.Collections.Generic;
using CarParking.Game.Manager;
using UnityEngine;
namespace CarParking.Game.Entity
{
    public class Park : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sr;
        public Route _route { get; private set; }
        private RouteManager routeManager;
        [SerializeField] private GameObject fxPark;
        [SerializeField] private Transform posFx;

        private Color colorFx;

        private void Start()
        {
            _route = GetComponentInParent<Route>();


            routeManager = _route._RouteManager;
        }
        public void setColor(Color newColor)
        {
            sr.color = newColor;
            colorFx = newColor;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent<Car>(out Car car))
            {
                if (_route == car._route)
                {
                    fxParkHandler();
                    routeManager.OnParkEnterDestination();
                }
            }
        }

        private void fxParkHandler()
        {
            // GameObject fx = ObjectPooling.Instance.GetObjToPools(fxPark);
            // fx.transform.position = posFx.position;
            // transform.rotation = Quaternion.Euler(-90, 0, 0);
            // fx.GetComponent<SpriteRenderer>().color = colorFx;
            // StartCoroutine(DelayDeactive(fx));
        }

        IEnumerator DelayDeactive(GameObject obj)
        {
            yield return new WaitForSeconds(0.75f);
            obj.SetActive(false);
        }
    }

}
