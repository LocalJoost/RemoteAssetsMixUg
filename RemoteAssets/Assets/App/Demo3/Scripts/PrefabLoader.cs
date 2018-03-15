using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PrefabLoader : MonoBehaviour {

    public string AssetUrl;

    public GameObject Container;

    private bool _isLoaded;

    public void StartLoading()
    {
        if (!_isLoaded)
        {
            StartCoroutine(LoadPrefab(AssetUrl));
            _isLoaded = true;
        }
    }

    private IEnumerator LoadPrefab(string url)
    {
        var request = UnityWebRequest.GetAssetBundle(url, 0);
        yield return request.SendWebRequest();
        var bundle = DownloadHandlerAssetBundle.GetContent(request);
        var asset = bundle.LoadAsset<GameObject>(bundle.GetAllAssetNames()[0]);
        Instantiate(asset, Container.transform);
    }
}
