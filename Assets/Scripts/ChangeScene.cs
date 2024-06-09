using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ChangeScene : MonoBehaviour
{
    public float hi;
    FadeOut fade;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeOut>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChangeThatScene()
    {
        fade.Fade();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(ChangeThatScene());
        }
    }
}
