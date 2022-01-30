using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticePanel : MonoBehaviour
{
    [SerializeField]private Transform ScrollViewContent;
    [SerializeField]private Notice NoticePrefab;


    public Notice CreateNotice()
    {
        Notice addedNotice = Instantiate(NoticePrefab, ScrollViewContent);
        addedNotice.gameObject.SetActive(false);

        return addedNotice;
    }

    public void RemoveNotice(Notice notice)
    {
        Destroy(notice);
    }
}
