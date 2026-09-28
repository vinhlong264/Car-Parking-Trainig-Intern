using System.Collections;
using UnityEngine;
public class Car : MonoBehaviour, IColor
{
    private Rigidbody rb;
    public Route _route { get; private set; }
    [SerializeField] private int index;
    [SerializeField] private float duration;
    [SerializeField] private bool isCollision;
    [SerializeField] private GameObject fxCarCollision;
    private Vector3 posDefault;

    private void Start()
    {
        _route = GetComponentInParent<Route>();
        rb = GetComponent<Rigidbody>();
        isCollision = false;
        posDefault = transform.position;
    }

    public void OnMoveCar(Vector3[] path)
    {

        for (int i = 0; i < path.Length; i++)
        {
            path[i].y = transform.position.y;
        }

        StartCoroutine(MoveCar(path));
    }


    IEnumerator MoveCar(Vector3[] path)
    {
        while (index < path.Length && !isCollision)
        {
            Vector3 startPos = transform.position;  //cập nhập điểm bắt đầu
            Vector3 endPos = path[index];           // Cập nhật điểm kết thúc

            Vector3 dir = (endPos - transform.position).normalized;
            float t = 0;
            while (t < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, t/duration);
                t += Time.deltaTime;
                yield return null;
            }
            
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position = endPos;
            index++;
            yield return null; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("obstacle"))
        {
            ColisionHandler(collision.GetContact(0).point);
        }
    }

    private void ColisionHandler(Vector3 hitDir)
    {
        GameManager.Instance.OnYouLose?.Invoke(); // kích hoạt delegate YouLose

        FxHandler();
        rb.AddExplosionForce(50, hitDir, 3f);
        rb.AddForceAtPosition(Vector3.up * 5f , hitDir , ForceMode.Impulse);
        rb.AddTorque(GetRandomDir(), GetRandomDir(), GetRandomDir());
        isCollision = true;
        
    }

    public void ResetValueDefalut()
    {
        this.transform.position = this.posDefault;
        isCollision = false;
        transform.rotation = Quaternion.identity;

        index = 0;
        StopAllCoroutines();
        rb.velocity = Vector3.zero;
    }

    private void FxHandler()
    {
        GameObject fx = ObjectPooling.Instance.GetObjToPools(fxCarCollision);
        if(fx != null)
        {
            fx.transform.position = transform.position + new Vector3(0, 3, 0);
            transform.rotation = Quaternion.identity;
        }
        StartCoroutine(DelayDeactive(fx));
    }


    IEnumerator DelayDeactive(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        obj.SetActive(false);
    }
    private float GetRandomDir()
    {
        float angle = 10f;
        float rand = Random.value;

        return rand < 0.5f ? angle : -angle;
    }


    public void setColor(Color newColor)
    {

    }
}
