using System.Collections;
using UnityEngine;

public class CukunityGetScreenCommand : CukunityCommand
{
	public override void Process(Hashtable req, Hashtable res)
	{
		res["width"] = Screen.width;
		res["height"] = Screen.height;
	}
}
