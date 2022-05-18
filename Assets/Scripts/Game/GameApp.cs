using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using protobuf;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameApp : MonoBehaviour
{
    private Queue<string> m_msgQueue;
    private void Awake()
    {
        m_msgQueue = new Queue<string>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // 注册消息回调
        ClientNet.instance.RegistRecvMsgCb((msg) =>
        {
            // 把消息缓存到队列中，注意不要在这里直接操作UI对象
            m_msgQueue.Enqueue(msg);
        });

        // 连接服务端
        ClientNet.instance.Connect("127.0.0.1", 3563, (ok) =>
        {
            Debug.Log("连接服务器, ok: " + ok);
        });

        // sendBtn.onClick.AddListener(SendMsg);
        LoginRequest req = new LoginRequest();
        req.Uid = "u2";
        SendMsg("LoginRequest", req);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_msgQueue.Count > 0)
        {
            // 从消息队列中取消息，并更新到聊天文本中
            Debug.Log(m_msgQueue.Dequeue());
        }

        // 按回车键，发送消息
        // if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        // {
        //     // Debug.Log(chatText.text);
        //     SendMsg();
        // }
    }
    private void SendMsg(string request = "",LoginRequest obj = null)
    {
        if (request != "" && ClientNet.instance.IsConnected())
        {
            var content = PackJsonByReq(JsonUtility.ToJson(obj),request);
            // string content = PackJsonByReq(ObjectToJSON(obj),request);
            var len = content.Length;
            
            // 把字符串转成字节流
            byte[] data = new byte[2];
            data[0] = (byte)(len >> 8);
            data[1] = (byte)len;
            byte[] data2= System.Text.Encoding.UTF8.GetBytes(content);
            // 发送给服务端
            ClientNet.instance.SendData(Combine(data,data2));
        }
        else
        {
            Debug.LogError("你还没连接服务器");
        }
    }

    public static string PackJsonByReq(string js, string req)
    {
        return "{\"" + req + "\": "+js+"}";
    }

    /// <summary>
    /// 将数组a,b进行合并
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    private static byte[] Combine(byte[] a, byte[] b)
    {
        byte[] c = new byte[a.Length + b.Length];

        Array.Copy(a, 0, c, 0, a.Length);
        Array.Copy(b, 0, c, a.Length, b.Length);

        return c;
    }
    // public void EnterGame()
    // {
    //     this.StartCoroutine(this.EnterGameScene());
    // }

    // public IEnumerator EnterGameScene()
    // {
    //     Debug.Log("StartGame");
    //
    //     WWWForm form = new WWWForm();
    //     form.AddField("name","root");
    //     form.AddField("pwd", "123456");
    //
    //     Uri uri = new Uri("http://127.0.0.1:3563");
    //     UnityWebRequest req = UnityWebRequest.Post(uri,form);
    //     // UnityWebRequest req = UnityWebRequest.Get("http://127.0.0.1:3563?u=root");
    //     yield return req.SendWebRequest();
    //     if(req.isHttpError || req.isNetworkError)
    //     {
    //         Debug.LogError(req.error);
    //     }
    //     else
    //     {
    //         string receiveContent = req.downloadHandler.text;
    //         Debug.Log(receiveContent);
    //     }
    //     Debug.Log(req.downloadHandler.text);
    // }
    private void OnDestroy() {
        ClientNet.instance.CloseSocket();
    }
}
