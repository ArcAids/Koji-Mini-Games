using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioStreamer : MonoBehaviour
{
    //[SerializeField] string url= "https://objects.koji-cdn.com/a9af2e36-573b-45ea-aee4-1e22cde17524/song.mp3";
    const string url = "https://file-examples.com/wp-content/uploads/2017/11/file_example_WAV_10MG.wav";
    AudioClip clip;
    AudioSource source;
    DownloadHandlerAudioClip streamingClip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(StreamAudioV3());
    }

    IEnumerator StreamAudio()
    {
        UnityWebRequest www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerAudioClip(url, AudioType.WAV);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            streamingClip = ((DownloadHandlerAudioClip)www.downloadHandler);
            streamingClip.streamAudio = true;
            clip = streamingClip.audioClip;
            if (clip != null)
            {
                source.clip = clip;
                source.Play();
            }

        }
    }
    IEnumerator StreamAudioV2()
    {
        //old code
        #region
        
        #endregion

        DownloadHandlerAudioClip downloadHandler = new DownloadHandlerAudioClip(string.Empty, AudioType.AUDIOQUEUE);
        downloadHandler.streamAudio = true;
        UnityWebRequest request = new UnityWebRequest(url, "GET", downloadHandler, null);


        UnityWebRequestAsyncOperation token = request.SendWebRequest();
        while (clip == null) 
        {
            try
            {
                clip = DownloadHandlerAudioClip.GetContent(request);
            }
            catch (Exception) { }
            yield return 1f;
        }

        Debug.LogFormat("Waiting for AudioClip: bytes={0}", request.downloadedBytes); 

        source.clip = clip;
        source.Play();

        yield return 0;

    }

    IEnumerator StreamAudioV3()
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url,AudioType.WAV);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            clip = DownloadHandlerAudioClip.GetContent(www);

            Debug.LogFormat("Waiting for AudioClip: bytes={0}", www.downloadedBytes); 

            if (clip != null)
            {
                source.clip = clip;
                source.Play();
            }

        }
    }
}
