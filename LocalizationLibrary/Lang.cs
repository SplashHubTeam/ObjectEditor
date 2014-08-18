using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Linq;
using System.Xml;

namespace LocalizationLibrary
{
	public class Lang
	{
		public const string RU = @"RU";
		public const string EN = @"EN";
        public static string TemplateFileName = @"{0}.xml";

		private static bool _isOldVersion = true;
		public static XmlDataProvider Provider { get; private set; }

		static Lang()
		{
			Provider = new XmlDataProvider {XPath = "LanguagesResource", IsAsynchronous = false};            
		    IsRegistrated = false;
	        GetLangs();
		}
		public static ComponentResourceKey res
		{
			get
			{
				return new ComponentResourceKey(typeof(Lang), "res");
			}
		}

        public static string[] GetLangs(string path = null)
		{
            if (path == null)
            {                
                path = GetLangPath();
            }

            if (!path.EndsWith(@"\"))
            {
                path += Path.DirectorySeparatorChar;
            }

         
            if (Directory.Exists(path))
            {                
                var list = Directory.GetFiles(path, "*.xml");
                if (list.Length != 0)
                {
                    _isOldVersion = true;
                    for (int i = 0; i < list.Length; i++) list[i] = Path.GetFileNameWithoutExtension(list[i]);
                    return list;
                }
                list = Directory.GetDirectories(path);
                if(list.Length != 0)
                {
                    _isOldVersion = false;
                    return list.Where(x =>!x.Contains(".")).ToList().ConvertAll(x => new DirectoryInfo(x).Name).ToArray();
                }

            }
            
            return null;    
			
		}

	    private static string GetLangPath()
	    {
	        var path = System.Windows.Forms.Application.StartupPath + Path.DirectorySeparatorChar + "Languages";
	        return path;
	    }

	    public static bool IsRegistrated { get; private set; }
		public void Registration()
		{
			if (IsRegistrated) return;

			Application.Current.Resources.Add(res, Provider);
			IsRegistrated = true;
		}

        public static void SetCulture()
        {
            try
            {                
                var langString = GetTitle(@"@culture", Thread.CurrentThread.CurrentCulture.IetfLanguageTag);                
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langString);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langString);
            }
            catch (UriFormatException)
            {
                Trace.WriteLine("Не удалось создать URL");
            }
        }
	    public static void SetFile(string filename)
		{			
			try
			{
               
				Provider.Source = new Uri(filename);
				var langString = GetTitle(@"@culture", Thread.CurrentThread.CurrentCulture.IetfLanguageTag);                
				Application.Current.MainWindow.Language = System.Windows.Markup.XmlLanguage.GetLanguage(langString);
                Application.Current.MainWindow.UpdateLayout();               
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langString);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langString);
			}
			catch (UriFormatException)
			{
				Trace.WriteLine("Не удалось создать URL");
			}
		}
        public static void SetDir(string lang)
        {
            try
            {
                var path = GetLangPath() + "\\" + lang;
                if(!Directory.Exists(path))
                {
                    if (GetLangs() == null) return;
                    lang = GetLangs()[0];
                    path = GetLangPath() + "\\" + lang;
                    if(!Directory.Exists(path)) return;
                }
                var list = Directory.GetFiles(path, "*.xml");
                if (list.Length == 0) return;
                Provider.Source = new Uri(list[0]);                
                var langString = GetTitle(@"@culture", Thread.CurrentThread.CurrentCulture.IetfLanguageTag);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langString);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langString);

				if (list.Length > 1)
				{
					var rootNode = Provider.Document.SelectSingleNode(Provider.XPath);
					if (rootNode != null)
					{
						var doc = new XmlDocument();
						for (int i = 1; i < list.Length; i++)
						{
							doc.Load(list[i]);
							var nodes = doc.SelectSingleNode("LanguagesResource").ChildNodes;

							foreach (var node in nodes)
							{
								rootNode.AppendChild(rootNode.OwnerDocument.ImportNode((XmlNode)node, true));
							}
						}
					}
				}

				if (Application.Current.MainWindow != null)
				{
					Application.Current.MainWindow.Language = System.Windows.Markup.XmlLanguage.GetLanguage(langString);
					Application.Current.MainWindow.UpdateLayout();
				}
            }
            catch (UriFormatException)
            {
                Trace.WriteLine("Не удалось создать URL");
            }
        }		
		public static void SetFileFromApplicationFolder(string filename)
		{
            SetFile(string.Format("pack://siteoforigin:,,,/{0}", filename));
		}
		public static void SetFileFromLangFolder(string filename)
		{
            if (!File.Exists(string.Format("Languages/{0}", filename)))
            {
                if(GetLangs()==null) return;
                var lang = GetLangs()[0];
                filename = string.Format(TemplateFileName, lang);
                if (!File.Exists(string.Format("Languages/{0}", filename))) return;
            }
            SetFileFromApplicationFolder(string.Format("Languages/{0}", filename));
		}

		public static void SetLang(string lang)
		{
            if (_isOldVersion)
            {
                SetFileFromLangFolder(string.Format(TemplateFileName, lang));
            }
            else
            {
                SetDir(lang);
            }

		}			
		public static string GetTitle(string xpath)
		{
            return GetTitle(xpath, xpath);
		}
		public static string GetTitle(string xpath, string defaultTitle)
		{
			try
			{
                if (Provider.Document == null) return defaultTitle;
			    var s = Provider.Document.SelectSingleNode(Provider.XPath);
                if (s == null) return defaultTitle;

                s = Provider.Document.SelectSingleNode(Provider.XPath).SelectSingleNode(xpath);
                if (s == null) return defaultTitle;
				return s.InnerText;	
			}
            catch (Exception)
            {
                return defaultTitle;
            }
		
		}

		public static string GetEnum(string xpath, object value)
		{
            return GetEnum(xpath, value, value.ToString());
		}

		public static string GetEnum(string xpath, object value, string defaultTitle)
		{
			return GetTitle(xpath + value, defaultTitle);
		}
	}
}
