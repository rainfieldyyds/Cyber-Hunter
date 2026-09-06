using UnityEngine;


public class BrightnessManager : MonoBehaviour
{

    public CanvasGroup blackPanel;



    public void SetBrightness(float value)
    {

        blackPanel.alpha =
        1-value;

    }

}