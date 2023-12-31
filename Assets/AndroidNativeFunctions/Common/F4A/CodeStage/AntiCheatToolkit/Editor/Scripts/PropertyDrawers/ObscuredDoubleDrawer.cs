﻿#region copyright
// ------------------------------------------------------------------------
//  Copyright (C) 2013-2019 Dmitriy Yukhanov - focus [http://codestage.net]
// ------------------------------------------------------------------------
#endregion

#if UNITY_EDITOR

namespace CodeStage.AntiCheat.EditorCode.PropertyDrawers
{
	using Common;
	using ObscuredTypes;

	using System.Runtime.InteropServices;
	using UnityEditor;
	using UnityEngine;

	[CustomPropertyDrawer(typeof(ObscuredDouble))]
	internal class ObscuredDoubleDrawer : ObscuredPropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			var hiddenValue = prop.FindPropertyRelative("hiddenValue");
			SetBoldIfValueOverridePrefab(prop, hiddenValue);

			var cryptoKey = prop.FindPropertyRelative("currentCryptoKey");
			var inited = prop.FindPropertyRelative("inited");
			var fakeValue = prop.FindPropertyRelative("fakeValue");
			var fakeValueActive = prop.FindPropertyRelative("fakeValueActive");

			var currentCryptoKey = cryptoKey.longValue;

			var union = new LongBytesUnion();
			double val = 0;

			if (!inited.boolValue)
			{
				if (currentCryptoKey == 0)
				{
					currentCryptoKey = cryptoKey.longValue = ObscuredDouble.cryptoKeyEditor;
				}

				inited.boolValue = true;

				union.l = ObscuredDouble.Encrypt(0, currentCryptoKey);
				hiddenValue.longValue = union.l;
			}
			else
			{
				union.l = hiddenValue.longValue;
				val = ObscuredDouble.Decrypt(union.l, currentCryptoKey);
			}

			EditorGUI.BeginChangeCheck();
			val = EditorGUI.DoubleField(position, label, val);
			if (EditorGUI.EndChangeCheck())
			{
				union.l = ObscuredDouble.Encrypt(val, currentCryptoKey);
				hiddenValue.longValue = union.l;

				fakeValue.doubleValue = val;
				fakeValueActive.boolValue = true;
			}
			
			ResetBoldFont();
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct LongBytesUnion
		{
			[FieldOffset(0)]
			public long l;

			[FieldOffset(0)]
			public ACTkByte8 b8;
		}
	}
}
#endif