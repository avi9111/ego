using UnityEngine;
using System.Collections;
using BestHTTP;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Text;

public partial class TionManager : MonoBehaviour {

	const string TAI_HTTP_HEADER = "f88a78a83463f1e03bbf0acb3883aefb";
	
	
	public delegate void OnWWWRequestComplete(Hashtable res);
    public delegate void OnWWWRequestFail(int errorCode,string exception = null,string stackTrace = null);
    public delegate void OnWWWRequestFailWithBan(int errorCode, Hashtable res = null, string exception = null, string stackTrace = null);
	IEnumerator BestHttpRequest(string URL, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null,OnWWWRequestFailWithBan banCallback = null)
	{
		Logger.Warn("[RCD] Start WWW request [URL]:" + URL);
		string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
		Logger.Warn("[RCD] Start WWW request IPv6 [URL]:" + URL2IPv6);

		HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6));
		req.AddHeader ("TYX-Request-Id", TAI_HTTP_HEADER);
		req.Send();
		
		yield return StartCoroutine(req);
		Debug.Log("fdajsldfjaldjfoiasjdfoiasjdf");
		Debug.Log(failCallback);
        BestHttpResponse(req, completeCallback, failCallback, banCallback);
	}

    IEnumerator BestHttpRequestForQuick(string URL, Hashtable ht, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null, OnWWWRequestFailWithBan banCallback = null)
	{
		Logger.Warn("[RCD] Start WWW request [URL]:" + URL);
		string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
		Logger.Warn("[RCD] Start WWW request IPv6 [URL]:" + URL2IPv6);

		HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6), HTTPMethods.Post, OnRequestFinished);
		req.AddHeader ("TYX-Request-Id", TAI_HTTP_HEADER);
		System.Collections.IDictionaryEnumerator item = ht.GetEnumerator();
		while(item.MoveNext()) 
		{
			string key = System.Convert.ToString(item.Key);
			string value = System.Convert.ToString(item.Value);
			req.AddField (key, value);
		}

		req.Send();
		
		yield return StartCoroutine(req);
		
		BestHttpResponse (req, completeCallback, failCallback,banCallback);
	}

	public IEnumerator BestHttpRequestForChat(string URL, Hashtable ht, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
	{
		Logger.Warn("[RCD] Start WWW request [URL]:" + URL);
		string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
		Logger.Warn("[RCD] Start WWW request IPv6 [URL]:" + URL2IPv6);

		HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6), HTTPMethods.Post, OnRequestFinished);
		System.Collections.IDictionaryEnumerator item = ht.GetEnumerator();
		while (item.MoveNext())
		{
			string key = System.Convert.ToString(item.Key);
			string value = System.Convert.ToString(item.Value);
			req.AddField(key, value);
		}

		req.ConnectTimeout = TimeSpan.FromSeconds(5);

		req.Send();

		yield return StartCoroutine(req);

		BestHttpResponse(req, completeCallback, failCallback);
	}

	public IEnumerator BestHttpRequestForNetworkError(string URL, Hashtable ht, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
	{
		Logger.Warn("[RCD] Start WWW request [URL]:" + URL);
		string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
		Logger.Warn("[RCD] Start WWW request IPv6 [URL]:" + URL2IPv6);

		HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6), HTTPMethods.Post, OnRequestFinished);


		string result = TaiJson.jsonEncode(ht);

		req.RawData = Encoding.UTF8.GetBytes(result);

		req.AddHeader("TYX-Request-Id", TAI_HTTP_HEADER); 

		req.Send();

		yield return StartCoroutine(req);

		BestHttpResponse(req, completeCallback, failCallback);
	}

	public IEnumerator BestHttpRequestForHDC(string URL, Hashtable ht , OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
	{
		Logger.Warn("[RCD] Start WWW request [URL]:" + URL);
		string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
		Logger.Warn("[RCD] Start WWW request IPv6 [URL]:" + URL2IPv6);
		
		HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6), HTTPMethods.Post, OnRequestFinished);
		req.AddHeader ("gameId", CONST_COMMON.HDC_GAMEID.ToString());
		
		string json = TaiJson.jsonEncode(ht);
		json = "[" + json + "]";
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		builder.Append("HDC").Append(json).Append(CONST_COMMON.HDC_APPKEY);

		
		string md5Str = Utils.Md5(builder.ToString());
		req.AddHeader("sign", md5Str);
//		Debug.LogError("json" + json);
//		Debug.LogError("builder === " + builder.ToString());
//		Debug.LogError("sign === " + md5Str);

		req.RawData = Encoding.UTF8.GetBytes(json);

		req.AddHeader("token", "c931f5b2e648acff3aede58a8645c713"); // for test

		req.Send();
		
		yield return StartCoroutine(req);
		
		BestHttpResponse (req, completeCallback, failCallback);

		if (ht.Contains("eventName"))
		{
			var eventName = ht["eventName"] as string;
			Logger.Debug("HDC: " + (eventName != null ? eventName : "") + " Send");
		}
	}

    public IEnumerator BestHttpRequestForAuthError(string URL, Hashtable ht, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
    {
        Logger.Warn("[RCD] Start WWW request [URL]:" + URL);
        string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
        Logger.Warn("[RCD] Start WWW request IPv6 [URL]:" + URL2IPv6);

        HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6), HTTPMethods.Post, OnRequestFinished);

        System.Collections.IDictionaryEnumerator item = ht.GetEnumerator();
        while (item.MoveNext())
        {
            string key = System.Convert.ToString(item.Key);
            string value = System.Convert.ToString(item.Value);
            req.AddField(key, value);
        }

        req.AddHeader("TYX-Request-Id", TAI_HTTP_HEADER);

        req.Send();

        yield return StartCoroutine(req);

        BestHttpResponse(req, completeCallback, failCallback);
    }

	void OnRequestFinished(HTTPRequest originalRequest, HTTPResponse response)
	{
		Logger.Debug ("OnRequestFinished");
	}

	private void BestHttpResponse(HTTPRequest httpRequest, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null,OnWWWRequestFailWithBan banCallback = null)
	{
		Logger.Warn ("[RCD] get http response");
		Debug.LogError("resp state=" + httpRequest.State + " isSuccess=" + httpRequest.Response.IsSuccess);
		HTTPRequest req = httpRequest;
		switch (req.State)
		{
			// The request finished without any problem.
		case HTTPRequestStates.Finished:
			HTTPResponse resp = req.Response;
			if (resp.IsSuccess)
			{
				try
				{
					Logger.Warn("[RCD] Decoding " + resp.DataAsText);
					Hashtable table = (Hashtable)TaiJson.jsonDecode(resp.DataAsText);
					
					if (string.Compare((string)table["result"], "ok") == 0)
					{
						Debug.Log("ok");
						//success
						if (completeCallback != null)
							completeCallback(table);
					}
					else if (string.Compare((string)table["result"], "retry") == 0)
					{
						Debug.Log("ok= fail");
						//retry is also a success
						if (completeCallback != null)
							completeCallback(table);
					}
					else
					{
						Debug.Log("ok= band");
						//TODO by RCD, this is an expected error from server, use error code instead of 1
						int errorCode = System.Convert.ToInt32(table["forclient"]);
						
						Logger.Warn("[RCD] result no " + errorCode.ToString());
						if(errorCode == 206 && banCallback != null)
                        {
                            banCallback(errorCode, table, "Finished IsSuccess True", "BestHttpResponse");
                        }
                        else if (failCallback != null)
                        {
							Debug.Log("fail call back");
                            failCallback(errorCode, "Finished IsSuccess True", "BestHttpResponse");
                        }
					}
					
				}
				catch (System.Exception ex)
				{
					Debug.Log("ok error =" + resp.DataAsText);
					Logger.Error("[RCD] Decode Error" + resp.DataAsText);
					PlanXNet.Output.LogException(ex);
					
					//success but cannot parse to hashtable
                    if (failCallback != null)
                    {
                        if (ex != null)
                        {
                            failCallback(CONST_NET.ERRORCODE_HTTP_NOTHASHTABLE, ex.Message, ex.StackTrace);
                        }
                        else
                        {
                            failCallback(CONST_NET.ERRORCODE_HTTP_NOTHASHTABLE, "Finished exception null", "BestHttpResponse");
                        }
                    }
				}
			}
			else
			{
				Debug.LogError(string.Format("[RCD] Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
				                           resp.StatusCode,
				                           resp.Message,
				                           resp.DataAsText));
				Logger.Error(string.Format("[RCD] Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
				                           resp.StatusCode,
				                           resp.Message,
				                           resp.DataAsText));

				Debug.LogError(failCallback);
				//finish but have errorcode
				if (failCallback != null)
                    failCallback(resp.StatusCode, "Finished IsSuccess False", "BestHttpResponse");
			}
			break;
			
			// The request finished with an unexpected error. The request's Exception property may contain more info about the error.
		case HTTPRequestStates.Error:
			Logger.Error("[RCD] Request Finished with Error! " + (req.Exception != null ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
            if (failCallback != null)
            {
                if (req.Exception != null)
                {
                    failCallback(CONST_NET.ERRORCODE_HTTP_UNEXPECTED, req.Exception.Message, req.Exception.StackTrace);
                }
                else
                {
                    failCallback(CONST_NET.ERRORCODE_HTTP_UNEXPECTED, "Error exception null", "BestHttpResponse");
                }
            }
			break;
			
			// The request aborted, initiated by the user.
		case HTTPRequestStates.Aborted:
			Logger.Error("[RCD] Request Aborted!");
			if (failCallback != null)
                failCallback(CONST_NET.ERRORCODE_HTTP_ABORTED, "Aborted", "BestHttpResponse");
			break;
			
			// Ceonnecting to the server is timed out.
		case HTTPRequestStates.ConnectionTimedOut:
			Logger.Error("[RCD] Connection Timed Out!");
			if (failCallback != null)
                failCallback(CONST_NET.ERRORCODE_HTTP_CONNECTIONTIMEOUT, "ConnectionTimedOut", "BestHttpResponse");
			break;
			
			// The request didn't finished in the given time.
		case HTTPRequestStates.TimedOut:
			Logger.Error("[RCD] Processing the request Timed Out!");
			if (failCallback != null)
                failCallback(CONST_NET.ERRORCODE_HTTP_TIMEDOUT, "TimedOut", "BestHttpResponse");
			break;
		}
	}

	// upload log to server
	// by zhangzhen 
	[HideInInspector]
	public bool _uploadLogDone = false;
	public IEnumerator BestHttpUpload(string URL)
	{
		string deviceName = SystemInfo.deviceModel;
		deviceName = PlanXNet.Secure.Base64URLEncoder.ToBase64String(System.Text.Encoding.UTF8.GetBytes(deviceName));
		string timeStamp = DateTime.Now.ToString ("yyyy-MM-dd-HH:mm:ss");

        string outDir = UnityEngine.Application.persistentDataPath;
		string[] ff = Directory.GetFiles(outDir,"*");

//		Logger.Debug (timeStamp);
//		Logger.Debug (deviceName);
//		Logger.Debug (outDir);

		foreach(string f in ff)
		{
			string reg = @".*\log_.*\.txt";
			if(new Regex(reg).IsMatch(f))
			{
				string fileName = Path.GetFileName(f);
				using (FileStream fileStream = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					// send
					HTTPRequest request = new HTTPRequest(new System.Uri(URL), HTTPMethods.Post, OnRequestFinishedDelegate);
					request.AddHeader("devicename", deviceName);
					request.AddHeader("timestamp",  timeStamp);
					request.AddHeader("filename",  fileName);
					request.UploadStream = fileStream;

					request.Send();
					yield return StartCoroutine(request);
					if (request.State == HTTPRequestStates.Finished)
					{
						Logger.Debug("BestHttpUpload done: " + f);
					}
					else
					{
						Logger.Error("BestHttpUpload error: " + f);
					}
				}
			}
		} 
		
		_uploadLogDone = true; 
	}

	void OnRequestFinishedDelegate(HTTPRequest originalRequest, HTTPResponse response)
	{
		Logger.Debug ("OnRequestFinishedDelegate");
	}

	//retrieve json string
	public delegate void OnWWWJsonRequestComplete(string json);
	public delegate void OnWWWJsonRequestFail(int errorcode);
	public IEnumerator BestHttpJsonRequest(string URL, OnWWWJsonRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
	{
        Logger.Debug(" [TiOn] Start WWW Json request with [URL]:" + URL);
        string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
        Logger.Debug(" [TiOn] Start WWW Json request with IPv6[URL]:" + URL2IPv6);

        HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6));
 
		req.Send();
		
		yield return StartCoroutine(req);

		if(CheckResponse(req, failCallback)) {
			if(completeCallback != null) {
				completeCallback(req.Response.DataAsText);
			}
		}
	}

    public IEnumerator BestHttpJsonPostRequest(string URL, OnWWWJsonRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
    {
        Logger.Debug(" [TiOn] Start WWW Json post request with [URL]:" + URL);
        string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
        Logger.Debug(" [TiOn] Start WWW Json post request with IPv6[URL]:" + URL2IPv6);

        HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6),HTTPMethods.Post,OnRequestFinished);
        req.Send();

        yield return StartCoroutine(req);

        if(CheckResponse(req, failCallback)) {
            if(completeCallback != null) {
                completeCallback(req.Response.DataAsText);
            }
        }
    }

	//IP need add a header
	public IEnumerator BestHttpJsonRequestWithIP(string URL, OnWWWJsonRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
	{
		Logger.Debug(" [TiOn] Start WWW Json request with IP[URL]:" + URL);
		string URL2IPv6 = PlanXNet.IPv6Uri.AppleIPv4Uri2IPv6String(URL);
		Logger.Debug(" [TiOn] Start WWW Json request with IPv6[URL]:" + URL2IPv6);
		
		HTTPRequest req = new HTTPRequest(new System.Uri(URL2IPv6));
		req.AddHeader("Host", AnnouncementManager.CurrentCDN);
		req.Send();
		
		yield return StartCoroutine(req);
		
		if(CheckResponse(req, failCallback)) {
			if(completeCallback != null) {
				completeCallback(req.Response.DataAsText);
			}
		}
	}


	public delegate void OnWWWDataRequestComplete(byte []data);
	public IEnumerator BestHttpDataRequestWithIP(string URL, OnWWWDataRequestComplete completeCallback = null, OnWWWRequestFail failCallback = null)
	{
		Logger.Debug(" [TiOn] Start WWW binary data request [URL]:" + URL);
		
		HTTPRequest req = new HTTPRequest(new System.Uri(URL));
		req.AddHeader("Host", AnnouncementManager.CurrentCDN);
		req.Send();
		
		yield return StartCoroutine(req);
		
		if(CheckResponse(req, failCallback)) {
			if(completeCallback != null) {
				completeCallback(req.Response.Data);
			}
		}
	}

	private bool CheckResponse(HTTPRequest req, OnWWWRequestFail failCallback) 
	{
		switch (req.State)
		{
			// The request finished without any problem.
		case HTTPRequestStates.Finished:
			HTTPResponse resp = req.Response;
			if (resp.IsSuccess)
			{
				//success
				return true;
			}
			else
			{
				Logger.Error(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
				                           resp.StatusCode,
				                           resp.Message,
				                           resp.DataAsText));
				//finish but have errorcode
				if (failCallback != null)
                    failCallback(resp.StatusCode, "Finished", "CheckResponse");
			}
			break;
			
			// The request finished with an unexpected error. The request's Exception property may contain more info about the error.
		case HTTPRequestStates.Error:
			Logger.Error("Request Finished with Error! " + (req.Exception != null ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
			if (failCallback != null)
            {
                if(req.Exception != null)
                {
                    failCallback(CONST_NET.ERRORCODE_HTTP_UNEXPECTED,req.Exception.Message,req.Exception.StackTrace);
                }
                else
                {
                    failCallback(CONST_NET.ERRORCODE_HTTP_UNEXPECTED, "Exception null", "CheckResponse");
                }
            }
			break;
			
			// The request aborted, initiated by the user.
		case HTTPRequestStates.Aborted:
			Logger.Error("Request Aborted!");
			if (failCallback != null)
                failCallback(CONST_NET.ERRORCODE_HTTP_ABORTED, "Aborted", "CheckResponse");
			break;
			
			// Ceonnecting to the server is timed out.
		case HTTPRequestStates.ConnectionTimedOut:
			Logger.Error("Connection Timed Out 6!");
			if (failCallback != null)
                failCallback(CONST_NET.ERRORCODE_HTTP_CONNECTIONTIMEOUT, "ConnectionTimedOut", "CheckResponse");
			break;
			
			// The request didn't finished in the given time.
		case HTTPRequestStates.TimedOut:
			Logger.Error("Processing the request Timed Out!");
			if (failCallback != null)
                failCallback(CONST_NET.ERRORCODE_HTTP_TIMEDOUT, "TimedOut", "CheckResponse");
			break;
		}
		return false;
	}
}
