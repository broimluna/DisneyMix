using System;
using System.Collections;
using UnityEngine;

public class CukunityCommand
{
	public delegate object GameObjectVisitor(GameObject obj, object context);

	public virtual void Process(Hashtable req, Hashtable res)
	{
		throw new Exception("@@UA_DEBUG@@  Not implemented");
	}

	protected void Traverse(GameObject obj, GameObjectVisitor visitor, object context)
	{
		context = visitor(obj, context);
	}
}
