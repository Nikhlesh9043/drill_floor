using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public int floor_count;
    public int max_block_value;
    public int min_block_value;

    public float hammer_interval;
    public int container_health_value;

    public LevelInfo(int floor, int max_value, int min_value, float interval, int container_health)
    {
        floor_count = floor;
        max_block_value = max_value;
        min_block_value = min_value;

        hammer_interval = interval;
        container_health_value = container_health;
    }
}
