using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GG_Controller : GG_AIPath
{
	public abstract void OnSelected ();

	public abstract void OnDeselected ();

	public abstract void OnPlayerCommand (MouseTarget mouseTarget);
}
