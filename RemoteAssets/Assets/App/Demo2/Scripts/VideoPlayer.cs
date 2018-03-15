using System.Collections;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.Networking;

public class VideoPlayer : BaseMediaLoader, IFocusable
{
    public GameObject VideoPlane;

    public AudioSource Audio;

    public GameObject LookText;

    public float FocusLostTimeout = 2f;

    private MovieTexture _movieTexture;

    private bool _isFocusExit;

    protected void Start()
    {
        VideoPlane.SetActive(false);
        LookText.SetActive(false);
    }

    protected override IEnumerator StartLoadMedia()
    {
        VideoPlane.SetActive(false);
        LookText.SetActive(false);
        yield return LoadMediaFromUrl(MediaUrl);
    }

    private IEnumerator LoadMediaFromUrl(string url)
    {
        var handler = new DownloadHandlerMovieTexture();

        yield return ExecuteRequest(url, handler);

        _movieTexture = handler.movieTexture;
        _movieTexture.loop = true;
        Audio.loop = true;

        VideoPlane.GetComponent<Renderer>().material.mainTexture = _movieTexture;
        Audio.clip = handler.movieTexture.audioClip;
        VideoPlane.SetActive(true);
        LookText.SetActive(true);
    }

    private void StartPlaying()
    {
        if (_movieTexture == null)
        {
            return;
        }
        _isFocusExit = false;
        if (!_movieTexture.isPlaying)
        {
            LookText.SetActive(false);
            _movieTexture.Play();
            Audio.Play();
        }
    }

    private void PausePlaying()
    {
        if (_movieTexture == null)
        {
            return;
        }
        LookText.SetActive(true);
        _movieTexture.Pause();
    }

    public void OnFocusEnter()
    {
       StartPlaying();
    }

    public void OnFocusExit()
    {
        _isFocusExit = true;
        StartCoroutine(PausePlayingAfterTimeout());
    }

    IEnumerator PausePlayingAfterTimeout()
    {
        yield return new WaitForSeconds(FocusLostTimeout);
        if (_isFocusExit)
        {
            PausePlaying();
        }
    }
}
