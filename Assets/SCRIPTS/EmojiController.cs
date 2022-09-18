using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    static EmojiController instance;

    [SerializeField] GameObject emotion_bubble;
    [SerializeField] SpriteRenderer emotion;
    public List<Sprite> emotion_list;
    [SerializeField] Transform camera_reference;
    [SerializeField] Transform player;

    bool isShown;
    float emoji_counter;

    int last_emoji_index;
    float[] timer_array = new float[] { 10,2,2,2,2 };

    private void Awake()
    {
        instance = this;
        ResetEmoji();
    }

    public static EmojiController GetInstance() { return instance; }

    private void Update()
    {
        transform.position = player.position;

        if (isShown)
        {
            emoji_counter -= Time.deltaTime;

            if (emoji_counter < 0) HideEmoji();
        }
        emotion_bubble.transform.LookAt(camera_reference.position, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ShowEmoji(1);
        }
    }

    public void ShowEmoji(int emoji_index)
    {
        emotion_bubble.SetActive(true);
        isShown = true;

        if (emoji_index == 3 && last_emoji_index >= 3) emoji_index = 4;
        emoji_counter = timer_array[emoji_index];
        emotion.sprite = emotion_list[emoji_index];

        last_emoji_index = emoji_index;
    }
    public void HideEmoji()
    {
        emotion_bubble.SetActive(false);
        isShown = false;

    }

    public void ResetEmoji()
    {
        HideEmoji();
        last_emoji_index = 2;
    }
}
