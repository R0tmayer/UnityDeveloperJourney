using System.Collections.Generic;
using UnityEngine;

public class SessionContext
{
    private List<GameObject> _downloadedPrefabs = new List<GameObject>();

    public int[] assetsIds; // ContentEntry[] content.file_id;
    public int[] relatedTeachersIds; // string[] users;
    public UserRole userRole; // UserRole userRole;
    public string userID; // код который мы вводим

    public SessionContext()
    {
        userRole = UserRole.Undefined;
    }

    public void Initialization(GetPresetResponse presetResponce, int[] relatedTeachersIds, UserRole userRole, string userID)
    {
        assetsIds = new int[presetResponce.content.Length];

        for (int i = 0; i < assetsIds.Length; i++)
        {
            assetsIds[i] = presetResponce.content[i].file_id;
        }

        this.relatedTeachersIds = relatedTeachersIds;
        this.userRole = userRole;
        this.userID = userID;
    }

    public void AddPrefabToList(GameObject prefab)
    {
        _downloadedPrefabs.Add(prefab);
    }
}

