using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneUrl;

    public GameObject Container;

    private bool _sceneIsLoaded;

    public void StartLoading()
    {
        if (!_sceneIsLoaded)
        {
            StartCoroutine(LoadScene(SceneUrl));
            _sceneIsLoaded = true;
        }
    }

    private IEnumerator LoadScene(string url)
    {
        var request = UnityWebRequest.GetAssetBundle(url, 0);
        yield return request.SendWebRequest();
        var bundle = DownloadHandlerAssetBundle.GetContent(request);
        var paths = bundle.GetAllScenePaths();
        if (paths.Length > 0)
        {
            var path = paths[0];
            yield return SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);
            var sceneHolder = GameObject.Find("SceneHolder");
            foreach (Transform child in sceneHolder.transform)
            {
                child.parent = Container.transform;
            }
            SceneManager.UnloadSceneAsync(path);
        }
    }
}
