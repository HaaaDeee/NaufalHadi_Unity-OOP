using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Awake()
    {
        animator.enabled = false;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
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
