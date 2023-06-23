using System.Collections;
using System.Collections.Generic;
using KooFrame;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class ToolTipPanel : BasePanel
{


    private Text toolTipText;               //��ʾ�ı�
    private Text contentText;               //�����ı�
    private CanvasGroup canvasGroup;        //��������� 
    private float targetAlpha = 0;          //Ŀ��Alphaֵ ������ʾ��͸����


    [Header("��ʾ�ٶ�")] public float smoothing = 1;             //�����ٶ�


    // Start is called before the first frame update
    private void Start()
    {
        toolTipText = GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    public void Show(string text)
    {
        toolTipText.text = text;
        targetAlpha = 1;
    }

    public void Hide()
    {
        targetAlpha = 0;
    }
    /// <summary>
    /// ����tooltip������
    /// </summary>
    /// <param name="position"></param>
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;

        //Debug.Log("��ȡλ��"+ position);
    }
}