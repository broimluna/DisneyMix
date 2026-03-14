using System.Collections;
using UnityEngine.SceneManagement;

public class CukunityGetLevelCommand : CukunityCommand
{
	public override void Process(Hashtable req, Hashtable res)
	{
		res["number"] = SceneManager.sceneCount;
		res["name"] = SceneManager.GetActiveScene().name;
		res["count"] = SceneManager.sceneCountInBuildSettings;
	}
}
