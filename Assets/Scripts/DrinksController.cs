using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrinksController : MonoBehaviour
{
    public int coffee;
    public int tea;
    public int sugar;
    public int condensed;
    public int evaporated;
    public int water;
    public string drinkText;
    public string preparedDrinkText;
    public string wantedDrinkText;
    public GameObject stirrer;
    public GameObject preparedHotCup;
    public GameObject preparedColdCup;
    public GameObject uncle;
    public GameObject uncleAngry;
    public GameObject uncleEyes;
    
    private void Awake() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("drink-stall")) Init();
    }

    private void refreshDrink()
    {
        drinkText = "";
        coffee = 0;
        tea = 0;
        sugar = 0;
        condensed = 0;
        evaporated = 0;
        water = 0;
    }
    void Init()
    {
        wantedDrinkText = "Teh-O";
        stirrer = GameObject.Find("Stirrer");
        preparedHotCup = GameObject.Find("Prepared Hot Cup");
        preparedHotCup.SetActive(false);
        preparedColdCup = GameObject.Find("Prepared Cold Cup");
        preparedColdCup.SetActive(false);
        uncle = GameObject.Find("Uncle");
        uncleAngry = GameObject.Find("Uncle Angry");
        uncleAngry.SetActive(false);
        uncleEyes = GameObject.Find("Uncle Eyes");
        uncleEyes.SetActive(false);
        refreshDrink();
        preparedDrinkText = "";
        Toolbox.Instance.Sfx.PlaySound("hawker_0", 0.1f);
    }

    public void Stir()
    {
        if (water < 1)
        {
            Toolbox.Instance.Get<GameController>().Print("Add some water!");
            return;
        }
        else if (coffee < 1 && tea < 1)
        {
            Toolbox.Instance.Get<GameController>().Print("Add some coffee or tea!");
            return;
        }
        else if (coffee >= 1 && tea >= 1) drinkText = drinkText + "Yuan Yang";
        else if (coffee >= 1) drinkText = drinkText + "Kopi";
        else if (tea >= 1) drinkText = drinkText + "Teh";
        
        if (evaporated >= 1) drinkText = drinkText + "-C";
        //else if (condensed >= 1) ;
        else if (condensed == 0 && evaporated == 0) drinkText = drinkText + "-O";

        if (sugar == 0) drinkText = drinkText + " Kosong";
        else if (sugar == 1) drinkText = drinkText + " Kurang Manis";
        //else if (sugar == 2) ;
        else if (sugar == 3) drinkText = drinkText + " Tambah Manis";

        Toolbox.Instance.Get<GameController>().Print("Stirring: " + drinkText);
        preparedDrinkText = drinkText;
        refreshDrink();

        var seq = LeanTween.sequence();
        seq.append(LeanTween.rotateY(stirrer, 180.0f, 0.1f).setLoopPingPong(5));
        seq.append(LeanTween.moveLocalY(stirrer, 1.0f, 0.1f).setLoopPingPong(5));
        seq.append(LeanTween.moveLocalX(stirrer, -3.0f, 0.1f).setLoopPingPong(5));
    }

    public void HotCup()
    {
        if(preparedDrinkText.Length == 0)
        {
            Toolbox.Instance.Get<GameController>().Print("Mix a drink first!");
            return;
        }
        Toolbox.Instance.Get<GameController>().Print("Served: " + preparedDrinkText);

        preparedHotCup.SetActive(true);
        //Do liquid filling sequence here

        checkDrink();
    }

    public void ColdCup()
    {
        if(preparedDrinkText.Length == 0)
        {
            Toolbox.Instance.Get<GameController>().Print("Mix a drink first!");
            return;
        }
        preparedDrinkText = preparedDrinkText + " Peng";
        Toolbox.Instance.Get<GameController>().Print("Served: " + preparedDrinkText);
        

        preparedColdCup.SetActive(true);
        //Do liquid filling sequence here

        checkDrink();
    }

    private void checkDrink()
    {
        if(string.Equals(preparedDrinkText, wantedDrinkText))
        {
            unclePositive();
        }
        else
        {
            uncleNegative();
        }
        preparedDrinkText = "";
    }

    public void unclePositive()
    {
        Toolbox.Instance.Get<GameController>().Print("Uncle is content.");
    }

    public void uncleNegative()
    {
        Toolbox.Instance.Get<GameController>().Print("Uncle is pissed!");
        uncle.SetActive(false);
        uncleAngry.SetActive(true);
        uncleEyes.SetActive(true);

        var seq = LeanTween.sequence();
        seq.append(LeanTween.moveLocalY(uncleAngry, 0.1f, 0.05f).setLoopPingPong(10));
        seq.append(LeanTween.moveLocalX(uncleAngry, -0.2f, 0.05f).setLoopPingPong(10));

        var seq2 = LeanTween.sequence();
        seq2.append(LeanTween.moveLocalY(uncleEyes, 0.1f, 0.05f).setLoopPingPong(10));
        seq2.append(LeanTween.moveLocalX(uncleEyes, -0.2f, 0.05f).setLoopPingPong(10));
    }
}
