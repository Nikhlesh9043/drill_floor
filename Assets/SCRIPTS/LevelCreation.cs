using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Cinemachine;

public class LevelCreation : MonoBehaviour
{
    private static LevelCreation instance;

    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject floorPrefab;
    [SerializeField] GameObject housePrefab;
    [SerializeField] NavMeshSurface navmesh;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject containerPrefab;
    [SerializeField] GameObject scorePopPrefab;

    GameObject current_block;
    BlockController current_block_script;

    GameObject container;
    GameObject house;
    [HideInInspector] public Transform car;
    [SerializeField] CinemachineStateDrivenCamera cinemachine_camera;

    [SerializeField] Transform player;

    public List<Color> color_list;
    [HideInInspector] public List<GameObject> ActiveBlocks = new List<GameObject>();

    public List<GameObject> activeBlocks;
    public List<GameObject> activeFloors;
    public List<GameObject> activeEnemies;
    public List<GameObject> activeScorePops;
    [SerializeField] Camera cam;

    public List<LevelInfo> levelInfoList;
    [SerializeField] TextMeshProUGUI levelText;

    public enum Droppable {bomb, chest };
    public List<GameObject> droppable_list;
    List<GameObject> activeDroppables;

    int height = 5;

    private void Awake()
    {
        instance = this;

        activeDroppables = new List<GameObject>();

        levelInfoList = new List<LevelInfo>();
        //Populate leveInfoList
        levelInfoList.Add(new LevelInfo(3, 30, 2, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(3, 30, 10, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(5, 50, 20, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(5, 50, 25, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(6, 60, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(6, 70, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(6, 70, 25, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(6, 70, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(8, 70, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(8, 70, 25, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(8, 70, 35, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(8, 70, 45, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(10, 70, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(10, 70, 35, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(10, 70, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(10, 70, 45, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(12, 70, 30, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(12, 70, 35, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(12, 70, 40, 0.1f, 50));
        levelInfoList.Add(new LevelInfo(12, 70, 45, 0.1f, 50));
    }

    public static LevelCreation GetInstance(){ return instance; }

    public void SetupLevel(int level_num)
    {
        levelText.text = "LEVEL " + GameValues.CurrentDisplayLevel.ToString();
        GameValues.MaxBlockValue = levelInfoList[level_num - 1].max_block_value;

        //Set Color Bases
        var diff = (GameValues.MaxBlockValue) / 5;
        GameValues.Base1 = diff;
        GameValues.Base2 = GameValues.Base1 + diff;
        GameValues.Base3 = GameValues.Base2 + diff;
        GameValues.Base4 = GameValues.Base3 + diff;



       

        for (int r = 1; r<= levelInfoList[level_num - 1].floor_count; r++)
        {
            for(int c=0; c<5; c++)
            {
                current_block = ObjectPooler.GetInstance().SpawnFromPool("block", new Vector3(c, r * height, 0), Quaternion.identity);

                current_block_script = current_block.GetComponent<BlockController>();
                current_block_script.AssignValue(Random.Range(GameValues.Base2, GameValues.MaxBlockValue));
                activeBlocks.Add(current_block);
                CheckForDroppable(current_block);
            }

            GameObject floor = ObjectPooler.GetInstance().SpawnFromPool("floor", new Vector3(2, r * height, 0), Quaternion.identity);
            activeFloors.Add(floor);
        }

        container = Instantiate(containerPrefab);
        container.GetComponent<ContainerController>().health_value = levelInfoList[GameValues.CurrentLevel - 1].container_health_value;
        car = container.transform.GetChild(3);

        house = Instantiate(housePrefab);
        house.transform.position = new Vector3(2, levelInfoList[level_num - 1].floor_count * height, 18f);

        navmesh.BuildNavMesh();



    }

    public void SpawnEnemies()
    {

        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = ObjectPooler.GetInstance().SpawnFromPool("enemy", new Vector3(2, (levelInfoList[GameValues.CurrentLevel - 1].floor_count * height) + 0.2f, 18), Quaternion.identity);

            enemy.GetComponent<EnemyController>().AssignTarget(player);
            activeEnemies.Add(enemy);
        }
    }

    void CheckForDroppable(GameObject block)
    {
        if(Random.Range(0, 10) > 8)
        {
            current_block.GetComponent<BlockController>().SetDroppable((Droppable)Random.Range(0, System.Enum.GetValues(typeof(Droppable)).Length));
            current_block_script.AssignValue(GameValues.MaxBlockValue);
        }
    }

    public void SpawnDroppableItem(Droppable drop, Vector3 position)
    {
        activeDroppables.Add(Instantiate(droppable_list[(int)drop], position, Quaternion.identity));
    }

    public void ResetLevel()
    {
        player.transform.parent = null;
        player.rotation = Quaternion.identity;

        foreach(GameObject obj in activeBlocks)
        {
            ObjectPooler.GetInstance().PutToPool("block", obj);
        }
        activeBlocks.Clear();
        

        foreach (GameObject obj in activeFloors)
        {
            ObjectPooler.GetInstance().PutToPool("floor", obj);
        }
        activeFloors.Clear();


        foreach (GameObject obj in activeEnemies)
        {
            ObjectPooler.GetInstance().PutToPool("enemy", obj);
        }
        activeEnemies.Clear();


        foreach (GameObject obj in activeScorePops)
        {
            Destroy(obj);
        }
        activeScorePops.Clear();


        foreach (GameObject obj in activeDroppables)
        {
            Destroy(obj);
        }
        activeDroppables.Clear();


        Destroy(container);
        Destroy(house);
    }

    public void EnemiesCelebrate()
    {
        foreach(GameObject enemy in activeEnemies)
        {
            enemy.GetComponent<EnemyController>().Celebrate(new Vector3(2, levelInfoList[GameValues.CurrentLevel - 1].floor_count * height, 18));
        }
    }

    public void ScorePop()
    {
        activeScorePops.Add(Instantiate(scorePopPrefab, cam.WorldToScreenPoint(player.position+Vector3.up), Quaternion.identity, transform));
    }
}

