using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]private NoticePanel noticePanel;

    public NoticePanel NoticePanel { get => noticePanel; set => noticePanel = value; }
}
