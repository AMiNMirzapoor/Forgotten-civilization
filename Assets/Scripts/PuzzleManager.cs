using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Singleton;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public List<string> selectedPattern = new List<string>();
    public List<string> desiredPattern = new List<string>();
    public GateController gate;

    private void Awake()
    {
        this.SetInstance(ref instance);
    }

    public void SymbolSelected(string symbolName)
    {
        // Add items to the list
        if (selectedPattern.Contains(symbolName))
            return;

        Debug.Log("New symbol selected : " + symbolName);
        selectedPattern.Add(symbolName);

        if (selectedPattern.Count == desiredPattern.Count)
        {
            // Check if the items in the list match the specific pattern
            bool hasPattern = CheckPattern(selectedPattern, desiredPattern);
            selectedPattern.Clear();
            UpdateSymbolStates(hasPattern);
            if (hasPattern)
            {
                gate.ShowOpenAnimation();
            }
            Debug.Log("Pattern matched: " + hasPattern);
        }
    }

    private void UpdateSymbolStates(bool hasPattern)
    {
        SymbolController[] symbolElements = gameObject.GetComponentsInChildren<SymbolController>();
        for (int i = 0; i < symbolElements.Length; i++)
        {
            symbolElements[i].UpdateState(hasPattern);
        }
    }

    public bool CheckPattern(List<string> list1, List<string> list2)
    {
        return list1.SequenceEqual(list2);
    }
    
}
