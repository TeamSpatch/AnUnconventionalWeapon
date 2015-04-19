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
    float scaleFactor;
    float bigHeight;
    float smallHeight;
    float borderSize;
    Gardener gardener;

    void Start()
    {
        scaleFactor = GameObject.Find("HUD").GetComponent<Canvas>().scaleFactor;
        bigHeight = 44f * scaleFactor;
        smallHeight = 32f * scaleFactor;
        borderSize = 3f * scaleFactor;
        scoreX = (1920f - 50f - 14f) * scaleFactor;
        deathX = (50f + 14f) * scaleFactor;
        gardener = GameObject.Find("Garden").GetComponent<Gardener>();
    }

    void OnGUI()
    {
        DoGood();
        DoBad();
    }

    void DoGood()
    {
        float bushLength = gardener.bushPower * gardener.bushCount / (gardener.tileSize * gardener.tileSize) * scaleFactor;
        if (bushLength > 0) {
            GUI.DrawTexture(new Rect(scoreX - bushLength, Screen.height - bigHeight, bushLength, bigHeight), bush);
            GUI.DrawTexture(new Rect(scoreX - bushLength, Screen.height - bigHeight - borderSize, bushLength, borderSize), border);
            GUI.DrawTexture(new Rect(scoreX - bushLength - borderSize, Screen.height - bigHeight - borderSize, borderSize, bigHeight + borderSize), border);
        }
        float golfLength = gardener.golfPower * gardener.golfCount / (gardener.tileSize * gardener.tileSize) * scaleFactor;
        if (golfLength > 0) {
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength, Screen.height - smallHeight, golfLength, smallHeight), golf);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength, Screen.height - smallHeight - borderSize, golfLength, borderSize), border);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength - borderSize, Screen.height - smallHeight - borderSize, borderSize, smallHeight + borderSize), border);
        }
    }

    void DoBad()
    {
        float clayLength = gardener.clayPower * gardener.clayCount / (gardener.tileSize * gardener.tileSize) * scaleFactor;
        if (clayLength > 0) {
            GUI.DrawTexture(new Rect(deathX, Screen.height - smallHeight, clayLength, smallHeight), clay);
            GUI.DrawTexture(new Rect(deathX, Screen.height - smallHeight - borderSize, clayLength, borderSize), border);
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - smallHeight - borderSize, borderSize, smallHeight + borderSize), border);
        }
        float molehillLength = gardener.molehillPower * gardener.molehillCount * scaleFactor;
        if (molehillLength > 0) {
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - smallHeight, molehillLength, smallHeight), molehill);
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - smallHeight - borderSize, molehillLength, borderSize), border);
            GUI.DrawTexture(new Rect(deathX + clayLength + molehillLength, Screen.height - smallHeight - borderSize, borderSize, smallHeight + borderSize), border);
        }
    }
}
