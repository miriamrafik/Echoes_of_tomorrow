using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;  // for video player
using UnityEngine.SceneManagement; // for scene manager



public class Cutscene2Loader : MonoBehaviour
{
    public string nextSceneName;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    
    if (Input.GetKeyDown(KeyCode.Escape))  // to skip 
    {
        SceneManager.LoadScene(nextSceneName);
    }

    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Level 5");
    }
}
