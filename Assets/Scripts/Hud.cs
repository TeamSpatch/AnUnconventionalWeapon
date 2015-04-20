using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hud : MonoBehaviour
{
    public Texture2D border;
    public Texture2D molehill;
    public Texture2D molehillIcon;
    public Texture2D clay;
    public Texture2D clayIcon;
    public Texture2D golf;
    public Texture2D golfIcon;
    public Texture2D bush;
    public Texture2D bushIcon;

    float scaleFactor;
    float scoreX;
    float deathX;
    float bigHeight;
    float smallHeight;
    float borderSize;
    float iconSize;
    bool hasFlashed;
    Gardener gardener;

    void Start()
    {
        scaleFactor = GameObject.Find("HUD").GetComponent<Canvas>().scaleFactor;
        scoreX = (1920f - 50f - 14f) * scaleFactor;
        deathX = (50f + 14f) * scaleFactor;
        bigHeight = 44f * scaleFactor;
        smallHeight = 32f * scaleFactor;
        borderSize = 3f * scaleFactor;
        iconSize = 100f * scaleFactor;
        hasFlashed = false;
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
            GUI.DrawTexture(new Rect(scoreX - bushLength / 2f - iconSize / 2f, Screen.height - 59f * scaleFactor - iconSize / 4f * 3f, iconSize, iconSize), bushIcon);
        }
        float golfLength = gardener.golfPower * gardener.golfCount / (gardener.tileSize * gardener.tileSize) * scaleFactor;
        if (golfLength > 0) {
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength, Screen.height - smallHeight, golfLength, smallHeight), golf);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength, Screen.height - smallHeight - borderSize, golfLength, borderSize), border);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength - borderSize, Screen.height - smallHeight - borderSize, borderSize, smallHeight + borderSize), border);
            GUI.DrawTexture(new Rect(scoreX - bushLength - golfLength / 2f - iconSize / 2f, Screen.height - 59f * scaleFactor - iconSize / 4f * 3f, iconSize, iconSize), golfIcon);
        }
    }

    void DoBad()
    {
        float clayLength = gardener.clayPower * gardener.clayCount / (gardener.tileSize * gardener.tileSize) * scaleFactor;
        if (clayLength > 0) {
            GUI.DrawTexture(new Rect(deathX, Screen.height - smallHeight, clayLength, smallHeight), clay);
            GUI.DrawTexture(new Rect(deathX, Screen.height - smallHeight - borderSize, clayLength, borderSize), border);
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - smallHeight - borderSize, borderSize, smallHeight + borderSize), border);
            GUI.DrawTexture(new Rect(deathX + 25f * scaleFactor, Screen.height - 59f * scaleFactor - iconSize / 4f * 3f, iconSize, iconSize), clayIcon);
        }
        float molehillLength = gardener.molehillPower * gardener.molehillCount * scaleFactor;
        if (molehillLength > 0) {
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - smallHeight, molehillLength, smallHeight), molehill);
            GUI.DrawTexture(new Rect(deathX + clayLength, Screen.height - smallHeight - borderSize, molehillLength, borderSize), border);
            GUI.DrawTexture(new Rect(deathX + clayLength + molehillLength, Screen.height - smallHeight - borderSize, borderSize, smallHeight + borderSize), border);
            GUI.DrawTexture(new Rect(deathX + 125f * scaleFactor, Screen.height - 59f * scaleFactor - iconSize / 4f * 3f, iconSize, iconSize), molehillIcon);
        }
        float bad = (clayLength + molehillLength) / scaleFactor;
        if (bad >= 360f) {
            if (hasFlashed == false) {
                hasFlashed = true;
                StartCoroutine(DoFlash());
            }
        } else {
            hasFlashed = false;
        }
    }

    IEnumerator DoFlash()
    {
        Image flash = GameObject.Find("Flash").transform.Find("Image").GetComponent<Image>();
        Color color = Color.white;
        color.a = 0.75f;
        flash.color = color;
        yield return new WaitForSeconds(0.03f);
        color.a = color.a / 2f;
        flash.color = color;
        yield return new WaitForSeconds(0.03f);
        color.a = 0f;
        flash.color = color;
        yield return new WaitForSeconds(0.03f);
        color = Color.red;
        for (int i = 0; i < 10; i++) {
            color.a = 1f - i * 0.1f;
            flash.color = color;
            yield return new WaitForSeconds(0.03f);
        }
        color.a = 0f;
        flash.color = color;
    }
}
