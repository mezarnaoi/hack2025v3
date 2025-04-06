using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject priceGrid;
    public Text priceText;

    public Item itemActual;
    public List<Item> itemList;
    public int indexItem;

    public SpriteRenderer playerSpriteRenderer;
    public Button buyButton;
    public Text buyText;

    private void Start()
    {
        UpdateAndGetDataInfo();
        UpdateItemExibition();
    }

    public Item ItemPurchasedAndEquiped(int index)
    {
        return itemList[index];
    }

    public void OnClickNextItemButton()
    {
        indexItem++;
        if (indexItem > itemList.Count - 1)
            indexItem = 0;
        UpdateItemExibition();
    }

    public void OnClickBackItemButton()
    {
        indexItem--;
        if (indexItem < 0)
            indexItem = itemList.Count - 1;
        UpdateItemExibition();
    }

    private void UpdateItemExibition()
    {
        playerSpriteRenderer.sprite = itemList[indexItem].image;
        priceText.text = itemList[indexItem].price.ToString();
        itemActual = itemList[indexItem];
        priceGrid.SetActive(true);

        if (itemActual.equiped)
        {
            buyButton.interactable = false;
            priceGrid.SetActive(false);
            buyText.text = "";
        }
        else if(!itemActual.equiped && itemActual.purchased)
        {
            buyButton.interactable = true;
            priceGrid.SetActive(false);
            buyText.text = "EQUIP";
        }
        else if(!itemActual.equiped && !itemActual.purchased && MenuManager.instance.playerData.coins >= itemActual.price)
        {
            buyButton.interactable = true;
            buyText.text = "BUY";
        }
        else
        {
            buyButton.interactable = false;
            buyText.text = "BUY";
        }
    }

    private void UpdateAndGetDataInfo()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < MenuManager.instance.playerData.idPurchasedItems.Count; j++)
            {
                if (itemList[i].id == MenuManager.instance.playerData.idPurchasedItems[j])
                    itemList[i].purchased = true;

                if (itemList[i].id == MenuManager.instance.playerData.idEquipedItem)
                    itemList[i].equiped = true;
                else
                    itemList[i].equiped = false;
            }
        }

        itemActual = ItemPurchasedAndEquiped(MenuManager.instance.playerData.idEquipedItem);
        indexItem = MenuManager.instance.playerData.idEquipedItem;
    }

    public void DesequipItems()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].equiped = false;
        }
    }

    public void OnClickBuyOrEquipItemButton()
    {
        if (!itemActual.equiped && !itemActual.purchased && MenuManager.instance.playerData.coins >= itemActual.price)
        {
            MenuManager.instance.playerData.coins -= itemActual.price;
            MenuManager.instance.playerData.idEquipedItem = itemActual.id;
            MenuManager.instance.playerData.idPurchasedItems.Add(itemActual.id);
            DesequipItems();
            itemActual.equiped = true;
            itemActual.purchased = true;
            DatabaseManager.Save("player_data", MenuManager.instance.playerData);
            UpdateItemExibition();
        }
        else if(!itemActual.equiped && itemActual.purchased)
        {
            MenuManager.instance.playerData.idEquipedItem = itemActual.id;
            DesequipItems();
            itemActual.equiped = true;
            DatabaseManager.Save("player_data", MenuManager.instance.playerData);
            UpdateItemExibition();
        }
        MenuManager.instance.UpdateUITextInfo();
    }

    public void OnClickExitShopButton()
    {
        UpdateAndGetDataInfo();
        UpdateItemExibition();
    }
}
