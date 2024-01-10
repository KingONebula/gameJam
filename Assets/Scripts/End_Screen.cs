using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End_Screen : MonoBehaviour
{
    // Start is called before the first frame update
    public static End_Screen instance;
    [SerializeField] GameObject UI_Element;
    [SerializeField] GameObject UI_Boons;
    public List<GameObject> boons;
    [SerializeField]GridLayoutGroup grid;
    void Start()
    {

        boons = new List<GameObject>();
        instance = this;
        int boonCount = 0;

        while (boonCount < 3 && PlayerData.instance.boonsCount() != 0)
        {
            int size = PlayerData.instance.boonsCount();
            Debug.Log(size);
            int boonsIndex = Random.Range(0, size);
            boons.Add( PlayerData.instance.popBoon(boonsIndex));
            
            boonCount++;
        }
        for(int i = 0; i < boons.Count; i++)
        {
            boons[i] = Instantiate(boons[i], UI_Boons.transform);
        }
    }

    public void selectedBoon(GameObject boonCard)
    {
        grid.enabled = false;
        for(int i = 0; i < UI_Boons.transform.childCount; i++)
        {
            if (boons[i] == boonCard)
            {
                boonCard.GetComponent<Animator>().CrossFade("Up", 0, 0);
                continue;
            }
            boons[i].GetComponent<Animator>().CrossFade("Down", 0, 0);
            BoonData boonScrObj= boons[i].GetComponent<BoonData>();
            PlayerData.instance.AddBoon(boonScrObj.boon.boonCard);
            Button b = boons[i].GetComponent<Button>();
            b.interactable = false;
            Time.timeScale = 1;
            StartCoroutine(nextFloor());
        }

    }
    
    IEnumerator nextFloor()
    {
        yield return new WaitForSecondsRealtime(1);
        PlayerData.instance.waveF();
        SceneManager.LoadScene(1);
    }
    public IEnumerator loadUI()
    {
        Time.timeScale = 0;
        UI_Element.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        foreach(GameObject boon in boons)
        {
            Button b = boon.GetComponent<Button>();
            b.interactable = true;
        }

    }
}
