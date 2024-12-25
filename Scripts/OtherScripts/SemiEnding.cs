using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SemiEnding : MonoBehaviour
{
    public static SemiEnding Instance { get; private set; } 

    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private float timeBtwnChars;
    [SerializeField] private float timeBtwnWords;
    [SerializeField] private string[] stringArr;
    private bool isDone = false;

    public bool GetIsDone() {  return isDone; }

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        Instance = this;
    }

    void Start()
    {
        EndCheck();
        print("HEY");
    }

    int i = 0;

    void EndCheck()
    {
        if (i < stringArr.Length)
        {
            _textMeshPro.text = stringArr[i];
            StartCoroutine(TextVisible());
        }
        Instance = this;
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                i++;
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter++;

            yield return new WaitForSeconds(timeBtwnChars);
        }

        yield return new WaitForSeconds(1.5f);

        isDone = true;

        print("TYPEWRITER DONE");

    }
}
