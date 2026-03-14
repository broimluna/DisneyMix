using System.Threading;

public class CukunityRequest
{
	private string request;

	private ManualResetEvent signal;

	private string response;

	public string Request
	{
		get
		{
			return request;
		}
	}

	public string Response
	{
		get
		{
			return response;
		}
	}

	public CukunityRequest(string request, ManualResetEvent signal)
	{
		this.request = request;
		this.signal = signal;
		response = string.Empty;
	}

	public void OnProcessed(string response)
	{
		this.response = response;
		signal.Set();
	}
}
