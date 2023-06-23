using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;
using UnityEngine.UI;

namespace FrameTest
{
    public class Player : MonoBehaviour
    {
        #region 基本属性

        private int basicStrength = 10; //健壮度

        public int BasicStrength
        {
            get { return basicStrength; }
            set { basicStrength = value; }
        }

        private int basicIntellect = 10; //智力

        public int BasicIntellect
        {
            get { return basicIntellect; }
            set { basicIntellect = value; }
        }

        private int basicAgility = 10; //敏捷

        public int BasicAgility
        {
            get { return basicAgility; }
            set { basicAgility = value; }
        }

        private int basicStamina = 10; //耐力

        public int BasicStamina
        {
            get { return basicStamina; }
            set { basicStamina = value; }
        }

        private int basicDamage = 10; //伤害

        public int BasicDamage
        {
            get { return basicDamage; }
            set { basicDamage = value; }
        }

        #endregion

        private Text coinText;

        private int coinAmount = 100;

        public int CoinAmount
        {
            get { return coinAmount; }
            set
            {
                coinAmount = value;
                coinText.text = coinAmount.ToString();
            }
        }


        void Start()
        {
            //coinText = GameObject.Find("Coin").GetComponentInChildren<Text>();
            //coinText.text = coinAmount.ToString();

            //开启输入检测
            InputMgr.Instance.StartOrEndCheck(true);
            EventCenter.Instance.AddEventListener<object>("某键按下", CheckInputDown);
            //EventCenter.Instance.AddEventListener<object>("某键抬起", CheckInputUp);
        }

        void Update()
        {
        }

        private bool isShowPanel = false;

        /// <summary>
        /// 检测按键按下
        /// </summary>
        /// <param name="key">传入的keyObj</param>
        private void CheckInputDown(object key)
        {
            KeyCode code = (KeyCode)key;
            switch (code)
            {
                case KeyCode.Tab:
                    if (isShowPanel == false)
                    {
                        UIMgr.Instance.GetPanel<InventoryPanel>("InventoryPanel").ShowMe();
                        isShowPanel = true;
                    }
                    else
                    {
                        UIMgr.Instance.GetPanel<InventoryPanel>("InventoryPanel").HideMe();
                        isShowPanel = false;
                    }

                    break;
                case KeyCode.G:
                    int id = Random.Range(1, 18);
                    //Debug.Log(id);
                    Knapsack.Instance.StoreItem(id);
                    break;
                case KeyCode.T:
                    Knapsack.Instance.DisplaySwitch();
                    break;
                case KeyCode.Y:
                    Chest.Instance.DisplaySwitch();
                    break;
                case KeyCode.U:
                    CharacterPanel.Instance.DisplaySwitch();
                    break;
                case KeyCode.P:
                    Vendor.Instance.DisplaySwitch();
                    break;


                case KeyCode.LeftShift:
                    //PoolMgr.Instance.Clear();
                    //MusicMgr.Instance.PlaySound("ChangeScene", false);
                    //if (SceneManager.GetActiveScene().name =="StartScene")
                    //{
                    //    SceneMgr.Instance.LoadSceneAsy("GameScene", () =>
                    //    {
                    //        MusicMgr.Instance.PlayBGMusic("Journey");
                    //        MusicMgr.Instance.ChangeBGMusicValue(0.05f);
                    //    });
                    //}
                    //if (SceneManager.GetActiveScene().name == "GameScene")
                    //{
                    //    SceneMgr.Instance.LoadSceneAsy("StartScene", () =>
                    //    {

                    //    });
                    //}
                    break;
            }
        }

        /// <summary>
        /// 消费金币
        /// </summary>
        /// <param name="amount">消费数量</param>
        /// <returns></returns>
        public bool ConsumeCoin(int amount)
        {
            if (coinAmount >= amount) //当持有的金钱数量大于需要消费的数量时
            {
                coinAmount -= amount;
                coinText.text = coinAmount.ToString();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 赚取金币
        /// </summary>
        /// <param name="amount">赚取数量</param>
        public void EarnCoin(int amount)
        {
            this.coinAmount += amount;
            coinText.text = coinAmount.ToString();
        }
    }
}