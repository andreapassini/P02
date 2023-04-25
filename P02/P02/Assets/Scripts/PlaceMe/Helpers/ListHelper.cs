using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceMe
{
	public class ListHelper
	{
		public static List<Transform> CopyList(List<Transform> list)
		{
			List<Transform> listCopy = new List<Transform>();

			foreach (Transform t in list)
			{
				// Save position
				Vector3 platPos = new Vector3(t.position.x, t.position.y, t.position.z);

				// Save rotation
				Quaternion platRot = new Quaternion(t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w);

				// Save Scale
				Vector3 plstScale = new Vector3(t.localScale.y, t.localScale.x, t.localScale.z);

				// Create Platfrom
				GameObject plat = new GameObject();
				plat.transform.SetPositionAndRotation(platPos, platRot);
				plat.transform.localScale = plstScale;

				listCopy.Add(plat.transform);
			}

			return listCopy;
		}

		public static List<Transform> InvertList(List<Transform> list)
		{
			List<Transform> listInverted = new List<Transform>();

			for (int i = 0; i < list.Count; i++)
			{
				int j = 0;
				Transform t = list[j];
				listInverted.Add(t);
			}

			return listInverted;
		}
	}
}
