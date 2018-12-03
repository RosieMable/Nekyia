using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManagerUI : MonoBehaviour {

    [SerializeField]
    private Image[] hearts;

    [SerializeField]
    private Sprite fullHeart;

    [SerializeField]
    private Sprite HalfHeart;

    [SerializeField]
    private Sprite emptyHeart;

    [SerializeField]
    private FloatValue heartContainers;

    public FloatValue PlayerCurrentHealth;


	// Use this for initialization
	public void Start () {
        InitHearts();
		
	}
	

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue -1; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = PlayerCurrentHealth.RuntimeValue / 2;

        for (int i = 0; i < heartContainers.RuntimeValue -1; i++)
        {
            if (i <= tempHealth -1)
            {
                //Full heart
                hearts[i].sprite = fullHeart;
            }
            else if(i >= tempHealth)
            {
                //Empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //Half Heart
                hearts[i].sprite = HalfHeart;
            }
        }
    }
}

