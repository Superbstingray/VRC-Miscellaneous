
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Superbstingray
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
	public class SetPickupInterpolation : UdonSharp.UdonSharpBehaviour
	{
	
	public bool setKinematic;
	bool kinematicRight, kinematicLeft, nullRight, nullLeft;

	VRCPlayerApi localPlayer;
	VRC_Pickup pickupRight, pickupLeft;
	Rigidbody rigidRight, rigidLeft;

		void Start()
		{
			localPlayer = Networking.LocalPlayer;
		}

		void FixedUpdate()
		{
			if (!VRC.SDKBase.Utilities.IsValid(localPlayer)) { return; }

			pickupLeft = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left);

			if (VRC.SDKBase.Utilities.IsValid(pickupLeft))
			{
				rigidLeft = pickupLeft.GetComponent<Rigidbody>();
				rigidLeft.interpolation = RigidbodyInterpolation.None;
				if(nullLeft)
				{
					kinematicLeft = rigidLeft.isKinematic;
				}

				if(setKinematic)
				{
					rigidLeft.isKinematic = true;
					nullLeft = false;
				}
			}
			else if (VRC.SDKBase.Utilities.IsValid(rigidLeft))
			{
				rigidLeft.isKinematic = kinematicLeft;
				rigidLeft = null;
				nullLeft = true;
			}

			pickupRight = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right);

			if (VRC.SDKBase.Utilities.IsValid(pickupRight))
			{
				rigidRight = pickupRight.GetComponent<Rigidbody>();
				rigidRight.interpolation = RigidbodyInterpolation.None;
				if(nullRight)
				{
					kinematicRight = rigidRight.isKinematic;
				}

				if(setKinematic)
				{
					rigidRight.isKinematic = true;
					nullRight = false;
				}
			}
			else if (VRC.SDKBase.Utilities.IsValid(rigidRight))
			{
				rigidRight.isKinematic = kinematicRight;
				rigidRight = null;
				nullRight = true;
			}
		}
	}
}
