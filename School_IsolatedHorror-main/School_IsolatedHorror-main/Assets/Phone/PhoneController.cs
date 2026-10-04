using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PhoneController : MonoBehaviour
{
    public GameObject notificationPrefab; // prefab
    public Transform notificationParent; // the notification transform

    public GameObject messagePrefabFrom; // prefab
    public GameObject messagePrefabTo; // prefab
    public Transform messageParentMother; // the notification transform
    public Transform messageParentUnknown; // the notification transform

    private Message[] messages;
    private int curMessage = -1;

    private int motherMSG = 0;
    private int unknownMSG = 0;
    private float motherMSGF = 0;
    private float unknownMSGF = 0;

    private Notification[] notifications;
    private int curNotification = -1;

    private List<ActiveNotification> activeNotifications = new List<ActiveNotification>();

    public GameObject[] pageGameObjecs = {};

    public AudioSource audioSource;

    public static PhoneController PS;

    private void Awake()
    {
        PS = this;
        Message[] _messages = Resources.LoadAll("Messages").Cast<Message>().ToArray();
        notifications = Resources.LoadAll("Notifications").Cast<Notification>().ToArray(); // empty...

        // sort messages, cuz unty sucks
        messages = new Message[_messages.Length];
        foreach (Message msg in _messages)
        {
            int nr = Convert.ToInt32(msg.name);
            messages[nr - 1] = msg;
        }

        //NextMessage();
        //NextNotification();
    }

    public enum pages
    {
        HomePage,
        Settings,
        Messeges,
        MotherMessages,
        UnknownMessages,
    }

    private pages[] pageBack = { pages.HomePage, pages.HomePage, pages.HomePage, pages.Messeges, pages.Messeges };
    private int curPage = 0;

    private bool justpressed = false;

    private void CheckController()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightInputDeviceCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightInputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            InputDevice inp = devices[0];

            inp.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 touchpad);
            inp.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool touched);

            if (!touched)
                justpressed = false;

            if (touched && !justpressed)
            {
                justpressed = true;

                if (touchpad.x < -0.8)
                {
                    curPage = (int)pageBack[curPage];
                    ActivateCurPage();
                }
            }
        }
    }

    private void ActivateCurPage()
    {
        foreach (GameObject g in pageGameObjecs)
        {
            g.SetActive(false);
        }

        pageGameObjecs[curPage].SetActive(true);
    }

    public void SetActivePage(int page)
    {
        curPage = page;
        ActivateCurPage();
    }

    public void Start()
    {
        ActivateCurPage();
    }

    private float k = 0;

    public void Update()
    {
        CheckController();

        for (int i = activeNotifications.Count-1; i >= 0; i--)
        {
            ActiveNotification activeNotification = activeNotifications[i];

            activeNotification.time -= Time.deltaTime;
            if (activeNotification.time <= 0)
            {
                Destroy(activeNotification.GO);
                activeNotifications.Remove(activeNotification);
            }
        }

        int activeCount = 0;
        // re position notifications
        foreach (ActiveNotification activeNotification in activeNotifications)
        {
            activeNotification.GO.transform.localPosition = new Vector3(0f, 60f-10f*activeCount, 0f);
            activeCount++;
        }
    }

    public void NextNotification()
    {
        curNotification++;
        
        if (!(curNotification < notifications.Length))
            return;
        
        // activate
        GameObject GO = Instantiate(notificationPrefab, notificationParent);
        ActiveNotification AN = new ActiveNotification(notifications[curNotification], 6f, GO);
        activeNotifications.Add(AN); // it is there for 10 seconds
    }

    public void NextMessage()
    {
        curMessage++;

        Debug.Log(curMessage);
        Debug.Log(messages.Length);
        if (!(curMessage < messages.Length))
            return;

        Message m = messages[curMessage];

        GameObject GO = null;
        switch (m.sender)
        {
            case 0: // from mother
                messageParentMother.transform.localPosition += new Vector3(0, 25, 0);
                GO = Instantiate(messagePrefabFrom, messageParentMother);
                GO.transform.localPosition -= new Vector3(0, 25 * motherMSG, 0);
                motherMSG += 1;

                GameObject GONotificationM = Instantiate(notificationPrefab, notificationParent);
                ActiveNotification ANM = new ActiveNotification("Message From Mother", 4f, GONotificationM);
                activeNotifications.Add(ANM);
                break;
            case 1: // from unknown
                messageParentUnknown.transform.localPosition += new Vector3(0, 25, 0);
                GO = Instantiate(messagePrefabFrom, messageParentUnknown);
                GO.transform.localPosition -= new Vector3(0, 25 * unknownMSG, 0);
                unknownMSG += 1;

                GameObject GONotificationU = Instantiate(notificationPrefab, notificationParent);
                ActiveNotification ANU = new ActiveNotification("Message From ??????", 4f, GONotificationU);
                activeNotifications.Add(ANU);
                break;
            case 2: // to mother
                messageParentMother.transform.localPosition += new Vector3(0, 25, 0);
                GO = Instantiate(messagePrefabTo, messageParentMother);
                GO.transform.localPosition -= new Vector3(0, 25 * motherMSG, 0);
                motherMSG += 1;
                break;
            case 3: // to unknown
                messageParentUnknown.transform.localPosition += new Vector3(0, 25, 0);
                GO = Instantiate(messagePrefabTo, messageParentUnknown);
                GO.transform.localPosition -= new Vector3(0, 25 * unknownMSG, 0);
                unknownMSG += 1;
                break;
        }

        GO.GetComponentInChildren<TMP_Text>().text = m.text;

        if (m.sender <= 1)
        {
            audioSource.Play();
            // get message, play sound
        }
    }

    public void PickUp()
    {
        curPage = 0;
        gameObject.SetActive(true);
    }

    public void PlaceBack()
    {
        gameObject.SetActive(false);
    }
}

public class ActiveNotification
{
    public Notification notification;
    public GameObject GO;
    public float time;

    public ActiveNotification(Notification notification, float time, GameObject go)
    {
        this.notification = notification;
        this.time = time;
        GO = go;

        TMP_Text t = GO.GetComponentInChildren<TMP_Text>();
        t.text = notification.text;
    }
    public ActiveNotification(string notificationtxt, float time, GameObject go)
    {
        this.notification = new Notification();
        this.time = time;
        GO = go;

        TMP_Text t = GO.GetComponentInChildren<TMP_Text>();
        t.text = notificationtxt;
    }
}