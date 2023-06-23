using KooFrame;
using UnityEngine;
using UnityEngine.UI;

public class VideoPanel : BasePanel
{
    public Sprite[] VideoStop;
    public Image PauseButtonImage;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        Slider slider = GetControl<Slider>("TVSlider");
        slider.minValue = 0;
        slider.maxValue = (float)VideoCtrl.videoPlayer.clip.length;
        slider.onValueChanged.AddListener((value) =>
        {
            VideoCtrl.videoPlayer.time = value;
        });
    }
    private void Update()
    {
        GetControl<Slider>("TVSlider").SetValueWithoutNotify((float)VideoCtrl.videoPlayer.time);
    }


    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitInfo()
    {

    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Pause":
                if (VideoCtrl.isStop == false)
                {
                    VideoCtrl.videoPlayer.Pause();
                    VideoCtrl.isStop = true;
                    PauseButtonImage.sprite = VideoStop[1];
                }
                else
                {
                    VideoCtrl.videoPlayer.Play();
                    VideoCtrl.isStop = false;
                    PauseButtonImage.sprite = VideoStop[0];
                }
                break;
            default:
                break;
        }
    }


    public override void ShowMe()   //显示面板后要做的事情
    {

    }
    public override void HideMe()
    {

    }

}
