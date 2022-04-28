using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Textpopup : MonoBehaviour
{
    public Color TextColor;
    private TextMeshPro TextMesh;
    private float DisappearTimer;

    public static Textpopup Create(Vector3 position, int Amount, Color color)
    {
        Transform TextpopupTransform = Instantiate(FindObjectOfType<GameManager>().GetTextPopup().transform, position, Quaternion.Euler(70, 0, 0));
        Textpopup textpopup = TextpopupTransform.GetComponent<Textpopup>();
        TextpopupTransform.GetComponent<TextMeshPro>().color = color;
        textpopup.Setup(Amount);
        textpopup.TextColor = color;
        return textpopup;
    }
    public static Textpopup Create(Vector3 position, string Text, Color color)
    {
        Transform TextpopupTransform = Instantiate(FindObjectOfType<GameManager>().GetTextPopup().transform, position, Quaternion.Euler(70, 0, 0));
        Textpopup textpopup = TextpopupTransform.GetComponent<Textpopup>();
        TextpopupTransform.GetComponent<TextMeshPro>().color = color;
        textpopup.Setup(Text);
        textpopup.TextColor = color;
        return textpopup;
    }
    public static void SetFontSize(Textpopup textpopup, int size)
    {
        textpopup.SetFontSize(size);
    }
    
    private void Awake()
    {
        TextMesh = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        
        transform.position += new Vector3(0, 2 * Time.deltaTime, 2 * Time.deltaTime);
        DisappearTimer -= Time.deltaTime;
        if(DisappearTimer < 0)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            TextColor.a -= 3 * Time.deltaTime;
            TextMesh.color = TextColor;
            if(TextMesh.color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetFontSize(int size)
    {
        TextMesh.fontSize = size;
    }
    public void Setup(int amount)
    {
        TextMesh.SetText(amount.ToString());
        DisappearTimer = 0.5f;
    }
    public void Setup(string text)
    {
        TextMesh.SetText(text);
        DisappearTimer = 0.5f;
    }
}
