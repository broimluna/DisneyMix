using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using JsonFx.Json;
using UnityEngine;

public class CukunityClient
{
	private TcpClient client;

	private NetworkStream stream;

	private byte[] buffer;

	private int bufferUsedCount;

	private string bufferedString;

	private CukunityServer server;

	public ManualResetEvent signal;

	public static bool debugOn;

	private static readonly int BufferIncrease = 1024;

	private static readonly Encoding enc = Encoding.UTF8;

	private static CukunityClient cukunityClient;

	public void init(TcpClient client, CukunityServer server, bool debug)
	{
		debugOn = debug;
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Client created");
		}
		this.client = client;
		this.server = server;
		stream = client.GetStream();
		buffer = new byte[BufferIncrease];
		bufferUsedCount = 0;
		bufferedString = string.Empty;
		signal = new ManualResetEvent(false);
	}

	public static CukunityClient getInstance()
	{
		if (cukunityClient == null)
		{
			Debug.Log("@@UA_DEBUG@@ NEW CLIENT ");
			cukunityClient = new CukunityClient();
		}
		return cukunityClient;
	}

	public void SendAck()
	{
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Sending ACK to client");
		}
		Hashtable value = new Hashtable();
		byte[] bytes = enc.GetBytes(JsonWriter.Serialize(value) + "\n");
		stream.BeginWrite(bytes, 0, bytes.Length, OnSendAckComplete, this);
	}

	private static void OnSendAckComplete(IAsyncResult result)
	{
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Ack completed");
		}
		CukunityClient cukunityClient = result.AsyncState as CukunityClient;
		cukunityClient.stream.EndWrite(result);
		cukunityClient.BeginRead();
	}

	private void BeginRead()
	{
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Begin read");
			Debug.LogWarning("@@UA_DEBUG@@ BufferUsedCCount : lengeth " + bufferUsedCount + " >= ?" + buffer.Length);
		}
		if (bufferUsedCount >= buffer.Length)
		{
			byte[] destinationArray = new byte[bufferUsedCount + BufferIncrease];
			Array.Copy(buffer, destinationArray, buffer.Length);
			buffer = destinationArray;
		}
		stream.BeginRead(buffer, bufferUsedCount, buffer.Length - bufferUsedCount, OnReadComplete, this);
	}

	public void Close()
	{
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Cukunity: client disconnected");
		}
		try
		{
			stream.Close();
			client.Close();
		}
		catch (Exception)
		{
		}
	}

	private static void OnReadComplete(IAsyncResult result)
	{
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Read complete");
		}
		CukunityClient.cukunityClient = result.AsyncState as CukunityClient;
		CukunityClient cukunityClient = CukunityClient.cukunityClient;
		int num = cukunityClient.stream.EndRead(result);
		if (num <= 0)
		{
			if (debugOn)
			{
				Debug.LogWarning("@@UA_DEBUG@@ NOTHING RECIEVED " + num);
			}
			cukunityClient.Close();
			return;
		}
		cukunityClient.bufferedString += enc.GetString(cukunityClient.buffer, cukunityClient.bufferUsedCount, num);
		cukunityClient.bufferUsedCount += num;
		string text = cukunityClient.bufferedString;
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Line read = " + text);
		}
		Debug.LogWarning("@@UA_DEBUG@@ GET HERE");
		if (text != null)
		{
			int byteCount = enc.GetByteCount(text);
			cukunityClient.bufferedString = cukunityClient.bufferedString.Remove(0, byteCount);
			cukunityClient.bufferUsedCount -= byteCount;
			string text2 = cukunityClient.ProcessLineRequest(text);
			if (debugOn)
			{
				Debug.LogWarning("@@UA_DEBUG@@ Response length " + text2.Length);
			}
			if (text2.Length <= 0)
			{
				if (debugOn)
				{
					Debug.LogWarning("@@UA_DEBUG@@ Closing client!!!");
				}
				cukunityClient.Close();
				return;
			}
			byte[] bytes = enc.GetBytes(text2.Length + "\n");
			cukunityClient.stream.BeginWrite(bytes, 0, bytes.Length, null, null);
			bytes = enc.GetBytes(text2 + "\n");
			cukunityClient.stream.BeginWrite(bytes, 0, bytes.Length, null, null);
		}
		if (debugOn)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Preparing for next read");
		}
		cukunityClient.BeginRead();
	}

	private string ProcessLineRequest(string line)
	{
		string request = line.TrimEnd('\r', '\n');
		CukunityRequest cukunityRequest = new CukunityRequest(request, signal);
		signal.Reset();
		server.EnqueueRequest(cukunityRequest);
		signal.WaitOne();
		return cukunityRequest.Response;
	}
}
