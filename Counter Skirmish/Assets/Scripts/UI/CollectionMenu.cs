using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionMenu : MonoBehaviour
{
    public delegate void OnCollectionLoad();
    public static event OnCollectionLoad onCollectionLoad;
    
    [SerializeField] private RectTransform _filters, _cards;
    private List<string> _keywords;

    private void OnEnable() => LoadCollection();

    public void LoadCollection()
    {
        /*foreach (GameObject card in _cards)
        {
            card.GetComponent<CardInfo>().Base
        }*/
        
        onCollectionLoad?.Invoke();
    }

    public void OpenFilter()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        foreach (RectTransform filter in _filters)
            filter.gameObject.SetActive(!selected.GetComponent<Toggle>().isOn);
        selected.SetActive(true);
    }
    
    public void SelectFilter(string keyword)
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
            _keywords.Add(keyword);
        else
            _keywords.Remove(keyword);

        LoadCollection();
    }
}