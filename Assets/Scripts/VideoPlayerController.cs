using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer video;
    public int faseAtual;
    public string nome;
    public float tempo;

    private void Start()
    {
        video = GetComponent<VideoPlayer>();
        PlayVideo();
        StartCoroutine(CoFunc());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(faseAtual);
        }
    }

    IEnumerator CoFunc()
    {
        yield return new WaitForSeconds(tempo+2);
        SceneManager.LoadScene(faseAtual);
    }
    
    void PlayVideo()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, nome);
        video.url = videoPath;
        video.Play();
        Debug.Log(video.length);
    }
}
