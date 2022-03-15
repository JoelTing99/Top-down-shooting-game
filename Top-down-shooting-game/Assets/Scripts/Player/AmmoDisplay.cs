using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject Bar;
    private Player player;
    private GameManager GameManager;
    private List<GameObject> Barlist = new List<GameObject>();
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        player.OnShoot += Player_OnShoot;
        SetAmmoNumber(GameManager.GetPlayerBulletCount());
    }
    private void Player_OnShoot(object sender, System.EventArgs e)
    {
        SetAmmoNumber(player.GetBulletCount());
    }
    private void SetAmmoNumber(int bulletCount)
    {
        if(Barlist != null)
        {
            foreach (var bar in Barlist)
            {
                Destroy(bar);
            }
        }
        int MaxBulletNumber = GameManager.GetPlayerBulletCount();
        Vector2 BarPos = new Vector2(0f, 6.5f);
        Vector2 BarSize = new Vector2(190 / MaxBulletNumber, 7.5f);
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bar = Instantiate(Bar, transform.Find("AmmoBar").transform);
            Barlist.Add(bar);
            BarPos.x = (-100 - (float)100 / MaxBulletNumber) + (i + 1) * (float)200 / MaxBulletNumber;
            bar.GetComponent<Image>().rectTransform.localPosition = BarPos;
            bar.GetComponent<Image>().rectTransform.sizeDelta = BarSize;
        }
    }
}
