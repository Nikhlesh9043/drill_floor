using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{// Initializations
    private float deltaNx, deltaNy; // travel in Normalized PLane (Normalized PLane is the touch screen with 0,0 at the center.)
    private float startNx, startNy, currentNx, currentNy; // co ords in Normalized Plane
    private float centerSx, centerSy; // center of touchScreen

    private Vector3 playerLocalPosition = Vector3.right * 2;
    private Vector3 previousLocalTravel = Vector3.right * 2;
    private float balancerUnit;

    [SerializeField] Transform player_reference;
    [SerializeField] Animator Player_animator;
    [SerializeField] Animator Hammer_animator;


    [SerializeField] GameObject Hammer_graphic;
    [SerializeField] GameObject Hammer_body_graphic;

    void Start()
    {
        centerSx = Screen.width / 2;
        centerSy = Screen.height / 2;
        balancerUnit = (float)20 / Screen.width;

    }

    void Update()
    {
        if (!GameValues.Win) player_reference.position = new Vector3(2, transform.position.y, 0);
        else player_reference.position = LevelCreation.GetInstance().car.GetChild(0).position;


        if (!GameValues.GameOver)
        {
            //Touch Controls
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        previousLocalTravel = transform.position;
                        playerLocalPosition = transform.position;
                        startNx = touch.position.x - centerSx;
                        break;

                    case TouchPhase.Moved:
                        currentNx = touch.position.x - centerSx;
                        currentNy = touch.position.y - centerSy;

                        deltaNx = (((currentNx - startNx) * balancerUnit) + previousLocalTravel.x);

                        // Constraints
                        if (deltaNx < -0.2f)
                        {
                            deltaNx = -0.2f;
                        }
                        else if (deltaNx > 4.2f)
                        {
                            deltaNx = 4.2f;
                        }
                        if (deltaNy < -2)
                        {
                            deltaNy = -2;
                        }
                        else if (deltaNy > 8)
                        {
                            deltaNy = 8;
                        }

                        playerLocalPosition = new Vector3(deltaNx, 0, 0);
                        break;

                    case TouchPhase.Ended:
                        break;
                }

                transform.position = new Vector3(playerLocalPosition.x, transform.position.y, transform.position.z);
            }
        }

    }

    public void PlayDrillAnimation()
    {
        Player_animator.Play("drill");
        Hammer_animator.Play("hammering");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !GameValues.GameOver)
        {
            Gameplay.GetInstance().GameOver(false);
            Player_animator.Play("lifted");
            Hammer_animator.Play("ideal");
            LevelCreation.GetInstance().EnemiesCelebrate();

            other.transform.GetChild(0).GetComponent<Animator>().Play("lift");
            transform.parent = other.transform;
            transform.localPosition = Vector3.up * -0.4f+ Vector3.forward * 0.3f;
            transform.localRotation = Quaternion.Euler(0,90,0);

            Hammer_body_graphic.SetActive(false);
            Hammer_graphic.SetActive(false);
        }
    }

    public void StopDrillAnimation()
    {
       Player_animator.Play("drill");
       Hammer_animator.Play("ideal");
    }

    public void ResetPlayer()
    {
        transform.position = new Vector3(2, (LevelCreation.GetInstance().levelInfoList[GameValues.CurrentLevel-1].floor_count * 5)+1, 0);
        player_reference.position = transform.position;
        GetComponent<CapsuleCollider>().isTrigger = false;

        Hammer_body_graphic.SetActive(true);
        Hammer_graphic.SetActive(true);
        StopDrillAnimation();

        playerLocalPosition = Vector3.right * 2;
        previousLocalTravel = Vector3.right * 2;

    }


}
