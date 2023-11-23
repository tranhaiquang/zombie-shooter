using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    private float hp;
    private float maxHp = 10;
    private float lerpTimer = 100f;
    public float chipSpeed = 2f;
    public Image frontHP;
    public Image backHP;
    private GameManager gameManager;
    void Start()
    {
        hp = maxHp;
        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
            gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
        
        updateHPUI();
        if (Input.GetKeyDown(KeyCode.P))
            hp -= 10;

        if (hp <= 0)
            gameManager.EndGame();
    }

    public void updateHPUI()
    {
        float fillFrontHP = frontHP.fillAmount;
        float fillBackHP = backHP.fillAmount;
        float hFraction = hp / maxHp;

        if (fillBackHP > hFraction)
        {
            frontHP.fillAmount = hFraction;
            backHP.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHP.fillAmount = Mathf.Lerp(fillBackHP, hFraction, percentComplete);
        }

    }

    public void takeDamage(float dmg)
    {
        hp -= dmg;
        lerpTimer = 0f;
    }
}
