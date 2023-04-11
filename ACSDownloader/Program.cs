using System.Text.RegularExpressions;

using HttpClient httpClient = new HttpClient();
string html = await httpClient.GetStringAsync("https://tmafe.com/ms-agent-hosting-2/");
var lines = html.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
HashSet<string> fileNames = new HashSet<string>();
int i = 0;
foreach(var line in lines)
{
    if (!line.StartsWith("_f(")) continue;
    //Console.WriteLine(line);
    string[] fields = line.Split(',');
    string nameUrl = fields[3].Trim('\'');
    string localName = "E:\\主同步盘\\我的坚果云\\技术资料\\珍贵软件\\MSAgent\\ACS_Files\\" + Uri.UnescapeDataString(nameUrl);
    if(File.Exists(localName))
    {
        //Console.WriteLine("文件已存在");
        continue;
    }
  
    string url = "https://tmafe.com/ms-agent-hosting-2/"+nameUrl;
    try
    {
        byte[] bytes = await httpClient.GetByteArrayAsync(url);
        File.WriteAllBytes(localName, bytes);
        //Console.WriteLine(localName + "   done");
        i++;
        Console.Title = $"{i}/{lines.Length} ({i*100.0/lines.Length}%)";
    }
    catch(Exception ex)
    {
        Console.WriteLine(line);
    }
}
Console.WriteLine("Done!");