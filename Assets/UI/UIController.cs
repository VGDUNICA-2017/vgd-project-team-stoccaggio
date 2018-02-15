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

        GameObject newObj;

        HorizontalLayoutGroup itemsLG = itemsPanel.GetComponent<HorizontalLayoutGroup>();
        newObj = (GameObject)Instantiate(itemPrefab, itemsLG.transform);
        newObj = (GameObject)Instantiate(itemPrefab, itemsLG.transform);
        newObj = (GameObject)Instantiate(itemPrefab, itemsLG.transform);
        newObj = (GameObject)Instantiate(itemPrefab, itemsLG.transform);
        newObj = (GameObject)Instantiate(itemPrefab, itemsLG.transform);

        VerticalLayoutGroup missionsLG = missionsPanel.GetComponent<VerticalLayoutGroup>();
        newObj = (GameObject)Instantiate(missionPrefab, missionsPanel.transform);
        newObj = (GameObject)Instantiate(missionPrefab, missionsPanel.transform);
    }
}
