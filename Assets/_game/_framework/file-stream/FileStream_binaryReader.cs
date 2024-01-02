using System;
using System.Collections.Generic;
using System.IO;
using UniRx;

public class FileStream_binaryReader : IFileStream
{
    private BinaryReader reader;

    public FileStream_binaryReader(BinaryReader reader)
    {
        this.reader = reader;
    }

    #region read primitive

    public void ReadOrWriteBool(ref bool val)
    {
        val = reader.ReadBoolean();
    }

    public void ReadOrWriteInt(ref int val)
    {
        val = reader.ReadInt32();
    }

    public void ReadOrWriteLong(ref long val)
    {
        val = reader.ReadInt64();
    }

    public void ReadOrWriteFloat(ref float val)
    {
        val = reader.ReadSingle();
    }

    public void ReadOrWriteDouble(ref double val)
    {
        val = reader.ReadDouble();
    }

    public void ReadOrWriteString(ref string val)
    {
        val = ReadString();
    }

    public void ReadOrWriteDateTime(ref DateTime val)
    {
        val = ReadDateTime();
    }

    public void ReadOrWriteEnum<T>(ref T val) where T : struct, IConvertible
    {
        val = ReadEnum<T>();
    }

    public void ReadOrWriteObj<T>(ref T val) where T : IFileStreamObject, new()
    {
        var version = reader.ReadInt32();
        val = new T();
        val.ReadOrWrite(this, version);
    }

    #endregion

    #region read rx property

    public void ReadOrWriteRxBool(ref ReactiveProperty<bool> val)
    {
        val = new ReactiveProperty<bool>(reader.ReadBoolean());
    }

	public void ReadOrWriteRxInt(ref ReactiveProperty<int> val)
	{
        val = new ReactiveProperty<int>(reader.ReadInt32());
	}

	public void ReadOrWriteRxLong(ref ReactiveProperty<long> val)
	{
        val = new ReactiveProperty<long>(reader.ReadInt64());
	}

	public void ReadOrWriteRxFloat(ref ReactiveProperty<float> val)
	{
        val = new ReactiveProperty<float>(reader.ReadSingle());
	}

	public void ReadOrWriteRxDouble(ref ReactiveProperty<double> val)
	{
        val = new ReactiveProperty<double>(reader.ReadDouble());
	}

	public void ReadOrWriteRxString(ref ReactiveProperty<string> val)
	{
        val = new ReactiveProperty<string>(ReadString());
	}

	public void ReadOrWriteRxDateTime(ref ReactiveProperty<DateTime> val)
	{
        val = new ReactiveProperty<DateTime>(ReadDateTime());
	}

    public void ReadOrWriteRxEnum<T>(ref ReactiveProperty<T> val) where T : struct, IConvertible
    {
        val = new ReactiveProperty<T>(ReadEnum<T>());
    }

	#endregion

	#region read list

	public void ReadOrWriteListBool(ref List<bool> val)
	{
		ReadList(ref val, reader.ReadBoolean);
	}

	public void ReadOrWriteListInt(ref List<int> val)
	{
		ReadList(ref val, reader.ReadInt32);
	}

	public void ReadOrWriteListLong(ref List<long> val)
	{
		ReadList(ref val, reader.ReadInt64);
	}

	public void ReadOrWriteListFloat(ref List<float> val)
	{
		ReadList(ref val, reader.ReadSingle);
	}

	public void ReadOrWriteListDouble(ref List<double> val)
	{
		ReadList(ref val, reader.ReadDouble);
	}

	public void ReadOrWriteListString(ref List<string> val)
	{
		ReadList(ref val, ReadString);
	}

	public void ReadOrWriteListDateTime(ref List<DateTime> val)
	{
		ReadList(ref val, ReadDateTime);
	}

	public void ReadOrWriteListEnum<T>(ref List<T> val) where T : struct, IConvertible
	{
		ReadList(ref val, ReadEnum<T>);
	}

	public void ReadOrWriteListObj<T>(ref List<T> val) where T : IFileStreamObject, new()
	{
		var version = reader.ReadInt32();
		ReadList(ref val, () =>
		{
			var obj = new T();
			obj.ReadOrWrite(this, version);
			return obj;
		});
	}

	#endregion

	#region read list of rx property

	public void ReadOrWriteListRxBool(ref List<ReactiveProperty<bool>> val)
	{
		ReadListRx(ref val, reader.ReadBoolean);
	}

	public void ReadOrWriteListRxInt(ref List<ReactiveProperty<int>> val)
	{
		ReadListRx(ref val, reader.ReadInt32);
	}

	public void ReadOrWriteListRxLong(ref List<ReactiveProperty<long>> val)
	{
		ReadListRx(ref val, reader.ReadInt64);
	}

	public void ReadOrWriteListRxFloat(ref List<ReactiveProperty<float>> val)
	{
		ReadListRx(ref val, reader.ReadSingle);
	}

	public void ReadOrWriteListRxDouble(ref List<ReactiveProperty<double>> val)
	{
		ReadListRx(ref val, reader.ReadDouble);
	}

	public void ReadOrWriteListRxString(ref List<ReactiveProperty<string>> val)
	{
		ReadListRx(ref val, ReadString);
	}

	public void ReadOrWriteListRxDateTime(ref List<ReactiveProperty<DateTime>> val)
	{
		ReadListRx(ref val, ReadDateTime);
	}

	public void ReadOrWriteListRxEnum<T>(ref List<ReactiveProperty<T>> val) where T : struct, IConvertible
	{
		ReadListRx(ref val, ReadEnum<T>);
	}

	#endregion

	#region read rx list

	public void ReadOrWriteRxListBool(ref ReactiveCollection<bool> val)
	{
		ReadRxList(ref val, reader.ReadBoolean);
	}

	public void ReadOrWriteRxListInt(ref ReactiveCollection<int> val)
	{
		ReadRxList(ref val, reader.ReadInt32);
	}

	public void ReadOrWriteRxListLong(ref ReactiveCollection<long> val)
	{
		ReadRxList(ref val, reader.ReadInt64);
	}

	public void ReadOrWriteRxListFloat(ref ReactiveCollection<float> val)
	{
		ReadRxList(ref val, reader.ReadSingle);
	}

	public void ReadOrWriteRxListDouble(ref ReactiveCollection<double> val)
	{
		ReadRxList(ref val, reader.ReadDouble);
	}

	public void ReadOrWriteRxListString(ref ReactiveCollection<string> val)
	{
		ReadRxList(ref val, ReadString);
	}

	public void ReadOrWriteRxListDateTime(ref ReactiveCollection<DateTime> val)
	{
		ReadRxList(ref val, ReadDateTime);
	}

	public void ReadOrWriteRxListEnum<T>(ref ReactiveCollection<T> val) where T : struct, IConvertible
	{
		ReadRxList(ref val, ReadEnum<T>);
	}

	public void ReadOrWriteRxListObj<T>(ref ReactiveCollection<T> val) where T : IFileStreamObject, new()
	{
		var version = reader.ReadInt32();
		ReadRxList(ref val, () =>
		{
			var obj = new T();
			obj.ReadOrWrite(this, version);
			return obj;
		});
	}

	#endregion

	#region read dictionary

	public void ReadOrWriteDicIntObj<T>(ref Dictionary<int, T> val) where T : IFileStreamObject, new()
	{
        val = ReadDicObj<int, T>(reader.ReadInt32);
	}

	public void ReadOrWriteDicStringObj<T>(ref Dictionary<string, T> val) where T : IFileStreamObject, new()
    {
        val = ReadDicObj<string, T>(ReadString);
    }

	public void ReadOrWriteDicEnumObj<TEnum, TObj>(ref Dictionary<TEnum, TObj> val)
		where TEnum : struct, IConvertible
		where TObj : IFileStreamObject, new()
	{
        val = ReadDicObj<TEnum, TObj>(ReadEnum<TEnum>);
	}

	#endregion

	#region utils

	private string ReadString()
    {
		return StaticUtils.Xor(reader.ReadString(), FileStreamConst.xorEncryptionKey);
	}

    private DateTime ReadDateTime()
    {
		return StaticUtils.UnixEpochToDateTime(reader.ReadInt64());
	}

    private T ReadEnum<T>() where T : struct, IConvertible
    {
		return StaticUtils.IntToEnum<T>(reader.ReadInt32());
	}

    private void ReadList<T>(ref List<T> l, Func<T> readFunc)
    {
        l = (List<T>)ReadIList(readFunc, length =>
        {
            var newList = new List<T>();
            for (var i = 0; i < length; i++)
            {
                newList.Add(default);
            }
            return newList;
        });
    }

	private void ReadListRx<T>(ref List<ReactiveProperty<T>> l, Func<T> readFunc)
	{
		l = (List<ReactiveProperty<T>>)ReadIList(() => new ReactiveProperty<T>(readFunc()), length =>
		{
			var newList = new List<ReactiveProperty<T>>();
			for (var i = 0; i < length; i++)
			{
				newList.Add(null);
			}
			return newList;
		});
	}

	private void ReadRxList<T>(ref ReactiveCollection<T> l, Func<T> readFunc)
	{
		l = (ReactiveCollection<T>)ReadIList(readFunc, length =>
		{
			var newList = new ReactiveCollection<T>();
			for (var i = 0; i < length; i++)
			{
				newList.Add(default);
			}
			return newList;
		});
	}

	private IList<T> ReadIList<T>(Func<T> readFunc, Func<int, IList<T>> createListFunc)
    {
        var length = reader.ReadInt32();
        var l = createListFunc(length);
        for (var i = 0; i < length; i++)
        {
            l[i] = readFunc.Invoke();
        }
        return l;
    }

    private Dictionary<TKey, TObj> ReadDicObj<TKey, TObj>(Func<TKey> readFunc) where TObj : IFileStreamObject, new()
    {
        var version = reader.ReadInt32();
        var length = reader.ReadInt32();
        var dic = new Dictionary<TKey, TObj>();
        for (var i = 0; i < length; i++)
        {
            var key = readFunc();
            var obj = new TObj();
            obj.ReadOrWrite(this, version);
            dic.Add(key, obj);
        }
        return dic;
    }

	#endregion
}
