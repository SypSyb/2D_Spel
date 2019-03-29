using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour

{
    public bool isStart;
    public bool isMenu;
    public Text ding = null;
    public GameObject fade;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
        if (isStart == true)
        {
            StartCoroutine(Button());
        }
        fade.gameObject.SetActive(false);
    }

    public IEnumerator Button()
    {
        yield return new WaitForSeconds(.5f);
        ding.material.color = new Color(0, 0, 0, 255);
        yield return new WaitForSeconds(.5f);
        ding.material.color = new Color(0, 0, 0, 0);
        StartCoroutine(Button());
    }

    public void Update()
    {
        if(Input.GetButton("CircleButton") || Input.GetButton("SquareButton") || Input.GetButton("CrossButton") || Input.GetButton("TriangleButton") || Input.GetButton("Jump" ))
        {
            if(isStart == true)
            {
                SceneManager.LoadScene(1);
            }
            
        }


    }

    public void Play()
    {
        if(isMenu == true)
        {
            StartCoroutine(Fade());
        }
        
    }

    public IEnumerator Fade()
    {
        canvas.SetActive(false);
        fade.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
}
