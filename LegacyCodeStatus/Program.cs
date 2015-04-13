using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace LegacyCodeStatus
{
	class Program
	{
		private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		static void Main(string[] args)
		{
			log4net.Config.XmlConfigurator.Configure();

			ReadCodeStatus(@"{your path here}");
			Console.ReadKey();
		}

		private static void ReadCodeStatus(string path)
		{
			FileStatus files = new FileStatus();
			files.ComputeASPStats(path);
			files.ComputeStatsCodeBehind(path);
			files.ComputeStatsFrontPages(path);
			files.CountJavascriptPages(path);



			log.Debug("HTML Pages: " + files.TotalHTMLPages.ToString("#,#"));
			log.Debug("HTML Lines: " + files.TotalHTMLLines.ToString("#,#"));
			log.Debug("");
			log.Debug("Javascript Pages: " + files.TotalJavascriptPages.ToString("#,#"));
			log.Debug("Javascript Lines: " + files.TotalJavascriptLines.ToString("#,#"));
			log.Debug("");
			log.Debug("Classic ASP Pages: " + files.TotalClassicASPPages.ToString("#,#"));
			log.Debug("Classic ASP Lines: " + files.TotalClassicASPLines.ToString("#,#"));
			log.Debug("VB Script Functions: " + files.TotalVBScriptFunctions.ToString("#,#"));
			log.Debug("");
			log.Debug("VB.Net Pages: " + files.TotalVBDotNetPages.ToString("#,#"));
			log.Debug("VB.Net Lines: " + files.TotalVBDotNetLines.ToString("#,#"));
			log.Debug("VB.Net Functions/subs: " + files.TotalVBDotNetFunctions.ToString("#,#"));
			log.Debug("");
		}
	}
}
