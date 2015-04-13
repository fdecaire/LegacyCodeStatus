using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace LegacyCodeStatus
{
	public class FileStatus
	{
		public int TotalHTMLPages;
		public int TotalHTMLLines;
		public int TotalJavascriptPages;
		public int TotalJavascriptLines;
		public int TotalClassicASPPages;
		public int TotalClassicASPLines;
		public int TotalVBDotNetPages;
		public int TotalVBDotNetLines;
		public int TotalVBDotNetFunctions;
		public int TotalVBScriptFunctions;


		public FileStatus()
		{
			TotalHTMLPages = 0;
			TotalHTMLLines = 0;
			TotalJavascriptPages = 0;
			TotalJavascriptLines = 0;
			TotalClassicASPPages = 0;
			TotalClassicASPLines = 0;
			TotalVBDotNetPages = 0;
			TotalVBDotNetLines = 0;
			TotalVBDotNetFunctions = 0;
			TotalVBScriptFunctions = 0;
		}

		public void ComputeASPStats(string path)
		{
			string[] aspFileList = Directory.GetFiles(path, "*.asp", SearchOption.AllDirectories);
			bool inHtml = false;
			bool inFunction = false;

			foreach (string aspFile in aspFileList)
			{
				TotalClassicASPPages++;
				using (var reader = new StreamReader(aspFile))
				{
					while (!reader.EndOfStream)
					{
						string text = reader.ReadLine().Trim().ToLower();

						// ignore comments and blank lines
						if (text.Length > 0 && text[0] != '\'')
						{
							// keep counting html lines until the end of the html block
							if (inHtml)
							{
								//TODO: need to count javascript
								//<script


								TotalHTMLLines++;
								if (text.IndexOf("</html>") > -1)
								{
									inHtml = false;
								}
								else
								{
									continue;
								}
							}

							// look for html code: <html
							if (text.IndexOf("<html") > -1)
							{
								TotalHTMLPages++;
								TotalHTMLLines++;
								inHtml = true;
								continue;
							}

							// ignore switch tags (that are not on the same line)
							if (text.IndexOf("<%") > -1 && text.IndexOf("%>") == -1)
							{
								continue;
							}
							if (text.IndexOf("<%") == -1 && text.IndexOf("%>") > -1)
							{
								continue;
							}

							// total lines not in html and not blank or comments
							TotalClassicASPLines++;

							if (inFunction)
							{
								if (text.IndexOf("end sub") == 0 || text.IndexOf("end function") == 0)
								{
									inFunction = false;
								}
								continue;
							}

							// find start of subs or functions
							if (text.IndexOf("sub ") == 0 || text.IndexOf("function ") == 0)
							{
								inFunction = true;
								TotalVBScriptFunctions++;
								continue;
							}
						}
					}
				}
			}
		}

		public void ComputeStatsCodeBehind(string path)
		{
			string[] dotNetFileList = Directory.GetFiles(path, "*.vb", SearchOption.AllDirectories);
			bool inFunction = false;

			foreach (string dotNetFile in dotNetFileList)
			{
				TotalVBDotNetPages++;
				using (var reader = new StreamReader(dotNetFile))
				{
					while (!reader.EndOfStream)
					{
						string text = reader.ReadLine().Trim().ToLower();

						// ignore comments and blank lines
						if (text.Length > 0 && text[0] != '\'')
						{
							TotalVBDotNetLines++;

							if (inFunction)
							{
								if (text.IndexOf("end sub") > -1 || text.IndexOf("end function") > -1)
								{
									inFunction = false;
								}
								continue;
							}

							// count number of functions, subs
							if (text.IndexOf(" sub ") > -1 || text.IndexOf("sub ") == 0 || text.IndexOf("function") > -1)
							{
								inFunction = true;
								TotalVBDotNetFunctions++;
								continue;
							}
						}
					}
				}
			}
		}

		public void ComputeStatsFrontPages(string path)
		{
			string[] dotNetFileList = Directory.GetFiles(path, "*.aspx", SearchOption.AllDirectories);

			foreach (string dotNetFile in dotNetFileList)
			{
				using (var reader = new StreamReader(dotNetFile))
				{
					while (!reader.EndOfStream)
					{
						string text = reader.ReadLine().Trim().ToLower();
						//count javascript as a page

						if (text.IndexOf("<form") > -1)
						{
							TotalHTMLPages++;
						}

						if (text.Length > 0 && text[0] != '\'')
						{
							TotalHTMLLines++; // technically, we should not count all the lines in the front page.
						}
					}
				}
			}
		}

		public void CountJavascriptPages(string path)
		{
			string[] fileList = Directory.GetFiles(path, "*.js", SearchOption.AllDirectories);

			foreach (string file in fileList)
			{
				TotalJavascriptPages++;
				using (var reader = new StreamReader(file))
				{
					while (!reader.EndOfStream)
					{
						string text = reader.ReadLine().Trim().ToLower();

						if (text.Length > 0 && text[0] != '\'')
						{
							TotalJavascriptLines++;
						}
					}
				}
			}
		}

	}
}
