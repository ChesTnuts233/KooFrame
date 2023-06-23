using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    #region Data
    public Item ItemObj { get; private set; }
    public int Amount { get; private set; }      //槽内物品数量
    #endregion

    #region UI Component
    private Image itemImage;
    private Text amountText;

    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }
    #endregion

    private float targetScale = 1f;

    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);

    private float smoothing = 4;

    void Update()
    {
        if (transform.localScale.x != targetScale)
        {
            //变化大小动画
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < .02f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }
    /// <summary>
    /// 设置物品
    /// </summary>
    /// <param name="item">物品</param>
    /// <param name="amount">初始数量</param>
    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
        this.ItemObj = item;
        this.Amount = amount;
        // update ui 
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (ItemObj.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        //update ui 
        if (ItemObj.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
    public void ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;
        //update ui 
        if (ItemObj.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        //update ui 
        if (ItemObj.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
            AmountText.text = "";
    }

    //当前物品 跟 另一个物品 交换显示
    public void Exchange(ItemUI itemUI)
    {
        Item itemTemp = itemUI.ItemObj;
        int amountTemp = itemUI.Amount;
        itemUI.SetItem(this.ItemObj, this.Amount);
        this.SetItem(itemTemp, amountTemp);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }


}
