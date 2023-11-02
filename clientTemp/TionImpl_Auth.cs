using System;
using UnityEngine;
using System.Collections;
using System.Text;
using BestHTTP;

public class AuthInfo
{
	public string _userId = ""; //allocate from server 
	public string _deviceId = ""; //for quick player
	public string _friendCode = "";
	public string _accountId = "";
	public string _userName = ""; //for registed player
	public string _pwd = "";
	public string _email = "";
	public string _shardId = "";
	public string _shardName = "";
	public string _shardState = "";
	public string _authToken = "";
	public string _authType = "";
	public string _atlas = ""; //atlas for quickplay

	public string _roleID = "";

	public string _authCode = "";

	public string DisplayName
	{
		get
		{
			if (string.IsNullOrEmpty(_userName))
			{
				return("玩家" + _atlas);
			}
			else
			{
				return(_userName);
			}			
		}
	}
	
	public void Clone(AuthInfo info)
	{
		if (!string.IsNullOrEmpty(info._userId))
			this._userId = info._userId;
			
		if (!string.IsNullOrEmpty(info._deviceId))
			this._deviceId = info._deviceId;
			
		if (!string.IsNullOrEmpty(info._friendCode))
			this._friendCode = info._friendCode;			
			
		if (!string.IsNullOrEmpty(info._accountId))
			this._accountId = info._accountId;
						
		if (!string.IsNullOrEmpty(info._userName))			
			this._userName = info._userName;
			
		if (!string.IsNullOrEmpty(info._pwd))	
			this._pwd = info._pwd;
			
		if (!string.IsNullOrEmpty(info._email))	
			this._email = info._email;
			
		if (!string.IsNullOrEmpty(info._shardId))	
			this._shardId = info._shardId;
			
		if (!string.IsNullOrEmpty(info._shardName))	
			this._shardName = info._shardName;

		if (!string.IsNullOrEmpty(info._shardState))	
			this._shardState = info._shardState;
			
		if (!string.IsNullOrEmpty(info._authToken))	
			this._authToken = info._authToken;
			
		if (!string.IsNullOrEmpty(info._authType))	
			this._authType = info._authType;

		if (!string.IsNullOrEmpty(info._atlas))	
			this._atlas = info._atlas;	

		if (!string.IsNullOrEmpty(info._roleID))	
			this._roleID = info._roleID;

		if (!string.IsNullOrEmpty(info._authCode))	
			this._authCode = info._authCode;
	}
	
	
	
	public void Clear()
	{
		_userId = "";
		_deviceId = "";
		_accountId = "";
		_friendCode = "";
		_userName = "";
		_pwd = "";	
		_shardId = "";	
		_shardName = "";	
		_shardState = "";
		_authToken = "";
		_authType = "";
		_email = "";
		_atlas = "";
		_roleID = "";
		_authCode = "";
	}
	
	public string EncodeToJson()
	{
		Hashtable ht = new Hashtable();
		ht.Add("userId", _userId);
		ht.Add("userName", _userName);
		ht.Add("accountId", _accountId);
		ht.Add("pwd", _pwd);		
		ht.Add("deviceId", _deviceId);	
		ht.Add("friendCode", _friendCode);	
		ht.Add("shardId", _shardId);	
		ht.Add("shardName", _shardName);	
		ht.Add("shardState", _shardState);
		ht.Add("authToken", _authToken);
		ht.Add("authType", _authType);
		ht.Add("email", _email);
		ht.Add("atlas", _atlas);
		ht.Add("roleId", _roleID);
		ht.Add("authCode", _authCode);
						
		return TaiJson.jsonEncode(ht);
	}
	
	public void DecodeFromJson(string json)
	{
		Hashtable ht = (Hashtable)TaiJson.jsonDecode(json);
		_userId = (string)ht["userId"];
		_userName = (string)ht["userName"];
		_accountId = (string)ht["accountId"];
		_pwd = (string)ht["pwd"];
		_deviceId = (string)ht["deviceId"];	
		_friendCode = (string)ht["friendCode"];	
		_shardId = (string)ht["shardId"];	
		_shardName = (string)ht["shardName"];	
		_shardState = (string)ht["shardState"];
		_authToken = (string)ht["authToken"];
		_authType = (string)ht["authType"];
		_email = (string)ht["email"];
		_atlas = (string)ht["atlas"];
		_roleID = (string)ht["roleId"];
		_authCode = (string)ht["authCode"];
	}

	public string GetNumberStringOfAccountID()
	{
		//ShardID
		string[] shardId = this._accountId.Split(':');
		string serverID = "1";
		if(shardId.Length >= 2)
		{
			serverID = shardId[0] + shardId[1];
		}
		
		//RoleID
		string roldID = shardId [2];
		string[] split = roldID.Split('-');
		string finalRoldID = serverID;
		for(int i = 0; i < split.Length; i++)
		{
			try
			{
				finalRoldID = finalRoldID + System.Convert.ToInt64(split[i].ToUpper(), 16).ToString();
			}
			catch(System.Exception e)
			{
				AppLogger.Debug(e.ToString());
			}
		}
		int length = finalRoldID.Length < 64 ? finalRoldID.Length : 64;
		finalRoldID = finalRoldID.Substring (0, length);
		
		return finalRoldID;
	}
}

public partial class TionManager : MonoBehaviour {

	//private AuthClient _authClient = null;
	private ArrayList _authInfoList; //auth info records list
	public ArrayList AuthInfoList
	{
		get
		{
			return _authInfoList;
		}
	}	
	
	private AuthInfo _myAuthInfo; //current auth info
	public AuthInfo myAuthInfo
	{
		get
		{
			return _myAuthInfo;
		}
	}	

	private AuthInfo _myAuthInfoLast;
	public AuthInfo myAuthInfoLast
	{
		get
		{
			return _myAuthInfoLast;
		}
	}
	
	private AuthInfo _tempAuthInfo; //temp auth info, save intermidiate auth/register info
	public AuthInfo TempAuthInfo
	{
		get
		{
			return _tempAuthInfo;
		}
	}	
	
//	private const string AuthURLPrefix = "http://10.0.1.21:8081"; //for local
//	private const string AuthURLPrefix = "http://127.0.0.1:8081"; //for local
//	private const string AuthURLPrefix = "http://awsdev.git.taiyouxi.cn:8081"; //for 3rd
//	private const string AuthURLPrefix = "http://54.223.169.5:8081"; //for steady server

//	private const string AuthURLPrefix = "http://10.0.1.58:8081"; // zhangzhen

	public string AuthURLPrefix  {
		 get 
		 {
			//return "http://10.0.1.102:8081";	//zhenzhenfuwuqi 
			//return "http://10.0.1.106:8081";//冰神服务器
			//return "http://10.0.1.179:8081";//小天服务器
            //return "http://10.0.1.242:8081";
             //return "http://10.0.1.201:8081";
            //return AppInfo.endpointURL;
            return AppInfo.endpointURL.Replace(" ", "");      
		}
	}

	//private const string AuthURLPrefix = "http://sl001.a3k.taiyouxi.cn:8081";




    private float _stepTime; //step计时
	private const string AuthInfoRecordsKey = "AuthInfoRecordsEncrypt"; //plist key
	private const string AuthInfoLastRecordKey = "AuthInfoLastRecordEncrypt";
	private bool _isAccountFirstLogin = false;
	public bool IsAccountFirstLogin     //判断该账号是第一次在此设备上登录
	{
		get
		{
			return _isAccountFirstLogin;
		}
	}

	public const string MarketVerInfoForBundleKey = "MarketVerInfoForBundleKey";
	public const string BundleUploadKey = "BundleUploadKey"; //最近bundle更新成功后，存bundle更新upload号


	private string _isFirstLogin;	//用来判断是否首次登陆游戏，是否需要下载Bundle
	public string IsFirstLogin		//只有在第一次调用的时候才会从本地读取对应的key。
	{
		get
		{
			if(string.IsNullOrEmpty(_isFirstLogin))
			{
				string MarketVerInfoForBundle = PlayerPrefs.GetString(MarketVerInfoForBundleKey);

				if (string.IsNullOrEmpty (MarketVerInfoForBundle)) {
					//没有完成过bundle加载，肯定是第一次
					_isFirstLogin = "true";
				} else if (!string.Equals (MarketVerInfoForBundle, AppInfo.marketVersion)) {
					//完成过，但是版本号对不上，应该是覆盖安装，也当作第一次
					_isFirstLogin = "true";
				} else {
					_isFirstLogin = "false";
				}

				return _isFirstLogin;
			}
			else
			{
				return _isFirstLogin;
			}
		}
	}

	//若第一次登陆并且成功下载所有bundle，本地存储一个首次登陆成功字段
	public void SaveEW3DownLoadInfo()
	{
		PlayerPrefs.SetString(MarketVerInfoForBundleKey, AppInfo.marketVersion);
		PlayerPrefs.Save ();

		this._isFirstLogin = "false";
	}

	private string _isRegister = "false";
	public string IsRegister
	{
		get
		{
			return _isRegister;
		}
		set
		{
			_isRegister = value;
		}
	}

	
	public delegate void DelegateNoParam();
	public delegate void DelegateIntParam(int intParam);
	public delegate void DelegateBoolParam(bool boolParam);
	public delegate void DelegateBoolIntParam(bool boolParam, int errorCode);   
	public delegate void DelegateStringParam(string stringParam);
    public delegate void DelegateHashParam(Hashtable hashtable);

	public DelegateBoolParam _authActiveConnectingUIDelegate;
	public DelegateBoolIntParam _authActiveErrorUIDelegate;
    public DelegateHashParam _authBanErrorUIDelegate;
	
	//read saved auth info locally
	public void LoadAuthInfoFromLocal()
	{
		_authInfoList.Clear();
		
		string authInfoRecordsJson = PlayerPrefs.GetString(AuthInfoRecordsKey);
		if(!string.IsNullOrEmpty(authInfoRecordsJson))
		{
			byte[] authInfoRecordsJsonFromBytes = PlanXNet.Secure.Base64PlanXEncoder.FromBase64String (authInfoRecordsJson);
			authInfoRecordsJson = Encoding.Default.GetString (authInfoRecordsJsonFromBytes);
		}
		if (!string.IsNullOrEmpty(authInfoRecordsJson))
		{
			ArrayList jsonList = TaiJson.jsonDecode(authInfoRecordsJson) as ArrayList;
			foreach(string jsonInfo in jsonList)
			{
				Logger.Debug("[Tion] Load Auth Info :{0}", jsonInfo);
				AuthInfo thisInfo = new AuthInfo();
				thisInfo.DecodeFromJson(jsonInfo);
				_authInfoList.Add(thisInfo);
			}
		}
		string authInfoLastRecordJson = PlayerPrefs.GetString (AuthInfoLastRecordKey);
		if(!string.IsNullOrEmpty(authInfoLastRecordJson))
		{
			byte[] authInfoLastRecordJsonFormBytes = PlanXNet.Secure.Base64PlanXEncoder.FromBase64String (authInfoLastRecordJson);
			authInfoLastRecordJson = Encoding.Default.GetString (authInfoLastRecordJsonFormBytes);
		}
		if (!string.IsNullOrEmpty(authInfoLastRecordJson))
		{
			AuthInfo authInfoTemp = new AuthInfo();
			authInfoTemp.DecodeFromJson(authInfoLastRecordJson);
			_myAuthInfoLast = new AuthInfo();
			_myAuthInfoLast.Clone(authInfoTemp);
		}


	}

	public void SaveAuthInfoToLocal(AuthInfo info = null)
	{
		if (info == null)
		{
			//use my auth info instead
			info = myAuthInfo;
		}
	
		//detect if same userid exist?
		bool exist = false;
		AuthInfo findInfo = null;			
		foreach(AuthInfo thisInfo in _authInfoList)
		{
			if (thisInfo._userName == info._userName)
			{
				exist = true;
				findInfo = thisInfo;
				break;
			}
		}

		//save to local if not exist		
		if (!exist)
		{
			//add info list
			_authInfoList.Add(info);
			Logger.Debug("[Tion] Add Auth Info :{0}", info.EncodeToJson());
			_isAccountFirstLogin = true;
		}
		else
		{
			//update this info
			findInfo.Clone(info);
			Logger.Debug("[Tion] Update Auth Info :{0}", info.EncodeToJson());	
		}
		
		//update plist
		ArrayList jsonList = new ArrayList();
		foreach(AuthInfo thisInfo in _authInfoList)
		{
			jsonList.Add(thisInfo.EncodeToJson());
		}
		
		byte[] _authInfoRecord2Bytes = Encoding.Default.GetBytes(TaiJson.jsonEncode (jsonList));
		byte[] _authInfoLastRecord2Bytes = Encoding.Default.GetBytes(myAuthInfo.EncodeToJson());
		string _authInfoRecord = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (_authInfoRecord2Bytes);
		string _authInfoLastRecord = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (_authInfoLastRecord2Bytes);
		PlayerPrefs.SetString(AuthInfoRecordsKey, _authInfoRecord);
		PlayerPrefs.SetString(AuthInfoLastRecordKey, _authInfoLastRecord);
		PlayerPrefs.Save();		
	}

	public void SaveAuthInfoFromBind(AuthInfo info)
	{
		AuthInfo tempInfo = null;
		
		foreach(AuthInfo thisInfo in _authInfoList)
		{
			if (thisInfo._deviceId == info._deviceId)
			{
				tempInfo = thisInfo;
				break;
			}
		}
		
		if(tempInfo != null)
		{
			_authInfoList.Remove(tempInfo);
			_authInfoList.Add(info);
		}
		
		ArrayList jsonList = new ArrayList();
		foreach(AuthInfo thisInfo in _authInfoList)
		{
			jsonList.Add(thisInfo.EncodeToJson());
		}

		byte[] _authInfoRecord2Bytes = Encoding.Default.GetBytes(TaiJson.jsonEncode (jsonList));
		byte[] _authInfoLastRecord2Bytes = Encoding.Default.GetBytes(myAuthInfo.EncodeToJson());
		string _authInfoRecord = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (_authInfoRecord2Bytes);
		string _authInfoLastRecord = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (_authInfoLastRecord2Bytes);
		PlayerPrefs.SetString(AuthInfoRecordsKey, _authInfoRecord);
		PlayerPrefs.SetString(AuthInfoLastRecordKey, _authInfoLastRecord);
		PlayerPrefs.Save();	
		
	}
	
	#region AuthSteps
	
				
	//Auth step1, get auth token by device id or username
	public void Auth(AuthInfo info) 
	{
		// 登录后重置热更版本号
		TionManager.Instance.ResetHotVer();

		Logger.Warn("[RCD] Auth Step1 - Auth Start");
		
		//active connection UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(true);
		
		TionManager._tionAuthStep = TionAuthStep.Auth;
		BIManager._biAuthStep = BIAuthStep.Auth;
		
		string URL;
		Hashtable ht = new Hashtable ();
		_tempAuthInfo = info;
		//three login infos all are empty

		if(string.IsNullOrEmpty(info._authType))
		{
			if (string.IsNullOrEmpty(info._userName) && string.IsNullOrEmpty(info._friendCode))
			{
				//quick play

				string deviceId = info._deviceId;
				byte[] deviceId2Byte = Encoding.Default.GetBytes(deviceId);
				string encodedDeviceId = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String(deviceId2Byte);
				URL = string.Format("{0}/auth/v1/device/{1}?crc={2}", AuthURLPrefix, deviceId, encodedDeviceId);
				Logger.Warn("[RCD] Auth Step1 -Auth Quickplay [URL]: " + URL);
			}
			//only firendCode is not empty
			else if (string.IsNullOrEmpty(info._userName) && !string.IsNullOrEmpty(info._friendCode))
			{
				//friend code
				string deviceId = info._deviceId;
				byte[] deviceId2Byte = Encoding.Default.GetBytes(deviceId);
				string encodedDeviceId = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String(deviceId2Byte);
				URL = string.Format("{0}/auth/v1/deviceWithCode/{1}?crc={2}&code={3}&apikey={4}",
					AuthURLPrefix,
					deviceId,
					encodedDeviceId,
					info._friendCode,
					"a8e47ca5dd12da1b846da4260109b82d");
					
				Logger.Warn("[RCD] Auth Step1 - Auth FriendCode [URL]: " + URL);
			}
			//login by username and pwd
			else
			{
				//auth with usrname/pwd
				//TODO by RCD, auth with usrname/pwd, not deviceId
				string username = info._userName;
				Assertion.Check(!string.IsNullOrEmpty(username));
				string urlName = PlanXNet.Secure.Base64URLEncoder.
					ToBase64String(
					Encoding.Default.GetBytes(username)
					);
				
				string pwd = info._pwd;
				Assertion.Check(!string.IsNullOrEmpty(pwd));
				string urlPwd;
				using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
				{
					byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(pwd));
					urlPwd = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String(hash);
				}

				string param = string.Format("name={0}&passwd={1}", 
											urlName, 
											urlPwd);
				URL = string.Format("{0}/auth/v1/user/login?{1}", AuthURLPrefix, param);
				Debug.LogWarning("[RCD] Auth Step1 - Auth usrname/pwd [URL]: " + URL);
				Logger.Warn("[RCD] Auth Step1 - Auth usrname/pwd [URL]: " + URL);
			}

			StartCoroutine(BestHttpRequest(URL, OnAuthComplete, OnAuthFail, OnAuthFailByBan));
		}
		else
		{
			if (info._authType == "quick") {
				if (QuickSdkManager.Instance.CurrentUserInfo == null) {
					return;
				}

				byte[] quickAuthUid2Byte = Encoding.Default.GetBytes (QuickSdkManager.Instance.CurrentUserInfo._uid);
				byte[] quickAuthToken2Byte = Encoding.Default.GetBytes (QuickSdkManager.Instance.CurrentUserInfo._token);
				byte[] quickAuthChannelID2Byte = Encoding.Default.GetBytes (QuickSdkManager.Instance.SubChannelID);
				
				string type = QuickSdkManager.Instance.GetPlatformTypeForAuth ();
				byte[] typByte = Encoding.Default.GetBytes (type); // "0" android, "1" ios

				byte[] quickAuthDevice = Encoding.Default.GetBytes (SystemInfo.deviceModel);
				
				string encodedQuickAuthUid = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (quickAuthUid2Byte);
				string encodedQuickAuthToken = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (quickAuthToken2Byte);
				string encodedQuickAuthChannelID = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (quickAuthChannelID2Byte);
				string encodedTyp = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (typByte);
				string encodedDevice = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String (quickAuthDevice);

				//[Special Android Channel]
				switch (int.Parse (QuickSdkManager.Instance.ChannelID)) {
				case CONST_COMMON.ANDROID_VIVO_CHANNEL:

					URL = string.Format ("{0}/auth/v2/deviceWithVivo", AuthURLPrefix);

					ht.Add ("token", encodedQuickAuthToken);
					ht.Add ("channelId", encodedQuickAuthChannelID);
					ht.Add ("device", encodedDevice);

					Logger.Warn ("[RCD] Auth Step1 - vivoSdk auth [URL]: " + URL);
					break;
				case CONST_COMMON.ANDROID_SAMSUNG_CHANNEL:
					
					URL = string.Format ("{0}/auth/v2/deviceWithHero", AuthURLPrefix);
					
					ht.Add ("token", encodedQuickAuthToken);
					ht.Add ("channelId", encodedQuickAuthChannelID);
					ht.Add ("device", encodedDevice);
					
					Logger.Warn ("[RCD] Auth Step1 - Samsung's Hero Login Sdk auth [URL]: " + URL);
					break;
				case CONST_COMMON.ANDROID_ENJOY_CHANNEL:
				case CONST_COMMON.IOS_ENJOY_CHANNEL:

					URL = string.Format ("{0}/auth/v2/deviceWithEnjoy", AuthURLPrefix);

					ht.Add ("uid", encodedQuickAuthUid);
					ht.Add ("token", encodedQuickAuthToken);
					ht.Add ("typ", encodedTyp); 
					ht.Add ("channelId", encodedQuickAuthChannelID);
					ht.Add ("device", encodedDevice);

					AppLogger.Debug ("[Enjoy]LoginUid:" + QuickSdkManager.Instance.CurrentUserInfo._uid);
					AppLogger.Debug ("[Enjoy]LoginToken:" + QuickSdkManager.Instance.CurrentUserInfo._token);
					AppLogger.Debug ("[Enjoy]channelId:" + QuickSdkManager.Instance.ChannelID);
					AppLogger.Debug ("[Enjoy]device:" + SystemInfo.deviceModel);

					Logger.Warn ("[RCD] Auth Step1 - Enjoy Login Sdk auth [URL]: " + URL);

					break;
                case CONST_COMMON.Android_ENJOY_KOREA_GP_CHANNEL:
                case CONST_COMMON.Android_ENJOY_KOREA_ONESTORE_CHANNEL:
                case CONST_COMMON.IOS_ENJOY_KOREA_CHANNEL:
                    URL = string.Format("{0}/auth/v2/deviceWithEnjoyKo", AuthURLPrefix);

                    ht.Add("uid", encodedQuickAuthUid);
                    ht.Add("token", encodedQuickAuthToken);
                    ht.Add("typ", encodedTyp);
                    ht.Add("channelId", encodedQuickAuthChannelID);
                    ht.Add("device", encodedDevice);

                    AppLogger.Debug ("[EnjoyHG]LoginUid:" + QuickSdkManager.Instance.CurrentUserInfo._uid);
					AppLogger.Debug ("[EnjoyHG]LoginToken:" + QuickSdkManager.Instance.CurrentUserInfo._token);
					AppLogger.Debug ("[EnjoyHG]channelId:" + QuickSdkManager.Instance.ChannelID);
					AppLogger.Debug ("[EnjoyHG]device:" + SystemInfo.deviceModel);

                    Logger.Warn("[RCD] Auth Step1 - EnjoyHG Login Sdk auth [URL]: " + URL);
                    break;
				case CONST_COMMON.ANDROID_JWSVN_CHANNEL:
				case CONST_COMMON.IOS_JWSVN_CHANNEL:
					URL = string.Format ("{0}/auth/v2/deviceWithJwsvn", AuthURLPrefix);

					ht.Add ("authCode", encodedQuickAuthToken);
					ht.Add ("typ", encodedTyp); 
					ht.Add ("channelId", encodedQuickAuthChannelID);
					ht.Add ("device", encodedDevice);

					AppLogger.Debug ("[Jwsvn]AuthCode:" + info._authCode);
					AppLogger.Debug ("[Jwsvn]channelId:" + QuickSdkManager.Instance.ChannelID);
					AppLogger.Debug ("[Jwsvn]device:" + SystemInfo.deviceModel);

					Logger.Warn ("[RCD] Auth Step1 - Jwsvn Login Sdk auth [URL]: " + URL);

					break;
                case CONST_COMMON.Android_JWSJA_CHANNEL:
                case CONST_COMMON.IOS_JWSJA_CHANNEL:
                    URL = string.Format("{0}/auth/v2/deviceWithEnjoyja", AuthURLPrefix);

                    ht.Add("uid", encodedQuickAuthUid);
                    ht.Add("token", encodedQuickAuthToken);
                    ht.Add("typ", encodedTyp);
                    ht.Add("channelId", encodedQuickAuthChannelID);
                    ht.Add("device", encodedDevice);

                    AppLogger.Debug ("[Jwsja]LoginUid:" + QuickSdkManager.Instance.CurrentUserInfo._uid);
                    AppLogger.Debug ("[Jwsja]LoginToken:" + QuickSdkManager.Instance.CurrentUserInfo._token);
                    AppLogger.Debug ("[jwsja]channelId:" + QuickSdkManager.Instance.ChannelID);
                    AppLogger.Debug ("[jwsja]device:" + SystemInfo.deviceModel);

                    Logger.Warn("[RCD] Auth Step1 - JWSJA Login Sdk auth [URL]: " + URL);
                    break;
				default:

					int muBaoID = QuickSdkManager.Instance.MuBaoID;
					switch (muBaoID) {

					case (int)MuBaoIDEnum.DEFAULT:
						{
							URL = string.Format ("{0}/auth/v2/deviceWithQuick", AuthURLPrefix);
						}
						break;
					case (int)MuBaoIDEnum.MUBAO1:
						{
							URL = string.Format ("{0}/auth/v2/deviceWithQuickMuBao1", AuthURLPrefix);
						}
						break;
					default:
						URL = string.Format ("{0}/auth/v2/deviceWithQuick", AuthURLPrefix);
						break;
					}

					ht.Add ("uid", encodedQuickAuthUid);
					ht.Add ("token", encodedQuickAuthToken);
					ht.Add ("channelId", encodedQuickAuthChannelID);
					ht.Add ("typ", encodedTyp); 
					ht.Add ("device", encodedDevice);

					Logger.Warn ("[RCD] Auth Step1 - quickSdk auth [URL]: " + URL);
					break;
				}
			}
			else
			{
				return;
			}

			StartCoroutine(BestHttpRequestForQuick(URL, ht, OnAuthComplete, OnAuthFail,OnAuthFailByBan));
			
		}
		//StartCoroutine(WWWRequest(URL, OnAuthComplete));

	}

/*
	public void AuthQuickPlay() 
	{
		Logger.Debug(" [TiOn] Auth Step1 Start");
		
		TionManager._tionAuthStep = TionAuthStep.Auth;
		
		myAuthInfo.Clear();
		myAuthInfo._deviceId = SystemInfo.deviceUniqueIdentifier;
		
		string deviceId = myAuthInfo._deviceId;
		byte[] deviceId2Byte = System.Text.Encoding.Default.GetBytes(deviceId);
		string encodedDeviceId = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String(deviceId2Byte);
		string URL = string.Format("{0}/auth/v1/device/{1}?crc={2}", AuthURLPrefix, deviceId, encodedDeviceId);
		
		StartCoroutine(WWWRequest(URL, OnAuthComplete));
	}
*/

	public DelegateNoParam _onAuthCompleteDelegate;
	void OnAuthComplete(Hashtable res)
	{	
		Debug.Log("complete 11111");
		if (_onAuthCompleteDelegate != null)
			_onAuthCompleteDelegate();

		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
	
		//clone temp info to my info
		myAuthInfo.Clone(TempAuthInfo);	
	
		myAuthInfo._authToken = (string)res["authtoken"];
		Logger.Warn("[RCD] Auth Step1 - OK, [AuthToken]: " + myAuthInfo._authToken);
		
		if (res["display"] != null)
		{
			//myAuthInfo._atlas = (string)res["display"];
			//生成本地时间戳
			myAuthInfo._atlas = System.DateTime.Now.ToString("ssffff");;
		}

		_shardIdListHaveRole.Clear ();
        _shardRoleLevelList.Clear();
Debug.Log("complete 222222");
		if (res["shardrolev250"] != null)
		{
            ArrayList shardArray = res["shardrolev250"] as ArrayList;
			
			foreach(var shard in shardArray)
			{
                Hashtable hashtable = shard as Hashtable;
			    string shardName = shard as string;
			    if (hashtable != null)
			    {
			        if (hashtable["shard"] != null)
			        {
                        _shardIdListHaveRole.Add(hashtable["shard"].ToString());
			        }
                    if (hashtable["level"] != null)
                    {
                        string str = hashtable["level"].ToString();
                        uint level = 0;
                        if (uint.TryParse(str, out level) && level > 0)
                        {
                            _shardRoleLevelList.Add(level);
                        }
			        }
			    }
                else if (shardName != null)
                {
                    _shardIdListHaveRole.Add(shardName);
                }
			}
		}
        else if (res["shardrole"] != null)
        {
            ArrayList shardArray = res["shardrole"] as ArrayList;

            foreach (var shard in shardArray)
            {
                _shardIdListHaveRole.Add((string)shard);
            }
        }

		_shardLastLogin = "";
Debug.Log("complete 33333333333");
		if (res["lastshard"] != null)
		{
			string shardLastLogin = res["lastshard"] as string;
			
			_shardLastLogin = shardLastLogin;
		}

		//因为英雄sdk需要服务器二次校验才能够拿到对应的username，所以如下更新CurrentUserInfo
		int channelID = int.Parse(QuickSdkManager.Instance.ChannelID);
		switch (channelID) {
		case CONST_COMMON.ANDROID_JWSVN_CHANNEL:
		case CONST_COMMON.IOS_JWSVN_CHANNEL:
        case CONST_COMMON.Android_JWSJA_CHANNEL:
        case CONST_COMMON.IOS_JWSJA_CHANNEL:

			string username = string.Empty;
			string uid = string.Empty;
			string sdkuserid = string.Empty;

			if (res ["username"] != null) {
				username = res ["username"] as string;
				AppLogger.Debug(username);
			}

			if (res ["id"] != null) {
				uid = res ["id"] as string;
				AppLogger.Debug(uid);
			}

			if (res ["sdkuserid"] != null) {
				sdkuserid = res ["sdkuserid"] as string;
				AppLogger.Debug(sdkuserid);
			}

			QuickSdkManager.Instance.UpdateCurrentUserInfo (sdkuserid, username);

            QuickSdkManager.Instance.OnJwsvnUpdateAccountInfo(QuickSdkManager.Instance.CurrentUserInfo);

            AppLogger.Debug (QuickSdkManager.Instance.CurrentUserInfo.ToString ());

			break;
		}



		//write the info into records
		//在android真机上，不需要在这一步存储账号信息，防止默认服务器列表错误
		#if !UNITY_ANDROID
		SaveAuthInfoToLocal();
		#endif

		//begin step2
		GetShardList();
		Debug.Log("complete done");
	}

    void OnAuthFailByBan(int errorCode = -1, Hashtable res = null, string exception = null, string stackTrace = null)
	{
		Debug.LogWarning("[RCD] Auth Step1 - fail(by ban), code " + errorCode.ToString());
		
		Logger.Warn ("[RCD] Auth Step1 - fail, code " + errorCode.ToString());
		
		System.Collections.Generic.Dictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string> ();
		dic.Add ("errorCode", errorCode.ToString());
		dic.Add ("AuthStep", "OnAuthFailByBan");
		BIManager.Instance.OnEvent ("ErrorWhenAuth", dic, true);
		//AppLogger.Error ("ErrorWhenAuth");
		
		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
        if (errorCode == 206 && _authBanErrorUIDelegate != null)
        {
            _authBanErrorUIDelegate(res);
            return;
        }
		//active error UI
		if (_authActiveErrorUIDelegate != null) {
			_authActiveErrorUIDelegate (true, errorCode);
		}

	}

    public void AuthFailMsg(string errorCode, string operation,string exception, string stackTrace)
    {
        string gamex = string.Format("{0}:{1}",
                                    _loginIp,
                                    _loginPort);
        Hashtable table = new Hashtable();
        table.Add("accountId", TionManager.Instance.myAuthInfo._accountId);
        table.Add("errorCode", errorCode);
        table.Add("auth", AuthURLPrefix);
        table.Add("gamex", gamex);
        table.Add("isFirstEnter", TionManager.Instance._isFirstLoginSuccOnce.ToString());
        table.Add("exception", exception);
        table.Add("stackTrace", stackTrace);
        table.Add("operation", operation);

        StartCoroutine(TionManager.Instance.BestHttpRequestForAuthError(AuthURLPrefix + "/debug/log", table));
    }

	public void LoadingMsg(string op, long timeCost)
    {
		Hashtable table = new Hashtable();

		table.Add("DeviceId", SystemInfo.deviceUniqueIdentifier);
		table.Add("operation", op);
		table.Add("time", timeCost.ToString());

		Debug.Log("LoadingMsg url=" + AuthURLPrefix + "/debug/log");
		StartCoroutine(TionManager.Instance.BestHttpRequestForAuthError(AuthURLPrefix + "/debug/log", table));

    }

    void OnAuthFail(int errorCode,string exception = null,string stackTrace = null)
    {
		Debug.Log("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
		Debug.LogWarning("[RCD] Auth Step1 - fail（return), code " + errorCode.ToString());
        Logger.Warn("[RCD] Auth Step1 - fail（return), code " + errorCode.ToString());
        float stepTime = Time.time - _stepTime;
        AuthFailMsg(errorCode.ToString(), "OnAuthFail:Step2:" + stepTime, exception, stackTrace);

		System.Collections.Generic.Dictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string> ();
		dic.Add ("errorCode", errorCode.ToString());
		dic.Add ("AuthStep", "OnAuthFail");
		BIManager.Instance.OnEvent ("ErrorWhenAuth", dic, true);
		//AppLogger.Error ("ErrorWhenAuth");
		
        //deactive connecting UI
        if (_authActiveConnectingUIDelegate != null)
            _authActiveConnectingUIDelegate(false);       
        //active error UI
        if (_authActiveErrorUIDelegate != null) {
			_authActiveErrorUIDelegate (true, errorCode);
		}
    }
	
	//Auth step2, get shard list
	private void GetShardList()
	{
		//active connection UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(true);
		
		TionManager._tionAuthStep = TionAuthStep.RetriveShards;
		BIManager._biAuthStep = BIAuthStep.RetriveShards;
	
		string urlBase = "{0}/login/v2/shards/{1}?at={2}";
		string urlBase1 = "{0}/login/v2/shards/{1}?at={2}&ver={3}&sp={4}";

		string URL = string.Format(urlBase,
									AuthURLPrefix, 
									AppInfo.productId,
									myAuthInfo._authToken);

		string cheatCode = AnnouncementUIHandler._whitelistBase64Input;
		if (!string.IsNullOrEmpty (cheatCode)) {
			URL = string.Format(urlBase1,
								AuthURLPrefix, 
								AppInfo.productId,
								myAuthInfo._authToken,
								AppInfo.marketVersion,
								cheatCode);
		}

		Logger.Warn("[RCD] Auth Step2 - Start, get shard list. [URL]: " + URL);	

		StartCoroutine(BestHttpRequest(URL, OnGetShardListComplete, OnGetShardListFail));
	}

	public DelegateNoParam _onGetShardListCompleteDelegate;

    private string GetServerName(Hashtable shard,out int index)
    {
        string multiLanguageStr= (string)shard["multi_lang"];
        bool isMuliLanguage = string.IsNullOrEmpty(multiLanguageStr) ? false : multiLanguageStr.Equals("1");
        string shardName = (string)shard["dn"];
        //处理多语言服务器名字
        if(isMuliLanguage)
        {
            shardName = (string)shard["lang"];
            if(!string.IsNullOrEmpty(shardName))
            {
                Hashtable ht = (Hashtable)TaiJson.jsonDecode(shardName);
                if (ht != null)
                {
                    Hashtable content = (Hashtable)ht[Localization.language];
                    if (content!=null)
                    {
                        object result = content["dn"];
                        if (result!=null)
                        {
                            shardName = result.ToString();
                        } 
                    }
                }
            }
        }

        index = 0;
        if (shardName.Contains("."))
        {
            int.TryParse(shardName.Split('.')[0], out index);
        }
        string name = "";
        if (shardName.Contains("-"))
        {
            name = shardName.Split('-')[1];
        }
        return name;
    }

	void OnGetShardListComplete(Hashtable res)
	{
		Logger.Warn("[RCD] Auth Step2 - OK");
		
		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
		
		_shardList.Clear();
        _yybShardList.Clear();
        ArrayList shardArray = res["shards"] as ArrayList;
		
		foreach(Hashtable shard in shardArray)
		{
            int index = 0;
            string name = GetServerName(shard, out index);

			ShardInfo shardInfo = new ShardInfo((string)shard["name"], name, (string)shard["ss"], index);
			if(shardInfo._order < 1000)
				_shardList.Add(shardInfo);
			else
				_yybShardList.Add(shardInfo);
		}
		
		if (_onGetShardListCompleteDelegate != null)
		{
			_onGetShardListCompleteDelegate();
		}
	}


    void OnGetShardListFail(int errorCode = -1, string exception = null, string stackTrace = null)
	{
		Debug.LogError("OnGetShardListFail fail fail");
		Logger.Warn("[RCD] Auth Step2 - fail! code " + errorCode.ToString());

		System.Collections.Generic.Dictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string> ();
		dic.Add ("errorCode", errorCode.ToString());
		dic.Add ("AuthStep", "OnGetShardListFail");
		BIManager.Instance.OnEvent ("ErrorWhenAuth", dic, true);
		//AppLogger.Error ("ErrorWhenAuth");
		
		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
		
		//active error UI
		if (_authActiveErrorUIDelegate != null)
			_authActiveErrorUIDelegate(true, errorCode);
	}	

	//Auth step, get shard list
	public void EnterShard()
	{
		//active connection UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(true);
	
		TionManager._tionAuthStep = TionAuthStep.Login;
		BIManager._biAuthStep = BIAuthStep.Login;
	
		Logger.Warn("[RCD] Auth Step3 - Start, enter shard with AT:{0}, Shard:{1}", myAuthInfo._authToken, myAuthInfo._shardId);


		string urlBase = "{0}/login/v1/getgate?at={1}&sn={2}&ver={3}";
		string urlBase1 = "{0}/login/v1/getgate?at={1}&sn={2}&ver={3}&sp={4}";

		string URL = string.Format(urlBase,
			AuthURLPrefix, 
			myAuthInfo._authToken, 
			myAuthInfo._shardId,
			AppInfo.marketVersion);

		string cheatCode = AnnouncementUIHandler._whitelistBase64Input;
		if (!string.IsNullOrEmpty (cheatCode)) {
			URL = string.Format(urlBase1,
				AuthURLPrefix, 
				myAuthInfo._authToken,
				myAuthInfo._shardId,
				AppInfo.marketVersion,
				cheatCode);
		}

		Logger.Warn("[RCD] Auth Step3, enter shard. [URL]: " + URL);	


		StartCoroutine(BestHttpRequest(URL, OnEnterShardComplete, OnEnterShardFail));
	}
	
	public DelegateNoParam _onEnterShardCompleteDelegate;
	public int _lastGetTokenTime = 0;
	void OnEnterShardComplete(Hashtable res)
	{
		Logger.Warn("[RCD] Auth Step3 - succ!");
	
		if (string.Compare((string)res["result"], "ok") == 0)
		{
			if (_onEnterShardCompleteDelegate != null)
			{
				_onEnterShardCompleteDelegate();
			}

			byte[] ip = PlanXNet.Secure.Base64PlanXEncoder.FromBase64String((string)res["ip"]);
			_loginIp = System.Text.Encoding.UTF8.GetString(ip);				
			string[] split = _loginIp.Split(new char[]{':'}, System.StringSplitOptions.None);
			_loginIp = split[0];
			_loginPort = System.Convert.ToInt32(split[1]);
			
			byte[] token = PlanXNet.Secure.Base64PlanXEncoder.FromBase64String((string)res["logintoken"]);
			_loginToken = System.Text.Encoding.UTF8.GetString(token);			
			_lastGetTokenTime = System.Convert.ToInt32(Time.realtimeSinceStartup);

			Logger.Debug(" [TiOn] Auth Step3 OK, ip:{0}, login token:{1}", _loginIp, _loginToken);						
			
			//set AB group
			if (res["Team"] != null)
			{
				string team = res["Team"] as string;
				
				if (string.IsNullOrEmpty(team))
				{
					ABTest.SetCurrentGroup(ABTest.ABGroup.GroupA);
				}
				else
				{
					ABTest.SetCurrentGroup(string.Equals("a", team) ? ABTest.ABGroup.GroupA : ABTest.ABGroup.GroupB);
				}
			}
			
			//connect to server
			Connect();

		}
		else
		{
			//retry
			int ms = System.Convert.ToInt32(res["ms"]);
			float time = (float)(ms / 1000);
			Invoke("EnterShard", time);
		}

	}	
	
	void OnEnterShardFail(int errorCode,string exception = null,string stackTrace = null)
	{
		Logger.Warn("[RCD] Auth Step3 - fail! code " + errorCode.ToString());
        float stepTime = Time.time - _stepTime;
        AuthFailMsg(errorCode.ToString(), "OnEnterShardFail:Step3:" + stepTime, exception, stackTrace);

		System.Collections.Generic.Dictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string> ();
		dic.Add ("errorCode", errorCode.ToString());
		dic.Add ("AuthStep", "OnEnterShardFail");
		BIManager.Instance.OnEvent ("ErrorWhenAuth", dic, true);
		//AppLogger.Error ("ErrorWhenAuth");
		
		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
		
		//active error UI
		if (_authActiveErrorUIDelegate != null)
			_authActiveErrorUIDelegate(true, errorCode);
			
			
		//for 401 error, quit game and re-auth
		if (errorCode == 401)
		{
			Logger.Warn("Application Quit 401!");		
			
			//back to preboot scene and re-login
		
			//Reset connectiongMgr的UI和请求队列
			ConnectManager.Instance.Reset();
		
			//popup notify UI
			if (UIMessageBoxManager.Instance != null)
			{
				UIMessageBoxManager.Instance.ShowMessageBox("",
				                                            Localization.Get("IDS_ERROR_LOST_CONNECTION"),
				                                            MB_TYPE.MB_OK,
				                                            (ID_BUTTON buttonID)=>{
					GameManager.RebootGame();
				});
			}
			else
			{
				GameManager.RebootGame();
			}
			
			
			
			//Luanch disconnect coroutine
			//GameManager.Instance.CountDownQuit(2);
		}
	}	
	
		
	#endregion //AuthSteps

	#region register
	//register with name/pwd
	public void Register(AuthInfo info, OnWWWRequestComplete completeCallback = null, OnWWWRequestFail failCallBack = null)
	{
		//active connection UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(true);
	
		Logger.Debug(" [TiOn] Register Start");
		TionManager._tionAuthStep = TionAuthStep.Register;
		BIManager._biAuthStep = BIAuthStep.Register;

		string URL;		
		_tempAuthInfo = info;
		
		string username = info._userName;
		Assertion.Check(!string.IsNullOrEmpty(username));
		string urlName = PlanXNet.Secure.Base64URLEncoder.
			ToBase64String(
				Encoding.Default.GetBytes(username)
				);
		
		string pwd = info._pwd;
		Assertion.Check(!string.IsNullOrEmpty(pwd));
		string urlPwd;
		using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
		{
			byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(pwd));
			urlPwd = PlanXNet.Secure.Base64PlanXEncoder.ToBase64String(hash);
		}
		
		string urlUserEmail = info._email;
		
		string param = string.Format("name={0}&passwd={1}",
									 urlName, 
									 urlPwd);
									 
		if (!string.IsNullOrEmpty(urlUserEmail))
		{
			param = param + "&email=" + urlUserEmail;
		}	
		
		URL = string.Format("{0}/auth/v1/user/reg/{1}?{2}", AuthURLPrefix, info._deviceId, param);
		Logger.Debug(" [TiOn] Register with [URL]: " + URL);		
		
		if (completeCallback == null)
			StartCoroutine(BestHttpRequest(URL, OnRegisterComplete, OnRegisterFail));
		else
			StartCoroutine(BestHttpRequest(URL, completeCallback, failCallBack));	
	}
	
	public DelegateNoParam _onRegisterCompleteDelegate;
	void OnRegisterComplete(Hashtable res)
	{
		if (_onRegisterCompleteDelegate != null)
			_onRegisterCompleteDelegate();
		
		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
	
		myAuthInfo._authToken = (string)res["authtoken"];
		Logger.Debug(" [TiOn] Register OK, [AuthToken]: " + myAuthInfo._authToken);
		
		//clone temp info to my info
		myAuthInfo.Clone(TempAuthInfo);
		_isRegister = "true";

		// DataEye
		//DataEyeInterface.register(myAuthInfo._userName, DCAccountType.DC_Anonymous);
		// init level for new user.
		//DataEyeInterface.setLevel(1);
		//DataEyeInterface.setChaLvl(0, 1);
		//DataEyeInterface.setChaLvl(1, 1);
		//DataEyeInterface.setChaLvl(2, 1);
		
		//write the info into records
		SaveAuthInfoToLocal();
		
		//begin step2
		GetShardList();
		
		Logger.Debug(" [TiOn] Register OK");

		BIManager.Instance.OnEvent("Register OK", null, true, true);
		//BIManager.Instance.OnRegisterForAdTracking(myAuthInfo._accountId);
	}

    void OnRegisterFail(int errorCode = -1, string exception = null, string stackTrace = null)
	{
		System.Collections.Generic.Dictionary<string, string> dic = new System.Collections.Generic.Dictionary<string, string> ();
		dic.Add ("errorCode", errorCode.ToString());
		dic.Add ("AuthStep", "OnRegisterFail");
		BIManager.Instance.OnEvent ("ErrorWhenAuth", dic, true);
		//AppLogger.Error ("ErrorWhenAuth");

		//deactive connecting UI
		if (_authActiveConnectingUIDelegate != null)
			_authActiveConnectingUIDelegate(false);
		
		//active error UI
		if (_authActiveErrorUIDelegate != null)
			_authActiveErrorUIDelegate(true, errorCode);
		
	}

	#endregion 
}
