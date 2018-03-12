using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject itemsPanel;
    public GameObject itemPrefab;

    public GameObject missionsPanel;
    public GameObject missionPrefab;

	void Start () {
        Instantiate(itemPrefab, itemsPanel.transform);
        Instantiate(itemPrefab, itemsPanel.transform);
        Instantiate(itemPrefab, itemsPanel.transform);
        Instantiate(itemPrefab, itemsPanel.transform);
        Instantiate(itemPrefab, itemsPanel.transform);

        Instantiate(missionPrefab, missionsPanel.transform);
        Instantiate(missionPrefab, missionsPanel.transform);
    }
}
