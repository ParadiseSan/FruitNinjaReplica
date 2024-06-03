using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    Spawnner spawnner;
    Blade blade;
    [SerializeField]Text ScoreText;
    [SerializeField] Image fadeImage;
    int score = 0;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawnner = FindObjectOfType<Spawnner>();
    }
    private void Start()
    {
        NewGame();
    }
     
    void NewGame()
    {
        score = 0;
        ScoreText.text = score.ToString();
        Time.timeScale = 1;
        blade.enabled = true;
        spawnner.enabled = true;

        ClearGame();
    }
    void ClearGame()
    {
        FruitSlice[] fruits = FindObjectsOfType<FruitSlice>();

        foreach (FruitSlice fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }

    }
    public void IncreaseScore()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    public void Explosion()
    {
         blade.enabled = false;
        spawnner.enabled = false;

        StartCoroutine(ExplosionSeq());
    }
    IEnumerator ExplosionSeq()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration) 
        { 
            float t = Mathf.Clamp01(elapsed / duration);

            Time.timeScale = 1f - t;
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);
            elapsed += Time.unscaledDeltaTime;
            //Debug.Log("Elapsed "+elapsed);
            //Debug.Log("T = "+t);
        
        yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);

            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);
            elapsed += Time.unscaledDeltaTime;
            

            yield return null;
        }

      
    }
}
