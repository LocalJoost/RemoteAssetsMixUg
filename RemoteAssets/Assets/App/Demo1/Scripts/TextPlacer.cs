using System.Collections;
using System.Collections.Generic;
using System.Net;
using DataServices.DataObjects;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class TextPlacer : MonoBehaviour
{
    public GameObject Container;

    public GameObject RemoteObjectTemplate;

    // Use this for initialization
    IEnumerator Start()
    {
        var request = UnityWebRequest.Get("https://mrdataservicesdemo.azurewebsites.net/api/spatialtext");
        request.SetRequestHeader("ZUMO-API-VERSION", "2.0.0");
        yield return request.SendWebRequest();
        var textList = JsonConvert.DeserializeObject<List<SpatialText>>(request.downloadHandler.text);
        for (int i = 0; i < textList.Count; i++)
        {
            var textData = textList[i];
            var text = Instantiate(RemoteObjectTemplate, Container.transform);
            var controller = text.GetComponent<TextController>();
            controller.SetText(textData.Text, textData.X, textData.Y, textData.Z);
        }
    }
}
