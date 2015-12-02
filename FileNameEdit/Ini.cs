using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileNameEdit
{
	public class Ini
	{
		IDictionary<string, string> content = new Dictionary<string, string>();
		string fileName = Path.Combine(Environment.CurrentDirectory, "FileNameEdit.ini");
		const string EQ = "=";
		const string fmt = "{0}={1}";

		public void Load()
		{
			if (File.Exists(fileName) == false) { return; }

			var ss = File.ReadAllLines(fileName);
			foreach (var item in ss)
			{
				content[item.before(EQ)] = item.after(EQ);
			}//for
		}//function

		public string this[string key]
		{
			get { return content.getValue(key) ?? string.Empty; }
			set {
				if (string.IsNullOrWhiteSpace(value) == false)
				{ content[key] = value; }
				else
					content.Remove(key);
			}
		}//function

		public void Save()
		{
			Func<string, string> kp2s = (s) =>
			{
				return fmt.fmt(s, content[s]);
			};
			IEnumerable<string> ss = content.Keys.Select(kp2s);
			File.WriteAllLines(fileName, ss);
		}//function
	}//class
}//namespace
