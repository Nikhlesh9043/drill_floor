using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    [SerializeField] LevelCreation.Droppable drop_type;
    [SerializeField] GameObject effectObj;
    [SerializeField] GameObject sfxObj;
    [SerializeField] GameObject graphic;
    GameObject effect;

    void Start()
    {
        Invoke("SpawnEffect",1f);
    }

    void SpawnEffect()
    {
        effect = Instantiate(effectObj, transform.position, Quaternion.identity);

        if(drop_type == LevelCreation.Droppable.bomb)
        {
            graphic.SetActive(false);
            Invoke("HideEffect", 1);
        }
        else if (drop_type == LevelCreation.Droppable.chest)
        {
            graphic.SetActive(false);
            Invoke("HideEffect", 2);
            GameValues.CoinCount += 5;
            PlayerPrefs.SetInt("CoinCount", GameValues.CoinCount);
            Gameplay.GetInstance().coinText.text = GameValues.CoinCount.ToString();
        }
    }

    void HideEffect()
    {
        effect.SetActive(false);
    }
}
