using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RingsList : MonoBehaviour
{
    public static RingsList instance;
 
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _ringPanelPrefab;
    [SerializeField] private TextMeshProUGUI _sanity;
    
    private List<RingInList> _rings = new List<RingInList>();

    private void Awake()
    {
        instance = this;
    }

    public void Toggle(bool open)
    {
        if (open && UIWindows.windowOpen)
            return;
        UIWindows.windowOpen = open;
        _canvasGroup.blocksRaycasts = open;
        _canvasGroup.alpha = BoolHelper.BoolToInt(open);
        _content.sizeDelta = new Vector2(1, 40 + Saves.Data.rings.Count * 220);
        Saves.Data.rings = Saves.Data.rings.OrderBy(t => t.power).ToList();
        int i = 0;
        if (open)
        {
            foreach (var ring in Saves.Data.rings)
            {
                RingInList newRing = Instantiate(_ringPanelPrefab, Vector3.zero, Quaternion.identity).GetComponent<RingInList>();
                newRing.rectTransform.SetParent(_content);
                newRing.rectTransform.anchoredPosition = new Vector2(0,  -200 * i);
                newRing.rectTransform.sizeDelta = new Vector2(0, 200);
                newRing.Init(ring);
                _rings.Add(newRing);
                i++;
            }
        }
        else
        {
            foreach (var ring in _rings)
            {
                Destroy(ring.gameObject);
            }
            _rings = new List<RingInList>();
        }
        UpdateSanity();
    }

    public void UpdateSanity()
    {
        _sanity.text = $"{Saves.Data.sanityUsed}/{Saves.Data.sanity}";
        if (Saves.Data.sanityUsed == Saves.Data.sanity || Saves.Data.rings.Count == Saves.Data.ringsActive.Count)
            _sanity.color = new Color(1, 1, 1, 0.4f);
        else
            _sanity.color = new Color(1, 1, 1, 1);
    }
}
