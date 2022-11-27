using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class VisualChatManager : MonoBehaviour
{
    public GameObject UserArea, BotArea, DateArea;
    public RectTransform ContentRect;
    public Scrollbar scrollBar;
    SetArea LastArea;


    // flag: 0 - back, 1 - bot, 2 - user, 3 - user
    public void VisualizeChat(int flag, string text, string user, Texture2D picture)
    {
        if (text.Trim() == "") return;
        bool isBottom = scrollBar.value <= 0.00001f;

        // print(text);

        // 1. make chat area and put text
        SetArea Area = Instantiate((flag == 2 || flag == 3) ? UserArea : BotArea).GetComponent<SetArea>();
        Area.transform.SetParent(ContentRect.transform, false);
        Area.BoxRect.sizeDelta = new Vector2(600, Area.BoxRect.sizeDelta.y);
        Area.TextRect.GetComponent<Text>().text = text;
        Fit(Area.BoxRect);

        // 2. shrink the area properly
        float X = Area.TextRect.sizeDelta.x + 42;
        float Y = Area.TextRect.sizeDelta.y;
        if (Y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                Area.BoxRect.sizeDelta = new Vector2(X - i * 2, Area.BoxRect.sizeDelta.y);
                Fit(Area.BoxRect);

                if (Y != Area.TextRect.sizeDelta.y) { Area.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y); break; }
            }
        }
        else Area.BoxRect.sizeDelta = new Vector2(X, Y);

        // 3. get now time
        DateTime t = DateTime.Now;
        Area.Time = t.ToString("yyyy-MM-dd-HH-mm");
        Area.User = user;

        // 4. put time text
        int hour = t.Hour;
        if (t.Hour == 0) hour = 12;
        else if (t.Hour > 12) hour -= 12;
        Area.TimeText.text = (t.Hour > 12 ? "오후 " : "오전 ") + hour + ":" + t.Minute.ToString("D2");

        // 5. delte time text, tail only if msg from same person at same datetime
        bool isSame = LastArea != null && LastArea.Time == Area.Time && LastArea.User == Area.User;
        Area.Tail.SetActive(!isSame);
        if (isSame) LastArea.TimeText.text = "";

        // 6. if bot is same, then delte profile img and name
        if (flag == 1)
        {
            Area.UserImage.gameObject.SetActive(!isSame);
            Area.UserText.gameObject.SetActive(!isSame);
            Area.UserText.text = Area.User;
            if (picture != null) Area.UserImage.sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f));
        }

        // 7. make date change area
        if (LastArea != null && LastArea.Time.Substring(0, 10) != Area.Time.Substring(0, 10))
        {
            Transform CurDateArea = Instantiate(DateArea).transform;
            CurDateArea.SetParent(ContentRect.transform, false);
            CurDateArea.SetSiblingIndex(CurDateArea.GetSiblingIndex() - 1);
            string week = "";
            switch (t.DayOfWeek)
            {
                case DayOfWeek.Sunday: week = "일"; break;
                case DayOfWeek.Monday: week = "월"; break;
                case DayOfWeek.Tuesday: week = "화"; break;
                case DayOfWeek.Wednesday: week = "수"; break;
                case DayOfWeek.Thursday: week = "목"; break;
                case DayOfWeek.Friday: week = "금"; break;
                case DayOfWeek.Saturday: week = "토"; break;
            }
            CurDateArea.GetComponent<SetArea>().DateText.text = t.Year + "년 " + t.Month + "월 " + t.Day + "일 " + week + "요일";
        }

        // 8. Fit the Areas and update Last Area
        Fit(Area.BoxRect);
        Fit(Area.AreaRect);
        Fit(ContentRect);
        LastArea = Area;

        // 9. if receive in upper area, then do not go down
        if ((flag == 1) && !isBottom) return;
        Invoke("ScrollDelay", 0.03f);
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    void ScrollDelay() => scrollBar.value = 0;
}
