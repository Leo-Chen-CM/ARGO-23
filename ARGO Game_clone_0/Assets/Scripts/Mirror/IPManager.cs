using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;
using UnityEditor;

namespace Mirror.Examples.Basic
{
    public class IPManager : MonoBehaviour
    {
        public TextMeshProUGUI myIPDisplayText;

        // Start is called before the first frame update
        void Start()
        {
            string ip = "My IP: " + GetIP();
            myIPDisplayText.text = ip;
        }
        
        public void changeIP()
        {
            TMP_InputField _InputField = this.GetComponent<TMP_InputField>();
            FindObjectOfType<NewNetworkRoomManager>().networkAddress = _InputField.text;
        }

        public void connect()
        {
            System.Uri finalIP = new System.Uri("kcp://" + FindObjectOfType<NewNetworkRoomManager>().networkAddress + ":7777");
            NetworkManager.singleton.StartClient(finalIP);

        }

        string GetIP()
        {
            IPHostEntry host;

            string localIP = "0.0.0.0";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress IP in host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = IP.ToString();
                    break;
                }
            }


            return localIP;
        }

    }
}