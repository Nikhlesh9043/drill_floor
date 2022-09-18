using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameValues
{
    private static int currentScore, coinCount, currentLevel, currentDisplayLevel;
    private static int hitScore, minBlockValue, maxBlockValue, bonusValue;
    private static bool gameOver = true;
    private static bool win = false;

    private static bool survivalMode = false;
    private static int bulletGap = 50;
    private static int fireRate = 1;
    private static float bonusPosition;
    private static int base1, base2, base3, base4 = 0;


    public static int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
        }
    }

    public static int CoinCount
    {
        get
        {
            return coinCount;
        }
        set
        {
            coinCount = value;
        }
    }

    public static int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }
    public static int CurrentDisplayLevel
    {
        get
        {
            return currentDisplayLevel;
        }
        set
        {
            currentDisplayLevel = value;
            if (value > 20)
            {
                currentLevel = (value % 10) + 10;
            }
            else currentLevel = value;
        }
    }

    public static int HitScore
    {
        get
        {
            return hitScore;
        }
        set
        {
            hitScore = value;
        }
    }

    public static int BulletGap
    {
        get
        {
            return bulletGap;
        }
        set
        {
            bulletGap = value;
        }
    }

    public static int FireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }

    public static int MaxBlockValue
    {
        get
        {
            return maxBlockValue;
        }
        set
        {
            maxBlockValue = value;
        }
    }

    public static int MinBlockValue
    {
        get
        {
            return minBlockValue;
        }
        set
        {
            minBlockValue = value;
        }
    }

    public static int BonusValue
    {
        get
        {
            return bonusValue;
        }
        set
        {
            bonusValue = value;
        }
    }

    public static float BonusPosition
    {
        get
        {
            return bonusPosition;
        }
        set
        {
            bonusPosition = value;
        }
    }

    public static bool GameOver
    {
        get
        {
            return gameOver;
        }
        set
        {
            gameOver = value;
        }
    }

    public static bool Win
    {
        get
        {
            return win;
        }
        set
        {
            win = value;
        }
    }

    public static bool SurvivalMode
    {
        get
        {
            return survivalMode;
        }
        set
        {
            survivalMode = value;
        }
    }

    public static int Base1
    {
        get
        {
            return base1;
        }
        set
        {
            base1 = value;
        }
    }

    public static int Base2
    {
        get
        {
            return base2;
        }
        set
        {
            base2 = value;
        }
    }

    public static int Base3
    {
        get
        {
            return base3;
        }
        set
        {
            base3 = value;
        }
    }

    public static int Base4
    {
        get
        {
            return base4;
        }
        set
        {
            base4 = value;
        }
    }
}
