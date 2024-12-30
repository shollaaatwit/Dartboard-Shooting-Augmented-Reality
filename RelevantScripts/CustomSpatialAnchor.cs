using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CustomSpatialAnchor : MonoBehaviour
{
    // Start is called before the first frame update

    public string anchorName = "frame";
    List<OVRSpatialAnchor.UnboundAnchor> _unboundAnchors = new();
    void Start()
    {
        LoadAnchor();
    }

    public void AddAnchor()
    {
        OVRSpatialAnchor _anchor;
        _anchor = transform.gameObject.AddComponent<OVRSpatialAnchor>();
        StartCoroutine(AnchorAdded(_anchor));

    }
    IEnumerator AnchorAdded(OVRSpatialAnchor anchor)
    {

        yield return new WaitUntil(() => anchor.Created && anchor.Localized);

        Debug.Log($"Created anchor {anchor.Uuid}");
        SaveAnchor(anchor);
    }

    public async void SaveAnchor(OVRSpatialAnchor anchor)
    {
        var result = await anchor.SaveAnchorAsync();

        if(result.Success)
        {
            SaveAnchorToPlayerPrefs(anchor.Uuid.ToString());
        }
    }

    public void SaveAnchorToPlayerPrefs(string uuidString)
    {
        PlayerPrefs.SetString(anchorName, uuidString);
    }

    public void LoadAnchor()
    {
        if(PlayerPrefs.HasKey(anchorName))
        {
            string uuidString = PlayerPrefs.GetString(anchorName);
            var uuids = new Guid[1];
            uuids[0] = new Guid(uuidString);

            LoadAnchorsByUuid(uuids);
        }
    }

    async void LoadAnchorsByUuid(IEnumerable<Guid> uuids)
    {
        var result = await OVRSpatialAnchor.LoadUnboundAnchorsAsync(uuids, _unboundAnchors);

        if(result.Success)
        {
            foreach(var unboundAnchor in result.Value)
            {
                unboundAnchor.LocalizeAsync().ContinueWith((success, anchor) =>
                {
                    if(success)
                    {
                        var spatialAnchor = transform.gameObject.AddComponent<OVRSpatialAnchor>();
                        unboundAnchor.BindTo(spatialAnchor);
                    }
                }, unboundAnchor);
            }
        }
    }

    public void EraseAnchor()
    {
        if(GetComponent<OVRSpatialAnchor>())
        {
            var anchor = GetComponent<OVRSpatialAnchor>();
            EraseAnchorAsync(anchor);
            Destroy(GetComponent<OVRSpatialAnchor>());
        }
    }

    async void EraseAnchorAsync(OVRSpatialAnchor _spatialAnchor)
    {
        var result = await _spatialAnchor.EraseAnchorAsync();
        if(result.Success)
        {
           PlayerPrefs.DeleteKey(anchorName);
        }
    }
}
