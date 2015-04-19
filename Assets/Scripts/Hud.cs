using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hud : MonoBehaviour
{
    public Texture2D border;
    public Texture2D molehill;
    public Texture2D clay;
    public Texture2D golf;
    public Texture2D bush;

    float scoreX;
    float deathX;
    Gardener gardener;

    void Start()
    {
        scoreX = Screen.width / 2f + 448f;
        deathX = Screen.width / 2f - 448f;
        gardener = GameObject.Find("Garden").GetComponent<Gardener>();
    }

    void OnGUI()
    {
        DoGood();
        DoBad();
    }

    void DoGood()
    {
        float bushLength = gardener.bushPower * gardener.bushCount / (gardener.tileSize * gardener.tileSize);
        if (bushLength > 0) {
            GUI.DrawTexture(new Rect(scoreX - bushLength, Screen.height - 22f, bushLength, 22f), bush);
            GUI.DrawTexture(new Rect(scoreX - bushLength, Screen.height - 22.5f, bushLength, 1.5f), border);
            GUI.DrawTexture(new Rect(scoreX - bushLength - 1.5f, Screen.height - 22.5f, 1.5f, 22.5f), border);
        }
        float golfLength = gardener.golfPower * gardener.golfCount / (gardener.tileSize * gardener.tileSize);
        if (golfLength > 0) {
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength, Screen.height - 16f, golfLength, 16f), golf);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength, Screen.height - 17.5f, golfLength, 1.5f), border);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength - 1.5f, Screen.height - 17.5f, 1.5f, 17.5f), border);
        }
    }

    void DoBad()
    {
        float clayLength = gardener.clayPower * gardener.clayCount / (gardener.tileSize * gardener.tileSize);
        if (clayLength > 0) {
            GUI.DrawTexture(new Rect(deathX, Screen.height - 17f, clayLength, 17f), clay);
            GUI.DrawTexture(new Rect(deathX, Screen.height - 18.5f, clayLength, 1.5f), border);
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - 18.5f, 1.5f, 18.5f), border);
        }
        float molehillLength = gardener.molehillPower * gardener.molehillCount;
        if (molehillLength > 0) {
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - 17f, molehillLength, 17f), molehill);
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - 18.5f, molehillLength, 1.5f), border);
            GUI.DrawTexture(new Rect(deathX + clayLength + molehillLength, Screen.height - 18.5f, 1.5f, 18.5f), border);
        }
    }
}
