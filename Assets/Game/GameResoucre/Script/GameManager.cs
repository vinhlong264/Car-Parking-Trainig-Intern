using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public LineDrawer lineDrawer;
    public System.Action OnResetGame;
    public System.Action OnYouWIn;
    public System.Action OnYouLose;

    [Header("UI")]
    [SerializeField] private GameObject YouWin;
    [SerializeField] private GameObject YouLose;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        QualitySettings.vSyncCount = 0;
    }

    private void OnEnable()
    {
        OnYouWIn += OnYouWinHandler;
        OnYouLose += OnYouLoseHandler;
    }

    private void OnDisable()
    {
        OnYouWIn -= OnYouWinHandler;
        OnYouLose -= OnYouLoseHandler;
    }

    private void OnYouLoseHandler()
    {
        Debug.Log("You lose");
        StartCoroutine(DelayEvent(() =>
        {
            YouLose.SetActive(true);
        }));
    }

    private void OnYouWinHandler()
    {
        StartCoroutine(DelayEvent(() =>
        {
            YouWin.SetActive(true);
        }));
    }

    public void OnResetGameInvoke()
    {
        OnResetGame?.Invoke();
    }

    IEnumerator DelayEvent(System.Action callBack)
    {
        yield return new WaitForSeconds(1.5f);
        callBack.Invoke();
    }

}
