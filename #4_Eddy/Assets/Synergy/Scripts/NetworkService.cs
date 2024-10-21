using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public class NetworkService : MonoBehaviour
{
    private const string urlWithoutUserCode = "https://vr.eddy.org.ua/get_preset_by_code?code=";
    private SessionContext _sessionContext;

    public event Action<GetPresetResponse, string> GotPresetResponce;

    public void Inject(SessionContext sessionContext)
    {
        _sessionContext = sessionContext;
    }

    public void StartPresetResponceCoroutine(string userCode)
    {
        StartCoroutine(GetPresetResponceCoroutine(userCode));
    }

    private IEnumerator GetPresetResponceCoroutine(string userCode)
    {
        string urlWithUserCode = urlWithoutUserCode + userCode;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlWithUserCode))
        {
            yield return webRequest.SendWebRequest();

            GetPresetResponse presetResponse = new GetPresetResponse();

            JObject jObject = JObject.Parse(webRequest.downloadHandler.text);

            JToken jUserRole = jObject.GetValue("user_role");
            presetResponse.userRole = (UserRole)Enum.Parse(typeof(UserRole), jUserRole.ToString());

            JArray jUsers = (JArray)jObject.GetValue("users");
            presetResponse.users = jUsers.ToObject<int[]>();

            JArray jFiles = (JArray)jObject.GetValue("files");
            presetResponse.content = jFiles.ToObject<ContentEntry[]>();

            foreach (var contentEntry in presetResponse.content)
            {
                StartCoroutine(DownloadPrefabContent(contentEntry.url));
            }

            GotPresetResponce?.Invoke(presetResponse, userCode);
            yield break;
        }
    }

    private IEnumerator DownloadPrefabContent(string url)
    {
        var www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();

        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(www);

        var prefabRequest = assetBundle.LoadAssetAsync<GameObject>("prefab.prefab");
        yield return prefabRequest;

        GameObject prefab = null;
        prefab = prefabRequest.asset as GameObject;
        _sessionContext.AddPrefabToList(prefab);
        Instantiate(prefab, prefab.transform.position, Quaternion.identity);
    }
}
