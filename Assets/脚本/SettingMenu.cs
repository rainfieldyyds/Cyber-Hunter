using System.Collections;
using UnityEngine;


public class SettingMenu : MonoBehaviour
{


    public RectTransform panel;


    public float moveTime = 0.5f;


    Vector2 hidePos;
    Vector2 showPos;


    void Start()
    {

        showPos = panel.anchoredPosition;


        hidePos = new Vector2(
            1000,
            showPos.y
        );


        panel.anchoredPosition = hidePos;

        

    }



    public void OpenSetting()
    {

        gameObject.SetActive(true);


        StopAllCoroutines();


        StartCoroutine(
            MovePanel(hidePos,showPos)
        );

    }



    public void CloseSetting()
    {

        StartCoroutine(
            Close()
        );

    }



    IEnumerator Close()
    {

        yield return
        MovePanel(showPos,hidePos);


        

    }




    IEnumerator MovePanel(Vector2 start,Vector2 end)
    {

        float timer=0;


        while(timer < moveTime)
        {

            timer += Time.deltaTime;


            panel.anchoredPosition =
            Vector2.Lerp(
                start,
                end,
                timer/moveTime
            );


            yield return null;

        }


        panel.anchoredPosition=end;

    }


}