using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour {

    public int amtOfMoney = 0;

    private UIController uiController;

	void Start () {
        uiController = GameObject.Find("Main UI").GetComponent<UIController>();
        if (uiController.moneyText.text == "")
        {
            uiController.Money(amtOfMoney);
        }
	}

    public void Increase (int amt)
    {
        amtOfMoney += amt;
        uiController.Money(amtOfMoney);
    }

    public void Decrease(int amt)
    {
        amtOfMoney -= amt;
        uiController.Money(amtOfMoney);
    }
}
