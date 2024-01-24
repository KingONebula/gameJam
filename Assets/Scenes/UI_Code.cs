using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Code : MonoBehaviour
{
    [SerializeField] GunScrOBJ gun;
    public void loadScene()
    {
        PlayerData.instance.setGun(gun);
        SceneManager.LoadScene(1);
    }
}
