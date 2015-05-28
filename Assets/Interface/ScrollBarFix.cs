using UnityEngine;
using UnityEngine.UI;
 
[ExecuteInEditMode]
public class ScrollBarFix : MonoBehaviour {
 
    void Update()
    {
        Fix();
    }
    void OnGUI()
    {
        Fix();
    }
    void OnRenderObject()
    {
        Fix();
    }
 
    private void Fix()
    {
        if (!Application.isPlaying)
        {
            Scrollbar scrollbar;
            RectTransform rectTransform;
 
            rectTransform = transform.GetComponent<RectTransform>();
            scrollbar = transform.parent.transform.parent.GetComponent<Scrollbar>();
            scrollbar.size = 0;
 
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 2);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -2);
 
            scrollbar.size = 1;
 
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 2);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -2);
        }
    }
}