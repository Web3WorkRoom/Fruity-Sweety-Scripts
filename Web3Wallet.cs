using System;
using System.Threading.Tasks;
using UnityEngine;
using Nethereum.Util;
using TMPro;

public class Web3Wallet
{
#if UNITY_ANDROID
    private static string url = "https://metamask.app.link/dapp/web3workroom.com/game-web3wallet/";
    //private static string url = "https://web3workroom.com/game-web3wallet/";

#elif UNITY_IPHONE
    private static string url = "https://web3workroom.com/game-web3wallet/";
#endif
    //private static string url = "https://metamask.app.link/dapp/web3workroom.com/game-web3wallet/";
    //private static string url = "https://brave.app.link/dapp/web3workroom.com/game-web3wallet/";

    //#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
    //       // private static string url = "https://metamask.app.link/dapp/chainsafe.github.io/game-web3wallet/";
    //       private static string url = "https://chainsafe.github.io/game-web3wallet/";
    //#else
    //    private static string url = "https://chainsafe.github.io/game-web3wallet/";
    //    #endif

    public static async Task<string> SendTransaction(string _chainId, string _to, string _value, string _data = "", string _gasLimit = "", string _gasPrice = "")
    {
        //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    url = "https://metamask.app.link/dapp/web3workroom.com/game-web3wallet/";
        //}
        //else
        //{
        //    url = "https://web3workroom.com/game-web3wallet/";
        //}
        //url = "https://web3workroom.com/game-web3wallet/";

        // open application
        Application.OpenURL(url + "?action=send" + "&chainId=" + _chainId + "&to=" + _to + "&value=" + _value + "&data=" + _data + "&gasLimit=" + _gasLimit + "&gasPrice=" + _gasPrice);
        // set clipboard to empty
        GUIUtility.systemCopyBuffer = "";
        // wait for clipboard response
        string clipBoard = "";
        while (clipBoard == "")
        {
            clipBoard = GUIUtility.systemCopyBuffer;
            await Task.Delay(100);
        }
        // check if clipboard response is valid
        if (clipBoard.StartsWith("0x") && clipBoard.Length == 66)
        {
            return clipBoard;
        }
        else
        {
            throw new Exception("transaction error");
        }
    }

    public static async Task<string> Sign(string _message, string clipBoard, TMP_InputField inf)
    {
        // open application
        //string message = Uri.EscapeDataString(_message);
        //Application.OpenURL(url + "?action=sign" + "&message=" + message);
        // set clipboard to empty
        GUIUtility.systemCopyBuffer = "";
        // wait for clipboard response
        //string clipBoard = "";
        while (clipBoard == "")
        {
            //    clipBoard = GUIUtility.systemCopyBuffer;
            clipBoard = inf.text;
            await Task.Delay(100);
        }
        // check if clipboard response is valid
        if (clipBoard.StartsWith("0x") && clipBoard.Length == 132)
        {
            return clipBoard;
        }
        else
        {
            throw new Exception("sign error");
        }
    }

    public static async Task<string> Sign2(string _message)
    {
        // open application
        string message = Uri.EscapeDataString(_message);
        Application.OpenURL(url + "?action=sign" + "&message=" + message);
        // set clipboard to empty
        GUIUtility.systemCopyBuffer = "";
        // wait for clipboard response
        string clipBoard = "";
        while (clipBoard == "")
        {
            clipBoard = GUIUtility.systemCopyBuffer;
            //clipBoard = inf.text;
            await Task.Delay(100);
        }
        // check if clipboard response is valid
        if (clipBoard.StartsWith("0x") && clipBoard.Length == 132)
        {
            return clipBoard;
        }
        else
        {
            throw new Exception("sign error");
        }
    }

    public static string Sha3(string _message)
    {
        Sha3Keccack hasher = new Sha3Keccack();
        string hash = hasher.CalculateHash(_message);
        // 0x1c8aff950685c2ed4bc3174f3472287b56d9517b9c948127319a09a7a36deac8
        return "0x" + hash;
    }
}
