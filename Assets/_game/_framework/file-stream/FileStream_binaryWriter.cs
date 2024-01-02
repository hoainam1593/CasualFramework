using System.IO;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using UniRx;
using System.Linq;

public class FileStream_binaryWriter : IFileStream
{
    private BinaryWriter writer;

    public FileStream_binaryWriter(BinaryWriter writer)
    {
        this.writer = writer;
    }

    #region write primitive

    public void ReadOrWriteBool(ref bool val)
    {
        writer.Write(val);
    }

    public void ReadOrWriteInt(ref int val)
    {
        writer.Write(val);
    }

    public void ReadOrWriteLong(ref long val)
    {
        writer.Write(val);
    }

    public void ReadOrWriteFloat(ref float val)
    {
        writer.Write(val);
    }

    public void ReadOrWriteDouble(ref double val)
    {
        writer.Write(val);
    }

    public void ReadOrWriteString(ref string val)
    {
        WriteString(val);
    }

    public void ReadOrWriteDateTime(ref DateTime val)
    {
        WriteDateTime(val);
    }

    public void ReadOrWriteEnum<T>(ref T val) where T : struct, IConvertible
    {
        WriteEnum(val);
    }

	public void ReadOrWriteObj<T>(ref T val) where T : IFileStreamObject, new()
	{
		writer.Write(val.ModelVersion);
        val.ReadOrWrite(this, val.ModelVersion);
	}

	#endregion

	#region write rx property

	public void ReadOrWriteRxBool(ref ReactiveProperty<bool> val)
	{
		writer.Write(val.Value);
	}

	public void ReadOrWriteRxInt(ref ReactiveProperty<int> val)
	{
		writer.Write(val.Value);
	}

	public void ReadOrWriteRxLong(ref ReactiveProperty<long> val)
	{
		writer.Write(val.Value);
	}

	public void ReadOrWriteRxFloat(ref ReactiveProperty<float> val)
	{
		writer.Write(val.Value);
	}

	public void ReadOrWriteRxDouble(ref ReactiveProperty<double> val)
	{
		writer.Write(val.Value);
	}

	public void ReadOrWriteRxString(ref ReactiveProperty<string> val)
	{
		WriteString(val.Value);
	}

	public void ReadOrWriteRxDateTime(ref ReactiveProperty<DateTime> val)
	{
		WriteDateTime(val.Value);
	}

	public void ReadOrWriteRxEnum<T>(ref ReactiveProperty<T> val) where T : struct, IConvertible
	{
		WriteEnum(val.Value);
	}

	#endregion

	#region write list

	public void ReadOrWriteListBool(ref List<bool> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteListInt(ref List<int> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteListLong(ref List<long> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteListFloat(ref List<float> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteListDouble(ref List<double> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteListString(ref List<string> val)
	{
		WriteIList(val, WriteString);
	}

	public void ReadOrWriteListDateTime(ref List<DateTime> val)
	{
		WriteIList(val, WriteDateTime);
	}

	public void ReadOrWriteListEnum<T>(ref List<T> val) where T : struct, IConvertible
	{
		WriteIList(val, WriteEnum);
	}

	public void ReadOrWriteListObj<T>(ref List<T> val) where T : IFileStreamObject, new()
	{
		WriteIListObj(val);
	}

	#endregion

	#region write list of rx property

	public void ReadOrWriteListRxBool(ref List<ReactiveProperty<bool>> val)
	{
		WriteIListRx(val, writer.Write);
	}

	public void ReadOrWriteListRxInt(ref List<ReactiveProperty<int>> val)
	{
		WriteIListRx(val, writer.Write);
	}

	public void ReadOrWriteListRxLong(ref List<ReactiveProperty<long>> val)
	{
		WriteIListRx(val, writer.Write);
	}

	public void ReadOrWriteListRxFloat(ref List<ReactiveProperty<float>> val)
	{
		WriteIListRx(val, writer.Write);
	}

	public void ReadOrWriteListRxDouble(ref List<ReactiveProperty<double>> val)
	{
		WriteIListRx(val, writer.Write);
	}

	public void ReadOrWriteListRxString(ref List<ReactiveProperty<string>> val)
	{
		WriteIListRx(val, WriteString);
	}

	public void ReadOrWriteListRxDateTime(ref List<ReactiveProperty<DateTime>> val)
	{
		WriteIListRx(val, WriteDateTime);
	}

	public void ReadOrWriteListRxEnum<T>(ref List<ReactiveProperty<T>> val) where T : struct, IConvertible
	{
		WriteIListRx(val, WriteEnum);
	}

	#endregion

	#region write rx list

	public void ReadOrWriteRxListBool(ref ReactiveCollection<bool> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteRxListInt(ref ReactiveCollection<int> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteRxListLong(ref ReactiveCollection<long> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteRxListFloat(ref ReactiveCollection<float> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteRxListDouble(ref ReactiveCollection<double> val)
	{
		WriteIList(val, writer.Write);
	}

	public void ReadOrWriteRxListString(ref ReactiveCollection<string> val)
	{
		WriteIList(val, WriteString);
	}

	public void ReadOrWriteRxListDateTime(ref ReactiveCollection<DateTime> val)
	{
		WriteIList(val, WriteDateTime);
	}

	public void ReadOrWriteRxListEnum<T>(ref ReactiveCollection<T> val) where T : struct, IConvertible
	{
		WriteIList(val, WriteEnum);
	}

	public void ReadOrWriteRxListObj<T>(ref ReactiveCollection<T> val) where T : IFileStreamObject, new()
	{
		WriteIListObj(val);
	}

	#endregion

	#region write dictionary

	public void ReadOrWriteDicIntObj<T>(ref Dictionary<int, T> val) where T : IFileStreamObject, new()
	{
		WriteDicObj(val, writer.Write);
	}

	public void ReadOrWriteDicStringObj<T>(ref Dictionary<string, T> val) where T : IFileStreamObject, new()
	{
		WriteDicObj(val, WriteString);
	}

	public void ReadOrWriteDicEnumObj<TEnum, TObj>(ref Dictionary<TEnum, TObj> val)
		where TEnum : struct, IConvertible
		where TObj : IFileStreamObject, new()
	{
		WriteDicObj(val, WriteEnum);
	}

	#endregion

	#region utils

	private void WriteString(string s)
    {
		writer.Write(StaticUtils.Xor(s, FileStreamConst.xorEncryptionKey));
	}

    private void WriteDateTime(DateTime dt)
    {
		writer.Write(StaticUtils.DateTimeToUnixEpoch(dt));
	}

    private void WriteEnum<T>(T e) where T : struct, IConvertible
    {
        writer.Write(Convert.ToInt32(e));
    }

	private void WriteIList<T>(IList<T> l, UnityAction<T> writeAction)
    {
        writer.Write(l.Count);
        foreach (var i in l)
        {
            writeAction.Invoke(i);
        }
    }

	private void WriteIListRx<T>(IList<ReactiveProperty<T>> l, UnityAction<T> writeAction)
	{
		WriteIList(l, x =>
		{
			writeAction(x.Value);
		});
	}

    private void WriteIListObj<T>(IList<T> l) where T : IFileStreamObject, new()
    {
        var version = l.Count > 0 ? l[0].ModelVersion : -1;
        writer.Write(version);
		WriteIList(l, obj =>
        {
            obj.ReadOrWrite(this, version);
        });
    }

	private void WriteDicObj<TKey, TObj>(Dictionary<TKey, TObj> dic, UnityAction<TKey> writeAction) where TObj : IFileStreamObject, new()
	{
		var version = dic.Count > 0 ? dic.First().Value.ModelVersion : -1;
		writer.Write(version);
		writer.Write(dic.Count);
		foreach (var i in dic)
		{
			writeAction(i.Key);
			i.Value.ReadOrWrite(this, version);
		}
	}

	#endregion
}
