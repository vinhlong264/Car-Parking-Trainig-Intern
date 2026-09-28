using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private SpawnLevel spawnLevel;
    [SerializeField] private LoadGameAnim anim;


    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void PlayBtnHandler(GameObject active)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (active != null)
        {
            active.SetActive(true);
        }
    }

    public void ExitLevelSelectHandler(GameObject active)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        active.SetActive(false);
    }

    public void OnNextLevelBtn(GameObject setlf)
    {
        if (spawnLevel == null) return;

        setlf.SetActive(false);
        anim.FadeAnimtion();
        StartCoroutine(DelayEvent(() =>
        {
            anim.ResetTrigger();
            spawnLevel.NextLevel();
        }));
    }

    IEnumerator DelayEvent(System.Action action = null)
    {
        yield return new WaitForSeconds(1f);
        action?.Invoke();
    }

    public void LoadMenu(GameObject self)
    {
        Debug.Log("call Back");
        self.SetActive(false);
        anim.FadeAnimtion();
        StartCoroutine(DelayEvent(() =>
        {
            SceneManager.LoadScene("Menu");
        }));
    }

    public void ExitGameHandler()
    {
        Application.Quit();
    }

    public void OnResetGameBtn(GameObject self)
    {
        GameManager.Instance.OnResetGameInvoke();
        self.SetActive(false);
    }
}
