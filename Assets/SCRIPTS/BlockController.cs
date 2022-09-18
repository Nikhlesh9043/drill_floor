using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    public TextMeshProUGUI NumText;
    [SerializeField] GameObject graphic;
    [SerializeField] GameObject droppable;

    bool isDroppable;
    LevelCreation.Droppable drop_item;

    Animator block_animator;

    [HideInInspector] public int value;

    // Start is called before the first frame update
    void Start()
    {
        block_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        droppable.SetActive(false);
        isDroppable = false;
    }

    public void AssignValue(int number)
    {
        value = number;
        NumText.text = value.ToString();

        AssignColor();
    }

    private void AssignColor()
    {
        if (value > GameValues.Base4)
        {
            graphic.GetComponent<Renderer>().material.color = LevelCreation.GetInstance().color_list[4];
        }
        else if (value > GameValues.Base3)
        {
            graphic.GetComponent<Renderer>().material.color = LevelCreation.GetInstance().color_list[3];
        }
        else if (value > GameValues.Base2)
        {
            graphic.GetComponent<Renderer>().material.color = LevelCreation.GetInstance().color_list[2];
        }
        else if (value > GameValues.Base1)
        {
            graphic.GetComponent<Renderer>().material.color = LevelCreation.GetInstance().color_list[1];
        }
        else
        {
            graphic.GetComponent<Renderer>().material.color = LevelCreation.GetInstance().color_list[0];
        }
    }

    public void DamageBlock()
    {
        block_animator.Play("pop");
        AssignValue(value-2);

        if (value < 0)
        {
            DestroyBlock(false);
        }
    }
    public void DamageBlock(int damage_value)
    {
        block_animator.Play("pop");
        AssignValue(value - damage_value);

        if (value < 0)
        {
            DestroyBlock(false);
        }
    }

    public void SetDroppable(LevelCreation.Droppable drop)
    {
        drop_item = drop;
        isDroppable = true;
        droppable.SetActive(true);

        droppable.transform.GetChild((int)drop + 1).gameObject.SetActive(true);
    }

    public void DestroyBlock(bool bonus)
    {
        if (!GameValues.GameOver)
        {
            //Gameplay.GetInstance().SpawnFracturedCube(gameObject.transform, bonus);
        }
        //LevelCreation.GetInstance().ActiveBlocks.Remove(gameObject);
        //ObjectPooler.GetInstance().PutToPool("block", gameObject);

        graphic.transform.localScale = new Vector3(50,30,50);

        droppable.transform.GetChild((int)drop_item + 1).gameObject.SetActive(false);
        if (isDroppable)
        {
            LevelCreation.GetInstance().SpawnDroppableItem(drop_item,transform.position);
            //EmojiController.GetInstance().ShowEmoji(3);
        }

        gameObject.SetActive(false);
        droppable.SetActive(false);
        isDroppable = false;
    }
}
