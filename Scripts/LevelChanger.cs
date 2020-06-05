using UnityEngine;
using UnityEngine.SceneManagement;


//this helps and goes after
//https://www.youtube.com/watch?v=Oadq-IrOazg


// this is a level (scene) changer
public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    private string levelToLoad;
    void Update()
    {

        if (Input.GetKeyDown("escape")) {

            FadeToLevel("Menu");
        }

    }

    public void FadeToLevel(string sceneName) {
        levelToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {

        SceneManager.LoadScene(levelToLoad);

    }
}
