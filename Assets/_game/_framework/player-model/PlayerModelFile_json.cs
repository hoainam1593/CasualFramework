
using Newtonsoft.Json;

public class PlayerModelFile_json : IPlayerModelFile
{
	public string Extension => "json";

	public void ReadModel(string filepath, BasePlayerModel model)
	{
		var jsonText = StaticUtils.ReadTextFile(filepath);
		JsonConvert.PopulateObject(jsonText, model);
	}

	public void WriteModel(string filepath, BasePlayerModel model)
	{
		var json = StaticUtils.JsonSerializeToFriendlyText(model);
		StaticUtils.WriteTextFile(filepath, json);
	}
}