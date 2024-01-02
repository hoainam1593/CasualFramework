using System;
using System.Collections.Generic;
using UniRx;

public interface IFileStream
{
	#region single

	void ReadOrWriteBool(ref bool val);
	void ReadOrWriteInt(ref int val);
	void ReadOrWriteLong(ref long val);
	void ReadOrWriteFloat(ref float val);
	void ReadOrWriteDouble(ref double val);
	void ReadOrWriteString(ref string val);
	void ReadOrWriteDateTime(ref DateTime val);
	void ReadOrWriteEnum<T>(ref T val) where T : struct, IConvertible;
	void ReadOrWriteObj<T>(ref T val) where T : IFileStreamObject, new();

	void ReadOrWriteRxBool(ref ReactiveProperty<bool> val);
	void ReadOrWriteRxInt(ref ReactiveProperty<int> val);
	void ReadOrWriteRxLong(ref ReactiveProperty<long> val);
	void ReadOrWriteRxFloat(ref ReactiveProperty<float> val);
	void ReadOrWriteRxDouble(ref ReactiveProperty<double> val);
	void ReadOrWriteRxString(ref ReactiveProperty<string> val);
	void ReadOrWriteRxDateTime(ref ReactiveProperty<DateTime> val);
	void ReadOrWriteRxEnum<T>(ref ReactiveProperty<T> val) where T : struct, IConvertible;

	#endregion

	#region list

	void ReadOrWriteListBool(ref List<bool> val);
	void ReadOrWriteListInt(ref List<int> val);
	void ReadOrWriteListLong(ref List<long> val);
	void ReadOrWriteListFloat(ref List<float> val);
	void ReadOrWriteListDouble(ref List<double> val);
	void ReadOrWriteListString(ref List<string> val);
	void ReadOrWriteListDateTime(ref List<DateTime> val);
	void ReadOrWriteListEnum<T>(ref List<T> val) where T : struct, IConvertible;
	void ReadOrWriteListObj<T>(ref List<T> val) where T : IFileStreamObject, new();

	void ReadOrWriteListRxBool(ref List<ReactiveProperty<bool>> val);
	void ReadOrWriteListRxInt(ref List<ReactiveProperty<int>> val);
	void ReadOrWriteListRxLong(ref List<ReactiveProperty<long>> val);
	void ReadOrWriteListRxFloat(ref List<ReactiveProperty<float>> val);
	void ReadOrWriteListRxDouble(ref List<ReactiveProperty<double>> val);
	void ReadOrWriteListRxString(ref List<ReactiveProperty<string>> val);
	void ReadOrWriteListRxDateTime(ref List<ReactiveProperty<DateTime>> val);
	void ReadOrWriteListRxEnum<T>(ref List<ReactiveProperty<T>> val) where T : struct, IConvertible;

	void ReadOrWriteRxListBool(ref ReactiveCollection<bool> val);
	void ReadOrWriteRxListInt(ref ReactiveCollection<int> val);
	void ReadOrWriteRxListLong(ref ReactiveCollection<long> val);
	void ReadOrWriteRxListFloat(ref ReactiveCollection<float> val);
	void ReadOrWriteRxListDouble(ref ReactiveCollection<double> val);
	void ReadOrWriteRxListString(ref ReactiveCollection<string> val);
	void ReadOrWriteRxListDateTime(ref ReactiveCollection<DateTime> val);
	void ReadOrWriteRxListEnum<T>(ref ReactiveCollection<T> val) where T : struct, IConvertible;
	void ReadOrWriteRxListObj<T>(ref ReactiveCollection<T> val) where T : IFileStreamObject, new();

	#endregion

	#region dictionary

	void ReadOrWriteDicIntObj<T>(ref Dictionary<int, T> val) where T : IFileStreamObject, new();
	void ReadOrWriteDicStringObj<T>(ref Dictionary<string, T> val) where T : IFileStreamObject, new();
	void ReadOrWriteDicEnumObj<TEnum, TObj>(ref Dictionary<TEnum, TObj> val) 
		where TEnum : struct, IConvertible 
		where TObj : IFileStreamObject, new();

	#endregion
}
