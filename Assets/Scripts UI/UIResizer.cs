 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 [AddComponentMenu("UI/UI Resizer")]
 [ExecuteInEditMode]
 public class UIResizer : MonoBehaviour
 {
     // The resolution that the UI was initially designed for.
     public const float resolution = 1080;
 
     void Start(){
         SizeResolution(Screen.height);
     }
 
 #if UNITY_EDITOR
     void Update()
     {
         SizeResolution(Screen.height);
     }
 #endif
 
     Vector3 scale;
     public void SizeResolution(float current)
     {
         scale = transform.localScale;
         scale.x =  current / resolution;
         scale.y = current / resolution;
         if(scale != transform.localScale)
             transform.localScale = scale;
     }
 
     public static void ForceUpdate(){
         UIResizer[] resizes = GameObject.FindObjectsOfType<UIResizer>();
         foreach(UIResizer ui in resizes){
             ui.SizeResolution(Screen.height);
         }
     }
 }