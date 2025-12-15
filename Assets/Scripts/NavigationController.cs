using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController: MonoBehaviour
{
   public void onClickStart(){
    SceneManager.LoadScene(1);
   }

   public void onClickQuit(){
    Application.Quit();
   }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}