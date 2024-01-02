
public class PlayerModelFile_binary : IPlayerModelFile
{
	public string Extension => "bin";

	public void ReadModel(string filepath, BasePlayerModel model)
	{
		StaticUtils.ReadBinaryFile(filepath, binaryReader =>
		{
			var reader = new FileStream_binaryReader(binaryReader);
			var version = 0;
			reader.ReadOrWriteInt(ref version);
			model.ReadOrWrite(reader, version);
		});
	}

	public void WriteModel(string filepath, BasePlayerModel model)
	{
		StaticUtils.WriteBinaryFile(filepath, binaryWriter =>
		{
			var writer = new FileStream_binaryWriter(binaryWriter);
			var version = model.ModelVersion;
			writer.ReadOrWriteInt(ref version);
			model.ReadOrWrite(writer, version);
		});
	}
}