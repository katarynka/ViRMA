﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class ViRMA_APIController : MonoBehaviour
{

    // cell
    // xAxis={"AxisType":"Hierarchy","TagsetId":0,"HierarchyNodeId":7}
    // yAxis={"AxisType":"Tagset","TagsetId":7,"HierarchyNodeId":0}
    // filters=[{"type":"tag","tagId":20},{"type":"tag","tagId":22}

    public class ViRMAParamHandler
    {
        public string Api { get; set; }
        public int Id { get; set; }
        public string Axis { get; set; }
        public int TagsetId { get; set; }
        public int HierarchyNodeId { get; set; }
    }


    public static IEnumerator CallAPI(string apiCall, Action<JSONNode> returnResponse)
    {
        string baseURL = "https://localhost:5001/api/";
        string getRequest = baseURL + apiCall;

        Debug.Log(getRequest);

        UnityWebRequest request = UnityWebRequest.Get(getRequest);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            yield break;
        }

        JSONNode response = JSON.Parse(request.downloadHandler.text);
        yield return null;
        returnResponse(response);
    }

    public static IEnumerator PeterAPI(int Id)
    {
        string baseURL = "https://localhost:5001/api/";
        string tagURL = baseURL + "tag/" + Id.ToString();
        print("Getting tag...");

        UnityWebRequest GetTagRequest = UnityWebRequest.Get(tagURL);
        yield return GetTagRequest.SendWebRequest();

        if (GetTagRequest.isNetworkError || GetTagRequest.isHttpError)
        {
            Debug.LogError(GetTagRequest.error);
            yield break;
        }

        JSONNode tag = JSON.Parse(GetTagRequest.downloadHandler.text);

        int tagId = tag["Id"];
        string tagName = tag["Name"];
        int tagsetId = tag["TagsetId"];
        print("Id: " + tagId.ToString() + " Name: " + tagName.ToString() + " tagsetId: " + tagsetId);
    }

    static void GenericPOST()
    {
        var url = "https://reqbin.com/echo/post/json";

        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        httpRequest.Method = "POST";

        httpRequest.Accept = "application/json";
        httpRequest.ContentType = "application/json";

        var data = @"{
          ""Id"": 78912,
          ""Customer"": ""Jason Sweet"",
          ""Quantity"": 1,
          ""Price"": 18.00
        }";

        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        {
            streamWriter.Write(data);
        }

        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }

        Debug.Log(httpResponse.StatusCode);
    }

}