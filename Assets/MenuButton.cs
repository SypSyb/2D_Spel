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

    // Start is called before the first frame update
    void Start()
    {
        if(isStart == true)
        {
            StartCoroutine(Button());
        }
       
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
            SceneManager.LoadScene(1);
        }

        if (Input.GetButton("Jump") && isMenu == true)
        {
            SceneManager.LoadScene(2);
        }
    }
}
