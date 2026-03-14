using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using JsonFx.Json;
using UnityEngine;

public class CukunityServer : MonoBehaviour
{
	private string ip = "127.0.0.1";

	private int port = 9921;

	private static bool debugOn = true;

	private TcpListener listener;

	private List<CukunityRequest> requests;

	public static CukunityServer instance;

	private void Awake()
	{
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Server awake");
		}
		if (instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		requests = new List<CukunityRequest>();
		StartCoroutine(Listen());
	}

	private IEnumerator Listen()
	{
		yield return new WaitForEndOfFrame();
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Listening on " + ip + ":" + port);
		}
		IPAddress localAddr = IPAddress.Parse(ip);
		listener = new TcpListener(localAddr, port);
		listener.Start();
		listener.BeginAcceptTcpClient(OnAcceptTcpClientComplete, this);
		StartCoroutine(MainLoop());
	}

	private IEnumerator MainLoop()
	{
		while (true)
		{
			List<CukunityRequest> pendingRequests = null;
			lock (requests)
			{
				if (requests.Count > 0)
				{
					pendingRequests = new List<CukunityRequest>(requests);
					requests.Clear();
				}
			}
			if (pendingRequests != null)
			{
				pendingRequests.ForEach(ProcessRequest);
				yield return new WaitForEndOfFrame();
			}
			else
			{
				yield return new WaitForSeconds(0.1f);
			}
		}
	}

	private void ProcessRequest(CukunityRequest req)
	{
		Hashtable hashtable = new Hashtable();
		try
		{
			Hashtable hashtable2 = JsonReader.Deserialize<Hashtable>(req.Request);
			string jsonString = Utility.GetJsonString(hashtable2, "command");
			if (jsonString == null || jsonString.Length == 0)
			{
				Debug.LogError("Cukunity: missing command in client's request (" + req.Request + ")");
				hashtable["error"] = "MissingCommandError";
				req.OnProcessed(JsonWriter.Serialize(hashtable));
				return;
			}
			CukunityCommand cukunityCommand;
			switch (jsonString)
			{
			case "get_screen":
				if (debugOn)
				{
					Debug.LogWarning("@@UA_DEBUG@@ Processing get screen");
				}
				cukunityCommand = new CukunityGetScreenCommand();
				break;
			case "get_scene":
				if (debugOn)
				{
					Debug.LogWarning("@@UA_DEBUG@@ Processing get scene");
				}
				cukunityCommand = new CukunityGetSceneCommand();
				break;
			case "get_level":
				if (debugOn)
				{
					Debug.LogWarning("@@UA_DEBUG@@ Processing get level");
				}
				cukunityCommand = new CukunityGetLevelCommand();
				break;
			case "load_level":
				if (debugOn)
				{
					Debug.LogWarning("@@UA_DEBUG@@ Processing load level");
				}
				cukunityCommand = new CukunityLoadLevelCommand();
				break;
			case "get_image":
				if (debugOn)
				{
					Debug.LogWarning("@@UA_DEBUG@@ Processing get image");
				}
				cukunityCommand = new CukunityGetImageCommand();
				break;
			default:
				Debug.LogError("Cukunity: unknown command in client's request (" + req.Request + ")");
				hashtable["error"] = "UnknownCommandError";
				req.OnProcessed(JsonWriter.Serialize(hashtable));
				return;
			}
			cukunityCommand.Process(hashtable2, hashtable);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat("Cukunity: exception while processing client's request (", ex, ")"));
			if (debugOn)
			{
				Debug.LogError(string.Concat("@@UA_DEBUG@@ Cukunity: exception while processing client's request (", ex, ")"));
			}
			hashtable["error"] = "ServerError";
		}
		req.OnProcessed(JsonWriter.Serialize(hashtable));
	}

	private void recur(Hashtable s, int i)
	{
		foreach (string key in s.Keys)
		{
			if (s[key].GetType() == typeof(List<Hashtable>))
			{
				foreach (Hashtable item in (List<Hashtable>)s[key])
				{
					recur(item, i + 1);
				}
			}
			else if (s[key].GetType() == typeof(Hashtable))
			{
				foreach (string key2 in ((Hashtable)s[key]).Keys)
				{
					Debug.LogWarning("@@UA_DEBUG@@     " + i + " " + key2 + " " + ((Hashtable)s[key])[key2]);
				}
			}
			else
			{
				Debug.LogWarning("@@UA_DEBUG@@  " + i + " " + s[key]);
			}
		}
	}

	private void OnDestroy()
	{
		try
		{
			if (listener != null)
			{
				listener.Stop();
			}
		}
		catch (SocketException)
		{
		}
	}

	public void EnqueueRequest(CukunityRequest req)
	{
		lock (requests)
		{
			requests.Add(req);
		}
	}

	private static void OnAcceptTcpClientComplete(IAsyncResult result)
	{
		CukunityServer cukunityServer = result.AsyncState as CukunityServer;
		TcpClient client = cukunityServer.listener.EndAcceptTcpClient(result);
		Debug.LogWarning("@@UA_DEBUG@@ Cukunity: client connected ");
		CukunityClient cukunityClient = CukunityClient.getInstance();
		cukunityClient.init(client, cukunityServer, debugOn);
		cukunityServer.listener.BeginAcceptTcpClient(OnAcceptTcpClientComplete, cukunityServer);
		cukunityClient.SendAck();
	}
}
