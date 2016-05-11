using UnityEngine;
using System.Collections;

public class MouseInput : MouseController {

	protected override void PlayerSelection(){
		foreach (UnitSelector selectableUnit in FindObjectsOfType<UnitSelector>()) {
			if (IsWithinSelectionBounds (selectableUnit.gameObject)) {
				selectableUnit.ActivateUnitSelector ();
				//currentSelectableUnits.Add (selectableUnit);
			} else {
				selectableUnit.DeactivateUnitSelector ();
			}
		}
	}
}
