using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] Animator animator;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        animator.SetTrigger("End");
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName);
        Player.Instance.transform.position = new(0, -4.5f);
        animator.SetTrigger("End");
    }

    public void LoadScene(string sceneName)
    {
        // animator.enabled = true;
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
