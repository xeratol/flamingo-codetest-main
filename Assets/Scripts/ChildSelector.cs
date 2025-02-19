using System.Linq;
using UnityEngine;

public class ChildSelector : MonoBehaviour
{
    [SerializeField]
    private int _defaultIndex = 0;
    private Canvas[] _children;

    void Start()
    {
        _children = GetComponentsInChildren<Canvas>(true);

        Select(_defaultIndex);
    }

    public void Select(int index)
    {
        if (index < 0 || index >= _children.Length)
        {
            Debug.LogWarning($"Invalid selected index ({index})");
            return;
        }

        for (var i = 0; i < _children.Length; i++)
        {
            _children[i].gameObject.SetActive(i == index);
        }
    }

    public void Select(string name)
    {
        var selectedCanvas = _children.FirstOrDefault(c => c.gameObject.name == name);
        if (selectedCanvas == default)
        {
            Debug.LogWarning($"No child with specified name ({name})");
            return;
        }

        foreach (var child in _children)
        {
            child.gameObject.SetActive(child == selectedCanvas);
        }
    }
}
