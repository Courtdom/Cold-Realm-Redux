using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;




public class HealthHeartsUI : MonoBehaviour
{

   // public static HealthHeartsUI Instance { get; private set; }

   


   // public GameObject Player;
   // private Player player;


    private int health;
    private int numOfHearts = 5;
    private int numOfHeartsInHearts;
    private int healthMaximum = 125;
    private int shardAmount;
    private int comboCount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite heart1;
    public Sprite heart2;
    public Sprite heart3;
    public Sprite emptyHeart;

    public Image magicPotionIcon;
    public Sprite potionEmpty;
    public Sprite potionBlue;
    public Sprite potionRed;
    public Sprite potionGreen;

    public Image shardIcon;
    public Sprite shard;
    public Text shardText;
    
    

    private void Start()
    {
        // shardIcon.sprite = shard;
        // player = FindObjectOfType<Player>();
        DoChecks();
    }

    private void Update()
    {
        DoChecks();

        UpdateHearts();
        UpdateMagicPotionIcon();
        UpdateShardAmount();

    }

   private void DoChecks()
    {
        health = Player.instance.currentHealth;
        comboCount = Player.instance.playerCombat.comboCount;
        shardAmount = Player.instance.experiencePoints;
    }


    public void UpdateHearts()
    {
        if (health >= 105)
        {
            Set4HeartFull();

            if (health >= 105 && health <= 109)
            {
                hearts[4].sprite = emptyHeart;
            }
            else if (health >= 110 && health <= 114)
            {
                hearts[4].sprite = heart3;
            }
            else if (health >= 115 && health <= 119)
            {
                hearts[4].sprite = heart2;

            }
            else if (health >= 120 && health <= 124)
            {
                hearts[4].sprite = heart1;

            }
            else if (health >= 124 && health <= 150)
            {
                hearts[4].sprite = fullHeart;

            }
        }
        else if (health >= 80)
        {
            Set3HeartFull();

            if (health >= 80 && health <= 84)
            {
                hearts[3].sprite = emptyHeart;
            }
            else if (health >= 85 && health <= 89)
            {
                hearts[3].sprite = heart3;

            }
            else if (health >= 90 && health <= 94)
            {
                hearts[3].sprite = heart2;

            }
            else if (health >= 95 && health <= 99)
            {
                hearts[3].sprite = heart1;

            }
            else if (health >= 100 && health <= 104)
            {
                hearts[3].sprite = fullHeart;

            }
        }
        else if (health >= 55) // 3 full hearts
        {
            Set2HeartFull();

           if (health >= 55 && health <= 59)
            {
                hearts[2].sprite = emptyHeart;
            }
            else if (health >= 60 && health <= 64)
            {
                hearts[2].sprite = heart3;

            }
            else if (health >= 65 && health <= 69)
            {
                hearts[2].sprite = heart2;

            }
            else if (health >= 70 && health <= 74)
            {
                hearts[2].sprite = heart1;

            }
            else if (health >= 75 && health <= 79)
            {
                hearts[2].sprite = fullHeart;

            }
        }

        else if (health >= 30) // 1 full heart
        {
            Set1HeartFull();

          if (health >= 30 && health <= 34)
            {
                hearts[1].sprite = emptyHeart;
            }
            else if (health >= 35 && health <= 39)
            {
                hearts[1].sprite = heart3;

            }
            else if (health >= 40 && health <= 44)
            {
                hearts[1].sprite = heart2;

            }
            else if (health >= 45 && health <= 49)
            {
                hearts[1].sprite = heart1;

            }
            else if (health >= 50 && health <= 54)
         
             {
                hearts[1].sprite = fullHeart;

             }
        }
        else if (health >= 0) // 0 full hearts
        {
            Set0HeartsFull();

            if (health >= 0 && health <= 9)
            {
                hearts[0].sprite = emptyHeart;
            }
            else if (health >= 10 && health <= 14)
            {
                hearts[0].sprite = heart3;

            }
            else if (health >= 15 && health <= 19)
            {
                hearts[0].sprite = heart2;

            }
            else if (health >= 20 && health <= 24)
            {
                hearts[0].sprite = heart1;

            }
            else if (health >= 25 && health <= 29)
            {
                hearts[0].sprite = fullHeart;

            }
        }
    }

    public void UpdateMagicPotionIcon()
    {
        if(comboCount > 6)
        {
            magicPotionIcon.sprite = potionGreen;
        }
        else if(comboCount > 4)
        {
            magicPotionIcon.sprite = potionRed;
        }
        else if (comboCount > 2)
        {
            magicPotionIcon.sprite = potionBlue;
        }
        else
        {
            magicPotionIcon.sprite = potionEmpty;
        }
    }


    public void UpdateShardAmount()
    {
        shardIcon.sprite = shard;
        shardText.text = shardAmount.ToString();
    }


    public void Set0HeartsFull()
    {
        hearts[1].sprite = emptyHeart;
        hearts[2].sprite = emptyHeart;
        hearts[3].sprite = emptyHeart;
        hearts[4].sprite = emptyHeart;
    }

    public void Set1HeartFull()
    {
        hearts[0].sprite = fullHeart;

        hearts[2].sprite = emptyHeart;
        hearts[3].sprite = emptyHeart;
        hearts[4].sprite = emptyHeart;
    }

    public void Set2HeartFull()
    {
        hearts[0].sprite = fullHeart;
        hearts[1].sprite = fullHeart;

        hearts[3].sprite = emptyHeart;
        hearts[4].sprite = emptyHeart;
    }

    public void Set3HeartFull()
    {
        hearts[0].sprite = fullHeart;
        hearts[1].sprite = fullHeart;
        hearts[2].sprite = fullHeart;

        hearts[4].sprite = emptyHeart;

    }

    public void Set4HeartFull()
    {
        hearts[0].sprite = fullHeart;
        hearts[1].sprite = fullHeart;
        hearts[2].sprite = fullHeart;
        hearts[3].sprite = fullHeart;


    }

}
