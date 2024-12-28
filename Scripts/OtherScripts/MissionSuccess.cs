using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissionSuccess : MonoBehaviour
{
    public static MissionSuccess Instance { get; private set; } 

    [SerializeField] private TextMeshProUGUI textMS;
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
            StartCoroutine(TextVisible());
        }
        Instance = this;
    }

    private IEnumerator TextVisible()
    {
        yield return new WaitForSeconds(1f);
        textMS.text = stringArr[i];

        textMS.ForceMeshUpdate();
        int totalVisibleCharacters = textMS.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            textMS.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                i++;
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter++;

            yield return new WaitForSeconds(timeBtwnChars);
        }

        yield return new WaitForSeconds(2.5f);

        isDone = true;

        print("TYPEWRITER DONE");

    }
}
