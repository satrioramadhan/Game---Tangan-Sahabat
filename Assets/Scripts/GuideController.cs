using UnityEngine;

public class GuideController : MonoBehaviour
{
    public GameObject guidePanel;

    public void CloseGuide()
    {
        guidePanel.SetActive(false);
    }
}
