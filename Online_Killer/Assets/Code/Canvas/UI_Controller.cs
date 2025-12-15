using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public Image Bala_1, Bala_2, Bala_3, Bala_4, Bala_5, Bala_6;
    public Sprite Bala_Full, BalaEmpty;
    private Shoot _SReference;
    // Start is called before the first frame update
    void Start()
    {
        _SReference = GameObject.Find("Player").GetComponent<Shoot>();
    }
    public void UpdateBulletDisplay()
    {
        switch (_SReference.bullet)
        {
            case 6:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = Bala_Full;
                Bala_3.sprite = Bala_Full;
                Bala_4.sprite = Bala_Full;
                Bala_5.sprite = Bala_Full;
                Bala_6.sprite = Bala_Full;
                break;
            case 5:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = Bala_Full;
                Bala_3.sprite = Bala_Full;
                Bala_4.sprite = Bala_Full;
                Bala_5.sprite = Bala_Full;
                Bala_6.sprite = BalaEmpty;
                break;
            case 4:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = Bala_Full;
                Bala_3.sprite = Bala_Full;
                Bala_4.sprite = Bala_Full;
                Bala_5.sprite = BalaEmpty;
                Bala_6.sprite = BalaEmpty;
                break;
            case 3:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = Bala_Full;
                Bala_3.sprite = Bala_Full;
                Bala_4.sprite = BalaEmpty;
                Bala_5.sprite = BalaEmpty;
                Bala_6.sprite = BalaEmpty;
                break;
            case 2:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = Bala_Full;
                Bala_3.sprite = BalaEmpty;
                Bala_4.sprite = BalaEmpty;
                Bala_5.sprite = BalaEmpty;
                Bala_6.sprite = BalaEmpty;
                break;
            case 1:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = BalaEmpty;
                Bala_3.sprite = BalaEmpty;
                Bala_4.sprite = BalaEmpty;
                Bala_5.sprite = BalaEmpty;
                Bala_6.sprite = BalaEmpty;
                break;
            case 0:
                Bala_1.sprite = BalaEmpty;
                Bala_2.sprite = BalaEmpty;
                Bala_3.sprite = BalaEmpty;
                Bala_4.sprite = BalaEmpty;
                Bala_5.sprite = BalaEmpty;
                Bala_6.sprite = BalaEmpty;
                break;
            default:
                Bala_1.sprite = Bala_Full;
                Bala_2.sprite = Bala_Full;
                Bala_3.sprite = Bala_Full;
                Bala_4.sprite = Bala_Full;
                Bala_5.sprite = Bala_Full;
                Bala_6.sprite = Bala_Full;
                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
